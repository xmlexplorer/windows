using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace XmlExplorer.Controls
{
	public partial class CertificateErrorWindow : Form
	{
		public X509Certificate Certificate { get; private set; }

		public const int CRYPTUI_DISABLE_ADDTOSTORE = 0x00000010;

		[DllImport("CryptUI.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern Boolean CryptUIDlgViewCertificate(
			 ref CRYPTUI_VIEWCERTIFICATE_STRUCT pCertViewInfo,
			 ref bool pfPropertiesChanged
		);

		public struct CRYPTUI_VIEWCERTIFICATE_STRUCT
		{
			public int dwSize;
			public IntPtr hwndParent;
			public int dwFlags;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String szTitle;
			public IntPtr pCertContext;
			public IntPtr rgszPurposes;
			public int cPurposes;
			public IntPtr pCryptProviderData; // or hWVTStateData
			public Boolean fpCryptProviderDataTrustedUsage;
			public int idxSigner;
			public int idxCert;
			public Boolean fCounterSigner;
			public int idxCounterSigner;
			public int cStores;
			public IntPtr rghStores;
			public int cPropSheetPages;
			public IntPtr rgPropSheetPages;
			public int nStartPage;
		}

		public CertificateErrorWindow()
		{
			InitializeComponent();
		}

		public CertificateErrorWindow(X509Certificate certificate)
			: this()
		{
			// TODO: Complete member initialization
			this.Certificate = certificate;
		}

		private void buttonViewCertificate_Click(object sender, EventArgs e)
		{
			try
			{
				// Get the cert
				X509Certificate2 cert = new X509Certificate2(this.Certificate);

				// Show the cert
				CRYPTUI_VIEWCERTIFICATE_STRUCT certViewInfo = new CRYPTUI_VIEWCERTIFICATE_STRUCT();
				certViewInfo.dwSize = Marshal.SizeOf(certViewInfo);
				certViewInfo.pCertContext = cert.Handle;
				certViewInfo.szTitle = "Certificate";
				certViewInfo.dwFlags = CRYPTUI_DISABLE_ADDTOSTORE;
				certViewInfo.nStartPage = 0;
				certViewInfo.hwndParent = this.Handle;
				bool fPropertiesChanged = false;
				CryptUIDlgViewCertificate(ref certViewInfo, ref fPropertiesChanged);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void buttonIgnoreErrors_Click(object sender, EventArgs e)
		{
			try
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			try
			{
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}
	}
}
