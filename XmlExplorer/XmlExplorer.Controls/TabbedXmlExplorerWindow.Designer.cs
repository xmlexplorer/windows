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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripButtonLaunchXpath = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemCopyFullPath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenContainingFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.toolStripStandardButtons.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(792, 495);
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
            this.toolStripMenuItemClose,
            this.toolStripSeparator,
            this.toolStripMenuItemSaveWithFormatting,
            this.toolStripMenuItemSaveAs,
            this.toolStripSeparator1,
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
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(232, 22);
            this.toolStripMenuItemOpen.Text = "&Open...";
            // 
            // toolStripMenuItemClose
            // 
            this.toolStripMenuItemClose.Name = "toolStripMenuItemClose";
            this.toolStripMenuItemClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.toolStripMenuItemClose.Size = new System.Drawing.Size(232, 22);
            this.toolStripMenuItemClose.Text = "&Close";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(229, 6);
            // 
            // toolStripMenuItemSaveWithFormatting
            // 
            this.toolStripMenuItemSaveWithFormatting.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemSaveWithFormatting.Image")));
            this.toolStripMenuItemSaveWithFormatting.Name = "toolStripMenuItemSaveWithFormatting";
            this.toolStripMenuItemSaveWithFormatting.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItemSaveWithFormatting.Size = new System.Drawing.Size(232, 22);
            this.toolStripMenuItemSaveWithFormatting.Text = "&Save (with formatting)";
            // 
            // toolStripMenuItemSaveAs
            // 
            this.toolStripMenuItemSaveAs.Name = "toolStripMenuItemSaveAs";
            this.toolStripMenuItemSaveAs.Size = new System.Drawing.Size(232, 22);
            this.toolStripMenuItemSaveAs.Text = "Save As... (with formatting)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(232, 22);
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
            this.toolStripMenuItemFont});
            this.toolStripMenuItemFormat.Name = "toolStripMenuItemFormat";
            this.toolStripMenuItemFormat.Size = new System.Drawing.Size(57, 20);
            this.toolStripMenuItemFormat.Text = "&Format";
            // 
            // toolStripMenuItemFont
            // 
            this.toolStripMenuItemFont.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemFont.Image")));
            this.toolStripMenuItemFont.Name = "toolStripMenuItemFont";
            this.toolStripMenuItemFont.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItemFont.Text = "&Font...";
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
            this.toolStripTextBoxXpath.Size = new System.Drawing.Size(523, 25);
            this.toolStripTextBoxXpath.ToolTipText = "Enter an XPath expression.\r\n\r\nPress Enter to select the first match.\r\nPress Shift" +
                "+Enter (or Launch button) to open the expression results in a new window.";
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
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripMenuItemClose,
            this.toolStripSeparator5,
            this.toolStripMenuItemCopyFullPath,
            this.toolStripMenuItemOpenContainingFolder});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(202, 76);
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
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripStandardButtons.ResumeLayout(false);
            this.toolStripStandardButtons.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ToolStripMenuItem contextMenuStripMenuItemClose;
        private ToolStripMenuItem toolStripMenuItemClose;
        private ContextMenuStrip contextMenuStrip;
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
    }
}