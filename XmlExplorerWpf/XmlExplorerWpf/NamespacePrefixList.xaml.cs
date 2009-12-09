using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace XmlExplorer
{
	public partial class NamespacePrefixList : UserControl
	{
		public NamespacePrefixList()
		{
			InitializeComponent();
		}

		public ObservableCollection<NamespaceDefinition> NamespaceDefinitions
		{
			get
			{
				return this.DataContext as ObservableCollection<NamespaceDefinition>;
			}

			set
			{
				this.DataContext = value;
			}
		}
	}
}
