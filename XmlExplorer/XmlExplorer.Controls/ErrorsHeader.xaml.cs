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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace XmlExplorer.Controls
{
	/// <summary>
	/// Interaction logic for ErrorsHeader.xaml
	/// </summary>
	public partial class ErrorsHeader : UserControl
	{
		public event EventHandler BrowseClicked;

		public ErrorsHeader()
		{
			InitializeComponent();
		}

		private void HyperlinkBrowse_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.BrowseClicked != null)
					this.BrowseClicked(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(ex.ToString());
			}
		}

		private void XmlSchemaLink_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Process.Start("http://www.w3schools.com/Schema");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(ex.ToString());
			}
		}
	}
}
