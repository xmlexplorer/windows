namespace XmlExplorer
{
    using System.Diagnostics;
    using System.Windows.Forms;
    using Microsoft.VisualBasic.ApplicationServices;
    using XmlExplorer.Controls;

    public class SingleInstanceApplication : WindowsFormsApplicationBase
    {
        private static SingleInstanceApplication _instance;

        private TabbedXmlExplorerWindow _mainWindow;

        public TabbedXmlExplorerWindow MainWindow
        {
            get
            {
                if (_mainWindow == null)
                    _mainWindow = new TabbedXmlExplorerWindow();

                return _mainWindow;
            }

            private set
            {
                _mainWindow = value;
            }
        }

        private SingleInstanceApplication()
        {
            base.IsSingleInstance = true;
            base.MinimumSplashScreenDisplayTime = 0;
        }

        public static SingleInstanceApplication Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SingleInstanceApplication();

                return _instance;
            }
        }

        protected override void OnCreateMainForm()
        {
            this.MainForm = this.MainWindow;
        }

        protected override void OnCreateSplashScreen()
        {
            this.SplashScreen = new SplashScreen();
        }
    }
}

