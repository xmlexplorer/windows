using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Xml.XPath;
using WpfControls;
using System.Diagnostics;
using System.Collections.ObjectModel;
using AvalonDock;
using System.Collections;

namespace XmlExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);

            this.dockingManager.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(dockingManager_PropertyChanged);
            this.ErrorListDockableContent.ErrorActivated += this.ErrorListDockableContent_ErrorActivated;
            this.dockingManager.Loaded += new RoutedEventHandler(dockingManager_Loaded);
            ColorFactory.ChangeColors(Colors.Black);
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.ErrorListDockableContent != null)
                this.ErrorListDockableContent.ErrorActivated -= this.ErrorListDockableContent_ErrorActivated;

            if (this.dockingManager != null)
            {
                this.dockingManager.PropertyChanged -= this.dockingManager_PropertyChanged;

                using (StringWriter writer = new StringWriter())
                {
                    this.dockingManager.SaveLayout(writer);
                    writer.Flush();
                    Properties.Settings.Default.DockLayout = writer.ToString();
                    Properties.Settings.Default.Save();
                }
            }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        void dockingManager_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string layout = Properties.Settings.Default.DockLayout;
                if (string.IsNullOrEmpty(layout))
                {
                    // set default layout options
                    this.dockingManager.ToggleAutoHide(this.ErrorListDockablePane);
                }
                else
                {
                    try
                    {
                        using (StringReader reader = new StringReader(layout))
                        {
                            this.dockingManager.RestoreLayout(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        App.HandleException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void Open()
        {
            string[] dialogFileNames = null;

            using (System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog())
            {
                dialog.Multiselect = true;

                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                dialogFileNames = dialog.FileNames;
            }

            foreach (string filename in dialogFileNames)
                this.Open(new FileInfo(filename));
        }

        internal void Open(IEnumerable<string> commandLine)
        {
            foreach (string arg in commandLine)
            {
                FileInfo fileInfo = new FileInfo(arg);

                this.Open(fileInfo);
            }
        }

        public void Open(FileInfo fileInfo)
        {
            XPathDocumentContent xpathDocumentContent = new XPathDocumentContent(fileInfo);

            xpathDocumentContent.TreeView.BeginOpen(fileInfo, this.OnDocumentLoaded, null);

            this.InitializeXPathDocumentContent(xpathDocumentContent);

            if (xpathDocumentContent.TreeView.DefaultNamespaceCount > 0)
                this.dockingManager.Show(this.NamespaceListDockableContent);

            if (xpathDocumentContent.TreeView.Errors != null && xpathDocumentContent.TreeView.Errors.Count > 0)
                this.dockingManager.Show(this.ErrorListDockableContent);
        }

        void OnDocumentLoaded(object sender, EventArgs e)
        {
            try
            {
                if (!this.Dispatcher.CheckAccess())
                {
                    this.Dispatcher.Invoke(new EventHandler(this.OnDocumentLoaded), sender, e);
                    return;
                }

                var selectedItem = this.GetSelectedXPathDocumentContent();
                if (selectedItem == null)
                    return;

                if (selectedItem.TreeView != sender)
                    return;

                this.LoadCurrentDocumentInformation();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void Open(string name, object document)
        {
            XPathDocumentContent xpathDocumentContent = new XPathDocumentContent(name, document);

            this.InitializeXPathDocumentContent(xpathDocumentContent);
        }

        private void InitializeXPathDocumentContent(XPathDocumentContent xpathDocumentContent)
        {
            xpathDocumentContent.TreeView.FontFamily = new FontFamily(Properties.Settings.Default.FontFamilyName);
            xpathDocumentContent.TreeView.FontSize = Properties.Settings.Default.FontSize;

            this.dockingManager.MainDocumentPane.Items.Add(xpathDocumentContent);

            this.dockingManager.ActiveDocument = xpathDocumentContent;
        }

        private void CopyXPath()
        {
            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();

            if (xpathDocumentContent == null)
                return;

            this.textBoxXPath.Text = xpathDocumentContent.TreeView.CopyXPath();
        }

        private void CopyOuterXml()
        {
            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();

            if (xpathDocumentContent == null)
                return;

            xpathDocumentContent.TreeView.CopyOuterXml();
        }

        private void CopyXml()
        {
            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();

            if (xpathDocumentContent == null)
                return;

            xpathDocumentContent.TreeView.CopyXml();
        }

        private void CollapseAll()
        {
            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
            if (xpathDocumentContent == null)
                return;

            xpathDocumentContent.TreeView.CollapseAll();
        }

        private void ExpandAll()
        {
            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
            if (xpathDocumentContent == null)
                return;

            xpathDocumentContent.TreeView.ExpandAll();
        }

        private void SelectXPath(string xpath)
        {
            try
            {
                XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
                if (xpathDocumentContent == null)
                    return;

                object result = xpathDocumentContent.TreeView.EvaluateXPath(xpath);

                if (result == null)
                    throw new XPathException("No matches found.  Please check the XPath expression, and make sure you're using namespace prefixes, if required.");

                // did the expression evaluate to a node set?
                XPathNodeIterator iterator = result as XPathNodeIterator;

                if (iterator != null)
                {
                    if (iterator.Count < 1)
                        throw new XPathException("No matches found.  Please check the XPath expression, and make sure you're using namespace prefixes, if required.");

                    // the expression evaluated to a node set
                    xpathDocumentContent.TreeView.SelectFirstResult(iterator);
                }
                else
                {
                    // the expression evaluated to something else, most likely a count() or another function
                    // show the result in a new window
                    this.DisplayXPathResult(result);
                }
            }
            catch (XPathException ex)
            {
                this.textBoxXPath.Background = Brushes.LightPink;
                this.statusBarItemMain.Background = Brushes.LightPink;
                this.statusBarItemMain.Text = ex.Message;
            }
        }

        private void LaunchXPath(string xpath)
        {
            try
            {
                XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
                if (xpathDocumentContent == null)
                    return;

                object result = xpathDocumentContent.TreeView.EvaluateXPath(xpath);

                // did the expression evaluate to a node set?
                XPathNodeIterator iterator = result as XPathNodeIterator;

                if (iterator != null)
                {
                    if (iterator.Count < 1)
                        throw new XPathException("No matches found.  Please check the XPath expression, and make sure you're using namespace prefixes, if required.");

                    // the expression evaluated to a node set
                    this.Open("XPath Results", iterator);
                }
                else
                {
                    this.DisplayXPathResult(result);
                }
            }
            catch (XPathException ex)
            {
                this.textBoxXPath.Background = Brushes.LightPink;
                this.statusBarItemMain.Background = Brushes.LightPink;
                this.statusBarItemMain.Text = ex.Message;
            }
        }

        private void DisplayXPathResult(object result)
        {
            //ExpressionResultsWindow dialog = new ExpressionResultsWindow();
            //dialog.Expression = xpath;
            //dialog.Result = result.ToString();
            //dialog.ShowDialog(this.FindForm());
            //return true;
            MessageBox.Show(result.ToString(), "XML Explorer - XPath expression result");
        }

        private XPathDocumentContent GetSelectedXPathDocumentContent()
        {
            return this.dockingManager.ActiveDocument as XPathDocumentContent;
        }

        private void Save(bool formatting)
        {
            // get the selected tab
            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
            if (xpathDocumentContent == null)
                return;

            xpathDocumentContent.TreeView.Save(formatting);
        }

        private void SaveAs(bool formatting)
        {
            // get the selected tab
            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
            if (xpathDocumentContent == null)
                return;

            xpathDocumentContent.TreeView.SaveAs(formatting);
        }

        private void Refresh()
        {
            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
            if (xpathDocumentContent == null)
                return;

            xpathDocumentContent.Close();

            this.Open(xpathDocumentContent.FileInfo);
        }

        private void ChooseFont()
        {
            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
            if (xpathDocumentContent == null)
                return;

            FontChooserLite fontChooser = new FontChooserLite();
            fontChooser.Owner = this;
            fontChooser.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            TreeView xpathDocumentContentTreeView = xpathDocumentContent.TreeView;

            fontChooser.SetPropertiesFromObject(xpathDocumentContentTreeView);

            if (!fontChooser.ShowDialog().Value)
                return;

            fontChooser.ApplyPropertiesToObject(xpathDocumentContentTreeView);

            // save the new font settings
            XmlExplorer.Properties.Settings settings = Properties.Settings.Default;

            settings.FontFamilyName = xpathDocumentContentTreeView.FontFamily.ToString();
            settings.FontSize = xpathDocumentContentTreeView.FontSize;

            settings.Save();

            foreach (object item in this.dockingManager.Documents)
            {
                XPathDocumentContent otherXPathDocumentContent = item as XPathDocumentContent;
                if (otherXPathDocumentContent == null)
                    continue;

                if (otherXPathDocumentContent == xpathDocumentContent)
                    continue;

                fontChooser.ApplyPropertiesToObject(otherXPathDocumentContent.TreeView);
            }
        }

        private void LoadCurrentDocumentInformation()
        {
            this.LoadErrorList();
            this.LoadNamespaceList();

            string loadedTime = null;
            var selectedItem = this.GetSelectedXPathDocumentContent();
            if (selectedItem != null && !selectedItem.TreeView.IsLoading)
                loadedTime = string.Format("Loaded in {0:N3} seconds", selectedItem.TreeView.LoadTime.TotalMilliseconds / 1000D);

            this.statusBarItemLoadTime.Text = loadedTime;
        }

        private void LoadErrorList()
        {
            IEnumerable items = null;

            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
            if (xpathDocumentContent != null)
                items = xpathDocumentContent.TreeView.Errors;

            this.ErrorListDockableContent.DataGridErrorList.ItemsSource = items;
        }

        private void LoadNamespaceList()
        {
            ObservableCollection<NamespaceDefinition> items = null;

            XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
            if (xpathDocumentContent != null)
                items = xpathDocumentContent.TreeView.NamespaceDefinitions;

            this.NamespaceList.NamespaceDefinitions = items;

            //foreach (NamespaceDefinition definition in treeView.NamespaceDefinitions)
            //{
            //    treeView.XmlNamespaceManager.RemoveNamespace(definition.OldPrefix, definition.Namespace);

            //    treeView.XmlNamespaceManager.AddNamespace(definition.NewPrefix, definition.Namespace);
            //}
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = Clipboard.ContainsText();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                XmlReaderSettings readerSettings = new XmlReaderSettings();

                readerSettings.ConformanceLevel = ConformanceLevel.Fragment;

                XPathDocument document = null;

                string text = Clipboard.GetText();

                using (StringReader stringReader = new StringReader(text))
                {
                    using (XmlReader reader = XmlReader.Create(stringReader, readerSettings))
                    {
                        document = new XPathDocument(reader);
                    }
                }

                var navigator = document.CreateNavigator();
                XmlNamespaceManager namespaceManager = new XmlNamespaceManager(navigator.NameTable);

                this.Open("New Document", navigator);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void OpenCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = true;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void OpenCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            try
            {
                this.Open();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void ExitCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = true;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void ExitCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            try
            {
                App.Current.Shutdown();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CloseCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = this.GetSelectedXPathDocumentContent() != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CloseCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            try
            {
                // get the selected tab
                XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();

                if (xpathDocumentContent != null)
                    xpathDocumentContent.Close();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void LaunchXPathCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                string xpath = e.Parameter as string;

                this.LaunchXPath(xpath);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void LaunchXPathCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SelectXPathCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                string xpath = e.Parameter as string;

                this.SelectXPath(xpath);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SelectXPathCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CopyFullPathCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CopyFullPathCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
                if (xpathDocumentContent == null)
                    return;

                xpathDocumentContent.CopyFullPath();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void OpenContainingFolderCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void OpenContainingFolderCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
                if (xpathDocumentContent == null)
                    return;

                xpathDocumentContent.OpenContainingFolder();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CopyXPathCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CopyXPathCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.CopyXPath();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CopyOuterXmlCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CopyOuterXmlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.CopyOuterXml();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CopyXmlCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CopyXmlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.CopyXml();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void XPathCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = true;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void XPathCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (this.textBoxXPath.Focusable)
                    this.textBoxXPath.Focus();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SaveCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = this.GetSelectedXPathDocumentContent() != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SaveCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            try
            {
                this.Save(true);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SaveWithFormattingCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = this.GetSelectedXPathDocumentContent() != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SaveWithFormattingCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.Save(true);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SaveWithoutFormattingCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = this.GetSelectedXPathDocumentContent() != null;
            }
            catch (Exception ex)
            {

                App.HandleException(ex);
            }
        }

        private void SaveWithoutFormattingCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.Save(false);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SaveAsCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = this.GetSelectedXPathDocumentContent() != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.SaveAs(true);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SaveAsWithFormattingCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = this.GetSelectedXPathDocumentContent() != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SaveAsWithFormattingCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.SaveAs(true);
            }
            catch (Exception ex)
            {

                App.HandleException(ex);
            }
        }

        private void SaveAsWithoutFormattingCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = this.GetSelectedXPathDocumentContent() != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void SaveAsWithoutFormattingCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.SaveAs(false);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void ExpandAllCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void ExpandAllCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.ExpandAll();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CollapseAllCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void CollapseAllCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.CollapseAll();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void RefreshCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void RefreshCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.Refresh();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void textBoxXPath_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        if (Keyboard.IsKeyDown(Key.LeftShift)
                            || Keyboard.IsKeyDown(Key.RightShift))
                            this.LaunchXPath(this.textBoxXPath.Text);
                        else
                            this.SelectXPath(this.textBoxXPath.Text);
                        break;
                }
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void textBoxXPath_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.textBoxXPath.Background = SystemColors.WindowBrush;
                this.statusBarItemMain.Background = SystemColors.ControlBrush;
                this.statusBarItemMain.Text = string.Empty;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void textBoxXPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //string text = this.textBoxXPath.Text;

                //if (!text.EndsWith("/"))
                //    return;

                //text += "*";

                //XPathDocumentContent xpathDocumentContent = this.GetSelectedXPathDocumentContent();
                //if (xpathDocumentContent == null)
                //    return;

                //object result = null;

                //try
                //{
                //    result = xpathDocumentContent.EvaluateXPath(text);
                //}
                //catch (XPathException) { }

                //XPathNodeIterator iterator = result as XPathNodeIterator;
                //if (iterator == null || iterator.Count < 1)
                //    return;

                //List<string> paths = new List<string>();

                //foreach (XPathNavigator navigator in iterator)
                //{
                //    string path = xpathDocumentContent.GetXmlNodeFullPath(navigator);

                //    if (paths.Contains(path))
                //        continue;

                //    paths.Add(path);
                //}

                //this.textBoxXPath.ItemsSource = paths;

                //this.textBoxXPath.IsDropDownOpen = true;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void AboutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = true;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void AboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                AboutWindow window = new AboutWindow();
                window.Owner = this;
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void ChooseFontCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (this.GetSelectedXPathDocumentContent()) != null;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void ChooseFontCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.ChooseFont();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void ValidateCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = true;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void ValidateCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.dockingManager.Show(this.ErrorListDockableContent);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void NamespacesCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = true;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void NamespacesCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.dockingManager.Show(this.NamespaceListDockableContent);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                    return;

                object data = e.Data.GetData(DataFormats.FileDrop);

                if (!(data is string[]))
                    return;

                string[] fileNames = (string[])data;

                this.Open(fileNames);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        void dockingManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "ActiveDocument")
                {
                    this.LoadCurrentDocumentInformation();
                }
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        void ErrorListDockableContent_ErrorActivated(object sender, EventArgs<Error> e)
        {
            try
            {
                if (e.Item == null || e.Item.SourceObject == null)
                    return;

                XPathNavigator navigator = e.Item.SourceObject as XPathNavigator;
                if (navigator == null)
                    return;

                var document = this.GetSelectedXPathDocumentContent();
                if (document == null)
                    return;

                document.TreeView.SelectXPathNavigator(navigator);
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }
    }
}
