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
			WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
			WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelChildCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabelLoadTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStripMain = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemNewFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemOpenUrl = new System.Windows.Forms.ToolStripMenuItem();
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
			this.toolStripMenuItemCopyNodeValueBase64 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCopyFormattedOuterXml = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCopyXPath = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCopyAttributesXPath = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemExpandAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFormat = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFont = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemUseHighlighting = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemShowNodeToolTips = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemFileTypes = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.importChildNodeDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportChildNodeDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripStandardButtons = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonOpenUrl = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonCopyFormattedOuterXml = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonExpandAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonCollapseAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripTextBoxXpath = new XmlExplorer.Controls.ToolStripSpringTextBox();
			this.toolStripButtonXPathExpression = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonLaunchXpath = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			this.toolStripLabelResults = new System.Windows.Forms.ToolStripLabel();
			this.contextMenuStripTabs = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemCopyFullPath = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemOpenContainingFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.contextMenuStripNodes = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuStripNodesItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripItemCopyNodeValueBase64 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
			this.contextMenuStripNodesItemCopyFormattedOuterXml = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripNodesItemCopyXPath = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripNodesItemCopyXPathAttributes = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.contextMenuStripItemNodesExpandAll = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripItemNodesCollapeAll = new System.Windows.Forms.ToolStripMenuItem();
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemCopyNodeTextBase64 = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripItemCopyNodeTextBase64 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripProgressBar,
            this.toolStripStatusLabelLoadTime});
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.Name = "statusStrip";
			// 
			// toolStripStatusLabelMain
			// 
			this.toolStripStatusLabelMain.Name = "toolStripStatusLabelMain";
			resources.ApplyResources(this.toolStripStatusLabelMain, "toolStripStatusLabelMain");
			this.toolStripStatusLabelMain.Spring = true;
			// 
			// toolStripStatusLabelChildCount
			// 
			this.toolStripStatusLabelChildCount.Name = "toolStripStatusLabelChildCount";
			resources.ApplyResources(this.toolStripStatusLabelChildCount, "toolStripStatusLabelChildCount");
			// 
			// toolStripProgressBar
			// 
			this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			resources.ApplyResources(this.toolStripProgressBar, "toolStripProgressBar");
			this.toolStripProgressBar.Name = "toolStripProgressBar";
			this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			// 
			// toolStripStatusLabelLoadTime
			// 
			this.toolStripStatusLabelLoadTime.Name = "toolStripStatusLabelLoadTime";
			resources.ApplyResources(this.toolStripStatusLabelLoadTime, "toolStripStatusLabelLoadTime");
			// 
			// menuStripMain
			// 
			this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemEdit,
            this.toolStripMenuItemView,
            this.toolStripMenuItemFormat,
            this.toolsToolStripMenuItem,
            this.toolStripMenuItemHelp});
			resources.ApplyResources(this.menuStripMain, "menuStripMain");
			this.menuStripMain.Name = "menuStripMain";
			// 
			// toolStripMenuItemFile
			// 
			this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNewFromClipboard,
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemOpenUrl,
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
			resources.ApplyResources(this.toolStripMenuItemFile, "toolStripMenuItemFile");
			// 
			// toolStripMenuItemNewFromClipboard
			// 
			this.toolStripMenuItemNewFromClipboard.Image = global::XmlExplorer.Controls.Properties.Resources.NewDocumentHS;
			this.toolStripMenuItemNewFromClipboard.Name = "toolStripMenuItemNewFromClipboard";
			resources.ApplyResources(this.toolStripMenuItemNewFromClipboard, "toolStripMenuItemNewFromClipboard");
			// 
			// toolStripMenuItemOpen
			// 
			resources.ApplyResources(this.toolStripMenuItemOpen, "toolStripMenuItemOpen");
			this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
			// 
			// toolStripMenuItemOpenUrl
			// 
			this.toolStripMenuItemOpenUrl.Image = global::XmlExplorer.Controls.Properties.Resources.OpenLink;
			this.toolStripMenuItemOpenUrl.Name = "toolStripMenuItemOpenUrl";
			resources.ApplyResources(this.toolStripMenuItemOpenUrl, "toolStripMenuItemOpenUrl");
			// 
			// toolStripMenuItemOpenInEditor
			// 
			this.toolStripMenuItemOpenInEditor.Name = "toolStripMenuItemOpenInEditor";
			resources.ApplyResources(this.toolStripMenuItemOpenInEditor, "toolStripMenuItemOpenInEditor");
			// 
			// toolStripMenuItemClose
			// 
			this.toolStripMenuItemClose.Name = "toolStripMenuItemClose";
			resources.ApplyResources(this.toolStripMenuItemClose, "toolStripMenuItemClose");
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
			// 
			// toolStripMenuItemSaveWithFormatting
			// 
			resources.ApplyResources(this.toolStripMenuItemSaveWithFormatting, "toolStripMenuItemSaveWithFormatting");
			this.toolStripMenuItemSaveWithFormatting.Name = "toolStripMenuItemSaveWithFormatting";
			// 
			// toolStripMenuItemSaveAs
			// 
			this.toolStripMenuItemSaveAs.Name = "toolStripMenuItemSaveAs";
			resources.ApplyResources(this.toolStripMenuItemSaveAs, "toolStripMenuItemSaveAs");
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
			// 
			// toolStripMenuItemSaveWithoutFormatting
			// 
			this.toolStripMenuItemSaveWithoutFormatting.Name = "toolStripMenuItemSaveWithoutFormatting";
			resources.ApplyResources(this.toolStripMenuItemSaveWithoutFormatting, "toolStripMenuItemSaveWithoutFormatting");
			// 
			// toolStripMenuItemSaveAsWithoutFormatting
			// 
			this.toolStripMenuItemSaveAsWithoutFormatting.Name = "toolStripMenuItemSaveAsWithoutFormatting";
			resources.ApplyResources(this.toolStripMenuItemSaveAsWithoutFormatting, "toolStripMenuItemSaveAsWithoutFormatting");
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// toolStripMenuItemRecentFiles
			// 
			this.toolStripMenuItemRecentFiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemClearRecentFileList});
			this.toolStripMenuItemRecentFiles.Name = "toolStripMenuItemRecentFiles";
			resources.ApplyResources(this.toolStripMenuItemRecentFiles, "toolStripMenuItemRecentFiles");
			// 
			// toolStripMenuItemClearRecentFileList
			// 
			this.toolStripMenuItemClearRecentFileList.Image = global::XmlExplorer.Controls.Properties.Resources.DeleteHS;
			this.toolStripMenuItemClearRecentFileList.Name = "toolStripMenuItemClearRecentFileList";
			resources.ApplyResources(this.toolStripMenuItemClearRecentFileList, "toolStripMenuItemClearRecentFileList");
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
			// 
			// toolStripMenuItemExit
			// 
			this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
			resources.ApplyResources(this.toolStripMenuItemExit, "toolStripMenuItemExit");
			// 
			// toolStripMenuItemEdit
			// 
			this.toolStripMenuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemCopyNodeTextBase64,
            this.toolStripMenuItemCopyNodeValueBase64,
            this.toolStripSeparator15,
            this.toolStripMenuItemCopyFormattedOuterXml,
            this.toolStripMenuItemCopyXPath,
            this.toolStripMenuItemCopyAttributesXPath,
            this.toolStripSeparator10,
            this.toolStripMenuItemExpandAll,
            this.toolStripMenuItemCollapseAll});
			this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
			resources.ApplyResources(this.toolStripMenuItemEdit, "toolStripMenuItemEdit");
			// 
			// toolStripMenuItemCopy
			// 
			resources.ApplyResources(this.toolStripMenuItemCopy, "toolStripMenuItemCopy");
			this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
			// 
			// toolStripMenuItemCopyNodeValueBase64
			// 
			this.toolStripMenuItemCopyNodeValueBase64.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
			this.toolStripMenuItemCopyNodeValueBase64.Name = "toolStripMenuItemCopyNodeValueBase64";
			resources.ApplyResources(this.toolStripMenuItemCopyNodeValueBase64, "toolStripMenuItemCopyNodeValueBase64");
			// 
			// toolStripMenuItemCopyFormattedOuterXml
			// 
			resources.ApplyResources(this.toolStripMenuItemCopyFormattedOuterXml, "toolStripMenuItemCopyFormattedOuterXml");
			this.toolStripMenuItemCopyFormattedOuterXml.Name = "toolStripMenuItemCopyFormattedOuterXml";
			// 
			// toolStripMenuItemCopyXPath
			// 
			this.toolStripMenuItemCopyXPath.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
			this.toolStripMenuItemCopyXPath.Name = "toolStripMenuItemCopyXPath";
			resources.ApplyResources(this.toolStripMenuItemCopyXPath, "toolStripMenuItemCopyXPath");
			// 
			// toolStripMenuItemCopyAttributesXPath
			// 
			this.toolStripMenuItemCopyAttributesXPath.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
			this.toolStripMenuItemCopyAttributesXPath.Name = "toolStripMenuItemCopyAttributesXPath";
			resources.ApplyResources(this.toolStripMenuItemCopyAttributesXPath, "toolStripMenuItemCopyAttributesXPath");
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
			// 
			// toolStripMenuItemExpandAll
			// 
			this.toolStripMenuItemExpandAll.Image = global::XmlExplorer.Controls.Properties.Resources.ExpandAll;
			this.toolStripMenuItemExpandAll.Name = "toolStripMenuItemExpandAll";
			resources.ApplyResources(this.toolStripMenuItemExpandAll, "toolStripMenuItemExpandAll");
			// 
			// toolStripMenuItemCollapseAll
			// 
			this.toolStripMenuItemCollapseAll.Image = global::XmlExplorer.Controls.Properties.Resources.CollapseAll;
			this.toolStripMenuItemCollapseAll.Name = "toolStripMenuItemCollapseAll";
			resources.ApplyResources(this.toolStripMenuItemCollapseAll, "toolStripMenuItemCollapseAll");
			// 
			// toolStripMenuItemView
			// 
			this.toolStripMenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRefresh});
			this.toolStripMenuItemView.Name = "toolStripMenuItemView";
			resources.ApplyResources(this.toolStripMenuItemView, "toolStripMenuItemView");
			// 
			// toolStripMenuItemRefresh
			// 
			resources.ApplyResources(this.toolStripMenuItemRefresh, "toolStripMenuItemRefresh");
			this.toolStripMenuItemRefresh.Name = "toolStripMenuItemRefresh";
			// 
			// toolStripMenuItemFormat
			// 
			this.toolStripMenuItemFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFont,
            this.toolStripMenuItemUseHighlighting,
            this.toolStripMenuItemShowNodeToolTips});
			this.toolStripMenuItemFormat.Name = "toolStripMenuItemFormat";
			resources.ApplyResources(this.toolStripMenuItemFormat, "toolStripMenuItemFormat");
			// 
			// toolStripMenuItemFont
			// 
			resources.ApplyResources(this.toolStripMenuItemFont, "toolStripMenuItemFont");
			this.toolStripMenuItemFont.Name = "toolStripMenuItemFont";
			// 
			// toolStripMenuItemUseHighlighting
			// 
			this.toolStripMenuItemUseHighlighting.Name = "toolStripMenuItemUseHighlighting";
			resources.ApplyResources(this.toolStripMenuItemUseHighlighting, "toolStripMenuItemUseHighlighting");
			// 
			// toolStripMenuItemShowNodeToolTips
			// 
			this.toolStripMenuItemShowNodeToolTips.Checked = true;
			this.toolStripMenuItemShowNodeToolTips.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripMenuItemShowNodeToolTips.Name = "toolStripMenuItemShowNodeToolTips";
			resources.ApplyResources(this.toolStripMenuItemShowNodeToolTips, "toolStripMenuItemShowNodeToolTips");
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOptions,
            this.toolStripSeparator12,
            this.toolStripMenuItemFileTypes,
            this.toolStripSeparator13,
            this.importChildNodeDefinitionsToolStripMenuItem,
            this.exportChildNodeDefinitionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
			// 
			// toolStripMenuItemOptions
			// 
			this.toolStripMenuItemOptions.Image = global::XmlExplorer.Controls.Properties.Resources.settings_16;
			this.toolStripMenuItemOptions.Name = "toolStripMenuItemOptions";
			resources.ApplyResources(this.toolStripMenuItemOptions, "toolStripMenuItemOptions");
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			resources.ApplyResources(this.toolStripSeparator12, "toolStripSeparator12");
			// 
			// toolStripMenuItemFileTypes
			// 
			this.toolStripMenuItemFileTypes.Name = "toolStripMenuItemFileTypes";
			resources.ApplyResources(this.toolStripMenuItemFileTypes, "toolStripMenuItemFileTypes");
			this.toolStripMenuItemFileTypes.Click += new System.EventHandler(this.toolStripMenuItemFileTypes_Click);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			resources.ApplyResources(this.toolStripSeparator13, "toolStripSeparator13");
			// 
			// importChildNodeDefinitionsToolStripMenuItem
			// 
			this.importChildNodeDefinitionsToolStripMenuItem.Name = "importChildNodeDefinitionsToolStripMenuItem";
			resources.ApplyResources(this.importChildNodeDefinitionsToolStripMenuItem, "importChildNodeDefinitionsToolStripMenuItem");
			this.importChildNodeDefinitionsToolStripMenuItem.Click += new System.EventHandler(this.importChildNodeDefinitionsToolStripMenuItem_Click);
			// 
			// exportChildNodeDefinitionsToolStripMenuItem
			// 
			this.exportChildNodeDefinitionsToolStripMenuItem.Name = "exportChildNodeDefinitionsToolStripMenuItem";
			resources.ApplyResources(this.exportChildNodeDefinitionsToolStripMenuItem, "exportChildNodeDefinitionsToolStripMenuItem");
			this.exportChildNodeDefinitionsToolStripMenuItem.Click += new System.EventHandler(this.exportChildNodeDefinitionsToolStripMenuItem_Click);
			// 
			// toolStripMenuItemHelp
			// 
			this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCheckForUpdates,
            this.toolStripSeparator8,
            this.toolStripMenuItemAbout});
			this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
			resources.ApplyResources(this.toolStripMenuItemHelp, "toolStripMenuItemHelp");
			// 
			// toolStripMenuItemCheckForUpdates
			// 
			this.toolStripMenuItemCheckForUpdates.Name = "toolStripMenuItemCheckForUpdates";
			resources.ApplyResources(this.toolStripMenuItemCheckForUpdates, "toolStripMenuItemCheckForUpdates");
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
			// 
			// toolStripMenuItemAbout
			// 
			this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
			resources.ApplyResources(this.toolStripMenuItemAbout, "toolStripMenuItemAbout");
			// 
			// toolStripStandardButtons
			// 
			this.toolStripStandardButtons.CanOverflow = false;
			this.toolStripStandardButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen,
            this.toolStripButtonOpenUrl,
            this.toolStripButtonSave,
            this.toolStripSeparator2,
            this.toolStripButtonCopyFormattedOuterXml,
            this.toolStripSeparator11,
            this.toolStripButtonExpandAll,
            this.toolStripButtonCollapseAll,
            this.toolStripSeparator3,
            this.toolStripButtonRefresh,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.toolStripTextBoxXpath,
            this.toolStripButtonXPathExpression,
            this.toolStripButtonLaunchXpath,
            this.toolStripButtonPrevious,
            this.toolStripButtonNext,
            this.toolStripLabelResults});
			resources.ApplyResources(this.toolStripStandardButtons, "toolStripStandardButtons");
			this.toolStripStandardButtons.Name = "toolStripStandardButtons";
			this.toolStripStandardButtons.Stretch = true;
			// 
			// toolStripButtonOpen
			// 
			this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.toolStripButtonOpen, "toolStripButtonOpen");
			this.toolStripButtonOpen.Name = "toolStripButtonOpen";
			// 
			// toolStripButtonOpenUrl
			// 
			this.toolStripButtonOpenUrl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonOpenUrl.Image = global::XmlExplorer.Controls.Properties.Resources.OpenLink;
			resources.ApplyResources(this.toolStripButtonOpenUrl, "toolStripButtonOpenUrl");
			this.toolStripButtonOpenUrl.Name = "toolStripButtonOpenUrl";
			// 
			// toolStripButtonSave
			// 
			this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.toolStripButtonSave, "toolStripButtonSave");
			this.toolStripButtonSave.Name = "toolStripButtonSave";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// toolStripButtonCopyFormattedOuterXml
			// 
			this.toolStripButtonCopyFormattedOuterXml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.toolStripButtonCopyFormattedOuterXml, "toolStripButtonCopyFormattedOuterXml");
			this.toolStripButtonCopyFormattedOuterXml.Name = "toolStripButtonCopyFormattedOuterXml";
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
			// 
			// toolStripButtonExpandAll
			// 
			this.toolStripButtonExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonExpandAll.Image = global::XmlExplorer.Controls.Properties.Resources.ExpandAll;
			this.toolStripButtonExpandAll.Name = "toolStripButtonExpandAll";
			resources.ApplyResources(this.toolStripButtonExpandAll, "toolStripButtonExpandAll");
			// 
			// toolStripButtonCollapseAll
			// 
			this.toolStripButtonCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonCollapseAll.Image = global::XmlExplorer.Controls.Properties.Resources.CollapseAll;
			this.toolStripButtonCollapseAll.Name = "toolStripButtonCollapseAll";
			resources.ApplyResources(this.toolStripButtonCollapseAll, "toolStripButtonCollapseAll");
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
			// 
			// toolStripButtonRefresh
			// 
			this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRefresh.Image = global::XmlExplorer.Controls.Properties.Resources.RefreshDocViewHS;
			resources.ApplyResources(this.toolStripButtonRefresh, "toolStripButtonRefresh");
			this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
			// 
			// toolStripTextBoxXpath
			// 
			this.toolStripTextBoxXpath.AcceptsReturn = true;
			this.toolStripTextBoxXpath.AcceptsTab = true;
			this.toolStripTextBoxXpath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.toolStripTextBoxXpath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.toolStripTextBoxXpath.Name = "toolStripTextBoxXpath";
			resources.ApplyResources(this.toolStripTextBoxXpath, "toolStripTextBoxXpath");
			// 
			// toolStripButtonXPathExpression
			// 
			this.toolStripButtonXPathExpression.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonXPathExpression.Image = global::XmlExplorer.Controls.Properties.Resources.unstarred;
			resources.ApplyResources(this.toolStripButtonXPathExpression, "toolStripButtonXPathExpression");
			this.toolStripButtonXPathExpression.Name = "toolStripButtonXPathExpression";
			// 
			// toolStripButtonLaunchXpath
			// 
			resources.ApplyResources(this.toolStripButtonLaunchXpath, "toolStripButtonLaunchXpath");
			this.toolStripButtonLaunchXpath.Name = "toolStripButtonLaunchXpath";
			// 
			// toolStripButtonPrevious
			// 
			this.toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.toolStripButtonPrevious, "toolStripButtonPrevious");
			this.toolStripButtonPrevious.Image = global::XmlExplorer.Controls.Properties.Resources.Up;
			this.toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			// 
			// toolStripButtonNext
			// 
			this.toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonNext.Image = global::XmlExplorer.Controls.Properties.Resources.Down;
			resources.ApplyResources(this.toolStripButtonNext, "toolStripButtonNext");
			this.toolStripButtonNext.Name = "toolStripButtonNext";
			// 
			// toolStripLabelResults
			// 
			this.toolStripLabelResults.Name = "toolStripLabelResults";
			resources.ApplyResources(this.toolStripLabelResults, "toolStripLabelResults");
			// 
			// contextMenuStripTabs
			// 
			this.contextMenuStripTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripMenuItemClose,
            this.toolStripSeparator5,
            this.toolStripMenuItemCopyFullPath,
            this.toolStripMenuItemOpenContainingFolder});
			this.contextMenuStripTabs.Name = "contextMenuStrip";
			resources.ApplyResources(this.contextMenuStripTabs, "contextMenuStripTabs");
			// 
			// contextMenuStripMenuItemClose
			// 
			this.contextMenuStripMenuItemClose.Name = "contextMenuStripMenuItemClose";
			resources.ApplyResources(this.contextMenuStripMenuItemClose, "contextMenuStripMenuItemClose");
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
			// 
			// toolStripMenuItemCopyFullPath
			// 
			this.toolStripMenuItemCopyFullPath.Name = "toolStripMenuItemCopyFullPath";
			resources.ApplyResources(this.toolStripMenuItemCopyFullPath, "toolStripMenuItemCopyFullPath");
			// 
			// toolStripMenuItemOpenContainingFolder
			// 
			this.toolStripMenuItemOpenContainingFolder.Name = "toolStripMenuItemOpenContainingFolder";
			resources.ApplyResources(this.toolStripMenuItemOpenContainingFolder, "toolStripMenuItemOpenContainingFolder");
			// 
			// contextMenuStripNodes
			// 
			this.contextMenuStripNodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripNodesItemCopy,
            this.contextMenuStripItemCopyNodeTextBase64,
            this.contextMenuStripItemCopyNodeValueBase64,
            this.toolStripSeparator14,
            this.contextMenuStripNodesItemCopyFormattedOuterXml,
            this.contextMenuStripNodesItemCopyXPath,
            this.contextMenuStripNodesItemCopyXPathAttributes,
            this.toolStripSeparator9,
            this.contextMenuStripItemNodesExpandAll,
            this.contextMenuStripItemNodesCollapeAll});
			this.contextMenuStripNodes.Name = "contextMenuStripNodes";
			resources.ApplyResources(this.contextMenuStripNodes, "contextMenuStripNodes");
			// 
			// contextMenuStripNodesItemCopy
			// 
			resources.ApplyResources(this.contextMenuStripNodesItemCopy, "contextMenuStripNodesItemCopy");
			this.contextMenuStripNodesItemCopy.Name = "contextMenuStripNodesItemCopy";
			// 
			// contextMenuStripItemCopyNodeValueBase64
			// 
			this.contextMenuStripItemCopyNodeValueBase64.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
			this.contextMenuStripItemCopyNodeValueBase64.Name = "contextMenuStripItemCopyNodeValueBase64";
			resources.ApplyResources(this.contextMenuStripItemCopyNodeValueBase64, "contextMenuStripItemCopyNodeValueBase64");
			// 
			// toolStripSeparator14
			// 
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			resources.ApplyResources(this.toolStripSeparator14, "toolStripSeparator14");
			// 
			// contextMenuStripNodesItemCopyFormattedOuterXml
			// 
			resources.ApplyResources(this.contextMenuStripNodesItemCopyFormattedOuterXml, "contextMenuStripNodesItemCopyFormattedOuterXml");
			this.contextMenuStripNodesItemCopyFormattedOuterXml.Name = "contextMenuStripNodesItemCopyFormattedOuterXml";
			// 
			// contextMenuStripNodesItemCopyXPath
			// 
			this.contextMenuStripNodesItemCopyXPath.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
			this.contextMenuStripNodesItemCopyXPath.Name = "contextMenuStripNodesItemCopyXPath";
			resources.ApplyResources(this.contextMenuStripNodesItemCopyXPath, "contextMenuStripNodesItemCopyXPath");
			// 
			// contextMenuStripNodesItemCopyXPathAttributes
			// 
			this.contextMenuStripNodesItemCopyXPathAttributes.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
			this.contextMenuStripNodesItemCopyXPathAttributes.Name = "contextMenuStripNodesItemCopyXPathAttributes";
			resources.ApplyResources(this.contextMenuStripNodesItemCopyXPathAttributes, "contextMenuStripNodesItemCopyXPathAttributes");
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
			// 
			// contextMenuStripItemNodesExpandAll
			// 
			this.contextMenuStripItemNodesExpandAll.Image = global::XmlExplorer.Controls.Properties.Resources.ExpandAll;
			this.contextMenuStripItemNodesExpandAll.Name = "contextMenuStripItemNodesExpandAll";
			resources.ApplyResources(this.contextMenuStripItemNodesExpandAll, "contextMenuStripItemNodesExpandAll");
			// 
			// contextMenuStripItemNodesCollapeAll
			// 
			this.contextMenuStripItemNodesCollapeAll.Image = global::XmlExplorer.Controls.Properties.Resources.CollapseAll;
			this.contextMenuStripItemNodesCollapeAll.Name = "contextMenuStripItemNodesCollapeAll";
			resources.ApplyResources(this.contextMenuStripItemNodesCollapeAll, "contextMenuStripItemNodesCollapeAll");
			// 
			// dockPanel
			// 
			this.dockPanel.ActiveAutoHideContent = null;
			resources.ApplyResources(this.dockPanel, "dockPanel");
			this.dockPanel.DockBackColor = System.Drawing.SystemColors.Control;
			this.dockPanel.DockBottomPortion = 0.4D;
			this.dockPanel.Name = "dockPanel";
			dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
			dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
			autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
			tabGradient1.EndColor = System.Drawing.SystemColors.Control;
			tabGradient1.StartColor = System.Drawing.SystemColors.Control;
			tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
			autoHideStripSkin1.TabGradient = tabGradient1;
			dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
			tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
			tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
			tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
			dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
			dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
			dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
			tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
			tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
			tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
			dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
			tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
			tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
			tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
			dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
			tabGradient5.EndColor = System.Drawing.SystemColors.Control;
			tabGradient5.StartColor = System.Drawing.SystemColors.Control;
			tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
			dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
			dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
			dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
			tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
			tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
			tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
			tabGradient7.EndColor = System.Drawing.Color.Transparent;
			tabGradient7.StartColor = System.Drawing.Color.Transparent;
			tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
			dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
			dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
			dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
			this.dockPanel.Skin = dockPanelSkin1;
			// 
			// toolStripSeparator15
			// 
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			resources.ApplyResources(this.toolStripSeparator15, "toolStripSeparator15");
			// 
			// toolStripMenuItemCopyNodeTextBase64
			// 
			this.toolStripMenuItemCopyNodeTextBase64.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
			this.toolStripMenuItemCopyNodeTextBase64.Name = "toolStripMenuItemCopyNodeTextBase64";
			resources.ApplyResources(this.toolStripMenuItemCopyNodeTextBase64, "toolStripMenuItemCopyNodeTextBase64");
			// 
			// contextMenuStripItemCopyNodeTextBase64
			// 
			this.contextMenuStripItemCopyNodeTextBase64.Image = global::XmlExplorer.Controls.Properties.Resources.CopyHS;
			this.contextMenuStripItemCopyNodeTextBase64.Name = "contextMenuStripItemCopyNodeTextBase64";
			resources.ApplyResources(this.contextMenuStripItemCopyNodeTextBase64, "contextMenuStripItemCopyNodeTextBase64");
			// 
			// TabbedXmlExplorerWindow
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.toolStripStandardButtons);
			this.Controls.Add(this.menuStripMain);
			this.Controls.Add(this.statusStrip);
			this.IsMdiContainer = true;
			this.Name = "TabbedXmlExplorerWindow";
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
		private ToolStripMenuItem toolStripMenuItemNewFromClipboard;
		private ToolStripSeparator toolStripSeparator9;
		private ToolStripMenuItem contextMenuStripItemNodesExpandAll;
		private ToolStripMenuItem contextMenuStripItemNodesCollapeAll;
		private ToolStripSeparator toolStripSeparator10;
		private ToolStripMenuItem toolStripMenuItemExpandAll;
		private ToolStripMenuItem toolStripMenuItemCollapseAll;
		private ToolStripStatusLabel toolStripStatusLabelLoadTime;
		private ToolStripSeparator toolStripSeparator11;
		private ToolStripButton toolStripButtonExpandAll;
		private ToolStripButton toolStripButtonCollapseAll;
		private ToolStripButton toolStripButtonNext;
		private ToolStripButton toolStripButtonPrevious;
		private ToolStripLabel toolStripLabelResults;
		private ToolStripMenuItem toolsToolStripMenuItem;
		private ToolStripMenuItem toolStripMenuItemOptions;
		private ToolStripMenuItem toolStripMenuItemOpenUrl;
		private ToolStripButton toolStripButtonOpenUrl;
		private ToolStripSeparator toolStripSeparator12;
		private ToolStripMenuItem importChildNodeDefinitionsToolStripMenuItem;
		private ToolStripMenuItem exportChildNodeDefinitionsToolStripMenuItem;
		private ToolStripMenuItem toolStripMenuItemFileTypes;
		private ToolStripSeparator toolStripSeparator13;
		private ToolStripMenuItem toolStripMenuItemShowNodeToolTips;
		private ToolStripMenuItem contextMenuStripNodesItemCopyXPathAttributes;
		private ToolStripMenuItem toolStripMenuItemCopyAttributesXPath;
		private ToolStripMenuItem toolStripMenuItemCopyNodeValueBase64;
		private ToolStripMenuItem contextMenuStripItemCopyNodeValueBase64;
		private ToolStripSeparator toolStripSeparator14;
		private ToolStripMenuItem toolStripMenuItemCopyNodeTextBase64;
		private ToolStripSeparator toolStripSeparator15;
		private ToolStripMenuItem contextMenuStripItemCopyNodeTextBase64;
	}
}