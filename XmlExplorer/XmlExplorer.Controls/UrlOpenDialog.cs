using System.Windows.Forms;

namespace XmlExplorer.Controls
{
	public partial class UrlOpenDialog : Form
	{
		public UrlOpenDialog()
		{
			InitializeComponent();
		}

		public string Url
		{
			get { return this.textBoxUrl.Text; }

			set { this.textBoxUrl.Text = value; }
		}

		private void buttonOpen_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
