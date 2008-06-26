using System.Windows.Forms;
using System.ComponentModel;
namespace XmlExplorer.Controls
{
    partial class TabbedXmlExplorerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabbedXmlExplorerWindow));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelChildCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.splitterTabBottom = new System.Windows.Forms.Splitter();
            this.panelExpressions = new System.Windows.Forms.Panel();
            this.pictureBoxSearch = new System.Windows.Forms.PictureBox();
            this.listViewExpressions = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderExpression = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderComments = new System.Windows.Forms.ColumnHeader();
            this.textBoxSearchExpressions = new System.Windows.Forms.TextBox();
            this.buttonCloseExpressions = new System.Windows.Forms.Button();
            this.labelExpressions = new System.Windows.Forms.Label();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenInEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSaveWithFormatting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopyFormattedOuterXml = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopyXPath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFont = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemUseHighlighting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemViewExpressions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStandardButtons = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCopyFormattedOuterXml = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxXpath = new XmlExplorer.Controls.ToolStripSpringTextBox();
            this.toolStripButtonXPathExpression = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLaunchXpath = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStripTabs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemCopyFullPath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenContainingFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripNodes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripNodesItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripNodesItemCopyFormattedOuterXml = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripNodesItemCopyXPath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelExpressions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSearch)).BeginInit();
            this.menuStripMain.SuspendLayout();
            this.toolStripStandardButtons.SuspendLayout();
            this.contextMenuStripTabs.SuspendLayout();
            this.contextMenuStripNodes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(792, 378);
            this.tabControl.TabIndex = 2;
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.tabControl);
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitterTabBottom);
            this.toolStripContainer.ContentPanel.Controls.Add(this.panelExpressions);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(792, 495);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(792, 566);
            this.toolStripContainer.TabIndex = 3;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStripMain);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStripStandardButtons);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMain,
            this.toolStripStatusLabelChildCount,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(792, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // toolStripStatusLabelMain
            // 
            this.toolStripStatusLabelMain.Name = "toolStripStatusLabelMain";
            this.toolStripStatusLabelMain.Size = new System.Drawing.Size(702, 17);
            this.toolStripStatusLabelMain.Spring = true;
            this.toolStripStatusLabelMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelChildCount
            // 
            this.toolStripStatusLabelChildCount.Name = "toolStripStatusLabelChildCount";
            this.toolStripStatusLabelChildCount.Size = new System.Drawing.Size(75, 17);
            this.toolStripStatusLabelChildCount.Text = "10 child nodes";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar.Visible = false;
            // 
            // splitterTabBottom
            // 
            this.splitterTabBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterTabBottom.Location = new System.Drawing.Point(0, 378);
            this.splitterTabBottom.Name = "splitterTabBottom";
            this.splitterTabBottom.Size = new System.Drawing.Size(792, 3);
            this.splitterTabBottom.TabIndex = 4;
            this.splitterTabBottom.TabStop = false;
            // 
            // panelExpressions
            // 
            this.panelExpressions.Controls.Add(this.pictureBoxSearch);
            this.panelExpressions.Controls.Add(this.listViewExpressions);
            this.panelExpressions.Controls.Add(this.textBoxSearchExpressions);
            this.panelExpressions.Controls.Add(this.buttonCloseExpressions);
            this.panelExpressions.Controls.Add(this.labelExpressions);
            this.panelExpressions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelExpressions.Location = new System.Drawing.Point(0, 381);
            this.panelExpressions.MinimumSize = new System.Drawing.Size(367, 0);
            this.panelExpressions.Name = "panelExpressions";
            this.panelExpressions.Size = new System.Drawing.Size(792, 114);
            this.panelExpressions.TabIndex = 3;
            // 
            // pictureBoxSearch
            // 
            this.pictureBoxSearch.Image = global::XmlExplorer.Controls.Properties.Resources.ZoomHS;
            this.pictureBoxSearch.Location = new System.Drawing.Point(172, 7);
            this.pictureBoxSearch.Name = "pictureBoxSearch";
            this.pictureBoxSearch.Size = new System.Drawing.Size(16, 20);
            this.pictureBoxSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSearch.TabIndex = 5;
            this.pictureBoxSearch.TabStop = false;
            // 
            // listViewExpressions
            // 
            this.listViewExpressions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewExpressions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderExpression,
            this.columnHeaderComments});
            this.listViewExpressions.FullRowSelect = true;
            this.listViewExpressions.HideSelection = false;
            this.listViewExpressions.LabelEdit = true;
            this.listViewExpressions.Location = new System.Drawing.Point(3, 32);
            this.listViewExpressions.Name = "listViewExpressions";
            this.listViewExpressions.Size = new System.Drawing.Size(786, 79);
            this.listViewExpressions.TabIndex = 4;
            this.listViewExpressions.UseCompatibleStateImageBehavior = false;
            this.listViewExpressions.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            // 
            // columnHeaderExpression
            // 
            this.columnHeaderExpression.Text = "Expression";
            this.columnHeaderExpression.Width = 600;
            // 
            // columnHeaderComments
            // 
            this.columnHeaderComments.Text = "Comments";
            this.columnHeaderComments.Width = 74;
            // 
            // textBoxSearchExpressions
            // 
            this.textBoxSearchExpressions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearchExpressions.Location = new System.Drawing.Point(194, 6);
            this.textBoxSearchExpressions.Name = "textBoxSearchExpressions";
            this.textBoxSearchExpressions.Size = new System.Drawing.Size(566, 20);
            this.textBoxSearchExpressions.TabIndex = 2;
            this.toolTip.SetToolTip(this.textBoxSearchExpressions, "Search Expressions");
            // 
            // buttonCloseExpressions
            // 
            this.buttonCloseExpressions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCloseExpressions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCloseExpressions.Image = global::XmlExplorer.Controls.Properties.Resources.close;
            this.buttonCloseExpressions.Location = new System.Drawing.Point(766, 4);
            this.buttonCloseExpressions.Name = "buttonCloseExpressions";
            this.buttonCloseExpressions.Size = new System.Drawing.Size(23, 23);
            this.buttonCloseExpressions.TabIndex = 1;
            this.toolTip.SetToolTip(this.buttonCloseExpressions, "Close the Expressions pane");
            this.buttonCloseExpressions.UseVisualStyleBackColor = true;
            // 
            // labelExpressions
            // 
            this.labelExpressions.AutoSize = true;
            this.labelExpressions.Location = new System.Drawing.Point(3, 9);
            this.labelExpressions.Name = "labelExpressions";
            this.labelExpressions.Size = new System.Drawing.Size(63, 13);
            this.labelExpressions.TabIndex = 0;
            this.labelExpressions.Text = "Expressions";
            // 
            // menuStripMain
            // 
            this.menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemEdit,
            this.toolStripMenuItemFormat,
            this.toolStripMenuItemView});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(792, 24);
            this.menuStripMain.TabIndex = 2;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemOpenInEditor,
            this.toolStripMenuItemClose,
            this.toolStripSeparator,
            this.toolStripMenuItemSaveWithFormatting,
            this.toolStripMenuItemSaveAs,
            this.toolStripSeparator1,
            this.toolStripMenuItemExit});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(35, 20);
            this.toolStripMenuItemFile.Text = "&File";
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemOpen.Image")));
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItemOpen.Text = "&Open...";
            // 
            // toolStripMenuItemOpenInEditor
            // 
            this.toolStripMenuItemOpenInEditor.Name = "toolStripMenuItemOpenInEditor";
            this.toolStripMenuItemOpenInEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.toolStripMenuItemOpenInEditor.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItemOpenInEditor.Text = "Open in &Editor";
            // 
            // toolStripMenuItemClose
            // 
            this.toolStripMenuItemClose.Name = "toolStripMenuItemClose";
            this.toolStripMenuItemClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.toolStripMenuItemClose.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItemClose.Text = "&Close";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(217, 6);
            // 
            // toolStripMenuItemSaveWithFormatting
            // 
            this.toolStripMenuItemSaveWithFormatting.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemSaveWithFormatting.Image")));
            this.toolStripMenuItemSaveWithFormatting.Name = "toolStripMenuItemSaveWithFormatting";
            this.toolStripMenuItemSaveWithFormatting.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItemSaveWithFormatting.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItemSaveWithFormatting.Text = "&Save (with formatting)";
            // 
            // toolStripMenuItemSaveAs
            // 
            this.toolStripMenuItemSaveAs.Name = "toolStripMenuItemSaveAs";
            this.toolStripMenuItemSaveAs.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItemSaveAs.Text = "Save As... (with formatting)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItemExit.Text = "E&xit";
            // 
            // toolStripMenuItemEdit
            // 
            this.toolStripMenuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemCopyFormattedOuterXml,
            this.toolStripMenuItemCopyXPath});
            this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
            this.toolStripMenuItemEdit.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItemEdit.Text = "&Edit";
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCopy.Image")));
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.ShortcutKeyDisplayString = "Ctrl+C";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(292, 22);
            this.toolStripMenuItemCopy.Text = "&Copy";
            // 
            // toolStripMenuItemCopyFormattedOuterXml
            // 
            this.toolStripMenuItemCopyFormattedOuterXml.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCopyFormattedOuterXml.Image")));
            this.toolStripMenuItemCopyFormattedOuterXml.Name = "toolStripMenuItemCopyFormattedOuterXml";
            this.toolStripMenuItemCopyFormattedOuterXml.ShortcutKeyDisplayString = "";
            this.toolStripMenuItemCopyFormattedOuterXml.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItemCopyFormattedOuterXml.Size = new System.Drawing.Size(292, 22);
            this.toolStripMenuItemCopyFormattedOuterXml.Text = "&Copy Formatted Outer XML";
            // 
            // toolStripMenuItemCopyXPath
            // 
            this.toolStripMenuItemCopyXPath.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
            this.toolStripMenuItemCopyXPath.Name = "toolStripMenuItemCopyXPath";
            this.toolStripMenuItemCopyXPath.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItemCopyXPath.Size = new System.Drawing.Size(292, 22);
            this.toolStripMenuItemCopyXPath.Text = "Copy Node &XPath to Address Bar";
            // 
            // toolStripMenuItemFormat
            // 
            this.toolStripMenuItemFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFont,
            this.toolStripMenuItemUseHighlighting});
            this.toolStripMenuItemFormat.Name = "toolStripMenuItemFormat";
            this.toolStripMenuItemFormat.Size = new System.Drawing.Size(53, 20);
            this.toolStripMenuItemFormat.Text = "F&ormat";
            // 
            // toolStripMenuItemFont
            // 
            this.toolStripMenuItemFont.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemFont.Image")));
            this.toolStripMenuItemFont.Name = "toolStripMenuItemFont";
            this.toolStripMenuItemFont.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItemFont.Text = "&Font...";
            // 
            // toolStripMenuItemUseHighlighting
            // 
            this.toolStripMenuItemUseHighlighting.Name = "toolStripMenuItemUseHighlighting";
            this.toolStripMenuItemUseHighlighting.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItemUseHighlighting.Text = "&Use Highlighing";
            // 
            // toolStripMenuItemView
            // 
            this.toolStripMenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRefresh,
            this.toolStripMenuItemViewExpressions});
            this.toolStripMenuItemView.Name = "toolStripMenuItemView";
            this.toolStripMenuItemView.Size = new System.Drawing.Size(41, 20);
            this.toolStripMenuItemView.Text = "&View";
            // 
            // toolStripMenuItemRefresh
            // 
            this.toolStripMenuItemRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemRefresh.Image")));
            this.toolStripMenuItemRefresh.Name = "toolStripMenuItemRefresh";
            this.toolStripMenuItemRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.toolStripMenuItemRefresh.Size = new System.Drawing.Size(131, 22);
            this.toolStripMenuItemRefresh.Text = "&Refresh";
            // 
            // toolStripMenuItemViewExpressions
            // 
            this.toolStripMenuItemViewExpressions.Checked = true;
            this.toolStripMenuItemViewExpressions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemViewExpressions.Name = "toolStripMenuItemViewExpressions";
            this.toolStripMenuItemViewExpressions.Size = new System.Drawing.Size(131, 22);
            this.toolStripMenuItemViewExpressions.Text = "Expressions";
            this.toolStripMenuItemViewExpressions.ToolTipText = "Show the Expressions sidebar";
            // 
            // toolStripStandardButtons
            // 
            this.toolStripStandardButtons.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripStandardButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen,
            this.toolStripButtonSave,
            this.toolStripSeparator2,
            this.toolStripButtonCopyFormattedOuterXml,
            this.toolStripSeparator3,
            this.toolStripButtonRefresh,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.toolStripTextBoxXpath,
            this.toolStripButtonXPathExpression,
            this.toolStripButtonLaunchXpath});
            this.toolStripStandardButtons.Location = new System.Drawing.Point(0, 24);
            this.toolStripStandardButtons.Name = "toolStripStandardButtons";
            this.toolStripStandardButtons.Size = new System.Drawing.Size(792, 25);
            this.toolStripStandardButtons.Stretch = true;
            this.toolStripStandardButtons.TabIndex = 3;
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpen.Text = "&Open";
            this.toolStripButtonOpen.ToolTipText = "Open (Ctrl+O)";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.Text = "Save with formatting (Ctrl+S)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonCopyFormattedOuterXml
            // 
            this.toolStripButtonCopyFormattedOuterXml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopyFormattedOuterXml.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCopyFormattedOuterXml.Image")));
            this.toolStripButtonCopyFormattedOuterXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopyFormattedOuterXml.Name = "toolStripButtonCopyFormattedOuterXml";
            this.toolStripButtonCopyFormattedOuterXml.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCopyFormattedOuterXml.Text = "Copy Formatted Outer XML";
            this.toolStripButtonCopyFormattedOuterXml.ToolTipText = "Copy Formatted Outer XML (Ctrl+Shift+C)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = global::XmlExplorer.Controls.Properties.Resources.RefreshDocViewHS;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.ToolTipText = "Refresh (F5)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "XPath:";
            // 
            // toolStripTextBoxXpath
            // 
            this.toolStripTextBoxXpath.AcceptsReturn = true;
            this.toolStripTextBoxXpath.AcceptsTab = true;
            this.toolStripTextBoxXpath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.toolStripTextBoxXpath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.toolStripTextBoxXpath.Name = "toolStripTextBoxXpath";
            this.toolStripTextBoxXpath.Size = new System.Drawing.Size(504, 25);
            this.toolStripTextBoxXpath.ToolTipText = "Enter an XPath expression.\r\n\r\nPress Enter to select the first match.\r\nPress Shift" +
                "+Enter (or Launch button) to open the expression results in a new window.";
            // 
            // toolStripButtonXPathExpression
            // 
            this.toolStripButtonXPathExpression.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonXPathExpression.Image = global::XmlExplorer.Controls.Properties.Resources.unstarred;
            this.toolStripButtonXPathExpression.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonXPathExpression.Name = "toolStripButtonXPathExpression";
            this.toolStripButtonXPathExpression.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonXPathExpression.ToolTipText = "Add expression to library";
            // 
            // toolStripButtonLaunchXpath
            // 
            this.toolStripButtonLaunchXpath.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLaunchXpath.Image")));
            this.toolStripButtonLaunchXpath.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonLaunchXpath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLaunchXpath.Name = "toolStripButtonLaunchXpath";
            this.toolStripButtonLaunchXpath.Size = new System.Drawing.Size(73, 22);
            this.toolStripButtonLaunchXpath.Text = "Launch...";
            this.toolStripButtonLaunchXpath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonLaunchXpath.ToolTipText = "Launch XPath expression in a new tab (Shift+Enter)";
            // 
            // contextMenuStripTabs
            // 
            this.contextMenuStripTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripMenuItemClose,
            this.toolStripSeparator5,
            this.toolStripMenuItemCopyFullPath,
            this.toolStripMenuItemOpenContainingFolder});
            this.contextMenuStripTabs.Name = "contextMenuStrip";
            this.contextMenuStripTabs.Size = new System.Drawing.Size(188, 76);
            // 
            // contextMenuStripMenuItemClose
            // 
            this.contextMenuStripMenuItemClose.Name = "contextMenuStripMenuItemClose";
            this.contextMenuStripMenuItemClose.Size = new System.Drawing.Size(187, 22);
            this.contextMenuStripMenuItemClose.Text = "&Close";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(184, 6);
            // 
            // toolStripMenuItemCopyFullPath
            // 
            this.toolStripMenuItemCopyFullPath.Name = "toolStripMenuItemCopyFullPath";
            this.toolStripMenuItemCopyFullPath.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemCopyFullPath.Text = "Copy Full Path";
            // 
            // toolStripMenuItemOpenContainingFolder
            // 
            this.toolStripMenuItemOpenContainingFolder.Name = "toolStripMenuItemOpenContainingFolder";
            this.toolStripMenuItemOpenContainingFolder.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemOpenContainingFolder.Text = "Open Containing Folder";
            // 
            // contextMenuStripNodes
            // 
            this.contextMenuStripNodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripNodesItemCopy,
            this.contextMenuStripNodesItemCopyFormattedOuterXml,
            this.contextMenuStripNodesItemCopyXPath});
            this.contextMenuStripNodes.Name = "contextMenuStripNodes";
            this.contextMenuStripNodes.Size = new System.Drawing.Size(233, 70);
            // 
            // contextMenuStripNodesItemCopy
            // 
            this.contextMenuStripNodesItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuStripNodesItemCopy.Image")));
            this.contextMenuStripNodesItemCopy.Name = "contextMenuStripNodesItemCopy";
            this.contextMenuStripNodesItemCopy.ShortcutKeyDisplayString = "";
            this.contextMenuStripNodesItemCopy.Size = new System.Drawing.Size(232, 22);
            this.contextMenuStripNodesItemCopy.Text = "&Copy";
            // 
            // contextMenuStripNodesItemCopyFormattedOuterXml
            // 
            this.contextMenuStripNodesItemCopyFormattedOuterXml.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuStripNodesItemCopyFormattedOuterXml.Image")));
            this.contextMenuStripNodesItemCopyFormattedOuterXml.Name = "contextMenuStripNodesItemCopyFormattedOuterXml";
            this.contextMenuStripNodesItemCopyFormattedOuterXml.ShortcutKeyDisplayString = "";
            this.contextMenuStripNodesItemCopyFormattedOuterXml.Size = new System.Drawing.Size(232, 22);
            this.contextMenuStripNodesItemCopyFormattedOuterXml.Text = "&Copy Formatted Outer XML";
            // 
            // contextMenuStripNodesItemCopyXPath
            // 
            this.contextMenuStripNodesItemCopyXPath.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
            this.contextMenuStripNodesItemCopyXPath.Name = "contextMenuStripNodesItemCopyXPath";
            this.contextMenuStripNodesItemCopyXPath.Size = new System.Drawing.Size(232, 22);
            this.contextMenuStripNodesItemCopyXPath.Text = "Copy Node &XPath to Address Bar";
            // 
            // TabbedXmlExplorerWindow
            // 
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.toolStripContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "TabbedXmlExplorerWindow";
            this.Text = "XmlExplorer";
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelExpressions.ResumeLayout(false);
            this.panelExpressions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSearch)).EndInit();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripStandardButtons.ResumeLayout(false);
            this.toolStripStandardButtons.PerformLayout();
            this.contextMenuStripTabs.ResumeLayout(false);
            this.contextMenuStripNodes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ToolStripMenuItem contextMenuStripMenuItemClose;
        private ToolStripMenuItem toolStripMenuItemClose;
        private ContextMenuStrip contextMenuStripTabs;
        protected ToolStripMenuItem toolStripMenuItemExit;
        protected ToolStripMenuItem toolStripMenuItemFile;
        private ToolStripMenuItem toolStripMenuItemFont;
        private ToolStripMenuItem toolStripMenuItemFormat;
        protected MenuStrip menuStripMain;
        internal ToolStripButton toolStripButtonOpen;
        internal ToolStripMenuItem toolStripMenuItemOpen;
        protected ToolStrip toolStripStandardButtons;
        private TabControl tabControl;
        private ToolStripContainer toolStripContainer;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabelMain;
        private ToolStripProgressBar toolStripProgressBar;
        protected ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem toolStripMenuItemView;
        private ToolStripMenuItem toolStripMenuItemRefresh;
        private ToolStripMenuItem toolStripMenuItemEdit;
        private ToolStripMenuItem toolStripMenuItemCopyFormattedOuterXml;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSpringTextBox toolStripTextBoxXpath;
        private ToolStripStatusLabel toolStripStatusLabelChildCount;
        private ToolStripButton toolStripButtonLaunchXpath;
        private ToolStripMenuItem toolStripMenuItemSaveWithFormatting;
        private ToolStripSeparator toolStripSeparator1;

        private ToolStripMenuItem toolStripMenuItemCopy;
        private ToolStripButton toolStripButtonSave;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButtonCopyFormattedOuterXml;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem toolStripMenuItemSaveAs;
        private ToolStripButton toolStripButtonRefresh;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem toolStripMenuItemCopyXPath;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem toolStripMenuItemCopyFullPath;
        private ToolStripMenuItem toolStripMenuItemOpenContainingFolder;
        private ToolStripMenuItem toolStripMenuItemOpenInEditor;
        private ToolStripMenuItem toolStripMenuItemUseHighlighting;
        private ToolStripButton toolStripButtonXPathExpression;
        private Panel panelExpressions;
        private Splitter splitterTabBottom;
        private Button buttonCloseExpressions;
        private Label labelExpressions;
        private TextBox textBoxSearchExpressions;
        private ListView listViewExpressions;
        private ToolTip toolTip;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderExpression;
        private ToolStripMenuItem toolStripMenuItemViewExpressions;
        private ContextMenuStrip contextMenuStripNodes;
        private ToolStripMenuItem contextMenuStripNodesItemCopy;
        private ToolStripMenuItem contextMenuStripNodesItemCopyFormattedOuterXml;
        private ToolStripMenuItem contextMenuStripNodesItemCopyXPath;
        private PictureBox pictureBoxSearch;
        private ColumnHeader columnHeaderComments;
    }
}