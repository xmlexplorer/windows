using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace XmlExplorer.TreeView
{
	/// <summary>
	/// Provides a UserControl that can scroll an Image for visualizing progress.
	/// </summary>	
	public sealed class MarqueeControl : System.Windows.Forms.UserControl
	{
		private Image _image;
		private int _offset;
		private int _step;
		private int _frameRate;
		private bool _isScrolling;
		//private BackgroundThread _thread;
		private ManualResetEvent _stopScrolling;

		/// <summary>
		/// Initializes a new instance of the MargqueeControl class
		/// </summary>
		public MarqueeControl()
		{
			this.InitializeComponent();
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.LoadDefaultImage();
			this.StepSize = 10;
			this.FrameRate = 33;
			_stopScrolling = new ManualResetEvent(false);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.IsScrolling = false;

				if (_image != null)
				{
					_image.Dispose();
					_image = null;
				}
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
			// 
			// MarqueeControl
			// 
			this.Name = "MarqueeControl";
			this.Size = new System.Drawing.Size(150, 10);

		}
		#endregion

		#region My Overrides

		/// <summary>
		/// Override the default painting, to scroll out image across the control
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			// clear the background using our backcolor first
			e.Graphics.Clear(this.BackColor);

			lock (this)
			{
				// do we have an image to scroll?
				if (_image != null)
				{
					// if it's not in design mode
					if (this.DesignMode)
					{
						e.Graphics.DrawImage(_image, 0, 0, this.Width, this.Height);
					}
					else
					{
						// get the image bounds
						GraphicsUnit gu = GraphicsUnit.Pixel;
						RectangleF rcImage = _image.GetBounds(ref gu);

						// calculate the width ratio
						float ratio = ((float)rcImage.Width / (float)this.Width);

						RectangleF rcDstRight = new RectangleF(_offset, 0, this.Width - _offset, this.Height);
						RectangleF rcSrcRight = new RectangleF(0, 0, rcDstRight.Width * ratio, rcImage.Height);

						RectangleF rcDstLeft = new RectangleF(0, 0, _offset, this.Height);
						RectangleF rcSrcLeft = new RectangleF(rcImage.Width - _offset * ratio, 0, _offset * ratio, rcImage.Height);

						e.Graphics.DrawImage(_image, rcDstRight, rcSrcRight, GraphicsUnit.Pixel);
						e.Graphics.DrawImage(_image, rcDstLeft, rcSrcLeft, GraphicsUnit.Pixel);

						// draw verticle line at offset, so we can see the seam
						//						e.Graphics.DrawLine(new Pen(Color.Red), new Point(_offset, 0), new Point(_offset, this.Height));
					}
				}
			}
		}

		#endregion

		#region My Public Properties

		/// <summary>
		/// Gets or sets the Image to scroll when the control is not is Design Mode.
		/// </summary>	
		[Category("Behavior")]
		[Description("The Image to scroll when the control is not is Design Mode.")]
		public Image ImageToScroll
		{
			get
			{
				return _image;
			}
			set
			{
				lock (this)
				{
					try
					{
						if (_image != null)
							_image.Dispose();

						_image = value;

						this.Invalidate();
					}
					catch (Exception ex)
					{
						Debug.WriteLine(ex);
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets a flag that determines whether or not the control is scrolling the selected image. 
		/// </summary>
		[Category("Behavior")]
		[Description("Determines whether or not the control is scrolling the selected image.")]
		public bool IsScrolling
		{
			get
			{
				return _isScrolling;
			}
			set
			{
				// bail if there is no change in the value
				if (_isScrolling == value)
					return;

				// update whether the image should be scrolling
				_isScrolling = value;

				if (_isScrolling)
				{
					// reset the event and start a thread to scroll the image
					_stopScrolling.Reset();
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.ScrollImage));
				}
				else
				{
					_stopScrolling.Set();
				}
			}
		}

		/// <summary>
		/// Gets or sets the number of pixels to move the image on each update. The default is 10.
		/// </summary>
		[Category("Behavior")]
		[Description("The number of pixels to move the image on each update. The default is 10.")]
		public int StepSize
		{
			get
			{
				return _step;
			}
			set
			{
				_step = value;
			}
		}

		/// <summary>
		/// Gets or sets the number of times per second the control will draw itself. The default is 30 times a second.
		/// </summary>
		[Category("Behavior")]
		[Description("The number of times per second the control will draw itself. The default is 30 times a second.")]
		public int FrameRate
		{
			get
			{
				return _frameRate;
			}
			set
			{
				_frameRate = value;
			}
		}

		#endregion

		#region My Public Methods

		/// <summary>
		/// Resets the Image to its default position.
		/// </summary>
		public void Reset()
		{
			_offset = 0;
			this.Invalidate();
		}

		#endregion

		#region My Private Methods

		/// <summary>
		/// Loads the default MarqueeControl Image from the Global resource file
		/// </summary>
		private void LoadDefaultImage()
		{
			_image = (Image)Properties.Resources.ResourceManager.GetObject("MarqueeControl");
			this.Invalidate();
		}

		/// <summary>
		/// The background thread procedure that will handle scrolling the Image
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ScrollImage(object stateObject)
		{
			try
			{
				while (true)
				{
					// step forward on the offset
					_offset += _step;

					// reset the offset if we hit the edge
					if (_offset >= this.Width)
						_offset = 0;

					// repaint
					this.Invalidate();

					// snooze a bit
					// Thread.Sleep(_frameRate);					
					if (_stopScrolling.WaitOne(_frameRate, false))
						return;
				}
			}
			catch (ThreadAbortException)
			{
				// watch out for this little guy. :P
				// some days i still feel like this is not right that an exception is thrown
				// but i suppose there's not really any better way for the framework to stop the thread
				// and give you control back
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		#endregion
	}
}
