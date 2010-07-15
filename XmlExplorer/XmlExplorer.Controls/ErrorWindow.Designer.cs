namespace XmlExplorer.Controls
{
    partial class ErrorWindow
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
			  System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorWindow));
			  this.listView = new System.Windows.Forms.ListView();
			  this.columnHeaderDefaultOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeaderFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.imageList = new System.Windows.Forms.ImageList(this.components);
			  this.SuspendLayout();
			  // 
			  // listView
			  // 
			  this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDefaultOrder,
            this.columnHeaderDescription,
            this.columnHeaderFile});
			  resources.ApplyResources(this.listView, "listView");
			  this.listView.FullRowSelect = true;
			  this.listView.HideSelection = false;
			  this.listView.MultiSelect = false;
			  this.listView.Name = "listView";
			  this.listView.SmallImageList = this.imageList;
			  this.listView.UseCompatibleStateImageBehavior = false;
			  this.listView.View = System.Windows.Forms.View.Details;
			  // 
			  // columnHeaderDefaultOrder
			  // 
			  resources.ApplyResources(this.columnHeaderDefaultOrder, "columnHeaderDefaultOrder");
			  // 
			  // columnHeaderDescription
			  // 
			  resources.ApplyResources(this.columnHeaderDescription, "columnHeaderDescription");
			  // 
			  // columnHeaderFile
			  // 
			  resources.ApplyResources(this.columnHeaderFile, "columnHeaderFile");
			  // 
			  // imageList
			  // 
			  this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			  this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			  this.imageList.Images.SetKeyName(0, "Error");
			  this.imageList.Images.SetKeyName(1, "Warning");
			  // 
			  // ErrorWindow
			  // 
			  resources.ApplyResources(this, "$this");
			  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			  this.CloseButton = false;
			  this.CloseButtonVisible = false;
			  this.Controls.Add(this.listView);
			  this.Name = "ErrorWindow";
			  this.TabText = "Errors";
			  this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeaderDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderDefaultOrder;
        private System.Windows.Forms.ColumnHeader columnHeaderFile;
        private System.Windows.Forms.ImageList imageList;
    }
}