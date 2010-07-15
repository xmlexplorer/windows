namespace XmlExplorer.Controls
{
    partial class NamespacesWindow
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
			  System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NamespacesWindow));
			  this.listView = new System.Windows.Forms.ListView();
			  this.columnHeaderPrefix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeaderNamespace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.imageList = new System.Windows.Forms.ImageList(this.components);
			  this.label1 = new System.Windows.Forms.Label();
			  this.SuspendLayout();
			  // 
			  // listView
			  // 
			  resources.ApplyResources(this.listView, "listView");
			  this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPrefix,
            this.columnHeaderNamespace});
			  this.listView.FullRowSelect = true;
			  this.listView.HideSelection = false;
			  this.listView.MultiSelect = false;
			  this.listView.Name = "listView";
			  this.listView.SmallImageList = this.imageList;
			  this.listView.UseCompatibleStateImageBehavior = false;
			  this.listView.View = System.Windows.Forms.View.Details;
			  // 
			  // columnHeaderPrefix
			  // 
			  resources.ApplyResources(this.columnHeaderPrefix, "columnHeaderPrefix");
			  // 
			  // columnHeaderNamespace
			  // 
			  resources.ApplyResources(this.columnHeaderNamespace, "columnHeaderNamespace");
			  // 
			  // imageList
			  // 
			  this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			  this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			  this.imageList.Images.SetKeyName(0, "Error");
			  this.imageList.Images.SetKeyName(1, "Warning");
			  // 
			  // label1
			  // 
			  resources.ApplyResources(this.label1, "label1");
			  this.label1.AutoEllipsis = true;
			  this.label1.Name = "label1";
			  // 
			  // NamespacesWindow
			  // 
			  resources.ApplyResources(this, "$this");
			  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			  this.CloseButton = false;
			  this.CloseButtonVisible = false;
			  this.Controls.Add(this.listView);
			  this.Controls.Add(this.label1);
			  this.Name = "NamespacesWindow";
			  this.TabText = "Namespaces";
			  this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeaderNamespace;
        private System.Windows.Forms.ColumnHeader columnHeaderPrefix;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label1;
    }
}