using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AvalonDock;
using System.IO;
using System.Diagnostics;
using System.Xml.XPath;

namespace XmlExplorer
{
    public partial class XPathDocumentContent : DocumentContent
    {
        private DocumentPane _parentDocumentPane;

        public XPathDocumentContent()
        {
            InitializeComponent();
        }

        public XPathDocumentContent(FileInfo fileInfo)
            : this()
        {
            this.FileInfo = fileInfo;
        }

        public XPathDocumentContent(FileInfo fileInfo, object document)
            : this()
        {
            this.FileInfo = fileInfo;
            this.TreeView.Document = document;
        }

        public XPathDocumentContent(string name, object document)
            : this()
        {
            this.FileInfo = null;

            this.TreeView.Document = document;
        }

        public FileInfo FileInfo
        {
            get
            {
                return this.TreeView.FileInfo;
            }

            set
            {
                this.TreeView.FileInfo = value;

                if (value == null)
                {
                    this.Title = "Untitled";
                    this.InfoTip = "Untitled";
                }
                else
                {
                    this.Title = value.Name;
                    this.InfoTip = value.FullName;
                }
            }
        }

        public void Refresh()
        {

        }

        private DocumentPane GetParentDocumentPane()
        {
            if (_parentDocumentPane != null)
                return _parentDocumentPane;

            DependencyObject obj = this;

            while ((obj = VisualTreeHelper.GetParent(obj)) != null)
            {
                if (obj is DocumentPane)
                    break;
            }

            _parentDocumentPane = obj as DocumentPane;

            return _parentDocumentPane;
        }

        public void CopyFullPath()
        {
            if (this.TreeView == null || this.FileInfo == null || string.IsNullOrEmpty(this.FileInfo.FullName))
                return;

            Clipboard.SetText(this.FileInfo.FullName);
        }

        public void OpenContainingFolder()
        {
            if (this.FileInfo == null || string.IsNullOrEmpty(this.FileInfo.FullName))
                return;

            string args = string.Format("/select,\"{0}\"", this.FileInfo.FullName);

            Process.Start("explorer", args);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);

            DocumentPane parentPane = ContainerPane as DocumentPane;
            if (parentPane != null && parentPane.SelectedItem != this)
            {
                parentPane.SelectedItem = this;
            }
        }

        private void CollapseAllCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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

        private void CollapseAllCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.TreeView.CollapseAll();
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
                e.CanExecute = true;
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
                this.TreeView.ExpandAll();
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
                e.CanExecute = true;
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
                this.TreeView.CopyXPath();
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
                e.CanExecute = true;
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
                this.TreeView.CopyOuterXml();
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        private void TreeView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton != MouseButton.Right)
                    return;

                DependencyObject obj = this.TreeView.InputHitTest(e.GetPosition(this.TreeView)) as DependencyObject;
                if (obj == null)
                    return;

                // cycle up the tree until you hit a TreeViewItem   
                while (obj != null && !(obj is TreeViewItem))
                {
                    obj = VisualTreeHelper.GetParent(obj);
                }

                TreeViewItem item = obj as TreeViewItem;
                if (item == null)
                    return;

                XPathNavigatorView view = item.DataContext as XPathNavigatorView;
                if (view == null)
                    return;

                view.IsSelected = true;

                ContextMenu contextMenu = this.Resources["treeContextMenu"] as ContextMenu;
                if (contextMenu == null)
                    return;

                contextMenu.PlacementTarget = item;
                contextMenu.IsOpen = true;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }
    }
}
