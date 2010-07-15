namespace XmlExplorer.TreeView
{
	partial class ExpressionResultsWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpressionResultsWindow));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxResult = new System.Windows.Forms.TextBox();
			this.labelExpression = new System.Windows.Forms.Label();
			this.labelResult = new System.Windows.Forms.Label();
			this.textBoxExpression = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.textBoxResult, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelExpression, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.labelResult, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBoxExpression, 1, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// textBoxResult
			// 
			resources.ApplyResources(this.textBoxResult, "textBoxResult");
			this.textBoxResult.Name = "textBoxResult";
			this.textBoxResult.ReadOnly = true;
			// 
			// labelExpression
			// 
			resources.ApplyResources(this.labelExpression, "labelExpression");
			this.labelExpression.Name = "labelExpression";
			// 
			// labelResult
			// 
			resources.ApplyResources(this.labelResult, "labelResult");
			this.labelResult.Name = "labelResult";
			// 
			// textBoxExpression
			// 
			resources.ApplyResources(this.textBoxExpression, "textBoxExpression");
			this.textBoxExpression.Name = "textBoxExpression";
			this.textBoxExpression.ReadOnly = true;
			// 
			// ExpressionResultsWindow
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.KeyPreview = true;
			this.Name = "ExpressionResultsWindow";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox textBoxResult;
		private System.Windows.Forms.Label labelExpression;
		private System.Windows.Forms.Label labelResult;
		private System.Windows.Forms.TextBox textBoxExpression;
	}
}