namespace XmlExplorer
{
    partial class DefaultProgramPrompt
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
			  System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultProgramPrompt));
			  this.label1 = new System.Windows.Forms.Label();
			  this.panel1 = new System.Windows.Forms.Panel();
			  this.buttonYes = new System.Windows.Forms.Button();
			  this.buttonNo = new System.Windows.Forms.Button();
			  this.pictureBox1 = new System.Windows.Forms.PictureBox();
			  this.panel1.SuspendLayout();
			  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			  this.SuspendLayout();
			  // 
			  // label1
			  // 
			  resources.ApplyResources(this.label1, "label1");
			  this.label1.Name = "label1";
			  // 
			  // panel1
			  // 
			  resources.ApplyResources(this.panel1, "panel1");
			  this.panel1.BackColor = System.Drawing.SystemColors.Control;
			  this.panel1.Controls.Add(this.buttonYes);
			  this.panel1.Controls.Add(this.buttonNo);
			  this.panel1.Name = "panel1";
			  // 
			  // buttonYes
			  // 
			  resources.ApplyResources(this.buttonYes, "buttonYes");
			  this.buttonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			  this.buttonYes.Name = "buttonYes";
			  this.buttonYes.UseVisualStyleBackColor = true;
			  this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
			  // 
			  // buttonNo
			  // 
			  resources.ApplyResources(this.buttonNo, "buttonNo");
			  this.buttonNo.DialogResult = System.Windows.Forms.DialogResult.No;
			  this.buttonNo.Name = "buttonNo";
			  this.buttonNo.UseVisualStyleBackColor = true;
			  this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
			  // 
			  // pictureBox1
			  // 
			  resources.ApplyResources(this.pictureBox1, "pictureBox1");
			  this.pictureBox1.Image = global::XmlExplorer.Properties.Resources.XmlExplorer48;
			  this.pictureBox1.Name = "pictureBox1";
			  this.pictureBox1.TabStop = false;
			  // 
			  // DefaultProgramPrompt
			  // 
			  this.AcceptButton = this.buttonYes;
			  resources.ApplyResources(this, "$this");
			  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			  this.BackColor = System.Drawing.SystemColors.Window;
			  this.CancelButton = this.buttonNo;
			  this.ControlBox = false;
			  this.Controls.Add(this.panel1);
			  this.Controls.Add(this.pictureBox1);
			  this.Controls.Add(this.label1);
			  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			  this.Name = "DefaultProgramPrompt";
			  this.ShowInTaskbar = false;
			  this.panel1.ResumeLayout(false);
			  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			  this.ResumeLayout(false);
			  this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonNo;
    }
}