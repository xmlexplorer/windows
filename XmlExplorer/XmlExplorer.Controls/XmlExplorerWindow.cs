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

namespace XmlExplorer.Controls
{
    public partial class XmlExplorerWindow : DockContent
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

        ///// <summary>
        ///// Occurs after a tab has been closed, and needs removed from the display.
        ///// </summary>
        //public event EventHandler<EventArgs> NeedsClosed;

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

        public XmlExplorerWindow()
            : base()
        {
            this.InitializeComponent();

            this.xmlTreeView.HideSelection = false;
            this.xmlTreeView.LabelEdit = true;
            this.xmlTreeView.AfterLabelEdit += new NodeLabelEditEventHandler(xmlTreeView_AfterLabelEdit);
            this.xmlTreeView.ItemDrag += new ItemDragEventHandler(xmlTreeView_ItemDrag);
        }

        public XmlExplorerWindow(string text)
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

        public List<ValidationEventArgs> ValidationEventArgs
        {
            get
            {
                return this.xmlTreeView.ValidationEventArgs;
            }
        }

        public string SchemaFileName
        {
            get
            {
                return this.xmlTreeView.SchemaFileName;
            }

            set
            {
                this.xmlTreeView.SchemaFileName = value;
            }
        }

        #endregion

        #region Methods

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
            //this.ToolTipText = filename;

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
            //this.ToolTipText = "This file was loaded from a stream (likely due to restricted permissions), and no filename is available.";

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
                Debug.WriteLine("Loading XPathDocument.");
                DateTime start = DateTime.Now;

                XmlReaderSettings readerSettings = new XmlReaderSettings();
                
                // load the document
                XmlReader reader = null;

                if (!string.IsNullOrEmpty(_filename))
                {
                    reader = XmlReader.Create(_filename, readerSettings);
                }
                else if (_stream != null)
                {
                    reader = XmlReader.Create(_filename, readerSettings);
                }

                XPathDocument document = new XPathDocument(reader);

                Debug.WriteLine(string.Format("Done. Elapsed: {0}ms.", DateTime.Now.Subtract(start).TotalMilliseconds));

                reader.Close();

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

            Debug.WriteLine("Loading UI.");
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
        public void Save(bool formatting)
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

            this.SaveAs(filename, formatting);
        }

        /// <summary>
        /// Prompts the user to save a copy of the tab page's XML file.
        /// </summary>
        public void SaveAs(bool formatting)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                dialog.FileName = Path.GetFileName(_filename);
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                _filename = dialog.FileName;
            }

            this.SaveAs(_filename, formatting);
        }

        /// <summary>
        /// Saves a copy of the tab page's XML file to the specified path.
        /// </summary>
        public void SaveAs(string filename, bool formatting)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Default))
                {
                    if (formatting)
                        writer.Formatting = Formatting.Indented;
                    else
                        writer.Formatting = Formatting.None;

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

        public List<ValidationEventArgs> ValidateSchema()
        {
            return this.xmlTreeView.Validate();
        }

        #endregion

        #region Overrides

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                CancelEventArgs args = new CancelEventArgs(false);

                // raise the BeforeClosed event to give the user the chance to cancel, if needed.
                if (this.BeforeClosed != null)
                    this.BeforeClosed(this, args);

                e.Cancel = args.Cancel;

                if (args.Cancel)
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
            finally
            {
                base.OnFormClosing(e);
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

