namespace XmlExplorer.Controls
{
    partial class XPathExpressionDialog
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
			  System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPathExpressionDialog));
			  this.buttonOK = new System.Windows.Forms.Button();
			  this.buttonCancel = new System.Windows.Forms.Button();
			  this.label1 = new System.Windows.Forms.Label();
			  this.label2 = new System.Windows.Forms.Label();
			  this.textBoxName = new System.Windows.Forms.TextBox();
			  this.textBoxExpression = new System.Windows.Forms.TextBox();
			  this.textBoxComments = new System.Windows.Forms.TextBox();
			  this.label3 = new System.Windows.Forms.Label();
			  this.SuspendLayout();
			  // 
			  // buttonOK
			  // 
			  resources.ApplyResources(this.buttonOK, "buttonOK");
			  this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			  this.buttonOK.Name = "buttonOK";
			  this.buttonOK.UseVisualStyleBackColor = true;
			  // 
			  // buttonCancel
			  // 
			  resources.ApplyResources(this.buttonCancel, "buttonCancel");
			  this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			  this.buttonCancel.Name = "buttonCancel";
			  this.buttonCancel.UseVisualStyleBackColor = true;
			  // 
			  // label1
			  // 
			  resources.ApplyResources(this.label1, "label1");
			  this.label1.Name = "label1";
			  // 
			  // label2
			  // 
			  resources.ApplyResources(this.label2, "label2");
			  this.label2.Name = "label2";
			  // 
			  // textBoxName
			  // 
			  resources.ApplyResources(this.textBoxName, "textBoxName");
			  this.textBoxName.Name = "textBoxName";
			  // 
			  // textBoxExpression
			  // 
			  resources.ApplyResources(this.textBoxExpression, "textBoxExpression");
			  this.textBoxExpression.Name = "textBoxExpression";
			  // 
			  // textBoxComments
			  // 
			  resources.ApplyResources(this.textBoxComments, "textBoxComments");
			  this.textBoxComments.Name = "textBoxComments";
			  // 
			  // label3
			  // 
			  resources.ApplyResources(this.label3, "label3");
			  this.label3.Name = "label3";
			  // 
			  // XPathExpressionDialog
			  // 
			  this.AcceptButton = this.buttonOK;
			  resources.ApplyResources(this, "$this");
			  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			  this.CancelButton = this.buttonCancel;
			  this.Controls.Add(this.textBoxComments);
			  this.Controls.Add(this.label3);
			  this.Controls.Add(this.textBoxExpression);
			  this.Controls.Add(this.textBoxName);
			  this.Controls.Add(this.label2);
			  this.Controls.Add(this.label1);
			  this.Controls.Add(this.buttonCancel);
			  this.Controls.Add(this.buttonOK);
			  this.Name = "XPathExpressionDialog";
			  this.ResumeLayout(false);
			  this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxExpression;
        private System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.Label label3;
    }
}