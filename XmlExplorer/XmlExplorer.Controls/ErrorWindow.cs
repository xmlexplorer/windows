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
using XmlExplorer.TreeView;

namespace XmlExplorer.Controls
{
    public partial class ErrorWindow : DockContent
    {
        #region Variables

        private List<Error> _errors;

        #endregion

        #region Constructors

        public ErrorWindow()
        {
            InitializeComponent();

            this.listView.ItemActivate += new EventHandler(listViewExpressions_ItemActivate);
        }

        #endregion

        #region Properties

        public List<Error> Errors
        {
            get
            {
                return _errors;
            }

            set
            {
                _errors = value;
                this.LoadErrors(_errors);
            }
        }

        #endregion

        #region Events

        public event EventHandler<EventArgs<Error>> ErrorActivated;

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

        public void LoadErrors(List<Error> errors)
        {
            try
            {
                this.listView.BeginUpdate();

                this.listView.Items.Clear();

                if (errors != null)
                {
                    foreach (Error error in errors)
                    {
                        ListViewItem item = new ListViewItem(error.DefaultOrder.ToString());
                        item.SubItems.Add(error.Description);
                        item.SubItems.Add(error.File);
                        item.Tag = error;
                        item.ImageKey = error.Category.ToString();
                        this.listView.Items.Add(item);
                    }
                }
            }
            finally
            {
                this.listView.AutoResizeColumns();
                this.listView.EndUpdate();
            }
        }

        #endregion

        #region Event Handlers

        void listViewExpressions_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if(this.listView.SelectedItems.Count < 1)
                    return;

                ListViewItem item = this.listView.SelectedItems[0];

                Error error = item.Tag as Error;
                if (error == null)
                    return;

                this.ErrorActivated(this, new EventArgs<Error>(error));
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
