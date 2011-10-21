using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace XmlExplorer.TreeView
{
	/// <summary>
	/// Provides a common dialog for displaying an Exception to the user.
	/// </summary>
	public partial class ExceptionDialog : Form
	{
		private Exception _exception;
		private string submitUrl;

		/// <summary>
		/// Initializes a new instance of the ExceptionDialog class.
		/// </summary>
		public ExceptionDialog()
		{
			InitializeComponent();

			this.StartPosition = FormStartPosition.CenterParent;
			this.AcceptButton = _buttonOK;
			this.KeyPreview = true;

			_textBox.ScrollBars = ScrollBars.Both;
			_textBox.WordWrap = false;
			_textBox.ReadOnly = true;

			_richTextBox.ReadOnly = true;
			_richTextBox.BackColor = SystemColors.Window;

			_buttonOK.Enabled = true;
			_buttonOK.Click += new EventHandler(OnButtonOkClick);

			this.linkLabelSendErrorReport.Visible = false;
			this.linkLabelSendErrorReport.Enabled = false;

			this.buttonCopy.Visible = false;

			this.linkLabelSendErrorReport.Click += new EventHandler(OnLinkLabelSendErrorReportClick);
		}

		/// <summary>
		/// Initializes a new instance of the ExceptionDialog class.
		/// </summary>
		/// <param name="ex"></param>
		public ExceptionDialog(Exception ex)
			: this()
		{
			_exception = ex;

			this.DisplayException(ref _exception);
		}

		protected override void OnShown(EventArgs e)
		{
			try
			{
				base.OnShown(e);

				this.CenterToParent();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.Control && e.KeyCode == Keys.C)
			{
				this.CopyExceptionToClipboard();
			}
		}

		private void CopyExceptionToClipboard()
		{
			if (_exception == null)
				return;

			Clipboard.SetText(_exception.ToString());
		}

		#region Control Events

		/// <summary>
		/// Occurs when the Ok button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnButtonOkClick(object sender, EventArgs e)
		{
			try
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				ExceptionDialog.ShowDialog(this, ex);
			}
		}

		void OnLinkLabelSendErrorReportClick(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.SubmitUrl))
					return;

				Process.Start(this.SubmitUrl);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				ExceptionDialog.ShowDialog(this, ex);
			}
		}

		private void buttonCopy_Click(object sender, EventArgs e)
		{
			try
			{
				this.CopyExceptionToClipboard();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				ExceptionDialog.ShowDialog(this, ex);
			}
		}

		#endregion

		/// <summary>
		/// Gets or sets the exception that is displayed.
		/// </summary>
		public Exception Exception
		{
			get { return _exception; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value", "The ExceptionDialog cannot display the details of an exception object that is null.");

				_exception = value;

				this.DisplayException(ref _exception);
			}
		}

		public string SubmitUrl
		{
			get
			{
				return this.submitUrl;
			}
			set
			{
				this.submitUrl = value;

				this.linkLabelSendErrorReport.Visible = !string.IsNullOrEmpty(this.submitUrl);
			}
		}

		protected InformationPanel InformationPanel
		{
			get { return _informationPanel; }
		}

		public Image InformationPanelImage
		{
			get
			{
				return this.InformationPanel.Image;
			}

			set
			{
				this.InformationPanel.Image = value;
			}
		}

		protected string MessageAsText
		{
			get { return _richTextBox.Text; }
			set { _richTextBox.Text = value; }
		}

		protected string MessageAsRtf
		{
			get { return _richTextBox.Rtf; }
			set
			{
				try
				{
					_richTextBox.Rtf = value;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex);
					ExceptionDialog.ShowDialog(this, ex);
				}
			}
		}

		protected string StackTrace
		{
			get { return _textBox.Text; }
			set { _textBox.Text = value; }
		}

		protected object SelectedObject
		{
			get { return _propertyGrid.SelectedObject; }
			set { _propertyGrid.SelectedObject = value; }
		}

		/// <summary>
		/// Displays the details fo the exception using the embedded web browser control and Html.
		/// </summary>
		/// <param name="ex"></param>
		protected virtual void DisplayException(ref Exception ex)
		{
			string title = null;

			this.buttonCopy.Visible = ex != null || !string.IsNullOrEmpty(this.MessageAsText);

			if (string.IsNullOrEmpty(title))
				title = "Exception Encountered";

			_informationPanel.Title = title;

			if (_tabControl.TabPages.Count > 1)
				_informationPanel.Description = "Please refer to the tabs below for more information.";
			else
				_informationPanel.Description = "Please see below for more information.";

			if (!string.IsNullOrEmpty(ex.Message.Trim()))
				this.MessageAsText = ex.Message;
			this.StackTrace = ex.ToString();
			this.SelectedObject = ex;
		}

		protected void RemoveStackTraceTab()
		{
			if (_tabControl.TabPages.Contains(_tabPageStackTrace))
				_tabControl.TabPages.Remove(_tabPageStackTrace);
		}

		protected void RemovePropertiesTab()
		{
			if (_tabControl.TabPages.Contains(_tabPageProperties))
				_tabControl.TabPages.Remove(_tabPageProperties);
		}

		/// <summary>
		/// Show's the ExceptionDialog modally while displaying the details of the exception.
		/// </summary>
		/// <param name="ex"></param>
		public static void ShowDialog(Exception ex)
		{
			try
			{
				using (ExceptionDialog dialog = new ExceptionDialog(ex))
				{
					dialog.ShowDialog();
				}
			}
			catch (Exception exc)
			{
				Debug.WriteLine(exc);
			}
		}

		private delegate void ShowDialogDelegate(Form owner, Exception ex);

		/// <summary>
		/// Show's the ExceptionDialog modally while displaying the details of the exception.
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="ex"></param>
		public static void ShowDialog(Form owner, Exception ex)
		{
			if (owner == null || owner.IsDisposed)
			{
				ShowDialog(ex);
				return;
			}

			if (owner.InvokeRequired)
			{
				owner.Invoke(new ShowDialogDelegate(ShowDialog), owner, ex);
				return;
			}

			using (ExceptionDialog dialog = new ExceptionDialog(ex))
			{
				dialog.ShowDialog(owner);
			}
		}

		public static void ShowDialog(IWin32Window owner, Exception ex)
		{
			if (Form.ActiveForm != null)
			{
				ShowDialog(Form.ActiveForm, ex);
				return;
			}

			using (ExceptionDialog dialog = new ExceptionDialog(ex))
			{
				dialog.ShowDialog(owner);
			}
		}

		public static void ShowDialog(Control owner, Exception ex)
		{
			Form form = null;

			if (owner != null && !owner.IsDisposed)
			{
				form = owner.FindForm();
			}

			ShowDialog(form, ex);
		}
	}
}