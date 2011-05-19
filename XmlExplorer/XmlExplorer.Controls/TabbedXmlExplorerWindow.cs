namespace XmlExplorer.Controls
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Configuration;
	using System.Diagnostics;
	using System.Drawing;
	using System.IO;
	using System.Net;
	using System.Security;
	using System.Security.Permissions;
	using System.Threading;
	using System.Windows.Forms;
	using System.Xml.XPath;
	using WeifenLuo.WinFormsUI.Docking;
	using XmlExplorer.TreeView;

	public partial class TabbedXmlExplorerWindow : Form
	{
		#region Variables

		/// <summary>
		/// Dialog used to change the font and forecolor for the window tree views.
		/// </summary>
		private FontDialog _fontDialog;

		private Font _treeFont;
		private Color _treeForeColor;

		/// <summary>
		/// A flag to bypass the custom drawing of syntax highlights,
		/// optionally used to improve performance on large documents.
		/// </summary>
		private bool _useSyntaxHighlighting = true;

		private ExpressionsWindow _expressionsWindow;
		private ErrorWindow _errorWindow;
		private NamespacesWindow _namespacesWindow;
		private SettingsWindow _settingsWindow;

		private string _dockSettingsFilename = null;
		private DeserializeDockContent _deserializeDockContent;
		private ChildNodeDefinitionCollection _childNodeDefinitions;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TabbedXmlExplorerWindow.
		/// </summary>
		public TabbedXmlExplorerWindow()
		{
			this.InitializeComponent();

			// handle file drag-drop operations
			try
			{
				new UIPermission(UIPermissionWindow.AllWindows).Demand();
				this.AllowDrop = true;
			}
			catch (SecurityException)
			{
			}

			this.DragOver += this.OnDragOver;
			this.DragDrop += this.OnDragDrop;

			// wire up all of the toolbar and menu events
			this.toolStripMenuItemFile.DropDownOpening += this.OnToolStripMenuItemFileDropDownOpening;
			this.toolStripMenuItemRecentFiles.DropDownOpening += this.OnToolStripMenuItemRecentFilesDropDownOpening;
			this.toolStripMenuItemEdit.DropDownOpening += this.OnToolStripMenuItemEditDropDownOpening;
			this.toolStripMenuItemView.DropDownOpening += this.OnToolStripMenuItemViewDropDownOpening;

			this.toolStripTextBoxXpath.KeyDown += this.OnToolStripTextBoxXpathKeyDown;
			this.toolStripTextBoxXpath.TextChanged += this.OnToolStripTextBoxXpathTextChanged;

			// where applicable, I wire the click events of multiple tools to the same event handler
			// to reduce redundant code (for example, for the Open menu item and the Open toolbar button).
			this.contextMenuStripMenuItemClose.Click += this.OnToolStripMenuItemCloseClick;
			this.toolStripMenuItemClose.Click += this.OnToolStripMenuItemCloseClick;

			this.toolStripMenuItemClearRecentFileList.Click += this.OnToolStripMenuItemClearRecentFileListClick;
			this.toolStripMenuItemExit.Click += this.OnToolStripMenuItemExitClick;
			this.toolStripMenuItemFont.Click += this.OnToolStripMenuItemFontClick;

			this.toolStripMenuItemUseHighlighting.Click += this.OnToolStripMenuItemUseHighlightingClick;

			this.toolStripMenuItemNewFromClipboard.Click += this.OnToolStripButtonNewFromClipboardClick;

			this.toolStripMenuItemOpen.Click += this.OnToolStripButtonOpenClick;
			this.toolStripButtonOpen.Click += this.OnToolStripButtonOpenClick;

			this.toolStripMenuItemOpenUrl.Click += this.OnToolStripMenuItemOpenUrlClick;
			this.toolStripButtonOpenUrl.Click += this.OnToolStripMenuItemOpenUrlClick;

			this.toolStripMenuItemOpenInEditor.Click += this.OnToolStripButtonOpenInEditorClick;

			this.toolStripButtonRefresh.Click += this.OnToolStripButtonRefreshClick;
			this.toolStripMenuItemRefresh.Click += this.OnToolStripButtonRefreshClick;

			this.toolStripButtonCopyFormattedOuterXml.Click += this.OnToolStripButtonCopyFormattedOuterXmlClick;
			this.toolStripMenuItemCopyFormattedOuterXml.Click += this.OnToolStripButtonCopyFormattedOuterXmlClick;
			this.contextMenuStripNodesItemCopyFormattedOuterXml.Click += this.OnToolStripButtonCopyFormattedOuterXmlClick;

			this.toolStripMenuItemCopy.Click += this.OnToolStripMenuItemCopyClick;
			this.contextMenuStripNodesItemCopy.Click += this.OnToolStripMenuItemCopyClick;

			this.toolStripMenuItemCopyXPath.Click += this.OnToolStripMenuItemCopyXPathClick;
			this.contextMenuStripNodesItemCopyXPath.Click += this.OnToolStripMenuItemCopyXPathClick;

			this.toolStripMenuItemCollapseAll.Click += this.OnToolStripMenuItemCollapseAllClick;
			this.contextMenuStripItemNodesCollapeAll.Click += this.OnToolStripMenuItemCollapseAllClick;
			this.toolStripButtonCollapseAll.Click += this.OnToolStripMenuItemCollapseAllClick;

			this.toolStripMenuItemExpandAll.Click += this.OnToolStripMenuItemExpandAllClick;
			this.contextMenuStripItemNodesExpandAll.Click += this.OnToolStripMenuItemExpandAllClick;
			this.toolStripButtonExpandAll.Click += this.OnToolStripMenuItemExpandAllClick;

			this.toolStripButtonPrevious.Click += this.OnToolStripButtonPreviousClick;
			this.toolStripButtonNext.Click += this.OnToolStripButtonNextClick;
			this.toolStripButtonLaunchXpath.Click += this.OnToolStripButtonLaunchXpathClick;

			this.toolStripButtonSave.Click += this.OnToolStripButtonSaveClick;
			this.toolStripMenuItemSaveWithFormatting.Click += this.OnToolStripButtonSaveClick;
			this.toolStripMenuItemSaveAs.Click += this.OnToolStripMenuItemSaveAsClick;
			this.toolStripMenuItemSaveWithoutFormatting.Click += this.OnToolStripMenuItemSaveWithoutFormattingClick;
			this.toolStripMenuItemSaveAsWithoutFormatting.Click += this.OnToolStripMenuItemSaveAsWithoutFormattingClick;

			this.toolStripMenuItemCopyFullPath.Click += this.OnToolStripMenuItemCopyFullPathClick;
			this.toolStripMenuItemOpenContainingFolder.Click += this.OnToolStripMenuItemOpenContainingFolderClick;

			this.toolStripMenuItemCheckForUpdates.Click += this.OnToolStripMenuItemCheckForUpdatesClick;
			this.toolStripMenuItemAbout.Click += this.OnToolStripMenuItemAboutClick;

			this.toolStripStatusLabelChildCount.Text = string.Empty;
			this.toolStripStatusLabelLoadTime.Text = string.Empty;

			this.toolStripButtonXPathExpression.Click += this.OnToolStripButtonXPathExpressionClick;

			this.toolStripMenuItemOptions.Click += this.OnToolStripMenuItemOptionsClick;

			this.RecentlyUsedFiles = new RecentlyUsedFilesStack();
			this.MinimumReleaseStatus = ReleaseStatus.Stable;

			// set up the expressions window
			_expressionsWindow = new ExpressionsWindow();
			_expressionsWindow.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
			_expressionsWindow.SelectedExpressionChanged += this.OnExpressionsWindow_SelectedExpressionChanged;
			_expressionsWindow.ExpressionsActivated += this.OnExpressionsWindow_ExpressionsActivated;

			// set up the namespaces window
			_namespacesWindow = new NamespacesWindow();
			_namespacesWindow.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;

			// set up the errors window
			_errorWindow = new ErrorWindow();
			_errorWindow.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
			_errorWindow.ErrorActivated += this.OnErrorWindow_ErrorActivated;
			_errorWindow.BrowseClicked += this.OnErrorWindow_BrowseClicked;

			// set up the settings window
			_settingsWindow = new SettingsWindow();
			_settingsWindow.ShowHint = DockState.DockRight;

			_deserializeDockContent = new DeserializeDockContent(this.GetContentFromPersistString);

			this.MdiChildActivate += this.OnMdiChildActivate;

			this.UpdateTools();
		}

		#endregion

		#region Properties

		public Font TreeFont
		{
			get { return _treeFont; }

			set
			{
				_treeFont = value;
				this.ApplyFont(_treeFont);
			}
		}

		public Color TreeForeColor
		{
			get { return _treeForeColor; }

			set
			{
				_treeForeColor = value;
				this.ApplyForeColor(_treeForeColor);
			}
		}

		public AutoCompleteMode AutoCompleteMode
		{
			get { return this.toolStripTextBoxXpath.AutoCompleteMode; }
			set { this.toolStripTextBoxXpath.AutoCompleteMode = value; }
		}

		/// <summary>
		/// Gets or sets whether the custom drawing of syntax highlights
		/// should be bypassed. This can be optionally used to improve 
		/// performance on large documents.
		/// </summary>
		public bool UseSyntaxHighlighting
		{
			get { return _useSyntaxHighlighting; }
			set
			{
				_useSyntaxHighlighting = value;
				this.toolStripMenuItemUseHighlighting.Checked = _useSyntaxHighlighting;
				this.SetXmlExplorerWindowHighlighting(_useSyntaxHighlighting);
			}
		}

		public XPathExpressionLibrary Expressions
		{
			get
			{
				return _expressionsWindow.Expressions;
			}

			set
			{
				_expressionsWindow.Expressions = value;
			}
		}

		public DockPanel DockPanel
		{
			get
			{
				return this.dockPanel;
			}
		}

		public DeserializeDockContent DeserializeDockContent
		{
			get
			{
				return _deserializeDockContent;
			}
		}

		public string DockSettingsFilename
		{
			get
			{
				return _dockSettingsFilename;
			}

			set
			{
				_dockSettingsFilename = value;
			}
		}

		public RecentlyUsedFilesStack RecentlyUsedFiles { get; set; }

		public string AutoUpdateUrl { get; set; }

		public ReleaseStatus MinimumReleaseStatus { get; set; }

		public ApplicationSettingsBase Settings
		{
			get
			{
				return _settingsWindow.Settings;
			}

			set
			{
				if (_settingsWindow.Settings != null)
					_settingsWindow.Settings.SettingChanging -= this.OnSettingChanging;

				_settingsWindow.Settings = value;

				if (_settingsWindow.Settings != null)
					_settingsWindow.Settings.SettingChanging += this.OnSettingChanging;
			}
		}

		public ChildNodeDefinitionCollection ChildNodeDefinitions
		{
			get
			{
				return _childNodeDefinitions;
			}

			set
			{
				_childNodeDefinitions = value;
				this.SetChildNodeDefinitions(_childNodeDefinitions);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Applies any changes to the font and forecolor performed from the font dialog.
		/// </summary>
		public void ApplyFont()
		{
			this.ApplyFont(_fontDialog.Font);
			this.ApplyForeColor(_fontDialog.Color);
		}

		public void ApplyFont(Font font)
		{
			// save the font
			_treeFont = font;

			// apply the font to any open windows
			this.SetXmlExplorerWindowFonts(font);
		}

		public void ApplyForeColor(Color color)
		{
			// save the forecolor
			_treeForeColor = color;

			// apply the forecolor to any open windows
			this.SetXmlExplorerWindowForeColors(color);
		}

		/// <summary>
		/// Creates a font dialog, and initializes it with the current saved settings.
		/// </summary>
		private void InitializeFontDialog()
		{
			if (_fontDialog == null)
			{
				_fontDialog = new FontDialog();
				_fontDialog.Apply += this.OnFontDialogApply;
				_fontDialog.ShowApply = true;
				_fontDialog.ShowColor = true;
				_fontDialog.ShowEffects = true;
				_fontDialog.Font = _treeFont;
				_fontDialog.Color = _treeForeColor;
			}
		}

		public void NewFromClipboard()
		{
			// create a window
			XmlExplorerWindow window = this.CreateXmlExplorerWindow();

			// show the window
			window.Show(this.dockPanel);

			window.TreeView.BeginOpen(Clipboard.GetText(TextDataFormat.UnicodeText));

			this.UpdateTools();
		}

		// Shows the open file dialog, and opens the file(s) specified by the user, if any.
		private void Open()
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Filter = "All Files (*.*)|*.*|Xml Documents (*.xml)|*.xml";
				dialog.Multiselect = true;
				if (dialog.ShowDialog(this) == DialogResult.OK)
				{
					this.Open(dialog.FileNames);
				}
			}
		}

		/// <summary>
		/// Opens a window for each specified file.
		/// </summary>
		/// <param name="filenames">The full paths of the files to open.</param>
		public void Open(ReadOnlyCollection<string> filenames)
		{
			foreach (string text in filenames)
			{
				this.Open(text);
			}
		}

		/// <summary>
		/// Opens a window for each specified file.
		/// </summary>
		/// <param name="filenames">The full paths of the files to open.</param>
		public void Open(string[] filenames)
		{
			foreach (string filename in filenames)
			{
				this.Open(filename);
			}
		}

		/// <summary>
		/// Opens a window for the specified file.
		/// </summary>
		/// <param name="filename">The full path of the file to open.</param>
		public void Open(string filename)
		{
			// create a window
			XmlExplorerWindow window = this.CreateXmlExplorerWindow();

			// show the window
			window.Show(this.dockPanel);

			// instruct the window to open the specified file.
			window.TreeView.BeginOpen(new FileInfo(filename));

			this.RecentlyUsedFiles.Add(filename);

			//if (window.XPathNavigatorTreeView.DefaultNamespaceCount > 0)
			//    this.dockPanel.activ(this.NamespaceListDockableContent);

			//if (xpathDocumentContent.TreeView.Errors != null && xpathDocumentContent.TreeView.Errors.Count > 0)
			//    this.dockingManager.Show(this.ErrorListDockableContent);

			this.UpdateTools();
		}

		/// <summary>
		/// Opens a window for the specified node set.  This method is used to open XPath expressions that 
		/// evaluate to a node set.
		/// </summary>
		/// <param name="iterator">An XPathNodeIterator with which to open a window.</param>
		public void Open(XPathNodeIterator iterator)
		{
			// create a window
			XmlExplorerWindow window = this.CreateXmlExplorerWindow();

			//// instruct the window to open the specified file.
			window.TreeView.LoadNodes(iterator);

			// show the window
			window.Show(this.dockPanel);

			this.UpdateTools();
		}

		private void Reload()
		{
			// get the selected window
			XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
			if (window == null)
				return;

			if (window.TreeView.FileInfo != null)
				window.TreeView.Reload();
			else if (window.TreeView.Uri != null)
				window.BeginOpenUri(window.TreeView.Uri.ToString());
		}

		public void OpenUri()
		{
			using (UrlOpenDialog dialog = new UrlOpenDialog())
			{
				if (dialog.ShowDialog(this) != DialogResult.OK)
					return;

				this.OpenUri(dialog.Url);
			}
		}

		public void OpenUri(string inputUri)
		{
			this.OpenUri(inputUri, false);
		}

		public void OpenUri(string inputUri, bool ignoreSslPolicyErrors)
		{
			// create a window
			XmlExplorerWindow window = this.CreateXmlExplorerWindow();

			// show the window
			window.Show(this.dockPanel);

			// instruct the window to open the specified file.
			window.BeginOpenUri(inputUri);

			this.RecentlyUsedFiles.Add(inputUri);

			this.UpdateTools();
		}

		/// <summary>
		/// Opens the selected window's file in the default editor for it's file type.
		/// </summary>
		private void OpenInEditor()
		{
			try
			{
				// get the selected window
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
				if (window == null)
					return;

				FileInfo fileInfo = window.TreeView.FileInfo;

				if (fileInfo == null)
					return;

				ProcessStartInfo info = new ProcessStartInfo(fileInfo.FullName);
				info.Verb = "Edit";

				Process.Start(info);
			}
			catch (System.ComponentModel.Win32Exception ex)
			{
				Debug.WriteLine(ex);

				if (ex.NativeErrorCode == 1155)
				{
					MessageBox.Show("No application is associated with the specified file for the verb 'Edit'.");
				}
				else
				{
					MessageBox.Show(this, ex.ToString());
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		/// <summary>
		/// Returns an initialized XmlExplorerWindow.
		/// </summary>
		/// <returns></returns>
		private XmlExplorerWindow CreateXmlExplorerWindow()
		{
			// the main window handle needs to be created before creating a new window
			if (!this.IsHandleCreated)
				this.CreateHandle();

			XmlExplorerWindow window = new XmlExplorerWindow();

			window.Icon = this.Icon;

			// read the options for the window, such as font and forecolor
			this.ReadWindowOptions(window);

			// wire to the window's events
			window.FormClosed += this.OnChildWindowFormClosed;
			window.TreeView.AfterSelect += this.OnWindow_XmlTreeView_AfterSelect;
			window.TreeView.MouseUp += this.OnWindow_XmlTreeView_MouseUp;
			window.TreeView.LoadingFinished += this.OnDocumentLoaded;
			window.TabPageContextMenuStrip = this.contextMenuStripTabs;

			window.MdiParent = this;

			return window;
		}

		/// <summary>
		/// Initializes an XmlExplorerWindow with saved settings.
		/// </summary>
		/// <param name="window">The XmlExplorerWindow to initialize.</param>
		private void ReadWindowOptions(XmlExplorerWindow window)
		{
			window.XmlFont = _treeFont;
			window.XmlForeColor = _treeForeColor;
			window.UseSyntaxHighlighting = _useSyntaxHighlighting;
			window.ChildNodeDefinitions = _childNodeDefinitions;
		}

		/// <summary>
		/// Shows the font dialog, and applies any changes made
		/// </summary>
		public void ShowFontDialog()
		{
			// load any saved settings for the font dialog
			this.InitializeFontDialog();
			if (_fontDialog.ShowDialog(this) == DialogResult.OK)
			{
				// apply font changes
				this.ApplyFont();
			}
		}

		/// <summary>
		/// Toggles the use of syntax highlighing
		/// </summary>
		public void TogggleSyntaxHighlighting()
		{
			this.ApplySyntaxHighlighting(!_useSyntaxHighlighting);
		}

		private void ApplySyntaxHighlighting(bool value)
		{
			_useSyntaxHighlighting = value;

			this.toolStripMenuItemUseHighlighting.Checked = _useSyntaxHighlighting;
			this.SetXmlExplorerWindowHighlighting(_useSyntaxHighlighting);
		}

		/// <summary>
		/// Evaluates the specified XPath expression against the xml content of the currently selected window.
		/// </summary>
		/// <param name="xpath">An XPath expression to evaluate against the currently selected window.</param>
		/// <returns></returns>
		private bool FindByXpath(string xpath)
		{
			return this.FindByXpath(xpath, Direction.Down);
		}

		private bool FindByXpath(string xpath, Direction direction)
		{
			bool result = false;

			try
			{
				// get the selected window, returning if none are found
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
				if (window == null)
					return false;

				// instruct the window to perform the expression evaluation
				result = window.TreeView.FindByXpath(xpath, direction);

				if (result)
				{
					this.toolStripStatusLabelMain.Text = "";
				}
				else
				{
					this.toolStripStatusLabelMain.Text = "No matches were found";
					this.toolStripLabelResults.Visible = false;
				}

				this.UpdateXPathTools(window.TreeView);
			}
			catch (System.Xml.XPath.XPathException ex)
			{
				// an invalid XPath expression has been attempted
				Debug.WriteLine(ex);

				// inform the user
				this.toolStripStatusLabelMain.Text = ex.Message;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}

			return result;
		}

		private void UpdateXPathTools(XPathNavigatorTreeView treeView)
		{
			if (treeView != null && treeView.ResultCount > 0)
			{
				this.toolStripLabelResults.Visible = true;
				this.toolStripLabelResults.Text = string.Format("{0} of {1}", treeView.CurrentResultPosition, treeView.ResultCount);

				this.toolStripButtonPrevious.Enabled = treeView.CurrentResultPosition > 1;
				this.toolStripButtonNext.Enabled = treeView.CurrentResultPosition < treeView.ResultCount;
			}
			else
			{
				this.toolStripLabelResults.Visible = false;
				this.toolStripButtonNext.Enabled = false;
				this.toolStripButtonPrevious.Enabled = false;
			}

			this.toolStripStandardButtons.PerformLayout();
		}

		/// <summary>
		/// Opens a new window with the evaluated results of the specified XPath expression.
		/// </summary>
		/// <param name="xpath">An XPath expression to evaluate against the currently selected window.</param>
		/// <returns></returns>
		private bool LaunchXpathResults(string xpath)
		{
			try
			{
				// get the selected window
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
				if (window == null)
					return false;

				// evaluate the expression
				XPathNodeIterator iterator = window.TreeView.SelectXmlNodes(xpath) as XPathNodeIterator;

				// check for empty results
				if (iterator == null || iterator.Count < 1)
					return false;

				// open the results in a new window
				this.Open(iterator);

				return true;
			}
			catch (XPathException ex)
			{
				// an invalid XPath expression has been attempted
				Debug.WriteLine(ex);

				// inform the user
				this.toolStripStatusLabelMain.Text = ex.Message;
			}
			catch (ArgumentException ex)
			{
				Debug.WriteLine(ex);
				this.toolStripStatusLabelMain.Text = ex.Message;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}

			return false;
		}

		/// <summary>
		/// Overwrites the selected window's file with XML formatting (tabs and crlf's)
		/// </summary>
		private void Save(bool formatting)
		{
			try
			{
				// get the selected window
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
				if (window == null)
					return;

				// instruct the window to save with formatting
				window.TreeView.Save(formatting);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		/// <summary>
		/// Prompts the user to save a copy of the selected window's XML file.
		/// </summary>
		private void SaveAs(bool formatting)
		{
			try
			{
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
				if (window == null)
					return;
				window.TreeView.SaveAs(formatting);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		/// <summary>
		/// Applies a fore color to all of the currently open windows.
		/// </summary>
		/// <param name="color">A color to apply as the fore color.</param>
		private void SetXmlExplorerWindowForeColors(Color color)
		{
			foreach (Form window in this.MdiChildren)
			{
				XmlExplorerWindow xmlExplorerWindow = window as XmlExplorerWindow;

				if (xmlExplorerWindow == null)
					continue;

				xmlExplorerWindow.XmlForeColor = color;
			}
		}

		/// <summary>
		/// Applies a font to all of the currently open windows.
		/// </summary>
		/// <param name="font">A font to apply.</param>
		private void SetXmlExplorerWindowFonts(Font font)
		{
			foreach (Form window in this.MdiChildren)
			{
				XmlExplorerWindow xmlExplorerWindow = window as XmlExplorerWindow;

				if (xmlExplorerWindow == null)
					continue;

				xmlExplorerWindow.XmlFont = font;
			}
		}

		/// <summary>
		/// Applies syntax highlighting changes to all of the currently open windows.
		/// </summary>
		/// <param name="useSyntaxHighlighting">Whether or not to use syntax highlighting.</param>
		private void SetXmlExplorerWindowHighlighting(bool useSyntaxHighlighting)
		{
			foreach (Form window in this.MdiChildren)
			{
				XmlExplorerWindow xmlExplorerWindow = window as XmlExplorerWindow;

				if (xmlExplorerWindow == null)
					continue;

				xmlExplorerWindow.UseSyntaxHighlighting = useSyntaxHighlighting;
			}
		}

		private void SetChildNodeDefinitions(ChildNodeDefinitionCollection childNodeDefinitions)
		{
			foreach (Form window in this.MdiChildren)
			{
				XmlExplorerWindow xmlExplorerWindow = window as XmlExplorerWindow;

				if (xmlExplorerWindow == null)
					continue;

				xmlExplorerWindow.ChildNodeDefinitions = childNodeDefinitions;
			}
		}

		/// <summary>
		/// Copies the current window's selected xml node (and all of it's sub nodes) to the clipboard
		/// as formatted XML text.
		/// </summary>
		private void CopyFormattedOuterXml()
		{
			// get the selectd window
			XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
			if (window == null)
				return;

			// instruct the window to copy
			window.TreeView.CopyFormattedOuterXml();
		}

		/// <summary>
		/// Copies the current window's selected xml node to the clipboard
		/// as XML text.
		/// </summary>
		private void CopyNodeText()
		{
			// get the selectd window
			XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
			if (window == null)
				return;

			// instruct the window to copy
			window.TreeView.CopyNodeText();
		}

		private void UpdateXPathExpressionTool(string text)
		{
			if (_expressionsWindow.Expressions.Contains(text))
			{
				this.toolStripButtonXPathExpression.Image = Properties.Resources.star;
				this.toolStripButtonXPathExpression.ToolTipText = "Edit expression";
			}
			else
			{
				this.toolStripButtonXPathExpression.Image = Properties.Resources.unstarred;
				this.toolStripButtonXPathExpression.ToolTipText = "Add expression to library";
			}
		}

		private void UpdateXPathAutoComplete(string text)
		{
			try
			{
				// we'll update the autocomplete custom source every time a / is entered
				if (string.IsNullOrEmpty(text) || !text.EndsWith("/"))
					return;

				// append a * to return all matching nodes
				string xpath = text + "*";

				// get the selected window
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
				if (window == null)
					return;

				// evaluate the expression
				XPathNodeIterator nodes = window.TreeView.SelectXmlNodes(xpath) as XPathNodeIterator;

				// return if an empty result set is returned
				if (nodes == null || nodes.Count < 1)
					return;

				// create a new autocomplete source
				AutoCompleteStringCollection source = new AutoCompleteStringCollection();

				// add the full path of each xml node to the source
				while (nodes.MoveNext())
				{
					string fullPath = text + nodes.Current.Name;

					// eliminate duplicates
					if (!source.Contains(fullPath))
						source.Add(fullPath);
				}

				// update the autocomplete source of the textbox
				// I beleive this is the source of the exception mentioned above
				this.toolStripTextBoxXpath.AutoCompleteCustomSource = source;
			}
			catch (System.Xml.XPath.XPathException ex)
			{
				Debug.WriteLine(ex);
			}
		}

		private void HandleExpressionKeyPress(KeyEventArgs e, string xpath)
		{
			this.toolStripTextBoxXpath.Text = xpath;
			if (e.Shift)
			{
				// reset any xpath expression error indicators
				this.toolStripTextBoxXpath.BackColor = SystemColors.Window;
				this.toolStripStatusLabelMain.Text = string.Empty;

				// Shift-Enter has been pressed, evaluate the expression
				// if successful, open results in a new window
				// if there is a problem with the expression, notify the user
				// by highlighting the XPath text box
				if (!this.LaunchXpathResults(xpath))
					this.toolStripTextBoxXpath.BackColor = Color.LightPink;
			}
			else
			{
				// Enter has been pressed
				this.SelectNextResult(xpath);
			}
		}

		private void SelectNextResult(string xpath)
		{
			this.SelectResult(xpath, Direction.Down);
		}

		private void SelectResult(string xpath, Direction direction)
		{
			// reset any xpath expression error indicators
			this.toolStripTextBoxXpath.BackColor = SystemColors.Window;
			this.toolStripStatusLabelMain.Text = string.Empty;

			// evaluate the expression
			// if successful, the next node of the result set will be selected
			// if there is a problem with the expression, notify the user
			// by highlighting the XPath text box
			if (!this.FindByXpath(xpath, direction))
				this.toolStripTextBoxXpath.BackColor = Color.LightPink;
			this.toolStripTextBoxXpath.SelectionStart = this.toolStripTextBoxXpath.TextLength;
		}

		private void UpdateTools()
		{
			this.UpdateTools(false);
		}

		private void UpdateTools(bool isLastWindowClosing)
		{
			try
			{
				// update any tools that depend on one or more windows being open
				bool hasOpenWindows = this.MdiChildren.Length > 0 && !isLastWindowClosing;

				bool clipboardContainsText = Clipboard.ContainsText();

				this.toolStripMenuItemNewFromClipboard.Enabled = clipboardContainsText;

				this.toolStripMenuItemOpenInEditor.Enabled = hasOpenWindows;
				this.toolStripMenuItemClose.Enabled = hasOpenWindows;
				this.toolStripMenuItemSaveAs.Enabled = hasOpenWindows;
				this.toolStripButtonSave.Enabled = hasOpenWindows;
				this.toolStripMenuItemSaveAs.Enabled = hasOpenWindows;
				this.toolStripMenuItemSaveAsWithoutFormatting.Enabled = hasOpenWindows;
				this.toolStripMenuItemSaveWithFormatting.Enabled = hasOpenWindows;
				this.toolStripMenuItemSaveWithoutFormatting.Enabled = hasOpenWindows;

				this.toolStripMenuItemCopyFormattedOuterXml.Enabled = hasOpenWindows;
				this.toolStripButtonCopyFormattedOuterXml.Enabled = hasOpenWindows;
				this.toolStripMenuItemCopy.Enabled = hasOpenWindows;
				this.toolStripMenuItemCopyXPath.Enabled = hasOpenWindows;

				this.toolStripMenuItemExpandAll.Enabled = hasOpenWindows;
				this.toolStripButtonExpandAll.Enabled = hasOpenWindows;

				this.toolStripMenuItemCollapseAll.Enabled = hasOpenWindows;
				this.toolStripButtonCollapseAll.Enabled = hasOpenWindows;

				this.toolStripMenuItemRefresh.Enabled = hasOpenWindows;
				this.toolStripButtonRefresh.Enabled = hasOpenWindows;

				this.toolStripTextBoxXpath.Enabled = hasOpenWindows;
				this.toolStripButtonNext.Enabled = hasOpenWindows;
				this.toolStripButtonPrevious.Enabled = hasOpenWindows;
				this.toolStripButtonLaunchXpath.Enabled = hasOpenWindows;
				this.toolStripButtonXPathExpression.Enabled = hasOpenWindows;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private IDockContent GetContentFromPersistString(string persistString)
		{
			if (persistString == typeof(ExpressionsWindow).ToString())
				return _expressionsWindow;
			else if (persistString == typeof(ErrorWindow).ToString())
				return _errorWindow;
			else if (persistString == typeof(NamespacesWindow).ToString())
				return _namespacesWindow;
			else if (persistString == typeof(SettingsWindow).ToString())
				return _settingsWindow;

			return null;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			try
			{
				if (!_expressionsWindow.Visible)
					_expressionsWindow.Show(this.dockPanel);
				if (!_namespacesWindow.Visible)
					_namespacesWindow.Show(this.dockPanel);
				if (!_errorWindow.Visible)
					_errorWindow.Show(this.dockPanel);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(ex.ToString());
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);

			try
			{
				string directoryName = Path.GetDirectoryName(_dockSettingsFilename);

				if (!Directory.Exists(directoryName))
					Directory.CreateDirectory(directoryName);

				this.dockPanel.SaveAsXml(_dockSettingsFilename);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(ex.ToString());
			}
		}

		public void CheckForUpdates(bool userRequested)
		{
			Thread thread = new Thread(delegate()
			{
				WebClient webClient = new WebClient();
				webClient.Proxy.Credentials = CredentialCache.DefaultCredentials;
				webClient.DownloadDataCompleted += this.OnDownloadDataCompleted;
				webClient.DownloadDataAsync(new Uri(this.AutoUpdateUrl), userRequested);
			});

			thread.IsBackground = true;
			thread.Start();
		}

		public void ExpandAll()
		{
			// get the selected window
			XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
			if (window == null)
				return;

			// instruct the window to copy
			window.TreeView.ExpandAll();
		}

		public void CollapseAll()
		{
			// get the selected window
			XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
			if (window == null)
				return;

			// instruct the window to copy
			window.TreeView.CollapseAll();
		}

		private void UpdateCurrentDocumentInformation()
		{
			this.UpdateErrorList();
			this.UpdateNamespaceList();

			string loadedTime = null;
			string windowText = "XML Explorer";
			XPathNavigatorTreeView treeView = null;
			bool isLoading = false;

			// get the selected window
			XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
			if (window != null)
			{
				windowText = string.Format("XML Explorer - [{0}]", window.Text);

				isLoading = window.TreeView.IsLoading;

				if (!isLoading)
					loadedTime = string.Format("Loaded in {0:N3} seconds", window.TreeView.LoadTime.TotalMilliseconds / 1000D);

				treeView = window.TreeView;
			}

			// update the main window text
			this.Text = windowText;

			this.toolStripStatusLabelLoadTime.Text = loadedTime;

			this.toolStripProgressBar.Visible = isLoading;

			this.UpdateSelectedNodeInformation(treeView);

			this.UpdateXPathTools(treeView);
		}

		private void UpdateErrorList()
		{
			List<Error> items = null;

			// get the selectd window
			XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
			if (window != null)
				items = window.TreeView.Errors;

			_errorWindow.Errors = items;
		}

		private void UpdateNamespaceList()
		{
			List<NamespaceDefinition> items = null;

			// get the selectd window
			XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
			if (window != null)
				items = window.TreeView.NamespaceDefinitions;

			_namespacesWindow.NamespaceDefinitions = items;
		}

		private void UpdateSelectedNodeInformation(XPathNavigatorTreeView treeView)
		{
			XPathNavigatorTreeNode node = null;
			if (treeView != null)
				node = treeView.SelectedNode as XPathNavigatorTreeNode;

			this.UpdateSelectedNodeInformation(node, treeView);
		}

		private void UpdateSelectedNodeInformation(XPathNavigatorTreeNode node, XPathNavigatorTreeView treeView)
		{
			string countText = "";
			string pathText = "";

			if (node != null)
			{
				// get the xml node's child count
				int count = node.Navigator.SelectChildren(XPathNodeType.Element).Count;

				countText = string.Format("{0} child node{1}", count, count == 1 ? string.Empty : "s");

				// get the xml node's path
				if (treeView != null)
					pathText = treeView.GetXmlNodeFullPath(node.Navigator);
			}

			// update the status bar with the child count
			this.toolStripStatusLabelChildCount.Text = countText;

			// update the status bar with the full path of the selected xml node
			this.toolStripStatusLabelMain.Text = pathText;
		}

		private void BrowseForSchemaFiles(XmlExplorerWindow window)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Multiselect = true;
				dialog.Filter = "XML Schema Files (*.xsd)|*.xsd|All Files (*.*)|*.*";
				if (dialog.ShowDialog(window) != System.Windows.Forms.DialogResult.OK)
					return;

				window.TreeView.Validate(null, dialog.FileNames);

				this.UpdateErrorList();
			}
		}

		private void ShowOptions()
		{
			if (!_settingsWindow.Visible)
				_settingsWindow.Show(this.dockPanel);
		}

		private void ExportChildNodeDefinitions()
		{
			using (SaveFileDialog dialog = new SaveFileDialog())
			{
				dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
				dialog.FileName = "Child Node Definitions.xml";
				if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
					return;

				this.ExportChildNodeDefinitions(dialog.FileName);
			}
		}

		private void ExportChildNodeDefinitions(string fileName)
		{
			this.ChildNodeDefinitions.Serialize(fileName);
		}

		private void ImportChildNodeDefinitions()
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

				if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
					return;

				this.ImportChildNodeDefinitions(dialog.FileName);
			}
		}

		private void ImportChildNodeDefinitions(string fileName)
		{
			this.ChildNodeDefinitions.Import(fileName);
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Occurs when one or more file paths are dropped onto the window.
		/// </summary>
		private void OnDragDrop(object sender, DragEventArgs e)
		{
			// if it's not files being dropped, cancel the operation.
			if (!e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.None;
				return;
			}

			// get the full paths of the file(s) dropped
			string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);

			// open the files
			this.Open(filenames);

			// allow the operation to complete
			e.Effect = DragDropEffects.Copy;
		}

		/// <summary>
		/// Occurs when one or more files are dragged over the window.
		/// </summary>
		private void OnDragOver(object sender, DragEventArgs e)
		{
			// if it's not files being dropped, cancel the operation
			if (!e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.None;
				return;
			}

			// allow the operation
			e.Effect = DragDropEffects.Copy;
		}

		/// <summary>
		/// Occurs when changes in the font dialog need applied.
		/// </summary>
		private void OnFontDialogApply(object sender, EventArgs e)
		{
			try
			{
				// apply the font changes
				this.ApplyFont();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnMdiChildActivate(object sender, EventArgs e)
		{
			try
			{
				// get the window
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;

				// unwire from child mdi window events
				foreach (Form form in this.MdiChildren)
				{
					XmlExplorerWindow otherWindow = form as XmlExplorerWindow;
					if (otherWindow == null)
						continue;

					otherWindow.TreeView.KeyDown -= this.OnWindow_XmlTreeView_KeyDown;
				}

				// wire to the newly active child mdi window events
				if (window != null)
					window.TreeView.KeyDown += this.OnWindow_XmlTreeView_KeyDown;

				this.UpdateCurrentDocumentInformation();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnWindow_XmlTreeView_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				switch (e.KeyCode)
				{
					case Keys.C:
						if (e.Control)
							// handle CTRL-C
							this.CopyNodeText();
						break;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		/// <summary>
		/// Occurs when the selected xml node of a window has changed.
		/// </summary>
		private void OnWindow_XmlTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				// get the newly selected node
				XPathNavigatorTreeNode node = e.Node as XPathNavigatorTreeNode;
				if (node == null)
					return;

				if (node.Navigator == null)
					return;

				XPathNavigatorTreeView treeView = sender as XPathNavigatorTreeView;
				if (treeView == null)
					return;

				this.UpdateSelectedNodeInformation(node, treeView);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnWindow_XmlTreeView_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button != MouseButtons.Right)
					return;

				// get the selectd window
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
				if (window == null)
					return;

				// get the node that was clicked
				TreeViewHitTestInfo info = window.TreeView.HitTest(e.Location);

				if (info.Node == null)
					return;

				XPathNavigatorTreeNode node = info.Node as XPathNavigatorTreeNode;
				if (node == null)
					return;

				if (node.Navigator == null)
					return;

				XPathNavigatorTreeView treeView = sender as XPathNavigatorTreeView;
				if (treeView == null)
					return;

				treeView.SelectedNode = node;

				this.contextMenuStripNodes.Show(treeView, e.Location);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnExpressionsWindow_ExpressionsActivated(object sender, EventArgs e)
		{
			try
			{
				// reset any xpath expression error indicators
				this.toolStripTextBoxXpath.BackColor = SystemColors.Window;
				this.toolStripStatusLabelMain.Text = string.Empty;

				if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
				{
					// hold shift to launch
					foreach (String xpath in _expressionsWindow.SelectedExpressions)
					{
						// evaluate the expression
						// if successful, open results in a new window
						// if there is a problem with the expression, notify the user
						// by highlighting the XPath text box
						if (!this.LaunchXpathResults(xpath))
							this.toolStripTextBoxXpath.BackColor = Color.LightPink;
					}
				}
				else if (_expressionsWindow.SelectedExpressions.Count > 0)
				{
					String xpath = _expressionsWindow.SelectedExpressions[0];

					// evaluate the expression
					// if successful, the first node of the result set will be selected
					// (selection is performed by the window)
					// if there is a problem with the expression, notify the user
					// by highlighting the XPath text box
					if (!this.FindByXpath(xpath))
						this.toolStripTextBoxXpath.BackColor = Color.LightPink;

					this.toolStripTextBoxXpath.Text = xpath;
					this.toolStripTextBoxXpath.SelectionStart = this.toolStripTextBoxXpath.TextLength;

					this.UpdateXPathExpressionTool(xpath);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnExpressionsWindow_SelectedExpressionChanged(object sender, EventArgs e)
		{
			try
			{
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		void OnErrorWindow_ErrorActivated(object sender, EventArgs<Error> e)
		{
			try
			{
				if (e.Item == null)
					return;

				if (e.Item.SourceObject == null)
				{
					// is it a no schemas warning?
					if (e.Item.Description == XmlExplorer.TreeView.XPathNavigatorTreeView.NoSchemaWarning)
					{
						this.BrowseForSchemaFiles(this.ActiveMdiChild as XmlExplorerWindow);
						return;
					}
				}

				XPathNavigator navigator = e.Item.SourceObject as XPathNavigator;
				if (navigator == null)
					return;

				// get the selectd window
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
				if (window == null)
					return;

				window.TreeView.SelectXmlTreeNode(navigator);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		void OnErrorWindow_BrowseClicked(object sender, EventArgs e)
		{
			try
			{
				this.BrowseForSchemaFiles(this.ActiveMdiChild as XmlExplorerWindow);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnChildWindowFormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				XmlExplorerWindow window = sender as XmlExplorerWindow;

				if (window == null)
					return;

				// unwire from the window's events
				window.FormClosed -= this.OnChildWindowFormClosed;
				window.TreeView.AfterSelect -= this.OnWindow_XmlTreeView_AfterSelect;
				window.TreeView.MouseUp -= this.OnWindow_XmlTreeView_MouseUp;
				window.TabPageContextMenuStrip = null;

				this.UpdateTools(this.MdiChildren.Length == 1);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			try
			{
				if (this.InvokeRequired)
				{
					this.Invoke(new DownloadDataCompletedEventHandler(this.OnDownloadDataCompleted), sender, e);
					return;
				}

				bool userRequested = false;
				if (e.UserState is bool)
					userRequested = (bool)e.UserState;

				Version currentVersion = AboutBox.AssemblyVersion;

				ReleaseInfoCollection releases = ReleaseInfoCollection.FromRss(e.Result);

				if (releases == null)
				{
					if (userRequested)
						MessageBox.Show(this, "No updates were found.", AboutBox.AssemblyProduct);
					return;
				}

				ReleaseInfo latest = releases.GetLatest(this.MinimumReleaseStatus);

				if (latest == null)
				{
					if (userRequested)
						MessageBox.Show(this, "No updates were found.", AboutBox.AssemblyProduct);

					return;
				}

				if (latest.Version > currentVersion)
				{
					// prompt user
					string message = string.Format("{0} version {1} is available.\n\nWould you like to visit the release page?", AboutBox.AssemblyProduct, latest.Version.ToString());

					DialogResult result = MessageBox.Show(this, message, AboutBox.AssemblyProduct, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

					if (result != DialogResult.Yes)
						return;

					Process.Start(latest.Url);
					return;
				}

				if (userRequested)
					MessageBox.Show(this, string.Format("{0} is up to date (v{1})", AboutBox.AssemblyProduct, currentVersion.ToString()), AboutBox.AssemblyProduct);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(ex.ToString());
			}
		}

		void OnDocumentLoaded(object sender, EventArgs e)
		{
			try
			{
				if (this.InvokeRequired)
				{
					this.Invoke(new EventHandler(this.OnDocumentLoaded), sender, e);
					return;
				}

				// get the selected window
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
				if (window == null)
					return;

				if (window.TreeView != sender)
					return;

				this.UpdateCurrentDocumentInformation();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(ex.ToString());
			}
		}

		void OnSettingChanging(object sender, SettingChangingEventArgs e)
		{
			try
			{
				switch (e.SettingName)
				{
					case "UseSyntaxHighlighting":
						this.ApplySyntaxHighlighting((bool)e.NewValue);
						break;

					case "ForeColor":
						this.ApplyForeColor((Color)e.NewValue);
						break;

					case "Font":
						this.ApplyFont((Font)e.NewValue);
						break;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(ex.ToString());
			}
		}

		#endregion

		#region Tool Event Handlers

		private void OnToolStripMenuItemFileDropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.UpdateTools();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemEditDropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.UpdateTools();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemViewDropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.UpdateTools();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemRecentFilesDropDownOpening(object sender, EventArgs e)
		{
			try
			{
				while (this.toolStripMenuItemRecentFiles.DropDownItems.Count > 1)
				{
					this.toolStripMenuItemRecentFiles.DropDownItems[0].Click -= this.OnRecentlyUsedFileItemClick;
					this.toolStripMenuItemRecentFiles.DropDownItems.RemoveAt(0);
				}

				foreach (string filename in this.RecentlyUsedFiles)
				{
					ToolStripItem item = new ToolStripMenuItem(filename);
					this.toolStripMenuItemRecentFiles.DropDownItems.Insert(
						 this.toolStripMenuItemRecentFiles.DropDownItems.IndexOf(this.toolStripMenuItemClearRecentFileList),
						 item);
					item.Click += this.OnRecentlyUsedFileItemClick;
					item.Tag = filename;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnRecentlyUsedFileItemClick(object sender, EventArgs e)
		{
			try
			{
				ToolStripItem item = sender as ToolStripItem;
				if (item == null)
					return;

				string filename = item.Tag as string;
				if (filename == null)
					return;

				if (!File.Exists(filename))
				{
					Uri result = null;

					if (Uri.TryCreate(filename, UriKind.RelativeOrAbsolute, out result))
					{
						this.OpenUri(result.ToString());
						return;
					}
				}

				this.Open(filename);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripTextBoxXpathKeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				switch (e.KeyCode)
				{
					case Keys.Enter:
						string xpath = this.toolStripTextBoxXpath.Text;
						this.HandleExpressionKeyPress(e, xpath);
						break;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripTextBoxXpathTextChanged(object sender, EventArgs e)
		{
			// I have intermittently been getting exceptions
			// while updating the AutoCompleteCustomSource
			// I have found numerous articles online on the matter, 
			// yet have found no solution.  May require a call to MSDN support.
			try
			{
				// get the current XPath expression
				string text = this.toolStripTextBoxXpath.Text;

				//this.UpdateXPathAutoComplete(text);

				this.UpdateXPathExpressionTool(text);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemCloseClick(object sender, EventArgs e)
		{
			try
			{
				// get the window for which the context menu was opened
				XmlExplorerWindow page = this.contextMenuStripTabs.Tag as XmlExplorerWindow;
				if (page == null)
					page = this.ActiveMdiChild as XmlExplorerWindow;

				// close the window
				if (page != null)
				{
					page.Close();
				}

				// reset the context menu's tag
				this.contextMenuStripTabs.Tag = null;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemExitClick(object sender, EventArgs e)
		{
			try
			{
				// close the main application window
				base.Close();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}


		private void OnToolStripMenuItemClearRecentFileListClick(object sender, EventArgs e)
		{
			try
			{
				this.RecentlyUsedFiles.Clear();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemFontClick(object sender, EventArgs e)
		{
			try
			{
				this.ShowFontDialog();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemUseHighlightingClick(object sender, EventArgs e)
		{
			try
			{
				this.TogggleSyntaxHighlighting();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		void OnToolStripButtonNewFromClipboardClick(object sender, EventArgs e)
		{
			try
			{
				this.NewFromClipboard();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripButtonOpenClick(object sender, EventArgs e)
		{
			try
			{
				this.Open();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripButtonOpenInEditorClick(object sender, EventArgs e)
		{
			try
			{
				this.OpenInEditor();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemOpenUrlClick(object sender, EventArgs e)
		{
			try
			{
				this.OpenUri();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripButtonRefreshClick(object sender, EventArgs e)
		{
			try
			{
				XmlExplorerWindow page = this.ActiveMdiChild as XmlExplorerWindow;
				if (page != null)
				{
					this.Reload();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripButtonCopyFormattedOuterXmlClick(object sender, EventArgs e)
		{
			try
			{
				this.CopyFormattedOuterXml();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemCopyClick(object sender, EventArgs e)
		{
			try
			{
				this.CopyNodeText();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemCopyXPathClick(object sender, EventArgs e)
		{
			try
			{
				XmlExplorerWindow window = this.ActiveMdiChild as XmlExplorerWindow;
				if (window == null)
					return;

				XPathNavigatorTreeNode node = window.TreeView.SelectedNode as XPathNavigatorTreeNode;
				if (node == null)
					return;

				if (node.Navigator == null)
					return;

				this.toolStripTextBoxXpath.Text = window.TreeView.GetXmlNodeFullPath(node.Navigator);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripButtonLaunchXpathClick(object sender, EventArgs e)
		{
			try
			{
				// reset any xpath expression error indicators
				this.toolStripTextBoxXpath.BackColor = SystemColors.Window;
				this.toolStripStatusLabelMain.Text = string.Empty;

				if (!this.LaunchXpathResults(this.toolStripTextBoxXpath.Text))
					this.toolStripTextBoxXpath.BackColor = Color.LightPink;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripButtonNextClick(object sender, EventArgs e)
		{
			try
			{
				this.SelectNextResult(this.toolStripTextBoxXpath.Text);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripButtonPreviousClick(object sender, EventArgs e)
		{
			try
			{
				this.SelectResult(this.toolStripTextBoxXpath.Text, Direction.Up);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemSaveAsClick(object sender, EventArgs e)
		{
			try
			{
				this.SaveAs(true);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripButtonSaveClick(object sender, EventArgs e)
		{
			try
			{
				this.Save(true);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemSaveWithoutFormattingClick(object sender, EventArgs e)
		{
			try
			{
				this.Save(false);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemSaveAsWithoutFormattingClick(object sender, EventArgs e)
		{
			try
			{
				this.SaveAs(false);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}


		private void OnToolStripMenuItemCopyFullPathClick(object sender, EventArgs e)
		{
			try
			{
				XmlExplorerWindow page = this.ActiveMdiChild as XmlExplorerWindow;
				if (page == null)
					page = this.ActiveMdiChild as XmlExplorerWindow;

				if (page == null)
					return;

				if (page.TreeView.FileInfo != null)
				{
					Clipboard.SetText(page.TreeView.FileInfo.FullName);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
			finally
			{
				this.contextMenuStripTabs.Tag = null;
			}
		}

		private void OnToolStripMenuItemOpenContainingFolderClick(object sender, EventArgs e)
		{
			try
			{
				// get the window for which the context menu was opened
				XmlExplorerWindow page = this.contextMenuStripTabs.Tag as XmlExplorerWindow;
				if (page == null)
					page = this.ActiveMdiChild as XmlExplorerWindow;

				if (page == null)
					return;

				// open an explorer window to the folder containing the selected window's file
				if (page.TreeView.FileInfo != null)
				{
					// open explorer to the file's parent folder, with the file selected
					string args = string.Format("/select,\"{0}\"", page.TreeView.FileInfo.FullName);
					Process.Start("explorer", args);
				}

				this.contextMenuStripTabs.Tag = null;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripButtonXPathExpressionClick(object sender, EventArgs e)
		{
			try
			{
				_expressionsWindow.AddOrEditXPathExpression(this.toolStripTextBoxXpath.Text);
				this.UpdateXPathExpressionTool(this.toolStripTextBoxXpath.Text);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemAboutClick(object sender, EventArgs e)
		{
			try
			{
				using (AboutBox aboutBox = new AboutBox())
				{
					aboutBox.ShowDialog(this);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemCheckForUpdatesClick(object sender, EventArgs e)
		{
			try
			{
				this.CheckForUpdates(true);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemExpandAllClick(object sender, EventArgs e)
		{
			try
			{
				this.ExpandAll();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemCollapseAllClick(object sender, EventArgs e)
		{
			try
			{
				this.CollapseAll();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void OnToolStripMenuItemOptionsClick(object sender, EventArgs e)
		{
			try
			{
				this.ShowOptions();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void exportChildNodeDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.ExportChildNodeDefinitions();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void importChildNodeDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.ImportChildNodeDefinitions();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		#endregion
	}
}

