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
    public partial class NamespacesWindow : DockContent
    {
        #region Variables

        private List<NamespaceDefinition> _namespaceDefinitions;

        #endregion

        #region Constructors

        public NamespacesWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public List<NamespaceDefinition> NamespaceDefinitions
        {
            get
            {
                return _namespaceDefinitions;
            }

            set
            {
                _namespaceDefinitions = value;
                this.LoadNamespaceDefinitions(_namespaceDefinitions);
            }
        }

        #endregion

        #region Events

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

        public void LoadNamespaceDefinitions(List<NamespaceDefinition> namespaceDefinitions)
        {
            try
            {
                this.listView.BeginUpdate();

                this.listView.Items.Clear();

                if (namespaceDefinitions != null)
                {
                    foreach (NamespaceDefinition namespaceDefinition in namespaceDefinitions)
                    {
                        ListViewItem item = new ListViewItem(namespaceDefinition.Prefix);
                        item.SubItems.Add(namespaceDefinition.Namespace);
                        item.Tag = namespaceDefinition;
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

        #endregion
    }
}
