using System;
using System.Collections.Generic;
using System.Text;

namespace XmlExplorer.Controls
{
	public class XPathExpression
	{
		public string Name { get; set; }
		public string Expression { get; set; }
		public string Comments { get; set; }

		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name))
				return this.Name;

			if (!string.IsNullOrEmpty(Expression))
				return this.Expression;

			return base.ToString();
		}
	}
}
