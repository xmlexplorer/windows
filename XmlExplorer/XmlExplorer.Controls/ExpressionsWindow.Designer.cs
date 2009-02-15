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
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderExpression = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderComments = new System.Windows.Forms.ColumnHeader();
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
            this.panelExpressions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExpressions.Location = new System.Drawing.Point(0, 0);
            this.panelExpressions.MinimumSize = new System.Drawing.Size(367, 0);
            this.panelExpressions.Name = "panelExpressions";
            this.panelExpressions.Size = new System.Drawing.Size(741, 448);
            this.panelExpressions.TabIndex = 4;
            // 
            // pictureBoxSearch
            // 
            this.pictureBoxSearch.Image = global::XmlExplorer.Controls.Properties.Resources.ZoomHS;
            this.pictureBoxSearch.Location = new System.Drawing.Point(3, 6);
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
            this.listViewExpressions.Size = new System.Drawing.Size(735, 413);
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
            this.textBoxSearchExpressions.Location = new System.Drawing.Point(25, 6);
            this.textBoxSearchExpressions.Name = "textBoxSearchExpressions";
            this.textBoxSearchExpressions.Size = new System.Drawing.Size(713, 20);
            this.textBoxSearchExpressions.TabIndex = 2;
            // 
            // ExpressionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 448);
            this.CloseButton = false;
            this.Controls.Add(this.panelExpressions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExpressionsWindow";
            this.TabText = "Expressions";
            this.Text = "Expressions";
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