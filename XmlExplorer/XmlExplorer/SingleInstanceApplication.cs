namespace XmlExplorer
{
    using Microsoft.VisualBasic.ApplicationServices;
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Windows.Forms;
	using System.Diagnostics;
using System.Security.Permissions;
    using System.Security;

    public class SingleInstanceApplication : WindowsFormsApplicationBase
    {
        public SingleInstanceApplication()
        {
            base.IsSingleInstance = true;
            base.EnableVisualStyles = true;
        }

        public SingleInstanceApplication(AuthenticationMode mode) : base(mode)
        {
            base.IsSingleInstance = true;
            base.EnableVisualStyles = true;
        }

        private void Run(ReadOnlyCollection<string> commandLineArgs)
        {
            ArrayList list = new ArrayList(commandLineArgs);
            string[] textArray = (string[]) list.ToArray(typeof(string));
            base.Run(textArray);
        }

        public virtual void Run(Form mainForm)
        {
            base.MainForm = mainForm;

            try
            {
                new EnvironmentPermission(EnvironmentPermissionAccess.Read, "PATH").Demand();
                this.Run(base.CommandLineArgs);
            }
            catch (SecurityException)
            {
                MessageBox.Show("Unable to access command-line args due to security restrictions.");
                this.Run(new string[] { });
            }
        }
    }
}

