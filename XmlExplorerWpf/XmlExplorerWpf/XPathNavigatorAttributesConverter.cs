using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Xml.XPath;

namespace XmlExplorer
{
	public class XPathNavigatorAttributesConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			XPathNavigator navigator;

			XPathNavigatorView view = value as XPathNavigatorView;

			if (view != null)
				navigator = view.XPathNavigator;
			else
				navigator = value as XPathNavigator;

			if (navigator == null)
				return null;

			if (!navigator.HasAttributes)
				return null;

			List<XPathNavigator> attributes = new List<XPathNavigator>();

			// clone the node's navigator (cursor), so it doesn't lose it's position
			XPathNavigator attributeNavigator = navigator.Clone();
			if (attributeNavigator.MoveToFirstAttribute())
			{
				do
				{
					attributes.Add(attributeNavigator.Clone());
				}
				while (attributeNavigator.MoveToNextAttribute());
			}

			return attributes;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
