using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace XmlExplorer.Controls
{
    public class XPathExpressionListViewItem : ListViewItem
    {
        public XPathExpression XPathExpression { get; set; }

        public XPathExpressionListViewItem()
            : base()
        {
        }

        public XPathExpressionListViewItem(XPathExpression xpathExpression)
            : base()
        {
            this.XPathExpression = xpathExpression;

            this.Initialize();
        }

        public void Initialize()
        {
            string name = null;
            string expression = null;
            string comments = null;

            if (this.XPathExpression != null)
            {
                name = this.XPathExpression.Name;
                expression = this.XPathExpression.Expression;
                comments = this.XPathExpression.Comments;
            }

            this.Text = name;

            if (this.SubItems.Count > 1)
                this.SubItems[1].Text = expression;
            else
                this.SubItems.Add(expression);

            if (this.SubItems.Count > 2)
                this.SubItems[2].Text = comments;
            else
                this.SubItems.Add(comments);
        }
    }
}
