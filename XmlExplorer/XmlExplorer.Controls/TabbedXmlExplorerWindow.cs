namespace XmlExplorer.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Xml;
    using System.Xml.XPath;
    using System.IO;
    using System.Security.Permissions;
    using System.Security;

    public partial class TabbedXmlExplorerWindow : Form
    {
        #region Variables

        /// <summary>
        /// Dialog used to change the font and forecolor for the tab tree views.
        /// </summary>
        private FontDialog _fontDialog;

        /// <summary>
        /// A list of the XmlExplorerTabPages that are currently loading an XML file.
        /// Used to display progress in the status bar.
        /// </summary>
        private List<object> _tabsCurrentlyLoading = new List<object>();

        private DateTime _startedLoading;

        private Font _treeFont;
        private Color _treeForeColor;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TabbedXmlExplorerDialog.
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

            // set up the tab control
            this.tabControl.ShowToolTips = true;
            this.tabControl.MouseUp += this.OnTabControlMouseUp;
            this.tabControl.Selecting += this.OnTabControlSelecting;
            this.tabControl.Selected += this.OnTabControlSelected;

            // wire up all of the toolbar and menu events
            this.toolStripMenuItemFile.DropDownOpening += this.OnToolStripMenuItemFileDropDownOpening;
			this.toolStripMenuItemEdit.DropDownOpening += this.OnToolStripMenuItemEditDropDownOpening;
            this.toolStripMenuItemView.DropDownOpening += this.OnToolStripMenuItemViewDropDownOpening;

            this.toolStripTextBoxXpath.KeyDown += this.OnToolStripTextBoxXpathKeyDown;
            this.toolStripTextBoxXpath.TextChanged += this.OnToolStripTextBoxXpathTextChanged;

			// where applicable, I wire the click events of multiple tools to the same event handler
			// to reduce redundant code (for example, for the Open menu item and the Open toolbar button).
            this.contextMenuStripMenuItemClose.Click += this.OnToolStripMenuItemCloseClick;
            this.toolStripMenuItemClose.Click += this.OnToolStripMenuItemCloseClick;

            this.toolStripMenuItemExit.Click += this.OnToolStripMenuItemExitClick;
            this.toolStripMenuItemFont.Click += this.OnToolStripMenuItemFontClick;

            this.toolStripMenuItemOpen.Click += this.OnToolStripButtonOpenClick;
            this.toolStripButtonOpen.Click += this.OnToolStripButtonOpenClick;

            this.toolStripMenuItemOpenInEditor.Click += this.OnToolStripButtonOpenInEditorClick;

            this.toolStripButtonRefresh.Click += this.OnToolStripButtonRefreshClick;
            this.toolStripMenuItemRefresh.Click += this.OnToolStripButtonRefreshClick;

            this.toolStripButtonCopyFormattedOuterXml.Click += this.OnToolStripButtonCopyFormattedOuterXmlClick;
            this.toolStripMenuItemCopyFormattedOuterXml.Click += this.OnToolStripButtonCopyFormattedOuterXmlClick;

            this.toolStripMenuItemCopy.Click += this.OnToolStripMenuItemCopyClick;
            this.toolStripMenuItemCopyXPath.Click += this.OnToolStripMenuItemCopyXPathClick;

            this.toolStripButtonLaunchXpath.Click += this.OnToolStripButtonLaunchXpathClick;

            this.toolStripButtonSave.Click += this.OnToolStripButtonSaveClick;
            this.toolStripMenuItemSaveWithFormatting.Click += this.OnToolStripButtonSaveClick;
            this.toolStripMenuItemSaveAs.Click += this.OnToolStripMenuItemSaveAsClick;

            this.toolStripMenuItemCopyFullPath.Click += this.OnToolStripMenuItemCopyFullPathClick;
            this.toolStripMenuItemOpenContainingFolder.Click += this.OnToolStripMenuItemOpenContainingFolderClick;

            this.toolStripStatusLabelChildCount.Text = string.Empty;
        }

        #endregion

        #region Properties

        public Font TreeFont
        {
            get { return _treeFont; }
            set { _treeFont = value; }
        }

        public Color TreeForeColor
        {
            get { return _treeForeColor; }
            set { _treeForeColor = value; }
        }

        public AutoCompleteMode AutoCompleteMode
        {
            get { return this.toolStripTextBoxXpath.AutoCompleteMode; }
            set { this.toolStripTextBoxXpath.AutoCompleteMode = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies any changes to the font and forecolor performed from the font dialog.
        /// </summary>
        public void ApplyFont()
        {
            // save the font and forecolor settings
            _treeFont = _fontDialog.Font;
            _treeForeColor = _fontDialog.Color;

            // apply the font and forecolor to any open tabs
            this.SetXmlExplorerTabFonts(_fontDialog.Font);
            this.SetXmlExplorerTabForeColors(_fontDialog.Color);
        }

        /// <summary>
        /// Closes the tab that exist at the specified point (used when a tab is clicked with
        /// the middle mouse button.
        /// </summary>
        /// <param name="point">The point at which to close a tab.</param>
        private void CloseTabAtPoint(Point point)
        {
            // get the tab at the specified point
            XmlExplorerTabPage tabAtPoint = this.GetTabAtPoint(point);

            // if a tab was found
            if (tabAtPoint != null)
            {
                // close it
                tabAtPoint.Close();
            }
        }

        /// <summary>
        /// Returns the tab that exists at the specified point (or null if none are found).
        /// </summary>
        /// <param name="point">The point at which to return a tab.</param>
        /// <returns></returns>
        private XmlExplorerTabPage GetTabAtPoint(Point point)
        {
            // loop through the tabs
            for (int index = 0; index < this.tabControl.TabCount; index++)
            {
                // get the tab's bounds
                Rectangle rectangle = this.tabControl.GetTabRect(index);

                // if the point is on a tab
                if (rectangle.Contains(point))
                {
                    // return the tab at the specified point
                    return this.tabControl.TabPages[index] as XmlExplorerTabPage;
                }
            }

            // no tabs found at the specified point, return null
            return null;
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

        // Shows the open file dialog, and opens the file(s) specified by the user, if any.
        private void Open()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Xml Documents (*.xml)|*.xml|All Files (*.*)|*.*";
                dialog.Multiselect = true;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        this.Open(dialog.FileNames);
                    }
                    catch (SecurityException)
                    {
                        dialog.Multiselect = false;
                        using (Stream stream = dialog.OpenFile())
                        {
                            this.Open(stream);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Opens a tab for each specified file.
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
        /// Opens a tab for each specified file.
        /// </summary>
        /// <param name="filenames">The full paths of the files to open.</param>
        public void Open(string[] filenames)
        {
            foreach (string text in filenames)
            {
                this.Open(text);
            }
        }

        /// <summary>
        /// Opens a tab for the specified file.
        /// </summary>
        /// <param name="filename">The full path of the file to open.</param>
        public void Open(string filename)
        {
            // create a tab page
            XmlExplorerTabPage tabPage = this.CreateXmlExplorerTabPage();

            // instruct the tab to open the specified file.
            tabPage.Open(filename);

            // add the tabpage to the tab control
            this.tabControl.TabPages.Add(tabPage);

            // select the newly opened tab
            this.tabControl.SelectedTab = tabPage;
        }

        /// <summary>
        /// Opens a tab for the specified stream.
        /// </summary>
        public void Open(Stream stream)
        {
            // create a tab page
            XmlExplorerTabPage tabPage = this.CreateXmlExplorerTabPage();

            // instruct the tab to open the specified stream.
            tabPage.Open(stream);

            // add the tabpage to the tab control
            this.tabControl.TabPages.Add(tabPage);

            // select the newly opened tab
            this.tabControl.SelectedTab = tabPage;
        }

        /// <summary>
        /// Opens a tab for the specified node set.  This method is used to open XPath expressions that 
        /// evaluate to a node set.
        /// </summary>
        /// <param name="iterator">An XPathNodeIterator with which to open a tab.</param>
        public void Open(XPathNodeIterator iterator)
        {
            // create a tab page
            XmlExplorerTabPage tabPage = this.CreateXmlExplorerTabPage();

            // instruct the tab to open the specified node set
            tabPage.Open(iterator);

            // add the tabpage to the tab control
            this.tabControl.TabPages.Add(tabPage);

            // select the newly opened tab
            this.tabControl.SelectedTab = tabPage;
        }

        /// <summary>
        /// Opens the selected tab page's file in the default editor for it's file type.
        /// </summary>
        private void OpenInEditor()
        {
            try
            {
                // get the selected tab
                XmlExplorerTabPage tabPage = this.tabControl.SelectedTab as XmlExplorerTabPage;
                if (tabPage == null)
                    return;

                if (string.IsNullOrEmpty(tabPage.Filename))
                    return;

                ProcessStartInfo info = new ProcessStartInfo(tabPage.Filename);
                info.Verb = "edit";

                Process.Start(info);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        /// <summary>
        /// Returns an initialized XmlExplorerTabPage.
        /// </summary>
        /// <returns></returns>
        private XmlExplorerTabPage CreateXmlExplorerTabPage()
        {
            // the main window handle needs to be created before creating a new tab page
            if (!this.IsHandleCreated)
                this.CreateHandle();

            XmlExplorerTabPage tabPage = new XmlExplorerTabPage();

            // read the options for the tab page, such as font and forecolor
            this.ReadTabPageOptions(tabPage);

            // wire to the tab's events
            tabPage.NeedsClosed += this.OnTabPageNeedsClosed;
            tabPage.LoadingFileCompleted += this.OnTabPageLoadingFileCompleted;
            tabPage.LoadingFileFailed += this.OnTabPageLoadingFileFailed;
            tabPage.LoadingFileStarted += this.OnTabPageLoadingFileStarted;
            tabPage.XPathNavigatorTreeView.AfterSelect += this.OnTabPage_XmlTreeView_AfterSelect;

            return tabPage;
        }

        /// <summary>
        /// Initializes an XmlExplorerTabPage with saved settings.
        /// </summary>
        /// <param name="tabPage">The XmlExplorerTabPage to initialize.</param>
        private void ReadTabPageOptions(XmlExplorerTabPage tabPage)
        {
            tabPage.XPathNavigatorTreeView.Font = _treeFont;
            tabPage.XPathNavigatorTreeView.ForeColor = _treeForeColor;
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
        /// Displays progress for tabs that are loading xml files.
        /// </summary>
        private void UpdateLoadingStatus()
        {
            // get the number of tabs that are currently loading xml files
            int count = _tabsCurrentlyLoading.Count;

            string loadingStatus = string.Empty;

            // build the status text
            if (count > 0)
            {
                string suffix = count == 1 ? string.Empty : "s";
                loadingStatus = string.Format("Loading {0} file{1}", count.ToString(), suffix);
            }
            else
            {
                TimeSpan elapsed = DateTime.Now - _startedLoading;
                loadingStatus = string.Format("Loaded in {0}", elapsed.ToString());
            }

            // update the progress bar, we want it to scroll if any tabs are still loading,
            // but be hidden if there are none loading
            this.toolStripProgressBar.Style = ProgressBarStyle.Marquee;
            this.toolStripProgressBar.Enabled = count > 0;
            this.toolStripProgressBar.Visible = count > 0;

            // update the status text
            this.toolStripStatusLabelMain.Text = loadingStatus;
        }

        /// <summary>
        /// Removes a tab from our list of tabs currently loading xml files.
        /// </summary>
        /// <param name="tab">The tab that has finished loading, and needs removed from
        /// the list of currently loading tabs.</param>
        private void RemoveLoadingTab(object tab)
        {
            // lock the list for thread safety
            lock (_tabsCurrentlyLoading)
            {
                // remove the tab if it's in the list
                if (_tabsCurrentlyLoading.Contains(tab))
                    _tabsCurrentlyLoading.Remove(tab);

                // update the loading status
                this.UpdateLoadingStatus();
            }
        }

        /// <summary>
        /// Evaluates the specified XPath expression against the xml content of the currently selected tab.
        /// </summary>
        /// <param name="xpath">An XPath expression to evaluate against the currently selected tab.</param>
        /// <returns></returns>
        private bool FindByXpath(string xpath)
        {
            try
            {
                // get the selected tab page, returning if none are found
                XmlExplorerTabPage tabPage = this.tabControl.SelectedTab as XmlExplorerTabPage;
                if (tabPage == null)
                    return false;

                // instruct the tab page to perform the expression evaluation
                return tabPage.FindByXpath(xpath);
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

            return false;
        }

        /// <summary>
        /// Opens a new tab page with the evaluated results of the specified XPath expression.
        /// </summary>
        /// <param name="xpath">An XPath expression to evaluate against the currently selected tab.</param>
        /// <returns></returns>
        private bool LaunchXpathResults(string xpath)
        {
            try
            {
                // get the selected tab page
                XmlExplorerTabPage tabPage = this.tabControl.SelectedTab as XmlExplorerTabPage;
                if (tabPage == null)
                    return false;

                // evaluate the expression
                XPathNodeIterator iterator = tabPage.SelectXmlNodes(xpath);

                // check for empty results
                if (iterator == null || iterator.Count < 1)
                    return false;

                // open the results in a new tab page
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
        /// Overwrites the selected tab page's file with XML formatting (tabs and crlf's)
        /// </summary>
        private void SaveWithFormatting()
        {
            try
            {
                // get the selected tab
                XmlExplorerTabPage tabPage = this.tabControl.SelectedTab as XmlExplorerTabPage;
                if (tabPage == null)
                    return;

                // instruct the tab to save with formatting
                tabPage.SaveWithFormatting();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        /// <summary>
        /// Prompts the user to save a copy of the selected tab page's XML file.
        /// </summary>
        private void SaveAs()
        {
            try
            {
                XmlExplorerTabPage tabPage = this.tabControl.SelectedTab as XmlExplorerTabPage;
                if (tabPage == null)
                    return;
                tabPage.SaveAs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        /// <summary>
        /// Applies a fore color to all of the currently open tab pages.
        /// </summary>
        /// <param name="color">A color to apply as the fore color.</param>
        private void SetXmlExplorerTabForeColors(Color color)
        {
            foreach (TabPage tabPage in this.tabControl.TabPages)
            {
                XmlExplorerTabPage xmlExplorerTabPage = tabPage as XmlExplorerTabPage;

                if (xmlExplorerTabPage == null)
                    continue;

                xmlExplorerTabPage.XPathNavigatorTreeView.ForeColor = color;
            }
        }

        /// <summary>
        /// Applies a font to all of the currently open tab pages.
        /// </summary>
        /// <param name="font">A font to apply.</param>
        private void SetXmlExplorerTabFonts(Font font)
        {
            foreach (TabPage tabPage in this.tabControl.TabPages)
            {
                XmlExplorerTabPage xmlExplorerTabPage = tabPage as XmlExplorerTabPage;

                if (xmlExplorerTabPage == null)
                    continue;

                xmlExplorerTabPage.XPathNavigatorTreeView.Font = font;
            }
        }

        /// <summary>
        /// Copies the current tab page's selected xml node (and all of it's sub nodes) to the clipboard
        /// as formatted XML text.
        /// </summary>
        private void CopyFormattedOuterXml()
        {
            // get the selectd tab page
            XmlExplorerTabPage tabPage = this.tabControl.SelectedTab as XmlExplorerTabPage;
            if (tabPage == null)
                return;

            // instruct the tab to copy
            tabPage.CopyFormattedOuterXml();
        }

        /// <summary>
        /// Copies the current tab page's selected xml node to the clipboard
        /// as XML text.
        /// </summary>
        private void CopyNodeText()
        {
            // get the selectd tab page
            XmlExplorerTabPage tabPage = this.tabControl.SelectedTab as XmlExplorerTabPage;
            if (tabPage == null)
                return;

            // instruct the tab to copy
            tabPage.CopyNodeText();
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

        /// <summary>
        /// Occurs when a mouse button is released over a tab.
        /// </summary>
        private void OnTabControlMouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                switch (e.Button)
                {
                    case MouseButtons.Right:
                        // right mouse button, show the context menu
                        this.contextMenuStrip.Tag = this.GetTabAtPoint(e.Location);
                        this.contextMenuStrip.Show(this.tabControl, e.Location);
                        break;

                    case MouseButtons.Middle:
                        // middle mouse button, close any tabs at the location
                        this.CloseTabAtPoint(e.Location);
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
        /// Occurs when the selected tab is about to change.
        /// </summary>
        private void OnTabControlSelecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                // get the tab
                XmlExplorerTabPage tabPage = e.TabPage as XmlExplorerTabPage;

                switch (e.Action)
                {
                    case TabControlAction.Deselecting:
                        // if the tab is becoming deselected, unwire from it's events
                        if (tabPage != null)
                            tabPage.XPathNavigatorTreeView.KeyDown -= this.OnTabPage_XmlTreeView_KeyDown;
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
        /// Occurs after the selected tab has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTabControlSelected(object sender, TabControlEventArgs e)
        {
            try
            {
                // get the tab
                XmlExplorerTabPage tabPage = e.TabPage as XmlExplorerTabPage;

                switch (e.Action)
                {
                    case TabControlAction.Selected:
                        // if a tab is becoming selected, unwire from it's events
                        // and the window's title bar text
                        if (tabPage == null)
                        {
                            this.Text = "XML Explorer";
                        }
                        else
                        {
                            this.Text = string.Format("XML Explorer - [{0}]", tabPage.Text);
                            tabPage.XPathNavigatorTreeView.KeyDown += this.OnTabPage_XmlTreeView_KeyDown;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        private void OnTabPage_XmlTreeView_KeyDown(object sender, KeyEventArgs e)
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
        /// Occurs when a user has chosen to close a tab, and the tab needs removed
        /// from the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTabPageNeedsClosed(object sender, EventArgs e)
        {
            try
            {
                // remove the tab
                this.tabControl.TabPages.Remove(sender as TabPage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        /// <summary>
        /// Occurs when a tab page has begun asynchronously loading an XML file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTabPageLoadingFileStarted(object sender, EventArgs e)
        {
            try
            {
                // this event will likely occur on a background thread,
                // marshal the event back to the window's thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler<EventArgs>(this.OnTabPageLoadingFileStarted), sender, e);
                    return;
                }

                // lock the list for thread safety
                lock (_tabsCurrentlyLoading)
                {
                    if (_tabsCurrentlyLoading.Count < 1)
                        _startedLoading = DateTime.Now;

                    // add the tab
                    _tabsCurrentlyLoading.Add(sender);

                    // update the status and progress
                    this.UpdateLoadingStatus();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        /// <summary>
        /// Occurs when an exception occurs while loading an XML file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTabPageLoadingFileFailed(object sender, EventArgs e)
        {
            try
            {
                // this event will likely occur on a background thread,
                // marshal the event back to the window's thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler<EventArgs>(this.OnTabPageLoadingFileFailed), sender, e);
                    return;
                }

                // remove the tab
                this.RemoveLoadingTab(sender);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        /// <summary>
        /// Occurs when a tab page has completed asynchronously loading an XML file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTabPageLoadingFileCompleted(object sender, EventArgs e)
        {
            try
            {
                // this event will likely occur on a background thread,
                // marshal the event back to the window's thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler<EventArgs>(this.OnTabPageLoadingFileCompleted), sender, e);
                    return;
                }

                // remove the tab
                this.RemoveLoadingTab(sender);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        /// <summary>
        /// Occurs when the selected xml node of a tab page has changed.
        /// </summary>
        private void OnTabPage_XmlTreeView_AfterSelect(object sender, TreeViewEventArgs e)
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

                // get the xml node's child count
                int count = node.Navigator.SelectChildren(XPathNodeType.Element).Count;

                // update the status bar with the child count
                this.toolStripStatusLabelChildCount.Text = string.Format("{0} child node{1}", count, count == 1 ? string.Empty : "s");

                // update the status bar with the full path of the selected xml node
                this.toolStripStatusLabelMain.Text = treeView.GetXmlNodeFullPath(node.Navigator);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        #endregion

        #region Tool Event Handlers

        private void OnToolStripMenuItemFileDropDownOpening(object sender, EventArgs e)
        {
            try
            {
                // update any tools that depend on one or more tab pages being open
                bool hasOpenTabPages = this.tabControl.TabPages.Count > 0;

                this.toolStripMenuItemOpenInEditor.Enabled = hasOpenTabPages;
                this.toolStripMenuItemClose.Enabled = hasOpenTabPages;
                this.toolStripMenuItemSaveAs.Enabled = hasOpenTabPages;
                this.toolStripMenuItemSaveWithFormatting.Enabled = hasOpenTabPages;
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
				// update any tools that depend on one or more tab pages being open
				bool hasOpenTabPages = this.tabControl.TabPages.Count > 0;

				this.toolStripMenuItemCopyFormattedOuterXml.Enabled = hasOpenTabPages;
				this.toolStripMenuItemCopy.Enabled = hasOpenTabPages;
				this.toolStripMenuItemCopyXPath.Enabled = hasOpenTabPages;
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
                // update any tools that depend on one or more tab pages being open
                this.toolStripMenuItemRefresh.Enabled = this.tabControl.TabPages.Count > 0;
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
                // reset any xpath expression error indicators
                this.toolStripTextBoxXpath.BackColor = SystemColors.Window;
                this.toolStripStatusLabelMain.Text = string.Empty;

                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        string xpath = this.toolStripTextBoxXpath.Text;
                        if (e.Shift)
                        {
                            // Shift-Enter has been pressed, evaluate the expression
                            // if successful, open results in a new tab page
                            // if there is a problem with the expression, notify the user
                            // by highlighting the XPath text box
                            if (!this.LaunchXpathResults(xpath))
                                this.toolStripTextBoxXpath.BackColor = Color.LightPink;
                        }
                        else
                        {
                            // Shift-Enter has been pressed, evaluate the expression
                            // if successful, the first node of the result set will be selected
                            // (selection is performed by the tab)
                            // if there is a problem with the expression, notify the user
                            // by highlighting the XPath text box
                            if (!this.FindByXpath(xpath))
                                this.toolStripTextBoxXpath.BackColor = Color.LightPink;
                            this.toolStripTextBoxXpath.SelectionStart = this.toolStripTextBoxXpath.TextLength;
                        }
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

                // we'll update the autocomplete custom source every time a / is entered
                if (string.IsNullOrEmpty(text) || !text.EndsWith("/"))
                    return;

                // append a * to return all matching nodes
                string xpath = text + "*";

                // get the selected tab
                XmlExplorerTabPage tabPage = this.tabControl.SelectedTab as XmlExplorerTabPage;
                if (tabPage == null)
                    return;

                // evaluate the expression
                XPathNodeIterator nodes = tabPage.SelectXmlNodes(xpath);

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
                // get the tab for which the context menu was opened
                XmlExplorerTabPage page = this.contextMenuStrip.Tag as XmlExplorerTabPage;
                if (page == null)
                    page = this.tabControl.SelectedTab as XmlExplorerTabPage;

                // close the tab
                if (page != null)
                {
                    page.Close();
                }

                // reset the context menu's tag
                this.contextMenuStrip.Tag = null;
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

        private void OnToolStripButtonRefreshClick(object sender, EventArgs e)
        {
            try
            {
                XmlExplorerTabPage page = this.tabControl.SelectedTab as XmlExplorerTabPage;
                if (page != null)
                {
                    page.Reload();
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
                XmlExplorerTabPage tabPage = this.tabControl.SelectedTab as XmlExplorerTabPage;
                if (tabPage == null)
                    return;

                XPathNavigatorTreeNode node = tabPage.XPathNavigatorTreeView.SelectedNode as XPathNavigatorTreeNode;
                if (node == null)
                    return;

                if (node.Navigator == null)
                    return;

                this.toolStripTextBoxXpath.Text = tabPage.XPathNavigatorTreeView.GetXmlNodeFullPath(node.Navigator);
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
                if (!this.LaunchXpathResults(this.toolStripTextBoxXpath.Text))
                    this.toolStripTextBoxXpath.BackColor = Color.LightPink;
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
                this.SaveAs();
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
                this.SaveWithFormatting();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }


        void OnToolStripMenuItemCopyFullPathClick(object sender, EventArgs e)
        {
            try
            {
                XmlExplorerTabPage page = this.contextMenuStrip.Tag as XmlExplorerTabPage;
                if (page == null)
                    page = this.tabControl.SelectedTab as XmlExplorerTabPage;

                if (page != null)
                {
                    Clipboard.SetText(page.Filename);
                }

                this.contextMenuStrip.Tag = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        void OnToolStripMenuItemOpenContainingFolderClick(object sender, EventArgs e)
        {
            try
            {
                // get the tab for which the context menu was opened
                XmlExplorerTabPage page = this.contextMenuStrip.Tag as XmlExplorerTabPage;
                if (page == null)
                    page = this.tabControl.SelectedTab as XmlExplorerTabPage;

                // open an explorer window to the folder containing the selected tab's file
                if (page != null && !string.IsNullOrEmpty(page.Filename))
                {
                    // open explorer to the file's parent folder, with the file selected
                    string args = string.Format("/select,\"{0}\"", page.Filename);
                    Process.Start("explorer", args);
                }

                this.contextMenuStrip.Tag = null;
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

