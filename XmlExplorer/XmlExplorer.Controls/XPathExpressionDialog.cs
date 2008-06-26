using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XmlExplorer.Controls
{
    public partial class XPathExpressionDialog : Form
    {
        private XPathExpression _xpathExpression;

        public XPathExpressionDialog()
        {
            InitializeComponent();
        }

        public XPathExpressionDialog(XPathExpression expression)
            : this()
        {
            this.XPathExpression = expression;
        }

        public XPathExpression XPathExpression
        {
            get
            {
                return _xpathExpression;
            }
            set
            {
                _xpathExpression = value;
                this.textBoxName.Text = value.Name;
                this.textBoxExpression.Text = value.Expression;
                this.textBoxComments.Text = value.Comments;
            }
        }

        public string Comments
        {
            get
            {
                return this.textBoxComments.Text;
            }
            set
            {
                this.textBoxComments.Text = value;
            }
        }

        public string ExpressionName
        {
            get
            {
                return this.textBoxName.Text;
            }
            set
            {
                this.textBoxName.Text = value;
            }
        }

        public string Expression
        {
            get
            {
                return this.textBoxExpression.Text;
            }
            set
            {
                this.textBoxExpression.Text = value;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (this.DialogResult == DialogResult.OK)
            {
                if (this.XPathExpression != null)
                {
                    this.XPathExpression.Name = this.ExpressionName;
                    this.XPathExpression.Expression = this.Expression;
                    this.XPathExpression.Comments = this.Comments;
                }
            }
        }
    }
}
