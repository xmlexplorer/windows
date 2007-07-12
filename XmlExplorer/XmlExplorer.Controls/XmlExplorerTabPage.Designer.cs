using System.Windows.Forms;
using System.Drawing;
namespace XmlExplorer.Controls
{
    partial class XmlExplorerTabPage
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
            this.xmlTreeView = new XPathNavigatorTreeView();
            base.SuspendLayout();
            this.xmlTreeView.Dock = DockStyle.Fill;
            this.xmlTreeView.HideSelection = false;
            this.xmlTreeView.LineColor = Color.Empty;
            this.xmlTreeView.Location = new Point(0, 0);
            this.xmlTreeView.Name = "xmlTreeView";
            this.xmlTreeView.Size = new Size(0x79, 0x61);
            this.xmlTreeView.TabIndex = 0;
            this.xmlTreeView.Navigator = null;
            base.Controls.Add(this.xmlTreeView);
            base.ResumeLayout(false);
        }

        #endregion

        private XPathNavigatorTreeView xmlTreeView;
    }
}