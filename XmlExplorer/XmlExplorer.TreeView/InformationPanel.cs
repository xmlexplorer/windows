using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XmlExplorer.TreeView
{
	/// <summary>
	/// Provides a UserControl for displaying information using a Title, Description, and Image. Alternatively providing
	/// access to a scrolling MarqueeControl to provide visual feedback for long running processes.
	/// </summary>
	[System.Diagnostics.DebuggerStepThrough()]
	public sealed class InformationPanel : System.Windows.Forms.UserControl
	{
		private Label _labelTitle;
		private Label _labelDescription;
		private PictureBox _pictureBox;
		private MarqueeControl _marquee;
		private ProgressBar _progressBar;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the InformationPanel class
		/// </summary>
		public InformationPanel()
		{
			this.InitializeComponent();

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}

				//if (_pictureBox.Image != null)
				//{
				//    _pictureBox.Image.Dispose();
				//    _pictureBox.Image = null;
				//}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationPanel));
			this._labelTitle = new System.Windows.Forms.Label();
			this._pictureBox = new System.Windows.Forms.PictureBox();
			this._labelDescription = new System.Windows.Forms.Label();
			this._progressBar = new System.Windows.Forms.ProgressBar();
			this._marquee = new MarqueeControl();
			((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// _labelTitle
			// 
			this._labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._labelTitle.BackColor = System.Drawing.Color.Transparent;
			this._labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._labelTitle.Location = new System.Drawing.Point(10, 10);
			this._labelTitle.Name = "_labelTitle";
			this._labelTitle.Size = new System.Drawing.Size(305, 20);
			this._labelTitle.TabIndex = 0;
			// 
			// _pictureBox
			// 
			this._pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._pictureBox.BackColor = System.Drawing.Color.Transparent;
			this._pictureBox.Location = new System.Drawing.Point(325, 10);
			this._pictureBox.Name = "_pictureBox";
			this._pictureBox.Size = new System.Drawing.Size(64, 64);
			this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this._pictureBox.TabIndex = 1;
			this._pictureBox.TabStop = false;
			// 
			// _labelDescription
			// 
			this._labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._labelDescription.BackColor = System.Drawing.Color.Transparent;
			this._labelDescription.Location = new System.Drawing.Point(30, 35);
			this._labelDescription.Name = "_labelDescription";
			this._labelDescription.Size = new System.Drawing.Size(285, 58);
			this._labelDescription.TabIndex = 2;
			// 
			// _progressBar
			// 
			this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._progressBar.Enabled = false;
			this._progressBar.Location = new System.Drawing.Point(0, 80);
			this._progressBar.Name = "_progressBar";
			this._progressBar.Size = new System.Drawing.Size(400, 23);
			this._progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this._progressBar.TabIndex = 3;
			this._progressBar.Visible = false;
			// 
			// _marquee
			// 
			this._marquee.BackColor = System.Drawing.Color.Transparent;
			this._marquee.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._marquee.FrameRate = 33;
			this._marquee.ImageToScroll = ((System.Drawing.Image)(resources.GetObject("_marquee.ImageToScroll")));
			this._marquee.IsScrolling = false;
			this._marquee.Location = new System.Drawing.Point(0, 98);
			this._marquee.Name = "_marquee";
			this._marquee.Size = new System.Drawing.Size(400, 5);
			this._marquee.StepSize = 10;
			this._marquee.TabIndex = 0;
			// 
			// InformationPanel
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this._progressBar);
			this.Controls.Add(this._marquee);
			this.Controls.Add(this._labelDescription);
			this.Controls.Add(this._pictureBox);
			this.Controls.Add(this._labelTitle);
			this.Name = "InformationPanel";
			this.Size = new System.Drawing.Size(400, 103);
			((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region My Overrides

		/// <summary>
		/// Override the Refresh method to refresh the child controls 
		/// </summary>
		public override void Refresh()
		{
			base.Refresh();

			// refresh all of the child controls to prevent flickering
			// this should really be all GDI+ stuff if we wanted to be pure
			foreach (Control c in this.Controls)
				c.Refresh();
		}

		#endregion

		#region My Public Properties

		/// <summary>
		/// Gets or sets the Title displayed
		/// </summary>
		[Category("Appearance")]
		[Description("Gets or sets the Title displayed")]
		public string Title
		{
			get
			{
				return _labelTitle.Text;
			}
			set
			{
				_labelTitle.Text = value;
				this.Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the description displayed
		/// </summary>		
		[Category("Appearance")]
		[Description("Gets or sets the description displayed by this InformationPanel")]
		public string Description
		{
			get
			{
				return _labelDescription.Text;
			}
			set
			{
				_labelDescription.Text = value;
				this.Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the Image displayed
		/// </summary>		
		[Category("Appearance")]
		[Description("Gets or sets the Image displayed by this InformationPanel")]
		public Image Image
		{
			get
			{
				return _pictureBox.Image;
			}
			set
			{
				_pictureBox.Image = value;
				this.Refresh();
			}
		}

		/// <summary>
		/// Returns the PictureBox that contains the Image displayed
		/// </summary>
		[Category("Child Controls")]
		[Description("Returns the PictureBox that contains the Image displayed by the InformationPanel.")]
		public PictureBox ImagePictureBox
		{
			get
			{
				return _pictureBox;
			}
		}

		/// <summary>
		/// Returns the MarqueeControl displayed
		/// </summary>
		[Category("Child Controls")]
		[Description("Returns the MarqueeControl displayed at the bottom of this InformationPanel")]
		public MarqueeControl Marquee
		{
			get
			{
				return _marquee;
			}
		}

		/// <summary>
		/// Returns the ProgressBar displayed
		/// </summary>
		[Category("Child Controls")]
		[Description("Returns the ProgressBar displayed at the bottom of this InformationPanel")]
		public ProgressBar ProgressBar
		{
			get
			{
				return _progressBar;
			}
		}

		#endregion
	}
}
