namespace XmlExplorer.Controls
{
	partial class SettingsWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.elementHost = new System.Windows.Forms.Integration.ElementHost();
			this.SuspendLayout();
			// 
			// propertyGrid
			// 
			this.propertyGrid.CommandsVisibleIfAvailable = false;
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.HelpVisible = false;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.propertyGrid.Size = new System.Drawing.Size(213, 341);
			this.propertyGrid.TabIndex = 0;
			this.propertyGrid.ToolbarVisible = false;
			// 
			// elementHost
			// 
			this.elementHost.AutoSize = true;
			this.elementHost.BackColor = System.Drawing.SystemColors.Window;
			this.elementHost.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.elementHost.Location = new System.Drawing.Point(0, 340);
			this.elementHost.Name = "elementHost";
			this.elementHost.Size = new System.Drawing.Size(213, 1);
			this.elementHost.TabIndex = 1;
			this.elementHost.Text = "elementHost1";
			this.elementHost.Child = null;
			// 
			// SettingsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(213, 341);
			this.Controls.Add(this.elementHost);
			this.Controls.Add(this.propertyGrid);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SettingsWindow";
			this.TabText = "Settings";
			this.Text = "Settings";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.Integration.ElementHost elementHost;
	}
}