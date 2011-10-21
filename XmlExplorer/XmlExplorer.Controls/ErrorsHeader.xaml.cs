using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using XmlExplorer.TreeView;

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
				ExceptionDialog.ShowDialog(ex);
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
				ExceptionDialog.ShowDialog(ex);
			}
		}
	}
}
