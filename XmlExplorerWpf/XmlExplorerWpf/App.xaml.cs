using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Reflection;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace XmlExplorer
{
	public class EntryPoint
	{
		[STAThread]
		public static void Main(string[] args)
		{
			SingleInstanceManager manager = new SingleInstanceManager();
			manager.Run(args);
		}
	}

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(System.Windows.StartupEventArgs e)
		{
			try
			{
				base.OnStartup(e);

				XmlExplorer.Properties.Settings settings = XmlExplorer.Properties.Settings.Default;

				/* 
				 * according to this article: http://blogs.msdn.com/rprabhu/articles/433979.aspx
				 * manual upgrade of settings is required, but only should only be done when the version number changes
				*/
				if (settings.UpgradeNeeded)
				{
					settings.Upgrade();
					settings.UpgradeNeeded = false;
					settings.Save();
				}

				Application.Current.Resources.MergedDictionaries.Add(
							Application.LoadComponent(
								new Uri("XmlExplorer;component/XPathNavigatorTemplates.xaml",
								UriKind.Relative)) as ResourceDictionary);

				MainWindow window = new MainWindow();

				window.Show();

				foreach (string arg in e.Args)
				{
					window.Open(new FileInfo(arg));
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);

				this.Shutdown(1);
			}
		}

		public void Activate(IEnumerable<string> commandLine)
		{
			// Reactivate application's main window
			this.MainWindow.Activate();
			MainWindow mainWindow = this.MainWindow as MainWindow;
			if (mainWindow == null)
				return;

			mainWindow.Open(commandLine);
		}

		public static void HandleException(Exception ex)
		{
			Debug.WriteLine(ex);
			MessageBox.Show(ex.ToString());
		}
	}

	// Using VB bits to detect single instances and process accordingly:
	//  * OnStartup is fired when the first instance loads
	//  * OnStartupNextInstance is fired when the application is re-run again
	//    NOTE: it is redirected to this instance thanks to IsSingleInstance
	public class SingleInstanceManager : WindowsFormsApplicationBase
	{
		public SingleInstanceManager()
		{
			this.IsSingleInstance = true;
		}

		protected override bool OnStartup(Microsoft.VisualBasic.ApplicationServices.StartupEventArgs e)
		{
			// First time app is launched
			new App();
			App.Current.Run();
			return false;
		}

		protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
		{
			// Subsequent launches
			base.OnStartupNextInstance(eventArgs);
			App app = Application.Current as App;
			app.Activate(eventArgs.CommandLine);
		}
	}
}
