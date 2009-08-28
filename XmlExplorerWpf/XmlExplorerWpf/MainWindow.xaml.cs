using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Xml.XPath;

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
			System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();

			dialog.Multiselect = true;

			if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return;

			foreach (string filename in dialog.FileNames)
				this.Open(new FileInfo(filename));
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

			fileTabItem.DataContext = new FileTab(fileInfo, document, namespaceManager);

			this.tabControl.Items.Add(fileTabItem);

			this.tabControl.SelectedItem = fileTabItem;
		}

		private void Open(string name, object document, XmlNamespaceManager namespaceManager)
		{
			FileTabItem fileTabItem = this.CreateNewFileTabItem();

			fileTabItem.DataContext = new FileTab(name, document, namespaceManager);

			this.tabControl.Items.Add(fileTabItem);

			this.tabControl.SelectedItem = fileTabItem;
		}

		private FileTabItem CreateNewFileTabItem()
		{
			FileTabItem fileTabItem = new FileTabItem();

			//fileTabItem.TreeView.FontSize = this.GetSelectedFontSize();
			//fileTabItem.TreeView.FontFamily = this.GetSelectedFont();

			return fileTabItem;
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
	}
}
