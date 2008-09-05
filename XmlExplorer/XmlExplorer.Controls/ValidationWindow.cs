using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml.Schema;
using System.Xml.XPath;

namespace XmlExplorer.Controls
{
    public partial class ValidationWindow : DockContent
    {
        #region Variables

        private List<ValidationEventArgs> _validationEventArgs;

        #endregion

        #region Constructors

        public ValidationWindow()
        {
            InitializeComponent();

            this.toolStripButtonValidate.Click += this.OnToolStripButtonValidateClick;
            this.toolStripSpringTextBoxSchema.TextChanged += this.OnToolStripSpringTextBoxSchema_TextChanged;
            this.toolStripButtonBrowseForSchema.Click += this.OnToolStripButtonBrowseForSchema_Click;
        }

        #endregion

        #region Properties

        public List<ValidationEventArgs> ValidationEventArgs
        {
            get
            {
                return _validationEventArgs;
            }

            set
            {
                _validationEventArgs = value;
                this.LoadValidationEventArgs(_validationEventArgs);
            }
        }

        public string SchemaFileName
        {
            get
            {
                return this.toolStripSpringTextBoxSchema.Text;
            }

            set
            {
                this.toolStripSpringTextBoxSchema.Text = value;
            }
        }

        #endregion

        #region Events

        public event EventHandler SchemaFileNameChanged;
        public event EventHandler ValidateSchema;

        #endregion

        #region Methods

        private void AutoSizeListViewColumns(ListView listView)
        {
            foreach (ColumnHeader header in listView.Columns)
            {
                header.Width = -1;
                int width = header.Width;
                header.Width = -2;
                if (width > header.Width)
                    header.Width = width;
            }
        }

        public void LoadValidationEventArgs(List<ValidationEventArgs> validationEventArgs)
        {
            try
            {
                this.listViewExpressions.BeginUpdate();

                this.listViewExpressions.Items.Clear();

                if (validationEventArgs != null)
                {
                    foreach (ValidationEventArgs args in validationEventArgs)
                    {
                        ListViewItem item = new ListViewItem(args.Message);
                        item.SubItems.Add(args.Exception.LineNumber.ToString());
                        item.SubItems.Add(args.Exception.LinePosition.ToString());
                        item.Tag = args;
                        this.listViewExpressions.Items.Add(item);
                    }
                }
            }
            finally
            {
                this.listViewExpressions.EndUpdate();
            }
        }

        #endregion

        #region Event Handlers

        void OnToolStripButtonBrowseForSchema_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "XSD Schema Files (*.xsd)|*.xsd|All Files (*.*)|*.*";
                    if (dialog.ShowDialog(this) != DialogResult.OK)
                        return;

                    this.toolStripSpringTextBoxSchema.Text = dialog.FileName;

                    if (this.SchemaFileNameChanged != null)
                        this.SchemaFileNameChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        void OnToolStripSpringTextBoxSchema_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.SchemaFileNameChanged != null)
                    this.SchemaFileNameChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        void OnToolStripButtonValidateClick(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateSchema != null)
                    this.ValidateSchema(this, EventArgs.Empty);
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
