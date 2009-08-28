using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Reflection;

namespace XmlExplorer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		static Version _version;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                System.Windows.Forms.Application.EnableVisualStyles();

				MainWindow window = new MainWindow();

                foreach (string arg in e.Args)
                {
                    window.Open(new FileInfo(arg));
                }

                window.Show();
            }
            catch (Exception ex)
            {
				HandleException(ex);

                this.Shutdown(1);
            }
        }

		public static void HandleException(Exception ex)
		{
			Debug.WriteLine(ex);
			MessageBox.Show(ex.ToString());
		}

		public static Version Version
		{
			get
			{
				if (_version == null)
				{
					_version = Assembly.GetEntryAssembly().GetName().Version;
				}

				return _version;
			}
		}
    }
}
