using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace XmlExplorer.Controls
{
	/// <summary>
	/// Interaction logic for SettingsHeader.xaml
	/// </summary>
	public partial class SettingsHeader : UserControl
	{
		public SettingsHeader()
		{
			InitializeComponent();
		}

		public event EventHandler BrowseClicked;

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
	}
}
