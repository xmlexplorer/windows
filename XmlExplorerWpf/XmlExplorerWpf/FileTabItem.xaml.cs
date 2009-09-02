using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Schema;

namespace XmlExplorer
{
    public partial class FileTabItem : TabItem
    {
		private List<ValidationEventArgs> _validationEventArgs;
        private TabControl _parentTabControl;

        public FileTabItem()
            : base()
        {
            InitializeComponent();
        }

        public FileTab FileTab
        {
            get
            {
                return this.DataContext as FileTab;
            }

            set
            {
                this.DataContext = value;
            }
        }

        private TabControl GetParentTabControl()
        {
            if (_parentTabControl != null)
                return _parentTabControl;

            DependencyObject obj = this;

            while ((obj = VisualTreeHelper.GetParent(obj)) != null)
            {
                if (obj is TabControl)
                    break;
            }

            _parentTabControl = obj as TabControl;

            return _parentTabControl;
        }

        /// <summary>
        /// Overwrites the tab page's file with XML formatting (tabs and crlf's)
        /// </summary>
        public void Save(bool formatting)
        {
            FileTab fileTab = this.FileTab;
            if (fileTab == null)
                return;

            if (fileTab.FileInfo == null)
            {
				System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();

                dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                fileTab.FileInfo = new FileInfo(dialog.FileName);
            }

            fileTab.Save(formatting);
        }

        /// <summary>
        /// Prompts the user to save a copy of the tab page's XML file.
        /// </summary>
        public void SaveAs(bool formatting)
        {
            FileTab fileTab = this.FileTab;
            if (fileTab == null)
                return;

			System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();

            dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

            if (fileTab.FileInfo != null)
                dialog.FileName = System.IO.Path.GetFileName(fileTab.FileInfo.FullName);

            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            fileTab.FileInfo = new FileInfo(dialog.FileName);

            this.Save(formatting);
        }

		public void Refresh()
		{
			
		}

        private void Close()
        {
            TabControl tabControl = this.GetParentTabControl();
            if (tabControl == null)
                return;

            tabControl.Items.Remove(this);
        }

        private void CloseAll()
        {
            TabControl tabControl = this.GetParentTabControl();
            if (tabControl == null)
                return;

            tabControl.Items.Clear();
        }

        private void CloseAllButThis()
        {
            TabControl tabControl = this.GetParentTabControl();
            if (tabControl == null)
                return;

            while (tabControl.Items.Count > 1)
            {
                object itemToRemove = null;

                foreach (object item in tabControl.Items)
                {
                    if (item == this)
                        continue;

                    itemToRemove = item;
                    break;
                }

                tabControl.Items.Remove(itemToRemove);
            }
        }

        private void CopyFullPath()
        {
            FileTab fileTab = this.FileTab;

            if (fileTab == null || fileTab.FileInfo == null || string.IsNullOrEmpty(fileTab.FileInfo.FullName))
                return;

            Clipboard.SetText(fileTab.FileInfo.FullName);
        }

        private void OpenContainingFolder()
        {
            FileTab fileTab = this.FileTab;

            if (fileTab == null || fileTab.FileInfo == null || string.IsNullOrEmpty(fileTab.FileInfo.FullName))
                return;

            string folderPath = fileTab.FileInfo.DirectoryName;

            Process.Start(folderPath);
        }

		public XPathNavigatorView GetSelectedNavigatorView()
		{
			// get the selected node
			XPathNavigatorView navigator = this.TreeView.SelectedItem as XPathNavigatorView;

			// if there is no selected node, default to the root node
			if (navigator == null && this.TreeView.Items.Count > 0)
				return this.TreeView.Items[0] as XPathNavigatorView;

			return navigator;
		}

		public void CopyXml()
		{
			XPathNavigatorView navigatorView = this.GetSelectedNavigatorView();
			if (navigatorView == null)
				return;

			string text = this.GetXPathNavigatorFormattedOuterXml(navigatorView.XPathNavigator);

			Clipboard.SetText(text);
		}

		private string GetXPathNavigatorFormattedOuterXml(XPathNavigator navigator)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				XmlWriterSettings settings = new XmlWriterSettings();

				settings.Encoding = Encoding.UTF8;
				settings.Indent = true;
				settings.OmitXmlDeclaration = true;

