using System;
using System.Collections.Generic;
using System.Text;

namespace XmlExplorer.Controls
{
    public class XPathExpressionLibrary : List<XPathExpression>
    {
        public bool Contains(string xpathExpression)
        {
            return this.Find(xpathExpression) != null;
        }

        public XPathExpression Find(string xpathExpression)
        {
            foreach (XPathExpression item in this)
                if (item.Expression == xpathExpression)
                    return item;

            return null;
        }
    }
}
