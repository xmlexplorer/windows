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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelChildCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenInEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSaveWithFormatting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSaveWithoutFormatting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveAsWithoutFormatting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemClearRecentFileList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
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
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
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
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.statusStrip.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.toolStripStandardButtons.SuspendLayout();
            this.contextMenuStripTabs.SuspendLayout();
            this.contextMenuStripNodes.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMain,
            this.toolStripStatusLabelChildCount,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 544);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(792, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // toolStripStatusLabelMain
            // 
            this.toolStripStatusLabelMain.Name = "toolStripStatusLabelMain";
            this.toolStripStatusLabelMain.Size = new System.Drawing.Size(694, 17);
            this.toolStripStatusLabelMain.Spring = true;
            this.toolStripStatusLabelMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelChildCount
            // 
            this.toolStripStatusLabelChildCount.Name = "toolStripStatusLabelChildCount";
            this.toolStripStatusLabelChildCount.Size = new System.Drawing.Size(83, 17);
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
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemEdit,
            this.toolStripMenuItemFormat,
            this.toolStripMenuItemView,
            this.toolStripMenuItemHelp});
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
            this.toolStripSeparator7,
            this.toolStripMenuItemSaveWithoutFormatting,
            this.toolStripMenuItemSaveAsWithoutFormatting,
            this.toolStripSeparator1,
            this.toolStripMenuItemRecentFiles,
            this.toolStripSeparator6,
            this.toolStripMenuItemExit});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItemFile.Text = "&File";
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemOpen.Image")));
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(235, 22);
            this.toolStripMenuItemOpen.Text = "&Open...";
            // 
            // toolStripMenuItemOpenInEditor
            // 
            this.toolStripMenuItemOpenInEditor.Name = "toolStripMenuItemOpenInEditor";
            this.toolStripMenuItemOpenInEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.toolStripMenuItemOpenInEditor.Size = new System.Drawing.Size(235, 22);
            this.toolStripMenuItemOpenInEditor.Text = "Open in &Editor";
            // 
            // toolStripMenuItemClose
            // 
            this.toolStripMenuItemClose.Name = "toolStripMenuItemClose";
            this.toolStripMenuItemClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.toolStripMenuItemClose.Size = new System.Drawing.Size(235, 22);
            this.toolStripMenuItemClose.Text = "&Close";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(232, 6);
            // 
            // toolStripMenuItemSaveWithFormatting
            // 
            this.toolStripMenuItemSaveWithFormatting.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemSaveWithFormatting.Image")));
            this.toolStripMenuItemSaveWithFormatting.Name = "toolStripMenuItemSaveWithFormatting";
            this.toolStripMenuItemSaveWithFormatting.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItemSaveWithFormatting.Size = new System.Drawing.Size(235, 22);
            this.toolStripMenuItemSaveWithFormatting.Text = "&Save (with formatting)";
            // 
            // toolStripMenuItemSaveAs
            // 
            this.toolStripMenuItemSaveAs.Name = "toolStripMenuItemSaveAs";
            this.toolStripMenuItemSaveAs.Size = new System.Drawing.Size(235, 22);
            this.toolStripMenuItemSaveAs.Text = "Save As... (with formatting)";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(232, 6);
            // 
            // toolStripMenuItemSaveWithoutFormatting
            // 
            this.toolStripMenuItemSaveWithoutFormatting.Name = "toolStripMenuItemSaveWithoutFormatting";
            this.toolStripMenuItemSaveWithoutFormatting.Size = new System.Drawing.Size(235, 22);
            this.toolStripMenuItemSaveWithoutFormatting.Text = "Save (without formatting)";
            // 
            // toolStripMenuItemSaveAsWithoutFormatting
            // 
            this.toolStripMenuItemSaveAsWithoutFormatting.Name = "toolStripMenuItemSaveAsWithoutFormatting";
            this.toolStripMenuItemSaveAsWithoutFormatting.Size = new System.Drawing.Size(235, 22);
            this.toolStripMenuItemSaveAsWithoutFormatting.Text = "Save As... (without formatting)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(232, 6);
            // 
            // toolStripMenuItemRecentFiles
            // 
            this.toolStripMenuItemRecentFiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemClearRecentFileList});
            this.toolStripMenuItemRecentFiles.Name = "toolStripMenuItemRecentFiles";
            this.toolStripMenuItemRecentFiles.Size = new System.Drawing.Size(235, 22);
            this.toolStripMenuItemRecentFiles.Text = "Recent &Files";
            // 
            // toolStripMenuItemClearRecentFileList
            // 
            this.toolStripMenuItemClearRecentFileList.Image = global::XmlExplorer.Controls.Properties.Resources.DeleteHS;
            this.toolStripMenuItemClearRecentFileList.Name = "toolStripMenuItemClearRecentFileList";
            this.toolStripMenuItemClearRecentFileList.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItemClearRecentFileList.Text = "Clear Recent File List";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(232, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(235, 22);
            this.toolStripMenuItemExit.Text = "E&xit";
            // 
            // toolStripMenuItemEdit
            // 
            this.toolStripMenuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemCopyFormattedOuterXml,
            this.toolStripMenuItemCopyXPath});
            this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
            this.toolStripMenuItemEdit.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItemEdit.Text = "&Edit";
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCopy.Image")));
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.ShortcutKeyDisplayString = "Ctrl+C";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(312, 22);
            this.toolStripMenuItemCopy.Text = "&Copy";
            // 
            // toolStripMenuItemCopyFormattedOuterXml
            // 
            this.toolStripMenuItemCopyFormattedOuterXml.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCopyFormattedOuterXml.Image")));
            this.toolStripMenuItemCopyFormattedOuterXml.Name = "toolStripMenuItemCopyFormattedOuterXml";
            this.toolStripMenuItemCopyFormattedOuterXml.ShortcutKeyDisplayString = "";
            this.toolStripMenuItemCopyFormattedOuterXml.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItemCopyFormattedOuterXml.Size = new System.Drawing.Size(312, 22);
            this.toolStripMenuItemCopyFormattedOuterXml.Text = "&Copy Formatted Outer XML";
            // 
            // toolStripMenuItemCopyXPath
            // 
            this.toolStripMenuItemCopyXPath.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
            this.toolStripMenuItemCopyXPath.Name = "toolStripMenuItemCopyXPath";
            this.toolStripMenuItemCopyXPath.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItemCopyXPath.Size = new System.Drawing.Size(312, 22);
            this.toolStripMenuItemCopyXPath.Text = "Copy Node &XPath to Address Bar";
            // 
            // toolStripMenuItemFormat
            // 
            this.toolStripMenuItemFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFont,
            this.toolStripMenuItemUseHighlighting});
            this.toolStripMenuItemFormat.Name = "toolStripMenuItemFormat";
            this.toolStripMenuItemFormat.Size = new System.Drawing.Size(57, 20);
            this.toolStripMenuItemFormat.Text = "F&ormat";
            // 
            // toolStripMenuItemFont
            // 
            this.toolStripMenuItemFont.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemFont.Image")));
            this.toolStripMenuItemFont.Name = "toolStripMenuItemFont";
            this.toolStripMenuItemFont.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItemFont.Text = "&Font...";
            // 
            // toolStripMenuItemUseHighlighting
            // 
            this.toolStripMenuItemUseHighlighting.Name = "toolStripMenuItemUseHighlighting";
            this.toolStripMenuItemUseHighlighting.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItemUseHighlighting.Text = "&Use Highlighing";
            // 
            // toolStripMenuItemView
            // 
            this.toolStripMenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRefresh});
            this.toolStripMenuItemView.Name = "toolStripMenuItemView";
            this.toolStripMenuItemView.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItemView.Text = "&View";
            // 
            // toolStripMenuItemRefresh
            // 
            this.toolStripMenuItemRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemRefresh.Image")));
            this.toolStripMenuItemRefresh.Name = "toolStripMenuItemRefresh";
            this.toolStripMenuItemRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.toolStripMenuItemRefresh.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItemRefresh.Text = "&Refresh";
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCheckForUpdates,
            this.toolStripSeparator8,
            this.toolStripMenuItemAbout});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItemHelp.Text = "&Help";
            // 
            // toolStripMenuItemCheckForUpdates
            // 
            this.toolStripMenuItemCheckForUpdates.Name = "toolStripMenuItemCheckForUpdates";
            this.toolStripMenuItemCheckForUpdates.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItemCheckForUpdates.Text = "Check for updates...";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(176, 6);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItemAbout.Text = "&About XML Explorer";
            // 
            // toolStripStandardButtons
            // 
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
            this.toolStripLabel1.Size = new System.Drawing.Size(41, 22);
            this.toolStripLabel1.Text = "XPath:";
            // 
            // toolStripTextBoxXpath
            // 
            this.toolStripTextBoxXpath.AcceptsReturn = true;
            this.toolStripTextBoxXpath.AcceptsTab = true;
            this.toolStripTextBoxXpath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.toolStripTextBoxXpath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.toolStripTextBoxXpath.Name = "toolStripTextBoxXpath";
            this.toolStripTextBoxXpath.Size = new System.Drawing.Size(500, 25);
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
            this.toolStripButtonLaunchXpath.Size = new System.Drawing.Size(75, 22);
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
            this.contextMenuStripTabs.Size = new System.Drawing.Size(202, 76);
            // 
            // contextMenuStripMenuItemClose
            // 
            this.contextMenuStripMenuItemClose.Name = "contextMenuStripMenuItemClose";
            this.contextMenuStripMenuItemClose.Size = new System.Drawing.Size(201, 22);
            this.contextMenuStripMenuItemClose.Text = "&Close";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(198, 6);
            // 
            // toolStripMenuItemCopyFullPath
            // 
            this.toolStripMenuItemCopyFullPath.Name = "toolStripMenuItemCopyFullPath";
            this.toolStripMenuItemCopyFullPath.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItemCopyFullPath.Text = "Copy Full Path";
            // 
            // toolStripMenuItemOpenContainingFolder
            // 
            this.toolStripMenuItemOpenContainingFolder.Name = "toolStripMenuItemOpenContainingFolder";
            this.toolStripMenuItemOpenContainingFolder.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItemOpenContainingFolder.Text = "Open Containing Folder";
            // 
            // contextMenuStripNodes
            // 
            this.contextMenuStripNodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripNodesItemCopy,
            this.contextMenuStripNodesItemCopyFormattedOuterXml,
            this.contextMenuStripNodesItemCopyXPath});
            this.contextMenuStripNodes.Name = "contextMenuStripNodes";
            this.contextMenuStripNodes.Size = new System.Drawing.Size(248, 70);
            // 
            // contextMenuStripNodesItemCopy
            // 
            this.contextMenuStripNodesItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuStripNodesItemCopy.Image")));
            this.contextMenuStripNodesItemCopy.Name = "contextMenuStripNodesItemCopy";
            this.contextMenuStripNodesItemCopy.ShortcutKeyDisplayString = "";
            this.contextMenuStripNodesItemCopy.Size = new System.Drawing.Size(247, 22);
            this.contextMenuStripNodesItemCopy.Text = "&Copy";
            // 
            // contextMenuStripNodesItemCopyFormattedOuterXml
            // 
            this.contextMenuStripNodesItemCopyFormattedOuterXml.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuStripNodesItemCopyFormattedOuterXml.Image")));
            this.contextMenuStripNodesItemCopyFormattedOuterXml.Name = "contextMenuStripNodesItemCopyFormattedOuterXml";
            this.contextMenuStripNodesItemCopyFormattedOuterXml.ShortcutKeyDisplayString = "";
            this.contextMenuStripNodesItemCopyFormattedOuterXml.Size = new System.Drawing.Size(247, 22);
            this.contextMenuStripNodesItemCopyFormattedOuterXml.Text = "&Copy Formatted Outer XML";
            // 
            // contextMenuStripNodesItemCopyXPath
            // 
            this.contextMenuStripNodesItemCopyXPath.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
            this.contextMenuStripNodesItemCopyXPath.Name = "contextMenuStripNodesItemCopyXPath";
            this.contextMenuStripNodesItemCopyXPath.Size = new System.Drawing.Size(247, 22);
            this.contextMenuStripNodesItemCopyXPath.Text = "Copy Node &XPath to Address Bar";
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 49);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(792, 495);
            this.dockPanel.TabIndex = 5;
            // 
            // TabbedXmlExplorerWindow
            // 
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.toolStripStandardButtons);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "TabbedXmlExplorerWindow";
            this.Text = "XmlExplorer";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripStandardButtons.ResumeLayout(false);
            this.toolStripStandardButtons.PerformLayout();
            this.contextMenuStripTabs.ResumeLayout(false);
            this.contextMenuStripNodes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private ToolTip toolTip;
        private ContextMenuStrip contextMenuStripNodes;
        private ToolStripMenuItem contextMenuStripNodesItemCopy;
        private ToolStripMenuItem contextMenuStripNodesItemCopyFormattedOuterXml;
        private ToolStripMenuItem contextMenuStripNodesItemCopyXPath;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private ToolStripMenuItem toolStripMenuItemRecentFiles;
        private ToolStripMenuItem toolStripMenuItemClearRecentFileList;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem toolStripMenuItemHelp;
        private ToolStripMenuItem toolStripMenuItemAbout;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem toolStripMenuItemSaveWithoutFormatting;
        private ToolStripMenuItem toolStripMenuItemSaveAsWithoutFormatting;
        private ToolStripMenuItem toolStripMenuItemCheckForUpdates;
        private ToolStripSeparator toolStripSeparator8;
    }
}