				using (XmlWriter writer = XmlTextWriter.Create(stream, settings))
				{
					navigator.WriteSubtree(writer);

					writer.Flush();

					return Encoding.UTF8.GetString(stream.ToArray());
				}
			}
		}

		/// <summary>
		/// Returns a string representing the full path of an XPathNavigator.
		/// </summary>
		/// <param name="navigator">An XPathNavigator.</param>
		/// <returns></returns>
		public string GetXmlNodeFullPath(XPathNavigator navigator)
		{
			// create a StringBuilder for assembling the path
			StringBuilder sb = new StringBuilder();

			// clone the navigator (cursor), so the node doesn't lose it's place
			navigator = navigator.Clone();

			// traverse the navigator's ancestry all the way to the top
			while (navigator != null)
			{
				// skip anything but elements
				if (navigator.NodeType == XPathNodeType.Element)
				{
					// insert the node and a seperator
					sb.Insert(0, navigator.Name);
					sb.Insert(0, "/");
				}
				if (!navigator.MoveToParent())
					break;
			}

			return sb.ToString();
		}

		public void ExpandAll()
		{
			XPathNavigatorView navigatorView = this.GetSelectedNavigatorView();
			if (navigatorView == null)
				return;

			navigatorView.ExpandAll();
		}

		public void CollapseAll()
		{
			XPathNavigatorView navigatorView = this.GetSelectedNavigatorView();
			if (navigatorView == null)
				return;

			navigatorView.CollapseAll();
		}

		public void SelectFirstResult(XPathNodeIterator iterator)
		{
			if (iterator == null || iterator.Count < 1)
				return;

			if (!iterator.MoveNext())
				return;

			// select the first node in the set
			XPathNavigator targetNavigator = iterator.Current;

			if (targetNavigator == null)
				return;

			// we've found a node, so build an ancestor stack
			Stack<XPathNavigator> ancestors = this.GetAncestors(targetNavigator);

			IEnumerable items = this.TreeView.Items;
			XPathNavigatorView targetView = null;

			while (ancestors.Count > 0)
			{
				XPathNavigator navigator = ancestors.Pop();

				foreach (object item in items)
				{
					XPathNavigatorView view = item as XPathNavigatorView;
					if (view == null)
						return;

					if (view.XPathNavigator.IsSamePosition(navigator))
					{
						if (ancestors.Count > 0)
						{
							view.IsExpanded = true;
							items = view.Children;
						}

						targetView = view;
						break;
					}
				}
			}

			if (targetView != null)
			{
				targetView.IsSelected = true;
			}

			//treeView.SetSelectedItem(ancestors, (x, y) => x.IsSamePosition(y));

			return;
		}

		/// <summary>
		/// Builds and returns a Stack of XPathNavigator ancestors for a given XPathNavigator.
		/// </summary>
		/// <param name="navigator">The XPathNavigator from which to build a stack.</param>
		/// <returns></returns>
		private Stack<XPathNavigator> GetAncestors(XPathNavigator navigator)
		{
			if (navigator == null)
				throw new ArgumentNullException("navigator");

			Stack<XPathNavigator> ancestors = new Stack<XPathNavigator>();

			// navigate up the xml tree, building the stack as we go
			while (navigator != null)
			{
				// push the current ancestor onto the stack
				ancestors.Push(navigator);

				// clone the current navigator cursor, so we don't lose our place
				navigator = navigator.Clone();

				// if we've reached the top, we're done
				if (!navigator.MoveToParent())
					break;

				// if we've reached the root, we're done
				if (navigator.NodeType == XPathNodeType.Root)
					break;
			}

			// return the result
			return ancestors;
		}

		public string CopyXPath()
		{
			XPathNavigatorView navigatorView = this.GetSelectedNavigatorView();

			string text = this.GetXmlNodeFullPath(navigatorView.XPathNavigator);

			Clipboard.SetText(text);

			return text;
		}

		public object EvaluateXPath(string xpath)
		{
			XPathNavigatorView navigatorView = this.GetSelectedNavigatorView();

			if (navigatorView == null)
				return null;

			// evaluate the expression, return the result
			return navigatorView.XPathNavigator.Evaluate(xpath, this.FileTab.XmlNamespaceManager);
		}

		public List<ValidationEventArgs> Validate(string schemaFileName)
		{
			XPathNavigator navigator = this.FileTab.Document as XPathNavigator;

			if (navigator == null)
				return null;

			_validationEventArgs = new List<ValidationEventArgs>();

			// try to dynamically discover the target namespace of the schema
			XPathDocument schemaDocument = new XPathDocument(schemaFileName);
			XPathNavigator schemaNavigator = schemaDocument.CreateNavigator();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(schemaNavigator.NameTable);
			schemaNavigator.MoveToFirstChild();
			xmlNamespaceManager.AddNamespace("", schemaNavigator.NamespaceURI);
			string targetNamespace = schemaNavigator.Evaluate("string(@targetNamespace)", xmlNamespaceManager) as string;

			// load the schema
			XmlSchemaSet schemas = new XmlSchemaSet();
			schemas.Add(targetNamespace, schemaFileName);

			// validate the document
			navigator.CheckValidity(schemas, this.OnValidationEvent);

			return _validationEventArgs;
		}

		private void OnValidationEvent(object sender, ValidationEventArgs e)
		{
			_validationEventArgs.Add(e);

			XmlSchemaValidationException exception = e.Exception as XmlSchemaValidationException;

			if (exception == null || exception.SourceObject == null)
				return;

			/* 
			 * exception.SourceObject is never provided, it's always null.
			 * I've reported this, and am awaiting a response
			 * https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=366355
			 * Until this is resolved, I cannot provide double-click navigation from a
			 * validation exception to the offending node
			*/
			Debug.WriteLine(exception.SourceObject);
		}

		private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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

		private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		private void CloseAllCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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

		private void CloseAllCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				this.CloseAll();
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		private void CloseAllButThisCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			try
			{
				TabControl tabControl = this.GetParentTabControl();
				if (tabControl == null)
					return;

				e.CanExecute = tabControl.Items.Count > 1;
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		private void CloseAllButThisCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				this.CloseAllButThis();
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
				e.CanExecute = true;
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
				this.CopyFullPath();
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
				e.CanExecute = true;
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
				this.OpenContainingFolder();
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
				this.CollapseAll();
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
				this.ExpandAll();
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
				this.CopyXPath();
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
				this.CopyXml();
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				if (e.ChangedButton == MouseButton.Left)
					return;

				this.IsSelected = true;
				if (this.Focusable)
					this.Focus();
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		private void TabItem_MouseUp(object sender, MouseButtonEventArgs e)
		{
			try
			{
				if (e.ChangedButton == MouseButton.Middle)
					this.Close();
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