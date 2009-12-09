using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Xml.XPath;

namespace XmlExplorer
{
	public class XPathNavigatorItemsConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			List<XPathNavigatorView> childNavigatorViews;

			XPathNodeIterator iterator = value as XPathNodeIterator;
			if (iterator != null)
			{
				childNavigatorViews = new List<XPathNavigatorView>();

				foreach (XPathNavigator childNavigator in iterator)
				{
					childNavigatorViews.Add(new XPathNavigatorView(childNavigator));
				}

				return childNavigatorViews;
			}

			XPathNavigator navigator;

			XPathNavigatorView view = value as XPathNavigatorView;

			if (view != null)
				navigator = view.XPathNavigator;
			else
				navigator = value as XPathNavigator;

			if (navigator == null)
				return null;

			childNavigatorViews = new List<XPathNavigatorView>();

			foreach (XPathNavigator childNavigator in navigator.SelectChildren(XPathNodeType.All))
			{
				childNavigatorViews.Add(new XPathNavigatorView(childNavigator));
			}

			return childNavigatorViews;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
