namespace XmlExplorer.Controls
{
    partial class ValidationWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValidationWindow));
            this.listViewExpressions = new System.Windows.Forms.ListView();
            this.columnHeaderDescription = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLine = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColumn = new System.Windows.Forms.ColumnHeader();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonValidate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSpringTextBoxSchema = new XmlExplorer.Controls.ToolStripSpringTextBox();
            this.toolStripButtonBrowseForSchema = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewExpressions
            // 
            this.listViewExpressions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDescription,
            this.columnHeaderLine,
            this.columnHeaderColumn});
            this.listViewExpressions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewExpressions.FullRowSelect = true;
            this.listViewExpressions.HideSelection = false;
            this.listViewExpressions.LabelEdit = true;
            this.listViewExpressions.Location = new System.Drawing.Point(0, 25);
            this.listViewExpressions.Name = "listViewExpressions";
            this.listViewExpressions.Size = new System.Drawing.Size(741, 423);
            this.listViewExpressions.TabIndex = 4;
            this.listViewExpressions.UseCompatibleStateImageBehavior = false;
            this.listViewExpressions.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderDescription
            // 
            this.columnHeaderDescription.Text = "Description";
            this.columnHeaderDescription.Width = 533;
            // 
            // columnHeaderLine
            // 
            this.columnHeaderLine.Text = "Line";
            this.columnHeaderLine.Width = 79;
            // 
            // columnHeaderColumn
            // 
            this.columnHeaderColumn.Text = "Column";
            this.columnHeaderColumn.Width = 74;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonValidate,
            this.toolStripSpringTextBoxSchema,
            this.toolStripButtonBrowseForSchema});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(741, 25);
            this.toolStrip.TabIndex = 5;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonValidate
            // 
            this.toolStripButtonValidate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonValidate.Image = global::XmlExplorer.Controls.Properties.Resources.CheckGrammarHS;
            this.toolStripButtonValidate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonValidate.Name = "toolStripButtonValidate";
            this.toolStripButtonValidate.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonValidate.Text = "Validate";
            // 
            // toolStripSpringTextBoxSchema
            // 
            this.toolStripSpringTextBoxSchema.Name = "toolStripSpringTextBoxSchema";
            this.toolStripSpringTextBoxSchema.Size = new System.Drawing.Size(652, 25);
            // 
            // toolStripButtonBrowseForSchema
            // 
            this.toolStripButtonBrowseForSchema.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonBrowseForSchema.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBrowseForSchema.Image")));
            this.toolStripButtonBrowseForSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBrowseForSchema.Name = "toolStripButtonBrowseForSchema";
            this.toolStripButtonBrowseForSchema.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonBrowseForSchema.Text = "...";
            // 
            // ValidationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 448);
            this.CloseButton = false;
            this.Controls.Add(this.listViewExpressions);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ValidationWindow";
            this.TabText = "Validation";
            this.Text = "Validation";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewExpressions;
        private System.Windows.Forms.ColumnHeader columnHeaderDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderLine;
        private System.Windows.Forms.ColumnHeader columnHeaderColumn;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonValidate;
        private ToolStripSpringTextBox toolStripSpringTextBoxSchema;
        private System.Windows.Forms.ToolStripButton toolStripButtonBrowseForSchema;
    }
}