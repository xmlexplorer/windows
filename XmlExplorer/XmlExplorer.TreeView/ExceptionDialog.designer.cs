namespace XmlExplorer.TreeView
{
	partial class ExceptionDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionDialog));
			this._buttonOK = new System.Windows.Forms.Button();
			this._tabControl = new System.Windows.Forms.TabControl();
			this._tabPageMessage = new System.Windows.Forms.TabPage();
			this._richTextBox = new System.Windows.Forms.RichTextBox();
			this._tabPageStackTrace = new System.Windows.Forms.TabPage();
			this._textBox = new System.Windows.Forms.TextBox();
			this._tabPageProperties = new System.Windows.Forms.TabPage();
			this._propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.linkLabelSendErrorReport = new System.Windows.Forms.LinkLabel();
			this.buttonCopy = new System.Windows.Forms.Button();
			this._informationPanel = new XmlExplorer.TreeView.InformationPanel();
			this._tabControl.SuspendLayout();
			this._tabPageMessage.SuspendLayout();
			this._tabPageStackTrace.SuspendLayout();
			this._tabPageProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// _buttonOK
			// 
			this._buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._buttonOK.Location = new System.Drawing.Point(385, 291);
			this._buttonOK.Name = "_buttonOK";
			this._buttonOK.Size = new System.Drawing.Size(75, 23);
			this._buttonOK.TabIndex = 28;
			this._buttonOK.Text = "Close";
			this._buttonOK.UseVisualStyleBackColor = true;
			// 
			// _tabControl
			// 
			this._tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._tabControl.Controls.Add(this._tabPageMessage);
			this._tabControl.Controls.Add(this._tabPageStackTrace);
			this._tabControl.Controls.Add(this._tabPageProperties);
			this._tabControl.Location = new System.Drawing.Point(12, 91);
			this._tabControl.Name = "_tabControl";
			this._tabControl.SelectedIndex = 0;
			this._tabControl.Size = new System.Drawing.Size(452, 190);
			this._tabControl.TabIndex = 31;
			// 
			// _tabPageMessage
			// 
			this._tabPageMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this._tabPageMessage.Controls.Add(this._richTextBox);
			this._tabPageMessage.Location = new System.Drawing.Point(4, 22);
			this._tabPageMessage.Name = "_tabPageMessage";
			this._tabPageMessage.Size = new System.Drawing.Size(444, 164);
			this._tabPageMessage.TabIndex = 0;
			this._tabPageMessage.Text = "Message";
			this._tabPageMessage.UseVisualStyleBackColor = true;
			// 
			// _richTextBox
			// 
			this._richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._richTextBox.Location = new System.Drawing.Point(0, 0);
			this._richTextBox.Name = "_richTextBox";
			this._richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this._richTextBox.Size = new System.Drawing.Size(444, 164);
			this._richTextBox.TabIndex = 1;
			this._richTextBox.Text = "";
			// 
			// _tabPageStackTrace
			// 
			this._tabPageStackTrace.Controls.Add(this._textBox);
			this._tabPageStackTrace.Location = new System.Drawing.Point(4, 22);
			this._tabPageStackTrace.Name = "_tabPageStackTrace";
			this._tabPageStackTrace.Size = new System.Drawing.Size(444, 164);
			this._tabPageStackTrace.TabIndex = 2;
			this._tabPageStackTrace.Text = "Stack Trace";
			this._tabPageStackTrace.UseVisualStyleBackColor = true;
			// 
			// _textBox
			// 
			this._textBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._textBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._textBox.HideSelection = false;
			this._textBox.Location = new System.Drawing.Point(0, 0);
			this._textBox.Multiline = true;
			this._textBox.Name = "_textBox";
			this._textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this._textBox.Size = new System.Drawing.Size(444, 164);
			this._textBox.TabIndex = 0;
			this._textBox.WordWrap = false;
			// 
			// _tabPageProperties
			// 
			this._tabPageProperties.Controls.Add(this._propertyGrid);
			this._tabPageProperties.Location = new System.Drawing.Point(4, 22);
			this._tabPageProperties.Name = "_tabPageProperties";
			this._tabPageProperties.Padding = new System.Windows.Forms.Padding(3);
			this._tabPageProperties.Size = new System.Drawing.Size(444, 164);
			this._tabPageProperties.TabIndex = 3;
			this._tabPageProperties.Text = "Properties";
			this._tabPageProperties.UseVisualStyleBackColor = true;
			// 
			// _propertyGrid
			// 
			this._propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this._propertyGrid.Location = new System.Drawing.Point(3, 3);
			this._propertyGrid.Name = "_propertyGrid";
			this._propertyGrid.Size = new System.Drawing.Size(438, 158);
			this._propertyGrid.TabIndex = 1;
			// 
			// linkLabelSendErrorReport
			// 
			this.linkLabelSendErrorReport.AutoSize = true;
			this.linkLabelSendErrorReport.Location = new System.Drawing.Point(93, 296);
			this.linkLabelSendErrorReport.Name = "linkLabelSendErrorReport";
			this.linkLabelSendErrorReport.Size = new System.Drawing.Size(101, 13);
			this.linkLabelSendErrorReport.TabIndex = 32;
			this.linkLabelSendErrorReport.TabStop = true;
			this.linkLabelSendErrorReport.Text = "&Send Error Report...";
			// 
			// buttonCopy
			// 
			this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCopy.Location = new System.Drawing.Point(12, 291);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(75, 23);
			this.buttonCopy.TabIndex = 28;
			this.buttonCopy.Text = "&Copy";
			this.buttonCopy.UseVisualStyleBackColor = true;
			this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
			// 
			// _informationPanel
			// 
			this._informationPanel.BackColor = System.Drawing.SystemColors.Window;
			this._informationPanel.Description = "";
			this._informationPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this._informationPanel.Image = ((System.Drawing.Image)(resources.GetObject("_informationPanel.Image")));
			this._informationPanel.Location = new System.Drawing.Point(0, 0);
			this._informationPanel.Name = "_informationPanel";
			this._informationPanel.Size = new System.Drawing.Size(472, 85);
			this._informationPanel.TabIndex = 26;
			this._informationPanel.Title = "";
			// 
			// ExceptionDialog
			// 
			this.AcceptButton = this._buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._buttonOK;
			this.ClientSize = new System.Drawing.Size(472, 326);
			this.Controls.Add(this.linkLabelSendErrorReport);
			this.Controls.Add(this._tabControl);
			this.Controls.Add(this.buttonCopy);
			this.Controls.Add(this._buttonOK);
			this.Controls.Add(this._informationPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ExceptionDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this._tabControl.ResumeLayout(false);
			this._tabPageMessage.ResumeLayout(false);
			this._tabPageStackTrace.ResumeLayout(false);
			this._tabPageStackTrace.PerformLayout();
			this._tabPageProperties.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _buttonOK;
		protected InformationPanel _informationPanel;
		protected System.Windows.Forms.TabControl _tabControl;
		protected System.Windows.Forms.TabPage _tabPageMessage;
		protected System.Windows.Forms.TabPage _tabPageStackTrace;
		protected System.Windows.Forms.TabPage _tabPageProperties;
		private System.Windows.Forms.PropertyGrid _propertyGrid;
		private System.Windows.Forms.TextBox _textBox;
		private System.Windows.Forms.RichTextBox _richTextBox;
		public System.Windows.Forms.LinkLabel linkLabelSendErrorReport;
		private System.Windows.Forms.Button buttonCopy;
	}
}