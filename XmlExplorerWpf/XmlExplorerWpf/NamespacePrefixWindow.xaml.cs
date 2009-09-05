using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace XmlExplorer
{
	/// <summary>
	/// Interaction logic for NamespacePrefixWindow.xaml
	/// </summary>
	public partial class NamespacePrefixWindow : Window
	{
		public NamespacePrefixWindow()
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
