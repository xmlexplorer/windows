using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using WpfControls;

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

			this.textBlockProductAndVersion.Text = "XML Explorer v" + AssemblyInfo.Default.Version.ToString();

			this.runUpdate.Text = "Checking for updates, please wait...";
			this.runUpdateHyperlink.Text = null;

			this.Loaded += new RoutedEventHandler(AboutWindow_Loaded);
		}

		void AboutWindow_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				Release.BeginCheckForNewerRelease(this.OnCheckForUpdateFinished);
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		private void OnCheckForUpdateFinished(object sender, CheckForNewerReleaseCompletedEventArgs e)
		{
			try
			{
				if (e.Cancelled)
				{
					this.runUpdate.Text = "Check for updates was cancelled.";
					return;
				}

				if (e.Error != null)
				{
					this.runUpdate.Text = "Error checking for updates";
					return;
				}

				Version currentVersion = AssemblyInfo.Default.Version;
				if (e.Result == null || e.Result.Version <= currentVersion)
				{
					this.runUpdate.Text = "XML Explorer is up to date";
					this.runUpdateHyperlink.Text = null;
					this.imageCheck.Visibility = System.Windows.Visibility.Visible;
					return;
				}

				this.runUpdate.Text = "An update is available: ";
				this.hyperlinkUpdate.NavigateUri = new Uri(e.Result.Url);
				this.runUpdateHyperlink.Text = e.Result.Version.ToString();
			}
			catch (Exception ex)
			{
				App.HandleException(ex);
			}
		}

		private void Hyperlink_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Hyperlink hyperlink = sender as Hyperlink;
				if (hyperlink == null)
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
