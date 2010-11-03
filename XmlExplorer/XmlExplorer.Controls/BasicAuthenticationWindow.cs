using System;
using System.Windows.Forms;

namespace XmlExplorer.Controls
{
	public partial class BasicAuthenticationWindow : Form
	{
		public BasicAuthenticationWindow()
		{
			InitializeComponent();
		}

		public string Username
		{
			get
			{
				return this.textBox1.Text;
			}
		}

		public string Password
		{
			get
			{
				return this.textBox2.Text;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
