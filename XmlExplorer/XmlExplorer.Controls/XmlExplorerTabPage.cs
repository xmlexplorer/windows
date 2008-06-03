namespace XmlExplorer.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml;
    using System.Diagnostics;
    using System.Threading;
    using System.Text;
    using System.Xml.XPath;

    public partial class XmlExplorerTabPage : TabPage
    {
        #region Variables

        // the background thread used to load xml file data 
        private Thread _loadFileThread;

        // the full path of the file currently opened, if opened
        // from a file, or once a node set has been saved to a file
        private string _filename;

        private Stream _stream;

        // the node set currently opened, if opened from
        // an XPath expression result
        private XPathNodeIterator _nodes;

        #endregion

        #region Events

        /// <summary>
        /// Occurs before a tab is closed.
        /// </summary>
        public event EventHandler<CancelEventArgs> BeforeClosed;

        /// <summary>
        /// Occurs after a tab has been closed, and needs removed from the display.
        /// </summary>
        public event EventHandler<EventArgs> NeedsClosed;

        /// <summary>
        /// Occurs when the asynchronous loading of an xml file has started.
        /// </summary>
        public event EventHandler<EventArgs> LoadingFileStarted;

        /// <summary>
        /// Occurs when the asynchronous loading of an xml file has completed.
        /// </summary>
        public event EventHandler<EventArgs> LoadingFileCompleted;

        /// <summary>
        /// Occurs when an exception is encountered while loading an xml file.
        /// </summary>
        public event EventHandler<EventArgs> LoadingFileFailed;

        #endregion

        #region Constructors

        public XmlExplorerTabPage()
            : base()
        {
            this.InitializeComponent();
            this.xmlTreeView.HideSelection = false;
            this.xmlTreeView.LabelEdit = true;
            this.xmlTreeView.AfterLabelEdit += new NodeLabelEditEventHandler(xmlTreeView_AfterLabelEdit);
            this.xmlTreeView.ItemDrag += new ItemDragEventHandler(xmlTreeView_ItemDrag);
        }

        public XmlExplorerTabPage(string text)
            : this()
        {
            base.Text = text;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets an XPathNavigator to display in the TreeView.
        /// </summary>
        public XPathNavigator Navigator
        {
            get
            {
                return this.xmlTreeView.Navigator;
            }
            set
            {
                this.xmlTreeView.Navigator = value;
            }
        }

        /// <summary>
        /// Gets the XPathNavigatorTreeView used to display XML data.
        /// </summary>
        public XPathNavigatorTreeView XPathNavigatorTreeView
        {
            get
            {
                return this.xmlTreeView;
            }
        }

        /// <summary>
        /// Gets the filename, if any, of the currently opened file.
        /// </summary>
        public string Filename
        {
            get
            {
                return _filename;
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

        #endregion

        #region Methods

        /// <summary>
        /// Closes the tab, aborting the background thread used for loading if needed.
        /// </summary>
        public void Close()
        {
            CancelEventArgs e = new CancelEventArgs(false);

            // raise the BeforeClosed event to give the user the chance to cancel, if needed.
            if (this.BeforeClosed != null)
                this.BeforeClosed(this, e);

            if (e.Cancel)
                return;

            // if an xml file is currently being loaded on a background thread
            if (_loadFileThread != null && _loadFileThread.IsAlive)
            {
                try
                {
                    // abort the load thread
                    Debug.WriteLine("Aborting xml document file load thread by user request...");
                    _loadFileThread.Abort();
                }
                catch (ThreadAbortException ex)
                {
                    Debug.WriteLine(ex);
                }
            }

            // raise the NeedsClosed event, so the tab can be removed from the display
            if (this.NeedsClosed != null)
                this.NeedsClosed(this, EventArgs.Empty);
        }

        /// <summary>
        /// Loads XML data from a string.
        /// </summary>
        /// <param name="xml"></param>
        public void LoadXml(string xml)
        {
            this.xmlTreeView.LoadXml(xml);
        }

        /// <summary>
        /// Loads XML file data from a given filename.
        /// </summary>
        /// <param name="filename"></param>
        public void Open(string filename)
        {
            _filename = filename;

            // set the tab text and tooltip
            this.Text = Path.GetFileName(filename);
            this.ToolTipText = filename;

            // begin loading the file on a background thread
            this.BeginLoadFile();
        }

        /// <summary>
        /// Loads XML file data from a given stream.
        /// </summary>
        public void Open(Stream stream)
        {
            _stream = stream;

            // set the tab text and tooltip
            this.Text = "<Unknown>";
            this.ToolTipText = "This file was loaded from a stream (likely due to restricted permissions), and no filename is available.";

            // begin loading the file on a background thread
            this.BeginLoadFile();
        }

        /// <summary>
        /// Loads an XML node set.
        /// </summary>
        /// <param name="iterator"></param>
        public void Open(XPathNodeIterator iterator)
        {
            _filename = string.Empty;
            this.Text = "XPath results";

            _nodes = iterator.Clone();

            if (this.LoadingFileStarted != null)
                this.LoadingFileStarted(this, EventArgs.Empty);

            this.xmlTreeView.BeginUpdate();
            try
            {
                this.xmlTreeView.LoadNodes(iterator, this.xmlTreeView.Nodes);

                if (this.LoadingFileCompleted != null)
                    this.LoadingFileCompleted(this, EventArgs.Empty);
            }
            catch (ThreadAbortException ex)
            {
                Debug.WriteLine(ex);
                if (this.LoadingFileFailed != null)
                    this.LoadingFileFailed(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
                if (this.LoadingFileFailed != null)
                    this.LoadingFileFailed(this, EventArgs.Empty);
            }
            finally
            {
                this.xmlTreeView.EndUpdate();
            }
        }

        /// <summary>
        /// Begins loading an XML file on a background thread.
        /// </summary>
        private void BeginLoadFile()
        {
            _loadFileThread = new Thread(new ThreadStart(this.LoadFile));
            _loadFileThread.IsBackground = true;
            _loadFileThread.Start();
        }

        /// <summary>
        /// Finds and selects a tree node from an XPath expression.
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public bool FindByXpath(string xpath)
        {
            return xmlTreeView.FindByXpath(xpath);
        }

        /// <summary>
        /// Evaluates an XPath expression, and returns the typed result.
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public XPathNodeIterator SelectXmlNodes(string xpath)
        {
            return this.XPathNavigatorTreeView.SelectXmlNodes(xpath) as XPathNodeIterator;
        }

        /// <summary>
        /// The background worker method used to load an XML file in the background.
        /// </summary>
        private void LoadFile()
        {
            try
            {
                if (this.LoadingFileStarted != null)
                    this.LoadingFileStarted(this, EventArgs.Empty);

                Debug.WriteLine(string.Format("Peak RAM Before......{0}", Process.GetCurrentProcess().PeakWorkingSet64.ToString()));
                Debug.Write("Loading XPathDocument.");
                DateTime start = DateTime.Now;

                // load the document
                XPathDocument document = null;

                if (!string.IsNullOrEmpty(_filename))
                    document = new XPathDocument(_filename);
                else if (_stream != null)
                    document = new XPathDocument(_stream);

                Debug.WriteLine(string.Format("Done. Elapsed: {0}ms.", DateTime.Now.Subtract(start).TotalMilliseconds));

                // the UI has to be updated on the thread that created it, so invoke back to the main UI thread.
                MethodInvoker del = delegate()
            {
                this.LoadDocument(document);
            };

                this.Invoke(del);

                if (this.LoadingFileCompleted != null)
                    this.LoadingFileCompleted(this, EventArgs.Empty);
            }
            catch (ThreadAbortException ex)
            {
                // do not display the exception to the user, as they most likely aborted the thread by 
                // closing the tab or application themselves
                Debug.WriteLine(ex);
                if (this.LoadingFileFailed != null)
                    this.LoadingFileFailed(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                MethodInvoker del = delegate()
                {
                    MessageBox.Show(this, ex.ToString());
                };

                this.Invoke(del);

                if (this.LoadingFileFailed != null)
                    this.LoadingFileFailed(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Loads an XPathDocument into the tree.
        /// </summary>
        /// <param name="document"></param>
        private void LoadDocument(XPathDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            Debug.Write("Loading UI.");
            DateTime start = DateTime.Now;
            this.xmlTreeView.Navigator = document.CreateNavigator();
            Debug.WriteLine(string.Format("Done. Elapsed: {0}ms.", DateTime.Now.Subtract(start).TotalMilliseconds));
            Debug.WriteLine(string.Format("Peak RAM After......{0}", Process.GetCurrentProcess().PeakWorkingSet64.ToString()));
        }

        /// <summary>
        /// Reloads the display from the original file or node set.
        /// </summary>
        public void Reload()
        {
            this.xmlTreeView.Nodes.Clear();

            if (!string.IsNullOrEmpty(_filename))
                this.Open(_filename);
            else if (_nodes != null)
                this.Open(_nodes);
        }

        /// <summary>
        /// Copies the current selected xml node (and all of it's sub nodes) to the clipboard
        /// as formatted XML text.
        /// </summary>
        public void CopyFormattedOuterXml()
        {
            XPathNavigatorTreeNode selected = this.xmlTreeView.SelectedNode as XPathNavigatorTreeNode;

            if (selected == null)
                return;

            string text = GetXmlTreeNodeFormattedOuterXml(selected);

            if (!string.IsNullOrEmpty(text))
                Clipboard.SetData(DataFormats.Text, text);
        }

        /// <summary>
        /// Copies the current selected xml node to the clipboard as XML text.
        /// </summary>
        public void CopyNodeText()
        {
            XPathNavigatorTreeNode selected = this.xmlTreeView.SelectedNode as XPathNavigatorTreeNode;

            if (selected == null)
                return;

            string text = selected.Text;

            if (!string.IsNullOrEmpty(text))
                Clipboard.SetData(DataFormats.Text, text);
        }

        /// <summary>
        /// Returns the selected xml node (and all of it's sub nodes) as formatted XML text.
        /// </summary>
        public string GetXmlTreeNodeFormattedOuterXml(XPathNavigatorTreeNode node)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Default))
                {
                    writer.Formatting = Formatting.Indented;

                    node.Navigator.WriteSubtree(writer);

                    writer.Flush();

                    return Encoding.Default.GetString(stream.ToArray());
                }
            }
        }

        /// <summary>
        /// Overwrites the tab page's file with XML formatting (tabs and crlf's)
        /// </summary>
        public void SaveWithFormatting()
        {
            string filename = _filename;

            if (string.IsNullOrEmpty(filename))
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                    if (dialog.ShowDialog(this) != DialogResult.OK)
                        return;

                    filename = dialog.FileName;
                }
            }

            this.SaveAs(filename);
        }

        /// <summary>
        /// Prompts the user to save a copy of the tab page's XML file.
        /// </summary>
        public void SaveAs()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                dialog.FileName = Path.GetFileName(_filename);
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                _filename = dialog.FileName;
            }

            this.SaveAs(_filename);
        }

        /// <summary>
        /// Saves a copy of the tab page's XML file to the specified path.
        /// </summary>
        public void SaveAs(string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Default))
                {
                    writer.Formatting = Formatting.Indented;

                    if (_nodes == null)
                    {
                        this.xmlTreeView.Navigator.WriteSubtree(writer);
                    }
                    else
                    {
                        foreach (XPathNavigator node in _nodes)
                        {
                            switch (node.NodeType)
                            {
                                case XPathNodeType.Attribute:
                                    writer.WriteString(node.Value);
                                    writer.WriteWhitespace(Environment.NewLine);
                                    break;

                                default:
                                    node.WriteSubtree(writer);
                                    if (node.NodeType == XPathNodeType.Text)
                                        writer.WriteWhitespace(Environment.NewLine);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Event Handlers

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Middle)
            {
                this.Close();
            }
        }

        void xmlTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;
        }

        void xmlTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string text = string.Empty;

            TreeNode node = e.Item as TreeNode;

            if (node == null)
                return;

            text = node.Text;

            XPathNavigatorTreeNode xmlTreeNode = node as XPathNavigatorTreeNode;
            if (xmlTreeNode != null)
                text = GetXmlTreeNodeFormattedOuterXml(xmlTreeNode);

            if (!string.IsNullOrEmpty(text))
                this.xmlTreeView.DoDragDrop(text, DragDropEffects.Copy);
        }

        #endregion
    }
}

