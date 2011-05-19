namespace XmlExplorer.Controls
{
	partial class XmlExplorerWindow
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
			this.xmlTreeView = new XmlExplorer.TreeView.XPathNavigatorTreeView();
			this.SuspendLayout();
			// 
			// xmlTreeView
			// 
			this.xmlTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xmlTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.xmlTreeView.HideSelection = false;
			this.xmlTreeView.Location = new System.Drawing.Point(0, 0);
			this.xmlTreeView.Name = "xmlTreeView";
			this.xmlTreeView.Size = new System.Drawing.Size(292, 266);
			this.xmlTreeView.TabIndex = 4;
			this.xmlTreeView.UseSyntaxHighlighting = true;
			// 
			// XmlExplorerWindow
			// 
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.xmlTreeView);
			this.Name = "XmlExplorerWindow";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.ResumeLayout(false);

		}

		#endregion

		private XmlExplorer.TreeView.XPathNavigatorTreeView xmlTreeView;

	}
}