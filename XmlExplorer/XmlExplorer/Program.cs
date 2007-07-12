using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Deployment.Application;
using System.Text.RegularExpressions;

using XmlExplorer.Controls;
using System.ComponentModel;
using System.Drawing;

namespace XmlExplorer
{
	static class Program
	{
        private static TabbedXmlExplorerWindow _window;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            // initialize the single instance application
			SingleInstanceApplication application = new SingleInstanceApplication();
			application.StartupNextInstance += Program.OnStartupNextInstance;

            // create a new application window
            _window = new TabbedXmlExplorerWindow();

            // read any options saved from a previous instance of the application
            // (window size and position, font, etc)
            ReadOptions(_window);

            // open it
            _window.Open(args);

            // start the application
            application.Run(_window);

            // application window has been closed, save settings
            Properties.Settings.Default.Save();
		}

        /// <summary>
        /// Applies any saved settings to a TabbedXmlExplorerWindow.
        /// </summary>
        private static void ReadOptions(TabbedXmlExplorerWindow window)
        {
            try
            {
                // AutoCompleteMode
                window.AutoCompleteMode = Properties.Settings.Default.AutoCompleteMode;

                _window.TreeFont = Properties.Settings.Default.Font;
                _window.TreeForeColor = Properties.Settings.Default.ForeColor;

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

        /// <summary>
        /// Occurs when attempting to start a single-instance application and the application is already active.
        /// </summary>
        private static void OnStartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            // an attempt to start the application a second time has been made
            try
            {
                // activate the main application window
                if (_window.WindowState == FormWindowState.Minimized)
                {
                    _window.WindowState = FormWindowState.Normal;
                }
                _window.Activate();

                // handle any command-line args
                _window.Open(e.CommandLine);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(ex.ToString());
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

        private static void OnClosing(object sender, CancelEventArgs e)
        {
            Form form = sender as Form;
            if (form == null)
                return;

            // the window is being closed, update the WindowState setting
            if (form.WindowState != FormWindowState.Minimized)
                Properties.Settings.Default.WindowState = form.WindowState;

            Properties.Settings.Default.Font = _window.TreeFont;
            Properties.Settings.Default.ForeColor = _window.TreeForeColor;
            Properties.Settings.Default.AutoCompleteMode = _window.AutoCompleteMode;
        }

        #endregion

	}
}