using System.Windows;
using System.Windows.Controls;
using System.Xml.XPath;

namespace XmlExplorer
{
	public class XPathNodeTypeDataTemplateSelector : DataTemplateSelector
	{
		public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
		{
			XPathNavigatorView view = item as XPathNavigatorView;

			if (view != null)
			{
				XPathNavigator navigator = view.XPathNavigator;

				if (navigator != null)
				{
					switch (navigator.NodeType)
					{
						case XPathNodeType.Root:
							return App.Current.FindResource("xmlDeclarationXmlNodeTemplate") as DataTemplate;

						case XPathNodeType.ProcessingInstruction:
							return App.Current.FindResource("processingInstructionXPathNavigatorTemplate") as DataTemplate;

						case XPathNodeType.Comment:
							return App.Current.FindResource("commentXPathNavigatorTemplate") as DataTemplate;

						case XPathNodeType.Element:
							return App.Current.FindResource("elementXPathNavigatorTemplate") as DataTemplate;

						case XPathNodeType.Text:
							return App.Current.FindResource("textXPathNavigatorTemplate") as DataTemplate;

						case XPathNodeType.Attribute:
							return App.Current.FindResource("attributeXPathNavigatorTemplate") as DataTemplate;
					}
				}
			}

			return base.SelectTemplate(item, container);
		}
	}
}
