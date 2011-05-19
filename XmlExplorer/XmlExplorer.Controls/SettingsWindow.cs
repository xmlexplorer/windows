using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace XmlExplorer.Controls
{
	public partial class SettingsWindow : DockContent
	{
		public SettingsWindow()
		{
			InitializeComponent();

			SettingsHeader header = new SettingsHeader();
			header.BrowseClicked += new System.EventHandler(header_BrowseClicked);
			this.elementHost.Child = header;
		}

		void header_BrowseClicked(object sender, System.EventArgs e)
		{
			string path = Application.ExecutablePath + ".config";

			// open explorer to the file's parent folder, with the file selected
			string args = string.Format("/select,\"{0}\"", path);
			Process.Start("explorer", args);
		}

		public ApplicationSettingsBase Settings
		{
			get
			{
				return this.propertyGrid.SelectedObject as ApplicationSettingsBase;
			}

			set
			{
				this.propertyGrid.SelectedObject = value;
			}
		}

		public PropertyGrid PropertyGrid
		{
			get
			{
				return this.propertyGrid;
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);

			if (e.CloseReason != CloseReason.UserClosing)
				return;

			this.Hide();

			e.Cancel = true;
		}
	}
}
