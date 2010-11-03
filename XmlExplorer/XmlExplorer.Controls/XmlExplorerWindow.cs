using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XmlExplorer.TreeView;

namespace XmlExplorer.Controls
{
	public partial class XmlExplorerWindow : DockContent
	{
		#region Variables

		#endregion

		#region Constructors

		public XmlExplorerWindow()
			: base()
		{
			this.InitializeComponent();

			this.xmlTreeView.HideSelection = false;
			this.xmlTreeView.LabelEdit = true;
			this.xmlTreeView.PropertyChanged += new PropertyChangedEventHandler(xmlTreeView_PropertyChanged);

			this.Text = "Untitled";
		}

		public XmlExplorerWindow(string text)
			: this()
		{
			base.Text = text;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the XPathNavigatorTreeView used to display XML data.
		/// </summary>
		public XPathNavigatorTreeView TreeView
		{
			get
			{
				return this.xmlTreeView;
			}
		}

		/// <summary>
		/// Gets or sets whether the custom drawing of syntax highlights
		/// should be bypassed. This can be optionally used to improve 
		/// performance on large documents.
		/// </summary>
		public bool UseSyntaxHighlighting
		{
			get { return this.xmlTreeView.UseSyntaxHighlighting; }
			set { this.xmlTreeView.UseSyntaxHighlighting = value; }
		}

		public List<Error> Errors
		{
			get
			{
				return this.xmlTreeView.Errors;
			}
		}

		public Font XmlFont
		{
			get
			{
				return this.xmlTreeView.Font;
			}
			set
			{
				this.xmlTreeView.Font = value;
			}
		}

		public Color XmlForeColor
		{
			get
			{
				return this.xmlTreeView.ForeColor;
			}
			set
			{
				this.xmlTreeView.ForeColor = value;
			}
		}

		#endregion

		#region Methods

		private void UpdateWindowText()
		{
			string text = "Untitled";

			FileInfo fileInfo = this.xmlTreeView.FileInfo;

			if (fileInfo != null)
				text = fileInfo.Name;
			else if (this.xmlTreeView.NodeIterator != null)
				text = "XPath results";
			else if (this.xmlTreeView.Uri != null)
				text = this.xmlTreeView.Uri.GetLeftPart(UriPartial.Path);

			this.Text = text;
		}

		public void BeginOpenUri(string uriString)
		{
			this.BeginOpenUri(uriString, null, null);
		}

		public void BeginOpenUri(string uriString, string username, string password)
		{
			this.TreeView.Uri = new Uri(uriString);

			// just ignore certificate errors for now
			ServicePointManager.ServerCertificateValidationCallback = this.OnRemoteCertificateValidation;

			WebClient client = new WebClient();

			if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
			{
				client.UseDefaultCredentials = false;
				client.Credentials = new NetworkCredential(username, password);
			}

			this.FormClosing += (sender, e) =>
				{
					if (client != null && client.IsBusy)
						client.CancelAsync();
				};

			client.DownloadStringCompleted += this.OnWebClientDownloadStringCompleted;

			client.DownloadStringAsync(new Uri(uriString), uriString);
		}

		bool OnRemoteCertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (this.InvokeRequired)
			{
				bool result = (bool)this.Invoke(new Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool>(this.OnRemoteCertificateValidation), sender, certificate, chain, sslPolicyErrors);
				return result;
			}

			using (CertificateErrorWindow window = new CertificateErrorWindow(certificate))
			{
				if (window.ShowDialog(this) != DialogResult.OK)
					return false;

				return true;
			}
		}

		void OnWebClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			try
			{
				string uriString = e.UserState as string;

				if (e.Cancelled)
					return;

				if (e.Error != null)
				{
					Debug.WriteLine(e.Error);

					WebException exception = e.Error as WebException;
					if (exception != null)
					{
						if (exception.Status == WebExceptionStatus.ProtocolError)
						{
							HttpWebResponse response = exception.Response as HttpWebResponse;
							if (response.StatusCode == HttpStatusCode.Unauthorized)
							{
								this.HandleBasicAuthentication(uriString);
								return;
							}
						}
					}

					this.TreeView.AddError(e.Error.Message);
					this.TreeView.OnLoadingFinished(EventArgs.Empty);
					return;
				}

				this.TreeView.BeginOpen(e.Result);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				this.TreeView.AddError(ex.Message);
			}
		}

		private void HandleBasicAuthentication(string uriString)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new Action<string>(this.HandleBasicAuthentication), uriString);
				return;
			}

			using (BasicAuthenticationWindow window = new BasicAuthenticationWindow())
			{
				if (window.ShowDialog(this) != DialogResult.OK)
					return;

				this.BeginOpenUri(uriString, window.Username, window.Password);
			}
		}

		#endregion

		#region Overrides

		#endregion

		#region Event Handlers

		protected override void OnMouseUp(MouseEventArgs e)
		{
			try
			{
				base.OnMouseUp(e);
				if (e.Button == MouseButtons.Middle)
				{
					this.Close();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		void xmlTreeView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			try
			{
				switch (e.PropertyName)
				{
					case "FileInfo":
					case "NodeIterator":
					case "Uri":
						this.UpdateWindowText();
						break;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		#endregion
	}
}

