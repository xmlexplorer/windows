using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XmlExplorer.Controls
{
	public partial class ExpressionResultsWindow : Form
	{
		#region Variables
		#endregion

		#region Constructors
		public ExpressionResultsWindow()
			: base()
		{
			InitializeComponent();

			this.KeyDown += new KeyEventHandler(ExpressionResultsDialog_KeyDown);
		}

		#endregion

		#region Properties

		public string Expression
		{
			get
			{
				return this.textBoxExpression.Text;
			}

			set
			{
				this.textBoxExpression.Text = value;
			}
		}

		public string Result
		{
			get
			{
				return this.textBoxResult.Text;
			}

			set
			{
				this.textBoxResult.Text = value;
			}
		}

		#endregion

		#region Event Declarations
		#endregion

		#region Event Handlers

		void ExpressionResultsDialog_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Escape:
					this.Close();
					break;
			}
		}

		#endregion

		#region Methods
		#endregion

		#region Overrides
		#endregion
	}
}