using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using XmlExplorer.Controls;
using XmlExplorer.TreeView;

namespace XmlExplorer
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				// initialize the single instance application
				SingleInstanceApplication application = SingleInstanceApplication.Instance;

				application.Startup += Program.OnStartup;
				application.StartupNextInstance += Program.OnStartupNextInstance;
				application.Shutdown += Program.OnShutdown;
				application.UnhandledException += Program.OnUnhandledException;

				application.Run(args);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				ExceptionDialog.ShowDialog(ex);
			}
		}

		/// <summary>
		/// Applies any saved settings to a TabbedXmlExplorerWindow.
		/// </summary>
		private static void ReadOptions(TabbedXmlExplorerWindow window)
		{
			try
			{
				window.Settings = Properties.Settings.Default;

				// AutoCompleteMode
				window.AutoCompleteMode = Properties.Settings.Default.AutoCompleteMode;

				window.TreeFont = Properties.Settings.Default.Font;
				window.TreeForeColor = Properties.Settings.Default.ForeColor;

				// Size
				if (!Properties.Settings.Default.ClientSize.IsEmpty)
					window.Size = Properties.Settings.Default.ClientSize;
				window.ResizeEnd += OnResizeEnd;

				// Location
				Point location = Properties.Settings.Default.Location;
				if (IsValidLocation(location))
				{
					window.StartPosition = FormStartPosition.Manual;
					window.Location = location;
				}
				window.LocationChanged += OnLocationChanged;

				// WindowState
				if (Properties.Settings.Default.WindowState != FormWindowState.Minimized)
					window.WindowState = Properties.Settings.Default.WindowState;
				window.FormClosing += OnClosing;

				// UseSyntaxHighlighting
				window.UseSyntaxHighlighting = Properties.Settings.Default.UseSyntaxHighlighting;
				window.ShowNodeToolTips = Properties.Settings.Default.ShowNodeToolTips;

				// XPath Expression Library
				window.Expressions = Properties.Settings.Default.Expressions;
				if (window.Expressions == null)
					window.Expressions = new XPathExpressionLibrary();

				// Recently Used Files
				window.RecentlyUsedFiles = Properties.Settings.Default.RecentlyUsedFiles;
				if (window.RecentlyUsedFiles == null)
				{
					window.RecentlyUsedFiles = new RecentlyUsedFilesStack();
				}
				else
				{
					// not sure why, but the list gets reversed when persisted
					window.RecentlyUsedFiles.Reverse();
				}

				window.AutoUpdateUrl = Properties.Settings.Default.UpdateUrl;
				window.MinimumReleaseStatus = Properties.Settings.Default.UpdateReleaseStatus;

				// ChildNodeDefinitions
				window.ChildNodeDefinitions = Properties.Settings.Default.ChildNodeDefinitions;
				if (window.ChildNodeDefinitions == null)
				{
					window.ChildNodeDefinitions = new ChildNodeDefinitionCollection();
					Properties.Settings.Default.ChildNodeDefinitions = window.ChildNodeDefinitions;
				}

				window.Shown += new EventHandler(OnWindowShown);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		/// <summary>
		/// Checks that a location is within the bounds of the current system display device(s).
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		private static bool IsValidLocation(Point location)
		{
			// loop through the current system display device(s)
			foreach (Screen screen in Screen.AllScreens)
			{
				// if the specified point is within the screen's bounds, return true
				if (screen.Bounds.Contains(location))
					return true;
			}

			// the specified point was outside the bounds of all current system display devices
			return false;
		}

		#region Event Handlers

		private static void OnStartup(object sender, StartupEventArgs e)
		{
			try
			{
				TabbedXmlExplorerWindow window = SingleInstanceApplication.Instance.MainWindow;

				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
				Debug.WriteLine(string.Format("Local user config path: {0}", config.FilePath));

				string dockFilename = Path.GetDirectoryName(config.FilePath);
				dockFilename = Path.Combine(dockFilename, "user.dock.config");
				Debug.WriteLine(string.Format("Local user dock config path: {0}", dockFilename));
				window.DockSettingsFilename = dockFilename;

				if (dockFilename != null && File.Exists(dockFilename))
					window.DockPanel.LoadFromXml(dockFilename, window.DeserializeDockContent);

				// upgrade settings from a previous version, if needed
				if (Properties.Settings.Default.UpgradeNeeded)
				{
					Properties.Settings.Default.Upgrade();
					Properties.Settings.Default.UpgradeNeeded = false;
					Properties.Settings.Default.Save();
				}

				// open it
				window.Open(e.CommandLine);

				// read any options saved from a previous instance of the application
				// (window size and position, font, etc)
				ReadOptions(window);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				ExceptionDialog.ShowDialog(ex);
			}
		}

		/// <summary>
		/// Occurs when attempting to start a single-instance application and the application is already active.
		/// </summary>
		private static void OnStartupNextInstance(object sender, StartupNextInstanceEventArgs e)
		{
			// an attempt to start the application a second time has been made
			try
			{
				// handle any command-line args
				SingleInstanceApplication.Instance.MainWindow.Open(e.CommandLine);

				e.BringToForeground = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				ExceptionDialog.ShowDialog(ex);
			}
		}

		private static void OnUnhandledException(object sender, Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs e)
		{
			e.ExitApplication = false;
			Debug.WriteLine(e.Exception);
			ExceptionDialog.ShowDialog(e.Exception);
		}

		private static void OnShutdown(object sender, EventArgs e)
		{
			try
			{
				// application window has been closed, save settings
				Properties.Settings.Default.Save();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		private static void OnResizeEnd(object sender, EventArgs e)
		{
			Form form = sender as Form;
			if (form == null)
				return;

			// the window has been resized, update the setting
			if (form.WindowState == FormWindowState.Normal)
				Properties.Settings.Default.ClientSize = form.Size;
		}

		private static void OnLocationChanged(object sender, EventArgs e)
		{
			Form form = sender as Form;
			if (form == null)
				return;

			// the window has been moved, update the setting
			if (form.WindowState == FormWindowState.Normal)
				Properties.Settings.Default.Location = form.Location;
		}

		private static void OnWindowShown(object sender, EventArgs e)
		{
			try
			{
				TabbedXmlExplorerWindow window = sender as TabbedXmlExplorerWindow;
				if (window == null)
					return;

				var settings = Properties.Settings.Default;

				if (settings.CheckForUpdates)
					window.CheckForUpdates(false);

				if (settings.CheckDefaultProgram)
				{
					settings.CheckDefaultProgram = false;
					settings.Save();

					if (System.Windows.DefaultApplications.IsAssociationsWindowSupported)
					{
						using (DefaultProgramPrompt prompt = new DefaultProgramPrompt())
						{
							DialogResult result = prompt.ShowDialog(window);

							if (result == DialogResult.Yes)
							{
								System.Windows.DefaultApplications.ShowAssociationsWindow("XML Explorer");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				ExceptionDialog.ShowDialog(ex);
			}
		}

		private static void OnClosing(object sender, CancelEventArgs e)
		{
			Form form = sender as Form;
			if (form == null)
				return;

			// the window is being closed, update the WindowState setting
			if (form.WindowState != FormWindowState.Minimized)
				Properties.Settings.Default.WindowState = form.WindowState;

			TabbedXmlExplorerWindow window = SingleInstanceApplication.Instance.MainWindow;

			Properties.Settings.Default.Font = window.TreeFont;
			Properties.Settings.Default.ForeColor = window.TreeForeColor;
			Properties.Settings.Default.AutoCompleteMode = window.AutoCompleteMode;
			Properties.Settings.Default.UseSyntaxHighlighting = window.UseSyntaxHighlighting;
			Properties.Settings.Default.Expressions = window.Expressions;
			Properties.Settings.Default.RecentlyUsedFiles = window.RecentlyUsedFiles;
			Properties.Settings.Default.ChildNodeDefinitions = window.ChildNodeDefinitions;
			Properties.Settings.Default.ShowNodeToolTips = window.ShowNodeToolTips;
		}

		#endregion

	}
}