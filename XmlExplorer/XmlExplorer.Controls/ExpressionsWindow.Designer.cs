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
			  this.pictureBoxSearch = new System.Windows.Forms.PictureBox();
			  this.listViewExpressions = new System.Windows.Forms.ListView();
			  this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeaderExpression = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeaderComments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.textBoxSearchExpressions = new System.Windows.Forms.TextBox();
			  this.panelExpressions.SuspendLayout();
			  ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSearch)).BeginInit();
			  this.SuspendLayout();
			  // 
			  // panelExpressions
			  // 
			  this.panelExpressions.Controls.Add(this.pictureBoxSearch);
			  this.panelExpressions.Controls.Add(this.listViewExpressions);
			  this.panelExpressions.Controls.Add(this.textBoxSearchExpressions);
			  resources.ApplyResources(this.panelExpressions, "panelExpressions");
			  this.panelExpressions.MinimumSize = new System.Drawing.Size(367, 0);
			  this.panelExpressions.Name = "panelExpressions";
			  // 
			  // pictureBoxSearch
			  // 
			  this.pictureBoxSearch.Image = global::XmlExplorer.Controls.Properties.Resources.ZoomHS;
			  resources.ApplyResources(this.pictureBoxSearch, "pictureBoxSearch");
			  this.pictureBoxSearch.Name = "pictureBoxSearch";
			  this.pictureBoxSearch.TabStop = false;
			  // 
			  // listViewExpressions
			  // 
			  resources.ApplyResources(this.listViewExpressions, "listViewExpressions");
			  this.listViewExpressions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderExpression,
            this.columnHeaderComments});
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
			  // textBoxSearchExpressions
			  // 
			  resources.ApplyResources(this.textBoxSearchExpressions, "textBoxSearchExpressions");
			  this.textBoxSearchExpressions.Name = "textBoxSearchExpressions";
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
			  ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSearch)).EndInit();
			  this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelExpressions;
        private System.Windows.Forms.PictureBox pictureBoxSearch;
        private System.Windows.Forms.ListView listViewExpressions;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderExpression;
        private System.Windows.Forms.ColumnHeader columnHeaderComments;
        private System.Windows.Forms.TextBox textBoxSearchExpressions;
    }
}