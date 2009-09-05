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
using Microsoft.WindowsAPICodePack.Taskbar;
using System.Diagnostics;
using System.Collections.ObjectModel;

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
			XmlReaderSettings readerSettings = new XmlReaderSettings();

			readerSettings.ConformanceLevel = ConformanceLevel.Fragment;

			XPathDocument document = null;

			using (XmlReader reader = XmlReader.Create(fileInfo.FullName, readerSettings))
			{
				document = new XPathDocument(reader);
			}

			var navigator = document.CreateNavigator();
			XmlNamespaceManager namespaceManager = new XmlNamespaceManager(navigator.NameTable);

			this.Open(fileInfo, navigator, namespaceManager);
		}

		private void Open(FileInfo fileInfo, object document, XmlNamespaceManager namespaceManager)
		{
			FileTabItem fileTabItem = this.CreateNewFileTabItem();

			FileTab fileTab = new FileTab(fileInfo, document, namespaceManager);

			fileTabItem.DataContext = fileTab;

			this.tabControl.Items.Add(fileTabItem);

			// select the tab
			this.tabControl.SelectedItem = fileTabItem;

			this.CreatedTabbedThumbnail(fileTabItem);

			if (fileTab.DefaultNamespaceCount > 0)
				this.ShowNamespacePrefixWindow(fileTab);
		}

		private void ShowNamespacePrefixWindow(FileTab fileTab)
		{
			NamespacePrefixWindow window = new NamespacePrefixWindow();
			window.Owner = this;

			window.NamespaceDefinitions = fileTab.NamespaceDefinitions;
			window.ShowDialog();

			foreach (NamespaceDefinition definition in fileTab.NamespaceDefinitions)
			{
				fileTab.XmlNamespaceManager.RemoveNamespace(definition.OldPrefix, definition.Namespace);

				fileTab.XmlNamespaceManager.AddNamespace(definition.NewPrefix, definition.Namespace);
			}
		}

		private void Open(string name, object document, XmlNamespaceManager namespaceManager)
		{
			FileTabItem fileTabItem = this.CreateNewFileTabItem();

			fileTabItem.DataContext = new FileTab(name, document, namespaceManager);

			this.tabControl.Items.Add(fileTabItem);

			this.tabControl.SelectedItem = fileTabItem;

			this.CreatedTabbedThumbnail(fileTabItem);
		}

		private FileTabItem CreateNewFileTabItem()
		{
			FileTabItem fileTabItem = new FileTabItem();

			fileTabItem.TreeView.FontFamily = new FontFamily(Properties.Settings.Default.FontFamilyName);
			fileTabItem.TreeView.FontSize = Properties.Settings.Default.FontSize;

			return fileTabItem;
		}

		private void CreatedTabbedThumbnail(FileTabItem fileTabItem)
		{
			try
			{
				// are taskbar enhancements available on this platform?
				if (TaskbarManager.IsPlatformSupported)
				{
					// add a new thumbnail preview
					Point point = fileTabItem.TreeView.TranslatePoint(new Point(0, 0), fileTabItem.TreeView);
					TabbedThumbnail preview = new TabbedThumbnail(this, fileTabItem.TreeView, new Vector(point.X, point.Y));

					preview.Title = fileTabItem.FileTab.Name;

					// wire up event handlers for this preview
					preview.TabbedThumbnailActivated += new EventHandler<TabbedThumbnailEventArgs>(preview_TabbedThumbnailActivated);
					preview.TabbedThumbnailClosed += new EventHandler<TabbedThumbnailEventArgs>(preview_TabbedThumbnailClosed);
					preview.TabbedThumbnailMaximized += new EventHandler<TabbedThumbnailEventArgs>(preview_TabbedThumbnailMaximized);
					preview.TabbedThumbnailMinimized += new EventHandler<TabbedThumbnailEventArgs>(preview_TabbedThumbnailMinimized);

					TaskbarManager.Instance.TabbedThumbnail.AddThumbnailPreview(preview);

					// select the taskbar tabbed thumbnail
					TaskbarManager.Instance.TabbedThumbnail.SetActiveTab(fileTabItem.TreeView);
				}
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		private void CopyXPath()
		{
			FileTabItem fileTabItem = this.GetSelectedFileTabItem();

			if (fileTabItem == null)
				return;

			this.textBoxXPath.Text = fileTabItem.CopyXPath();
		}

		private void CopyXml()
		{
			FileTabItem fileTabItem = this.GetSelectedFileTabItem();

			if (fileTabItem == null)
				return;

			fileTabItem.CopyXml();
		}

		private void CollapseAll()
		{
			FileTabItem fileTabItem = this.GetSelectedFileTabItem();
			if (fileTabItem == null)
				return;

			fileTabItem.CollapseAll();
		}

		private void ExpandAll()
		{
			FileTabItem fileTabItem = this.GetSelectedFileTabItem();
			if (fileTabItem == null)
				return;

			fileTabItem.ExpandAll();
		}

		private void SelectXPath(string xpath)
		{
			try
			{
				FileTabItem fileTabItem = this.GetSelectedFileTabItem();
				if (fileTabItem == null)
					return;

				object result = fileTabItem.EvaluateXPath(xpath);

				if (result == null)
					throw new XPathException("No matches found.");

				// did the expression evaluate to a node set?
				XPathNodeIterator iterator = result as XPathNodeIterator;

				if (iterator != null)
				{
					if (iterator.Count < 1)
						throw new XPathException("No matches found.");

					// the expression evaluated to a node set
					fileTabItem.SelectFirstResult(iterator);
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
				FileTabItem fileTabItem = this.GetSelectedFileTabItem();
				if (fileTabItem == null)
					return;

				object result = fileTabItem.EvaluateXPath(xpath);

				// did the expression evaluate to a node set?
				XPathNodeIterator iterator = result as XPathNodeIterator;

				if (iterator != null)
				{
					// the expression evaluated to a node set
					this.Open("XPath Results", iterator, fileTabItem.FileTab.XmlNamespaceManager);
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
			MessageBox.Show(result.ToString());
		}

		private FileTabItem GetSelectedFileTabItem()
		{
			return this.tabControl.SelectedItem as FileTabItem;
		}

		private void Save(bool formatting)
		{
			// get the selected tab
			FileTabItem fileTabItem = this.tabControl.SelectedItem as FileTabItem;
			if (fileTabItem == null)
				return;

			fileTabItem.Save(formatting);
		}

		private void SaveAs(bool formatting)
		{
			// get the selected tab
			FileTabItem fileTabItem = this.tabControl.SelectedItem as FileTabItem;
			if (fileTabItem == null)
				return;

			fileTabItem.SaveAs(formatting);
		}

		private void Refresh()
		{
			FileTabItem fileTabItem = this.GetSelectedFileTabItem();
			if (fileTabItem == null)
				return;

			this.tabControl.Items.Remove(fileTabItem);

			this.Open(fileTabItem.FileTab.FileInfo);
		}

		private void ChooseFont()
		{
			FileTabItem fileTabItem = this.GetSelectedFileTabItem();
			if (fileTabItem == null)
				return;

			FontChooserLite fontChooser = new FontChooserLite();
			fontChooser.Owner = this;
			fontChooser.WindowStartupLocation = WindowStartupLocation.CenterOwner;

			TreeView fileTabItemTreeView = fileTabItem.TreeView;

			fontChooser.SetPropertiesFromObject(fileTabItemTreeView);

			if (!fontChooser.ShowDialog().Value)
				return;

			fontChooser.ApplyPropertiesToObject(fileTabItemTreeView);

			// save the new font settings
			XmlExplorer.Properties.Settings settings = Properties.Settings.Default;

			settings.FontFamilyName = fileTabItemTreeView.FontFamily.ToString();
			settings.FontSize = fileTabItemTreeView.FontSize;

			settings.Save();

			foreach (object item in this.tabControl.Items)
			{
				FileTabItem otherFileTabItem = item as FileTabItem;
				if (otherFileTabItem == null)
					continue;

				if (otherFileTabItem == fileTabItem)
					continue;

				fontChooser.ApplyPropertiesToObject(otherFileTabItem.TreeView);
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
				e.CanExecute = this.tabControl.SelectedItem != null;
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
				FileTabItem fileTabItem = this.GetSelectedFileTabItem();

				if (fileTabItem != null)
					this.tabControl.Items.Remove(fileTabItem);
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
				e.CanExecute = (this.tabControl.SelectedItem as FileTabItem) != null;
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
				e.CanExecute = (this.tabControl.SelectedItem as FileTabItem) != null;
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
				e.CanExecute = (this.tabControl.SelectedItem as FileTabItem) != null;
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
				e.CanExecute = (this.tabControl.SelectedItem as FileTabItem) != null;
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
				e.CanExecute = this.tabControl.SelectedItem != null;
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
				e.CanExecute = this.tabControl.SelectedItem != null;
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
				e.CanExecute = this.tabControl.SelectedItem != null;
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
				e.CanExecute = this.tabControl.SelectedItem != null;
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
				e.CanExecute = this.tabControl.SelectedItem != null;
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
				e.CanExecute = this.tabControl.SelectedItem != null;
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
				e.CanExecute = (this.tabControl.SelectedItem as FileTabItem) != null;
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
				e.CanExecute = (this.tabControl.SelectedItem as FileTabItem) != null;
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
				e.CanExecute = (this.tabControl.SelectedItem as FileTabItem) != null;
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

				//FileTabItem fileTabItem = this.GetSelectedFileTabItem();
				//if (fileTabItem == null)
				//    return;

				//object result = null;

				//try
				//{
				//    result = fileTabItem.EvaluateXPath(text);
				//}
				//catch (XPathException) { }

				//XPathNodeIterator iterator = result as XPathNodeIterator;
				//if (iterator == null || iterator.Count < 1)
				//    return;

				//List<string> paths = new List<string>();

				//foreach (XPathNavigator navigator in iterator)
				//{
				//    string path = fileTabItem.GetXmlNodeFullPath(navigator);

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
				e.CanExecute = (this.tabControl.SelectedItem as FileTabItem) != null;
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
				e.CanExecute = (this.tabControl.SelectedItem as FileTabItem) != null;
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
				FileTabItem fileTabItem = this.tabControl.SelectedItem as FileTabItem;
				if (fileTabItem == null)
					return;

				fileTabItem.Validate(@"C:\Users\jcoon\Projects\Note.xsd");
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
				e.CanExecute = (this.tabControl.SelectedItem as FileTabItem) != null;
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
				FileTabItem fileTabItem = this.tabControl.SelectedItem as FileTabItem;
				if (fileTabItem == null)
					return;

				this.ShowNamespacePrefixWindow(fileTabItem.FileTab);
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		void preview_TabbedThumbnailMinimized(object sender, TabbedThumbnailEventArgs e)
		{
			try
			{
				// User clicked on the minimize button on the thumbnail's context menu
				// Minimize the app
				this.WindowState = WindowState.Minimized;
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		void preview_TabbedThumbnailMaximized(object sender, TabbedThumbnailEventArgs e)
		{
			try
			{
				// User clicked on the maximize button on the thumbnail's context menu
				// Maximize the app
				this.WindowState = WindowState.Maximized;

				this.Activate();

				//// If there is a selected tab, take it's screenshot
				//// invalidate the tab's thumbnail
				//// update the "preview" object with the new thumbnail
				//if (tabControl1.Size != Size.Empty && tabControl1.TabPages.Count > 0 && tabControl1.SelectedTab != null)
				//    UpdatePreviewBitmap(tabControl1.SelectedTab);
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		///// <summary>
		///// Helper method to update the thumbnail preview for a given tab page.
		///// </summary>
		///// <param name="tabPage"></param>
		//private void UpdatePreviewBitmap(FileTabItem fileTabItem)
		//{
		//    if (fileTabItem == null)
		//        return;

		//    TabbedThumbnail preview = TaskbarManager.Instance.TabbedThumbnail.GetThumbnailPreview(fileTabItem.TreeView);

		//    if (preview == null)
		//        return;

		//    System.Drawing.Bitmap bitmap = TabbedThumbnailScreenCapture.GrabWindowBitmap(fileTabItem.TreeView,  );
		//    preview.SetImage(bitmap);
		//}

		void preview_TabbedThumbnailClosed(object sender, TabbedThumbnailEventArgs e)
		{
			try
			{
				// Remove the event handlers from the tab preview
				e.TabbedThumbnail.TabbedThumbnailActivated -= new EventHandler<TabbedThumbnailEventArgs>(preview_TabbedThumbnailActivated);
				e.TabbedThumbnail.TabbedThumbnailClosed -= new EventHandler<TabbedThumbnailEventArgs>(preview_TabbedThumbnailClosed);
				e.TabbedThumbnail.TabbedThumbnailMaximized -= new EventHandler<TabbedThumbnailEventArgs>(preview_TabbedThumbnailMaximized);
				e.TabbedThumbnail.TabbedThumbnailMinimized -= new EventHandler<TabbedThumbnailEventArgs>(preview_TabbedThumbnailMinimized);

				FileTabItem fileTabItem = null;

				foreach (object item in this.tabControl.Items)
				{
					fileTabItem = item as FileTabItem;
					if (fileTabItem == null)
						continue;

					if (fileTabItem.TreeView != e.WindowsControl)
						continue;

					// Select the tab in the application UI as well as taskbar tabbed thumbnail list
					fileTabItem.IsSelected = true;
					TaskbarManager.Instance.TabbedThumbnail.SetActiveTab(fileTabItem.TreeView);
				}

				if (fileTabItem == null)
					return;

				// Finally, remove the tab from our UI
				this.tabControl.Items.Remove(fileTabItem);
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		void preview_TabbedThumbnailActivated(object sender, TabbedThumbnailEventArgs e)
		{
			try
			{
				// User selected a tab via the thumbnail preview
				// Select the corresponding control in our app
				foreach (object item in this.tabControl.Items)
				{
					FileTabItem fileTabItem = item as FileTabItem;
					if (fileTabItem == null)
						continue;

					if (fileTabItem.TreeView != e.WindowsControl)
						continue;

					// Select the tab in the application UI as well as taskbar tabbed thumbnail list
					fileTabItem.IsSelected = true;
					TaskbarManager.Instance.TabbedThumbnail.SetActiveTab(fileTabItem.TreeView);
				}

				// Also activate our parent form (incase we are minimized, this will restore it)
				if (this.WindowState == WindowState.Minimized)
					this.WindowState = WindowState.Normal;

				this.Activate();
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
	}
}
