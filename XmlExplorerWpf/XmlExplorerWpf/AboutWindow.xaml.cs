using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace XmlExplorer
{
	/// <summary>
	/// Interaction logic for AboutWindow.xaml
	/// </summary>
	public partial class AboutWindow : Window
	{
		public AboutWindow()
		{
			InitializeComponent();

			this.textBlockProductAndVersion.Text = "XML Explorer v" + App.Version.ToString();
		}

		private void Hyperlink_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Hyperlink hyperlink = sender as Hyperlink;
				if(hyperlink == null)
					return;

				Process.Start(hyperlink.NavigateUri.ToString());
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}
	}
}
