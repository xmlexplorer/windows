namespace XmlExplorer.Controls
{
	partial class CertificateErrorWindow
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
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonIgnoreErrors = new System.Windows.Forms.Button();
			this.buttonViewCertificate = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(35, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(272, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "There is a problem with this website\'s security certificate.";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::XmlExplorer.Controls.Properties.Resources.Error16;
			this.pictureBox1.Location = new System.Drawing.Point(13, 13);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(240, 58);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonIgnoreErrors
			// 
			this.buttonIgnoreErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonIgnoreErrors.Location = new System.Drawing.Point(159, 58);
			this.buttonIgnoreErrors.MinimumSize = new System.Drawing.Size(75, 23);
			this.buttonIgnoreErrors.Name = "buttonIgnoreErrors";
			this.buttonIgnoreErrors.Size = new System.Drawing.Size(75, 23);
			this.buttonIgnoreErrors.TabIndex = 2;
			this.buttonIgnoreErrors.Text = "Ignore Errors";
			this.buttonIgnoreErrors.UseVisualStyleBackColor = true;
			this.buttonIgnoreErrors.Click += new System.EventHandler(this.buttonIgnoreErrors_Click);
			// 
			// buttonViewCertificate
			// 
			this.buttonViewCertificate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonViewCertificate.AutoSize = true;
			this.buttonViewCertificate.Location = new System.Drawing.Point(63, 58);
			this.buttonViewCertificate.Name = "buttonViewCertificate";
			this.buttonViewCertificate.Size = new System.Drawing.Size(90, 23);
			this.buttonViewCertificate.TabIndex = 1;
			this.buttonViewCertificate.Text = "&View Certificate";
			this.buttonViewCertificate.UseVisualStyleBackColor = true;
			this.buttonViewCertificate.Click += new System.EventHandler(this.buttonViewCertificate_Click);
			// 
			// CertificateErrorWindow
			// 
			this.AcceptButton = this.buttonIgnoreErrors;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(327, 93);
			this.Controls.Add(this.buttonViewCertificate);
			this.Controls.Add(this.buttonIgnoreErrors);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CertificateErrorWindow";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Certificate Error";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonIgnoreErrors;
		private System.Windows.Forms.Button buttonViewCertificate;

	}
}