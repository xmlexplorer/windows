namespace XmlExplorer.Controls
{
    partial class ExpressionsWindow
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
			  System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpressionsWindow));
			  this.panelExpressions = new System.Windows.Forms.Panel();
			  this.listViewExpressions = new System.Windows.Forms.ListView();
			  this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeaderExpression = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeaderComments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			  this.toolStripButtonLaunchXpath = new System.Windows.Forms.ToolStripButton();
			  this.toolStripButtonEditExpressions = new System.Windows.Forms.ToolStripButton();
			  this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
			  this.toolStripLabelFilter = new System.Windows.Forms.ToolStripLabel();
			  this.toolStripSpringTextBoxFilter = new XmlExplorer.Controls.ToolStripSpringTextBox();
			  this.panelExpressions.SuspendLayout();
			  this.toolStrip1.SuspendLayout();
			  this.SuspendLayout();
			  // 
			  // panelExpressions
			  // 
			  this.panelExpressions.Controls.Add(this.listViewExpressions);
			  this.panelExpressions.Controls.Add(this.toolStrip1);
			  resources.ApplyResources(this.panelExpressions, "panelExpressions");
			  this.panelExpressions.MinimumSize = new System.Drawing.Size(367, 0);
			  this.panelExpressions.Name = "panelExpressions";
			  // 
			  // listViewExpressions
			  // 
			  this.listViewExpressions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderExpression,
            this.columnHeaderComments});
			  resources.ApplyResources(this.listViewExpressions, "listViewExpressions");
			  this.listViewExpressions.FullRowSelect = true;
			  this.listViewExpressions.HideSelection = false;
			  this.listViewExpressions.LabelEdit = true;
			  this.listViewExpressions.Name = "listViewExpressions";
			  this.listViewExpressions.UseCompatibleStateImageBehavior = false;
			  this.listViewExpressions.View = System.Windows.Forms.View.Details;
			  // 
			  // columnHeaderName
			  // 
			  resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
			  // 
			  // columnHeaderExpression
			  // 
			  resources.ApplyResources(this.columnHeaderExpression, "columnHeaderExpression");
			  // 
			  // columnHeaderComments
			  // 
			  resources.ApplyResources(this.columnHeaderComments, "columnHeaderComments");
			  // 
			  // toolStrip1
			  // 
			  this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLaunchXpath,
            this.toolStripButtonEditExpressions,
            this.toolStripButtonDelete,
            this.toolStripLabelFilter,
            this.toolStripSpringTextBoxFilter});
			  resources.ApplyResources(this.toolStrip1, "toolStrip1");
			  this.toolStrip1.Name = "toolStrip1";
			  // 
			  // toolStripButtonLaunchXpath
			  // 
			  resources.ApplyResources(this.toolStripButtonLaunchXpath, "toolStripButtonLaunchXpath");
			  this.toolStripButtonLaunchXpath.Name = "toolStripButtonLaunchXpath";
			  this.toolStripButtonLaunchXpath.Click += new System.EventHandler(this.toolStripButtonLaunchXpath_Click);
			  // 
			  // toolStripButtonEditExpressions
			  // 
			  this.toolStripButtonEditExpressions.Image = global::XmlExplorer.Controls.Properties.Resources.EditTableHS;
			  resources.ApplyResources(this.toolStripButtonEditExpressions, "toolStripButtonEditExpressions");
			  this.toolStripButtonEditExpressions.Name = "toolStripButtonEditExpressions";
			  this.toolStripButtonEditExpressions.Click += new System.EventHandler(this.toolStripButtonEditExpressions_Click);
			  // 
			  // toolStripButtonDelete
			  // 
			  this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			  this.toolStripButtonDelete.Image = global::XmlExplorer.Controls.Properties.Resources.DeleteHS;
			  resources.ApplyResources(this.toolStripButtonDelete, "toolStripButtonDelete");
			  this.toolStripButtonDelete.Name = "toolStripButtonDelete";
			  this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
			  // 
			  // toolStripLabelFilter
			  // 
			  this.toolStripLabelFilter.Image = global::XmlExplorer.Controls.Properties.Resources.ZoomHS;
			  this.toolStripLabelFilter.Name = "toolStripLabelFilter";
			  resources.ApplyResources(this.toolStripLabelFilter, "toolStripLabelFilter");
			  // 
			  // toolStripSpringTextBoxFilter
			  // 
			  this.toolStripSpringTextBoxFilter.Name = "toolStripSpringTextBoxFilter";
			  resources.ApplyResources(this.toolStripSpringTextBoxFilter, "toolStripSpringTextBoxFilter");
			  // 
			  // ExpressionsWindow
			  // 
			  resources.ApplyResources(this, "$this");
			  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			  this.CloseButton = false;
			  this.CloseButtonVisible = false;
			  this.Controls.Add(this.panelExpressions);
			  this.Name = "ExpressionsWindow";
			  this.TabText = "Expressions";
			  this.panelExpressions.ResumeLayout(false);
			  this.panelExpressions.PerformLayout();
			  this.toolStrip1.ResumeLayout(false);
			  this.toolStrip1.PerformLayout();
			  this.ResumeLayout(false);

        }

        #endregion

		  private System.Windows.Forms.Panel panelExpressions;
        private System.Windows.Forms.ListView listViewExpressions;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderExpression;
		  private System.Windows.Forms.ColumnHeader columnHeaderComments;
		  private System.Windows.Forms.ToolStrip toolStrip1;
		  private System.Windows.Forms.ToolStripButton toolStripButtonEditExpressions;
		  private System.Windows.Forms.ToolStripLabel toolStripLabelFilter;
		  private ToolStripSpringTextBox toolStripSpringTextBoxFilter;
		  private System.Windows.Forms.ToolStripButton toolStripButtonLaunchXpath;
		  private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
    }
}