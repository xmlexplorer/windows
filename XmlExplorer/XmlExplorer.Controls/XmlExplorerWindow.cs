using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;
using System.Drawing;
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
            else if(this.xmlTreeView.NodeIterator != null)
                text = "XPath results";

            this.Text = text;
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

