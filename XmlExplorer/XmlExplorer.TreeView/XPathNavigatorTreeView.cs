using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

using Wmhelp.XPath2;

namespace XmlExplorer.TreeView
{
    public class XPathNavigatorTreeView : System.Windows.Forms.TreeView, INotifyPropertyChanged
    {
        #region Variables

        private FileInfo _fileInfo;
        private Uri _uri;
        private XPath2NodeIterator _nodeIterator;
        private XmlNamespaceManager _xmlNamespaceManager;
        private List<NamespaceDefinition> _namespaceDefinitions;
        private int _defaultNamespaceCount;
        private List<Error> _errors;
        private bool _isLoading = true;
        private TimeSpan _loadTime;
        private bool _canValidate;
        private string _lastXPath;
        private XPath2NodeIterator _xpathResultsIterator;

        /// <summary>
        /// The XPathNavigator that represents the root of the tree.
        /// </summary>
        private XPathNavigator _navigator;

        /// <summary>
        /// A flag to bypass the custom drawing of syntax highlights,
        /// optionally used to improve performance on large documents.
        /// </summary>
        private bool _useSyntaxHighlighting = true;

        private DateTime _loadingStarted;

        /// <summary>
        /// Default StringFormat used for measuring and rendering TreeNode text.
        /// </summary>
        private StringFormat _stringFormat;

        /// <summary>
        /// A debug flag used to troubleshoot the rendering of TreeNode text.
        /// </summary>
        private bool _displayCharacterRangeBounds = false;

        #endregion

        #region Events

        public event EventHandler LoadingFinished;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new XPathNavigatorTreeView.
        /// </summary>
        public XPathNavigatorTreeView()
        {
            // to implement syntax highlighting, we'll be owner drawing the node text
            this.DrawMode = TreeViewDrawMode.OwnerDrawText;

            // this is required for sequentially rendering adjacent text, see the article
            // 'Why text appears different when drawn with GDIPlus versus GDI', section 'How to Display Adjacent Text'
            // http://support.microsoft.com/?id=307208 for more details
            _stringFormat = new StringFormat(StringFormat.GenericTypographic);
            _stringFormat.FormatFlags = _stringFormat.FormatFlags | StringFormatFlags.FitBlackBox | StringFormatFlags.NoWrap | StringFormatFlags.NoClip;
            _stringFormat.LineAlignment = StringAlignment.Center;

            // I always end up forgetting to turn this off each time I use a TreeView,
            // so I'll just take care of it here automatically.
            base.HideSelection = false;

            this.CopyNodeOnDrag = true;
            this.ItemDrag += this.OnItemDrag;
            this.AfterLabelEdit += this.OnAfterLabelEdit;

            this.AfterExpand += new TreeViewEventHandler(XPathNavigatorTreeView_AfterExpand);
            this.AfterCollapse += new TreeViewEventHandler(XPathNavigatorTreeView_AfterCollapse);
            this.DrawNode += new DrawTreeNodeEventHandler(XPathNavigatorTreeView_DrawNode);
        }

        #endregion

        #region Properties

        public static String NoSchemaWarning
        {
            get
            {
                return Properties.Resources.NoSchemaWarning;
            }
        }

        /// <summary>
        /// Gets the file, if any, this treeview is currently displaying.
        /// </summary>
        public FileInfo FileInfo
        {
            get { return _fileInfo; }
            set
            {
                _fileInfo = value;
                this.RaisePropertyChanged("FileInfo");
            }
        }

        public Uri Uri
        {
            get { return _uri; }

            set
            {
                _uri = value;
                this.RaisePropertyChanged("Uri");
            }
        }

        /// <summary>
        /// Gets the list of nodes, if any, this treeview is currently displaying.
        /// </summary>
        public XPath2NodeIterator NodeIterator
        {
            get { return _nodeIterator; }
            private set
            {
                _nodeIterator = value;
                this.RaisePropertyChanged("NodeIterator");
            }
        }

        /// <summary>
        /// Gets the XPathNavigator, this treeview is currently displaying.
        /// </summary>
        public XPathNavigator Navigator
        {
            get { return _navigator; }
            private set
            {
                _navigator = value;
                this.RaisePropertyChanged("Navigator");
            }
        }

        /// <summary>
        /// Gets or sets whether the custom drawing of syntax highlights
        /// should be bypassed. This can be optionally used to improve 
        /// performance on large documents.
        /// </summary>
        public bool UseSyntaxHighlighting
        {
            get
            {
                return _useSyntaxHighlighting;
            }

            set
            {
                _useSyntaxHighlighting = value;
                this.DrawMode = _useSyntaxHighlighting ? TreeViewDrawMode.OwnerDrawText : TreeViewDrawMode.Normal;

                this.RaisePropertyChanged("UseSyntaxHighlighting");
            }
        }

        public XmlNamespaceManager XmlNamespaceManager
        {
            get { return _xmlNamespaceManager; }
            private set
            {
                _xmlNamespaceManager = value;
                this.RaisePropertyChanged("XmlNamespaceManager");
            }
        }

        public List<NamespaceDefinition> NamespaceDefinitions
        {
            get { return _namespaceDefinitions; }
            private set
            {
                _namespaceDefinitions = value;
                this.RaisePropertyChanged("NamespaceDefinitions");
            }
        }

        public int DefaultNamespaceCount
        {
            get { return _defaultNamespaceCount; }
            private set
            {
                _defaultNamespaceCount = value;
                this.RaisePropertyChanged("DefaultNamespaceCount");
            }
        }

        public List<Error> Errors
        {
            get { return _errors; }
            private set
            {
                _errors = value;
                this.RaisePropertyChanged("Errors");
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                this.RaisePropertyChanged("IsLoading");
            }
        }

        public TimeSpan LoadTime
        {
            get { return _loadTime; }
            private set
            {
                _loadTime = value;
                this.RaisePropertyChanged("LoadTime");
            }
        }

        /// <summary>
        /// Gets whether the document, if any, specifies schema information and can, therefore, be validated.
        /// </summary>
        public bool CanValidate
        {
            get { return _canValidate; }
            private set
            {
                _canValidate = value;
                this.RaisePropertyChanged("CanValidate");
            }
        }

        public bool CopyNodeOnDrag { get; set; }

        public int CurrentResultPosition { get; private set; }
        public int ResultCount { get; private set; }

        public ChildNodeDefinitionCollection ChildNodeDefinitions { get; set; }

        #endregion

        #region Methods

        public void Reload()
        {
            if (this.FileInfo != null)
            {
                this.BeginOpen(this.FileInfo);
            }
        }

        /// <summary>
        /// Load the specified XML file into the tree.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="OnFinished"></param>
        /// <param name="OnException"></param>
        public void BeginOpen(FileInfo fileInfo)
        {
            this._loadingStarted = DateTime.Now;

            this.IsLoading = true;

            this.FileInfo = fileInfo;

            ThreadStart start = delegate()
            {
                try
                {
                    _navigator = ReadXPathNavigator(fileInfo);

                    this.LoadNamespaceDefinitions(_navigator);
                    this.Validate(_navigator);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);

                    this.AddError(ex.Message);
                }
                finally
                {
                    this.Invoke(new MethodInvoker(this.LoadNavigator));

                    this.OnLoadingFinished(EventArgs.Empty);
                }
            };
            new Thread(start).Start();
        }

        /// <summary>
        /// Loads the specified XML data into the tree.
        /// </summary>
        /// <param name="xml">XML string data to load.</param>
        public void BeginOpen(string xml)
        {
            this._loadingStarted = DateTime.Now;

            this.IsLoading = true;

            this.FileInfo = null;

            ThreadStart start = delegate()
            {
                try
                {
                    _navigator = ReadXPathNavigator(xml);

                    this.LoadNamespaceDefinitions(_navigator);
                    this.Validate(_navigator);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);

                    this.AddError(ex.Message);
                }
                finally
                {
                    this.Invoke(new MethodInvoker(this.LoadNavigator));

                    this.OnLoadingFinished(EventArgs.Empty);
                }
            };
            new Thread(start).Start();
        }

        /// <summary>
        /// Loads the specified XML data into the tree.
        /// </summary>
        /// <param name="bytes">XML data to load.</param>
        public void BeginOpen(byte[] bytes)
        {
            this._loadingStarted = DateTime.Now;

            this.IsLoading = true;

            this.FileInfo = null;

            ThreadStart start = delegate()
            {
                try
                {
                    _navigator = ReadXPathNavigator(bytes);

                    this.LoadNamespaceDefinitions(_navigator);
                    this.Validate(_navigator);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);

                    this.AddError(ex.Message);
                }
                finally
                {
                    this.Invoke(new MethodInvoker(this.LoadNavigator));

                    this.OnLoadingFinished(EventArgs.Empty);
                }
            };
            new Thread(start).Start();
        }

        public void Clear()
        {
            this.FileInfo = null;
            this.Navigator = null;
            this.NodeIterator = null;
            this.BeginUpdate();
            try
            {
                this.Nodes.Clear();
            }
            finally
            {
                this.EndUpdate();
            }
        }

        private void LoadNavigator()
        {
            XPathNavigatorTreeNode node = null;

            this.UseWaitCursor = true;

            // suspend drawing of the tree while loading nodes to improve performance
            // as adding each node would normally require an entire redraw of the tree
            base.BeginUpdate();
            try
            {
                // clear all the nodes in the tree
                base.Nodes.Clear();

                // setting the navigator to null should just clear the tree
                // no exception needs thrown
                if (_navigator != null)
                {
                    // if it's the root node of the document, load it's children
                    // we don't display the root
                    if (_navigator.NodeType == XPathNodeType.Root)
                    {
                        this.LoadNodes(_navigator.SelectChildren(XPathNodeType.Element), base.Nodes);
                    }
                    else
                    {
                        // otherwise, create a tree node and load it
                        node = new XPathNavigatorTreeNode(_navigator);
                        base.Nodes.Add(node);
                    }
                }
            }
            finally
            {
                if (node != null)
                {
                    node.Expand();
                }

                // resume drawing of the tree
                base.EndUpdate();

                this.UseWaitCursor = false;

                this.IsLoading = false;
                this.LoadTime = DateTime.Now - this._loadingStarted;
            }
        }

        private static XPathNavigator ReadXPathNavigator(FileInfo fileInfo)
        {
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ConformanceLevel = ConformanceLevel.Auto;
            readerSettings.DtdProcessing = DtdProcessing.Ignore;

            XPathDocument document = null;

            using (XmlReader reader = XmlReader.Create(fileInfo.FullName, readerSettings))
            {
                document = new XPathDocument(reader);
            }

            XPathNavigator navigator = document.CreateNavigator();

            return navigator;
        }

        private static XPathNavigator ReadXPathNavigator(string xml)
        {
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ConformanceLevel = ConformanceLevel.Fragment;

            XPathDocument document = null;

            using (StringReader stringReader = new StringReader(xml))
            {
                using (XmlReader reader = XmlReader.Create(stringReader, readerSettings))
                {
                    document = new XPathDocument(reader);
                }
            }

            XPathNavigator navigator = document.CreateNavigator();

            return navigator;
        }

        private static XPathNavigator ReadXPathNavigator(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return ReadXPathNavigator(stream);
            }
        }

        private static XPathNavigator ReadXPathNavigator(MemoryStream stream)
        {
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ConformanceLevel = ConformanceLevel.Fragment;

            using (XmlReader reader = XmlReader.Create(stream, readerSettings))
            {
                XPathDocument document = null;

                document = new XPathDocument(reader);

                XPathNavigator navigator = document.CreateNavigator();

                return navigator;
            }
        }

        /// <summary>
        /// Loads an XPathNavigatorTreeNode for each XPathNavigator in the specified XPathNodeIterator, into the TreeView.
        /// </summary>
        /// <param name="iterator"></param>
        public void LoadNodes(XPath2NodeIterator iterator)
        {
            this._loadingStarted = DateTime.Now;

            this.UseWaitCursor = true;

            // suspend drawing of the tree while loading nodes to improve performance
            // as adding each node would normally require an entire redraw of the tree
            base.BeginUpdate();
            try
            {
                this.NodeIterator = iterator;

                this.LoadNodes(iterator, this.Nodes);
            }
            finally
            {
                // resume drawing of the tree
                base.EndUpdate();

                this.UseWaitCursor = false;

                this.IsLoading = false;
                this.LoadTime = DateTime.Now - this._loadingStarted;
            }
        }

        /// <summary>
        /// Loads an XPathNavigatorTreeNode for each XPathNavigator in the
        /// specified XPathNode2Iterator, into the specified
        /// TreeNodeCollection. If an item in the iterator is not an instance
        /// of the XPathNavigator, then basic TreeNode with item's text is
        /// created instead.
        /// </summary>
        /// <param name="iterator"></param>
        /// <param name="treeNodeCollection"></param>
        private void LoadNodes(XPath2NodeIterator iterator, TreeNodeCollection treeNodeCollection)
        {
            // handle null arguments
            if (iterator == null)
                throw new ArgumentNullException("iterator");

            if (treeNodeCollection == null)
                throw new ArgumentNullException("treeNodeCollection");

            treeNodeCollection.Clear();

            List<TreeNode> nodes = new List<TreeNode>();

            // create and add a node for each navigator
            foreach (object item in iterator)
            {
                var navigator = item as XPathNavigator;

                TreeNode node;

                if (navigator != null)
                {
                    bool hasCustomChildNodes = this.HasCustomChildNodes(navigator, this.ChildNodeDefinitions);

                    node = new XPathNavigatorTreeNode(navigator.Clone(), hasCustomChildNodes);
                }
                else
                {
                    node = new TreeNode(item.ToString());
                }

                nodes.Add(node);
            }

            treeNodeCollection.AddRange(nodes.ToArray());
        }

        /// <summary>
        /// Loads an XPathNavigatorTreeNode for each XPathNavigator in the specified XPathNodeIterator, into the 
        /// specified TreeNodeCollection.
        /// </summary>
        /// <param name="iterator"></param>
        /// <param name="treeNodeCollection"></param>
        private void LoadNodes(XPathNodeIterator iterator, TreeNodeCollection treeNodeCollection)
        {
            // handle null arguments
            if (iterator == null)
                throw new ArgumentNullException("iterator");

            if (treeNodeCollection == null)
                throw new ArgumentNullException("treeNodeCollection");

            treeNodeCollection.Clear();

            List<TreeNode> nodes = new List<TreeNode>();

            // create and add a node for each navigator
            foreach (XPathNavigator navigator in iterator)
            {
                bool hasCustomChildNodes = this.HasCustomChildNodes(navigator, this.ChildNodeDefinitions);

                XPathNavigatorTreeNode node = new XPathNavigatorTreeNode(navigator.Clone(), hasCustomChildNodes);

                nodes.Add(node);
            }

            treeNodeCollection.AddRange(nodes.ToArray());
        }

        private bool HasCustomChildNodes(XPathNavigator navigator, ChildNodeDefinitionCollection childNodeDefinitions)
        {
            if (childNodeDefinitions == null)
                return false;

            foreach (var childNodeDefinition in childNodeDefinitions)
            {
                var iterator = this.GetCustomChildNodes(navigator, childNodeDefinition);

                if (iterator != null && iterator.Count > 0)
                    return true;
            }

            return false;
        }

        private void LoadCustomChildNodes(XPathNavigatorTreeNode node, ChildNodeDefinitionCollection childNodeDefinitions)
        {
            if (childNodeDefinitions == null)
                return;

            foreach (var childNodeDefinition in childNodeDefinitions)
            {
                var iterator = this.GetCustomChildNodes(node.Navigator, childNodeDefinition);

                if (iterator == null)
                    continue;

                List<TreeNode> nodes = new List<TreeNode>();

                // create and add a node for each navigator
                foreach (XPathNavigator navigator in iterator)
                {
                    bool hasCustomChildNodes = this.HasCustomChildNodes(navigator, this.ChildNodeDefinitions);

                    XPathNavigatorTreeNode childNode = new XPathNavigatorTreeNode(navigator.Clone(), hasCustomChildNodes, childNodeDefinition.IdXpath);

                    nodes.Add(childNode);
                }

                node.Nodes.AddRange(nodes.ToArray());
            }
        }

        private XPathNodeIterator GetCustomChildNodes(XPathNavigator navigator, ChildNodeDefinition childNodeDefinition)
        {
            try
            {
                // get the id of the child node
                string id = navigator.Evaluate("string(" + childNodeDefinition.IdXpath + ")") as string;
                if (string.IsNullOrEmpty(id))
                    return null;

                // get the child xpath
                string xpath = string.Format(childNodeDefinition.ChildXpath, id);

                // get any nodes that match the child xpath 
                XPathNodeIterator iterator = navigator.Select(xpath);

                return iterator;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                return null;
            }
        }

        private void LoadNamespaceDefinitions(XPathNavigator navigator)
        {
            if (navigator == null)
            {
                this.XmlNamespaceManager = null;
                return;
            }

            this.XmlNamespaceManager = new XmlNamespaceManager(navigator.NameTable);

            XPathNodeIterator namespaces = navigator.Evaluate(@"//namespace::*[not(. = ../../namespace::*)]") as XPathNodeIterator;

            this.NamespaceDefinitions = new List<NamespaceDefinition>();

            this.DefaultNamespaceCount = 0;

            // add any namespaces within the scope of the navigator to the namespace manager
            foreach (var namespaceElement in namespaces)
            {
                XPathNavigator namespaceNavigator = namespaceElement as XPathNavigator;
                if (namespaceNavigator == null)
                    continue;

                NamespaceDefinition definition = new NamespaceDefinition()
                {
                    Prefix = namespaceNavigator.Name,
                    Namespace = namespaceNavigator.Value,
                };

                if (string.IsNullOrEmpty(definition.Prefix))
                {
                    if (DefaultNamespaceCount > 0)
                        definition.Prefix = "default" + (this.DefaultNamespaceCount + 1).ToString();
                    else
                        definition.Prefix = "default";

                    this.DefaultNamespaceCount++;
                }

                this.NamespaceDefinitions.Add(definition);

                this.XmlNamespaceManager.AddNamespace(definition.Prefix, definition.Namespace);
            }
        }

        /// <summary>
        /// Performs validation against any schemas provided in the document, if any.
        /// If validation is not possible, because no schemas are provided, an InvalidOperationException is thrown.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException" />
        public List<Error> Validate(XPathNavigator navigator, string[] schemaFilenames = null)
        {
            try
            {
                this.CanValidate = false;

                this.Errors = new List<Error>();

                if (navigator == null)
                    navigator = this.Navigator;

                if (navigator == null)
                    return null;

                string xsiPrefix = this.XmlNamespaceManager.LookupPrefix("http://www.w3.org/2001/XMLSchema-instance");

                XmlSchemaSet schemas = new XmlSchemaSet();

                if (!string.IsNullOrEmpty(xsiPrefix))
                {
                    /*
					 * I can't believe I have to do this manually, but, here we go...
					 * 
					 * When loading a document with an XmlReader and performing validation, it will automatically
					 * detect schemas specified in the document with @xsi:schemaLocation and @xsi:noNamespaceSchemaLocation attributes.
					 * We get line number and column, but not a reference to the actual offending xml node or xpath navigator.
					 * 
					 * When validating an xpath navigator, it ignores these attributes, doesn't give us line number or column,
					 * but does give us the offending xpath navigator.
					 * 
					 * */
                    foreach (var schemaAttribute in navigator.Select(
                         string.Format("//@{0}:noNamespaceSchemaLocation", xsiPrefix),
                         this.XmlNamespaceManager))
                    {
                        XPathNavigator attributeNavigator = schemaAttribute as XPathNavigator;
                        if (attributeNavigator == null)
                            continue;

                        string value = attributeNavigator.Value;
                        value = this.ResolveSchemaFileName(value);

                        // add the schema
                        schemas.Add(null, value);
                    }
                    foreach (var schemaAttribute in navigator.Select(
                         string.Format("//@{0}:schemaLocation", xsiPrefix),
                         this.XmlNamespaceManager))
                    {
                        XPathNavigator attributeNavigator = schemaAttribute as XPathNavigator;
                        if (attributeNavigator == null)
                            continue;

                        string value = attributeNavigator.Value;

                        List<KeyValuePair<string, string>> namespaceDefs = this.ParseNamespaceDefinitions(value);

                        foreach (var pair in namespaceDefs)
                            schemas.Add(pair.Key, pair.Value);
                    }
                }

                if (schemaFilenames != null && schemaFilenames.Length > 0)
                {
                    // add any manually specified schema files
                    foreach (string schemaFilename in schemaFilenames)
                    {
                        schemas.Add(null, this.ResolveSchemaFileName(schemaFilename));
                    }
                }

                if (schemas.Count < 1)
                {
                    this.AddError(Properties.Resources.NoSchemaWarning, XmlSeverityType.Warning);
                    return this.Errors;
                }

                // validate the document
                navigator.CheckValidity(schemas, this.OnValidationEvent);

                this.CanValidate = true;
            }
            catch (FileNotFoundException ex)
            {
                string message = string.Format(
                     "Cannot find the schema document at '{0}'", ex.FileName);
                this.AddError(message);
            }
            catch (Exception ex)
            {
                this.AddError(ex.Message);
            }

            return this.Errors;
        }

        private List<KeyValuePair<string, string>> ParseNamespaceDefinitions(string value)
        {
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();

            // the value should be like: http://www.w3schools.com note.xsd
            // namespace path
            // split it on space
            string[] values = value.Split(" ".ToCharArray());

            for (int i = 0; i < values.Length; i += 2)
            {
                // first value is namespace (it's not supposed to have spaces in it)
                string targetNamespace = values[i];

                // second value should be the filename
                string schemaFileName = null;
                if (i < values.Length - 1)
                {
                    schemaFileName = values[i + 1];
                    schemaFileName = this.ResolveSchemaFileName(schemaFileName);
                }

                KeyValuePair<string, string> pair = new KeyValuePair<string, string>(targetNamespace, schemaFileName);

                pairs.Add(pair);
            }

            return pairs;
        }

        private string ResolveSchemaFileName(string schemaFileName)
        {
            if (!File.Exists(schemaFileName))
            {
                // try prepending the current file's path
                if (this.FileInfo != null)
                {
                    string filename = Path.Combine(this.FileInfo.DirectoryName, schemaFileName);
                    if (File.Exists(filename))
                        return filename;
                }
            }
            return schemaFileName;
        }

        /// <summary>
        /// Finds and selects an XPathNavigatorTreeNode for the given XPathNavigator.
        /// </summary>
        /// <param name="navigator">An XPathNavigator to find and select in the tree.</param>
        /// <returns></returns>
        public bool SelectXmlTreeNode(XPathNavigator navigator)
        {
            if (navigator == null)
                throw new ArgumentNullException("navigator");

            // we've found a node, so build an ancestor stack
            Stack<XPathNavigator> ancestors = this.GetAncestors(navigator);

            // now find treenodes to match the ancestors
            TreeNode treeNode = this.GetXmlTreeNode(ancestors);

            if (treeNode == null)
                return false;

            // select the node
            this.SelectedNode = treeNode;

            return true;
        }

        /// <summary>
        /// Finds a TreeNode for a given stack of XPathNavigators.
        /// </summary>
        /// <param name="ancestors">A Stack of XPathNavigators with which to find a TreeNode.</param>
        /// <returns></returns>
        private TreeNode GetXmlTreeNode(Stack<XPathNavigator> ancestors)
        {
            XPathNavigator navigator = null;

            // start at the root
            TreeNodeCollection nodes = this.Nodes;

            TreeNode treeNode = null;

            // loop through the ancestor XPathNavigators
            while (ancestors.Count > 0 && (navigator = ancestors.Pop()) != null)
            {
                // loop through the TreeNodes at the current level
                foreach (TreeNode node in nodes)
                {
                    // make sure it's an XPathNavigatorTreeNode
                    XPathNavigatorTreeNode xmlTreeNode = node as XPathNavigatorTreeNode;
                    if (xmlTreeNode == null)
                        continue;

                    // check to see if we've found the correct TreeNode
                    if (xmlTreeNode.Navigator.IsSamePosition(navigator))
                    {
                        // expand the tree node, if it hasn't alreay been expanded
                        if (!node.IsExpanded)
                            node.Expand();

                        // we've taken another step towards the target node
                        treeNode = node;

                        // update the current level
                        nodes = node.Nodes;

                        // handle the next level, if any
                        break;
                    }
                }
            }

            // return the result, if any was found
            return treeNode;
        }

        /// <summary>
        /// Builds and returns a Stack of XPathNavigator ancestors for a given XPathNavigator.
        /// </summary>
        /// <param name="navigator">The XPathNavigator from which to build a stack.</param>
        /// <returns></returns>
        private Stack<XPathNavigator> GetAncestors(XPathNavigator navigator)
        {
            if (navigator == null)
                throw new ArgumentNullException("navigator");

            Stack<XPathNavigator> ancestors = new Stack<XPathNavigator>();

            // navigate up the xml tree, building the stack as we go
            while (navigator != null)
            {
                // push the current ancestor onto the stack
                ancestors.Push(navigator);

                // clone the current navigator cursor, so we don't lose our place
                navigator = navigator.Clone();

                // if we've reached the top, we're done
                if (!navigator.MoveToParent())
                    break;

                // if we've reached the root, we're done
                if (navigator.NodeType == XPathNodeType.Root)
                    break;
            }

            // return the result
            return ancestors;
        }

        /// <summary>
        /// Evaluates the XPath expression and returns the typed result.
        /// </summary>
        /// <param name="xpath">A string representing an XPath expression that can be evaluated.</param>
        /// <returns></returns>
        public object SelectXmlNodes(string xpath)
        {
            // get the selected node
            XPathNavigatorTreeNode node = this.SelectedNode as XPathNavigatorTreeNode;

            // if there is no selected node, default to the root node
            if (node == null && this.Nodes.Count > 0)
                node = this.GetRootXmlTreeNode();

            if (node == null)
                return null;

            // evaluate the expression, return the result
            return node.Navigator.XPath2Evaluate(xpath, this.XmlNamespaceManager);
        }

        /// <summary>
        /// Returns the root XPathNavigatorTreeNode in the tree.
        /// </summary>
        /// <returns></returns>
        private XPathNavigatorTreeNode GetRootXmlTreeNode()
        {
            foreach (TreeNode node in this.Nodes)
            {
                XPathNavigatorTreeNode xmlTreeNode = node as XPathNavigatorTreeNode;

                if (xmlTreeNode == null || xmlTreeNode.Navigator == null)
                    continue;

                if (xmlTreeNode.Navigator.NodeType != XPathNodeType.Element)
                    continue;

                return xmlTreeNode;
            }

            return this.Nodes[0] as XPathNavigatorTreeNode;
        }

        /// <summary>
        /// Finds and selects an XPathNavigatorTreeNode using an XPath expression.
        /// </summary>
        /// <param name="xpath">An XPath expression.</param>
        /// <returns></returns>
        public bool FindByXpath(string xpath)
        {
            return this.FindByXpath(xpath, Direction.Down);
        }

        public bool FindByXpath(string xpath, Direction direction)
        {
            if (this.CurrentResultPosition < 2 && direction == Direction.Up)
                throw new InvalidOperationException(Properties.Resources.CannotMoveUpError);

            object result = null;

            // no need to re-evaluate the expression results if the xpath hasn't changed since last time, and they're not searching up
            if (xpath != _lastXPath || direction == Direction.Up || _xpathResultsIterator == null)
            {
                // have to re-evaluate the expression, so the iterator is reset at -1
                result = this.SelectXmlNodes(xpath);

                if (xpath != _lastXPath || _xpathResultsIterator == null)
                {
                    this.CurrentResultPosition = 0;
                    this.ResultCount = 0;

                    _lastXPath = xpath;
                }

                if (result == null)
                    return false;

                // did the expression evaluate to a node set?
                _xpathResultsIterator = result as XPath2NodeIterator;
            }

            if (_xpathResultsIterator != null)
            {
                // the expression evaluated to a node set
                this.ResultCount = _xpathResultsIterator.Count;

                if (this.ResultCount < 1)
                    return false;

                if (direction == Direction.Up)
                {
                    this.CurrentResultPosition--;

                    for (int i = 0; i < this.CurrentResultPosition; i++)
                    {
                        if (!_xpathResultsIterator.MoveNext())
                            break;
                    }
                }
                else
                {
                    if (_xpathResultsIterator.MoveNext())
                        this.CurrentResultPosition++;
                }

                // select the first node in the set
                if (_xpathResultsIterator.Current is XPathNavigator)
                    return this.SelectXmlTreeNode((XPathNavigator)_xpathResultsIterator.Current);
            }

            // the expression evaluated to something else, most likely a count()
            // show the result in a new window
            ExpressionResultsWindow dialog = new ExpressionResultsWindow();
            dialog.Expression = xpath;
            dialog.Result = result.ToString();
            dialog.ShowDialog(this.FindForm());
            return true;
        }

        /// <summary>
        /// Returns a string representing the full path of an XPathNavigator.
        /// </summary>
        /// <param name="navigator">An XPathNavigator.</param>
        /// <returns></returns>
        public string GetXmlNodeFullPath(XPathNavigator navigator)
        {
            if (navigator == null)
                return null;

            // create a StringBuilder for assembling the path
            StringBuilder sb = new StringBuilder();

            // clone the navigator (cursor), so the node doesn't lose it's place
            navigator = navigator.Clone();

            // traverse the navigator's ancestry all the way to the top
            while (navigator != null)
            {
                // skip anything but elements
                if (navigator.NodeType == XPathNodeType.Element)
                {
                    // insert the node and a seperator
                    sb.Insert(0, navigator.Name);
                    sb.Insert(0, "/");
                }
                if (!navigator.MoveToParent())
                    break;
            }

            return sb.ToString();
        }

        public List<KeyValuePair<string, string>> GetAttributeXPaths(XPathNavigator navigator)
        {
            XPathNavigator n = navigator.Clone();
            if (!n.HasAttributes)
                return null;

            string xpath = this.GetXmlNodeFullPath(navigator);
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();

            // first attribute
            n.MoveToFirstAttribute();
            pairs.Add(this.FormatNameAndXPath(xpath, n.Name, n.Value));

            // subsequent attributes
            while (n.MoveToNextAttribute())
                pairs.Add(this.FormatNameAndXPath(xpath, n.Name, n.Value));

            return pairs;
        }

        public KeyValuePair<string, string> FormatNameAndXPath(string nodeXPath, string name, string value)
        {
            string displayName = "@" + name;
            string attributeValue = string.IsNullOrWhiteSpace(value) ? "value" : value;
            string xpath = string.Format("{0}[@{1}='{2}']", nodeXPath, name, attributeValue);

            // key=@name, value=/a/b/c[@name='value']
            return new KeyValuePair<string, string>(displayName, xpath);
        }

        private void OnValidationEvent(object sender, ValidationEventArgs e)
        {
            Error error = new Error();
            error.Description = e.Message;
            error.Category = e.Severity;

            XmlSchemaValidationException exception = e.Exception as XmlSchemaValidationException;
            if (exception != null)
            {
                error.SourceObject = exception.SourceObject;
            }

            this.AddError(error);
        }

        public void AddError(string description)
        {
            this.AddError(description, XmlSeverityType.Error);
        }

        private void AddError(string description, XmlSeverityType category)
        {
            Error error = new Error();
            error.Description = description;
            error.Category = category;

            this.AddError(error);
        }

        private void AddError(Error error)
        {
            if (this.FileInfo == null)
                error.File = Properties.Resources.Untitled;
            else
                error.File = this.FileInfo.FullName;

            if (this.Errors == null)
                this.Errors = new List<Error>();

            this.Errors.Add(error);

            error.DefaultOrder = this.Errors.Count;
        }

        /// <summary>
        /// Copies the current selected xml node (and all of it's sub nodes) to the clipboard
        /// as formatted XML text.
        /// </summary>
        public void CopyFormattedOuterXml()
        {
            XPathNavigatorTreeNode selected = this.SelectedNode as XPathNavigatorTreeNode;

            if (selected == null)
                return;

            string text = GetXmlTreeNodeFormattedOuterXml(selected);

            if (!string.IsNullOrEmpty(text))
                Clipboard.SetText(text);
        }

        public Stream GetFormattedOuterXmlStream()
        {
            XPathNavigatorTreeNode selected = this.GetRootXmlTreeNode();

            if (selected == null)
                return null;

            Stream stream = GetXPathNavigatorFormattedOuterXmlStream(selected.Navigator);

            return stream;
        }

        /// <summary>
        /// Copies the current selected xml node to the clipboard as XML text.
        /// </summary>
        public void CopyNodeText()
        {
            XPathNavigatorTreeNode selected = this.SelectedNode as XPathNavigatorTreeNode;

            if (selected == null)
                return;

            string text = selected.Text;

            if (!string.IsNullOrEmpty(text))
                Clipboard.SetText(text);
        }

        public string GetNodeTextBase64Decoded()
        {
            XPathNavigatorTreeNode selected = this.SelectedNode as XPathNavigatorTreeNode;
            if (selected != null)
            {
                byte[] bytes = Convert.FromBase64String(selected.Text);
                if (bytes != null)
                    return Encoding.UTF8.GetString(bytes);
            }
            return string.Empty;
        }

        public string GetNodeValueBase64Decoded()
        {
            XPathNavigatorTreeNode selected = this.SelectedNode as XPathNavigatorTreeNode;
            if (selected != null)
            {
                byte[] bytes = Convert.FromBase64String(selected.Navigator.Value);
                if (bytes != null)
                    return Encoding.UTF8.GetString(bytes);
            }
            return string.Empty;
        }

        /// <summary>
        /// Returns the selected xml node (and all of it's sub nodes) as formatted XML text.
        /// </summary>
        public string GetXmlTreeNodeFormattedOuterXml(XPathNavigatorTreeNode node)
        {
            return this.GetXPathNavigatorFormattedOuterXml(node.Navigator);
        }

        public string GetXPathNavigatorFormattedOuterXml(XPathNavigator navigator)
        {
            StringBuilder stringBuilder = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;

            using (XmlWriter writer = XmlTextWriter.Create(stringBuilder, settings))
            {
                navigator.WriteSubtree(writer);

                writer.Flush();

                return stringBuilder.ToString();
            }
        }

        public Stream GetXPathNavigatorFormattedOuterXmlStream(XPathNavigator navigator)
        {
            Stream stream = new MemoryStream();

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;

            using (XmlWriter writer = XmlTextWriter.Create(stream, settings))
            {
                navigator.WriteSubtree(writer);

                writer.Flush();
            }

            if (stream.CanSeek)
                stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public new void ExpandAll()
        {
            TreeNode selected = this.SelectedNode;

            if (selected == null)
            {
                if (this.Nodes.Count > 0)
                    selected = this.Nodes[0];
                else
                    return;
            }

            try
            {
                this.BeginUpdate();

                selected.ExpandAll();
            }
            finally
            {
                this.EndUpdate();
            }
        }

        public new void CollapseAll()
        {
            TreeNode selected = this.SelectedNode as TreeNode;

            if (selected == null)
            {
                if (this.Nodes.Count > 0)
                    selected = this.Nodes[0];
                else
                    return;
            }

            try
            {
                this.BeginUpdate();

                this.CollapseAll(selected);
            }
            finally
            {
                this.EndUpdate();
            }
        }

        private void CollapseAll(TreeNode treeNode)
        {
            if (treeNode == null)
                return;

            if (treeNode.Nodes != null)
            {
                foreach (TreeNode childNode in treeNode.Nodes)
                {
                    this.CollapseAll(childNode);
                }
            }

            if (treeNode.IsExpanded)
                treeNode.Collapse();
        }

        /// <summary>
        /// Overwrites the tab page's file with XML formatting (tabs and crlf's)
        /// </summary>
        public void Save(bool formatting)
        {
            if (this.FileInfo == null)
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                    if (dialog.ShowDialog(this) != DialogResult.OK)
                        return;

                    this.FileInfo = new System.IO.FileInfo(dialog.FileName);
                }
            }

            this.SaveAs(this.FileInfo.FullName, formatting);
        }

        /// <summary>
        /// Prompts the user to save a copy of the tab page's XML file.
        /// </summary>
        public void SaveAs(bool formatting)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

                if (this.FileInfo != null)
                {
                    dialog.FileName = Path.GetFileName(this.FileInfo.FullName);

                    if (string.Compare(this.FileInfo.Extension, ".xml", true) != 0)
                        dialog.Filter = string.Format("{0} Files (*{1})|*{1}|", this.FileInfo.Extension.TrimStart('.').ToUpper(), this.FileInfo.Extension) + dialog.Filter;
                }

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                this.FileInfo = new FileInfo(dialog.FileName);
            }

            this.SaveAs(this.FileInfo.FullName, formatting);
        }

        /// <summary>
        /// Saves a copy of the tab page's XML file to the specified path.
        /// </summary>
        public void SaveAs(string filename, bool formatting)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                this.Save(stream, formatting);
            }
        }

        private void Save(Stream stream, bool formatting)
        {
            XmlWriterSettings settings = new XmlWriterSettings();

            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = false;
            settings.Indent = formatting;

            using (XmlWriter writer = XmlTextWriter.Create(stream, settings))
            {
                if (this.Navigator != null)
                {
                    this.Navigator.WriteSubtree(writer);
                }
                else if (this.NodeIterator != null)
                {
                    foreach (XPathItem item in this.NodeIterator)
                    {
                        XPathNavigator node = item as XPathNavigator; 
                        if (node != null)
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
                        else
                        {
                            writer.WriteString(item.Value);
                            writer.WriteWhitespace(Environment.NewLine);
                        }
                    }
                }

                writer.Flush();
            }
        }

        public virtual void OnLoadingFinished(EventArgs e)
        {
            this.IsLoading = false;

            if (this.LoadingFinished != null)
                this.LoadingFinished(this, e);
        }

        private bool DrawStrings(string text, Rectangle bounds, Graphics graphics)
        {
            bool drawn = false;

            // draw delimiters
            Color color = Color.FromArgb(0, 0, 255);
            drawn = this.DrawStrings(text, bounds, graphics, color, RegularExpressions.Xml, "Delimiter");

            // draw attribute values
            drawn |= this.DrawStrings(text, bounds, graphics, color, RegularExpressions.Xml, "AttributeValue");

            // draw text
            color = SystemColors.ControlText;
            drawn |= this.DrawStrings(text, bounds, graphics, color, RegularExpressions.Xml, "Text");

            // draw name
            color = Color.FromArgb(163, 21, 21);
            drawn |= this.DrawStrings(text, bounds, graphics, color, RegularExpressions.Xml, "Name");

            // draw attribute names
            color = Color.FromArgb(255, 0, 0);
            drawn |= this.DrawStrings(text, bounds, graphics, color, RegularExpressions.Xml, "AttributeName");

            // draw comment delimiters
            color = Color.FromArgb(0, 0, 255);
            drawn |= this.DrawStrings(text, bounds, graphics, color, RegularExpressions.Comments, "Delimiter");

            // draw comments
            color = Color.FromArgb(0, 128, 0);
            drawn |= this.DrawStrings(text, bounds, graphics, color, RegularExpressions.Comments, "Comments");

            return drawn;
        }

        private bool DrawStrings(string text, Rectangle bounds, Graphics graphics, Color color, Regex regex, string groupName)
        {
            if (!regex.IsMatch(text))
                return false;

            MatchCollection matches = regex.Matches(text);

            bool result = false;

            foreach (Match match in matches)
            {
                using (SolidBrush brush = new SolidBrush(color))
                {
                    foreach (Capture capture in match.Groups[groupName].Captures)
                    {
                        // create a character range for the capture
                        CharacterRange[] characterRanges = new CharacterRange[] {
                                          new CharacterRange(capture.Index, capture.Length)};

                        // create a new string format, using the default one as a prototype
                        StringFormat stringFormat = new StringFormat(_stringFormat);

                        stringFormat.SetMeasurableCharacterRanges(characterRanges);

                        // measure the character ranges for the capture, getting an array of regions
                        Region[] regions = new Region[1];
                        regions = graphics.MeasureCharacterRanges(text, this.Font, bounds, stringFormat);

                        // Draw each measured string within each region.
                        foreach (Region region in regions)
                        {
                            RectangleF rectangle = region.GetBounds(graphics);

                            graphics.DrawString(capture.Value, this.Font, brush, rectangle, _stringFormat);

                            // draw character range bounding rectangles, for troubleshooting only
                            if (_displayCharacterRangeBounds)
                                using (Pen pen = new Pen(brush.Color))
                                    graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

                            result = true;
                        }
                    }
                }
            }

            return result;
        }

        private void HandleException(Exception exception)
        {
            try
            {
                Debug.WriteLine(exception);
                ExceptionDialog.ShowDialog(this, exception);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #endregion

        #region Event Handlers

        private void OnItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                if (!this.CopyNodeOnDrag)
                    return;

                string text = string.Empty;

                TreeNode node = e.Item as TreeNode;

                if (node == null)
                    return;

                text = node.Text;

                XPathNavigatorTreeNode xmlTreeNode = node as XPathNavigatorTreeNode;
                if (xmlTreeNode != null)
                    text = GetXmlTreeNodeFormattedOuterXml(xmlTreeNode);

                if (!string.IsNullOrEmpty(text))
                    this.DoDragDrop(text, DragDropEffects.Copy);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ExceptionDialog.ShowDialog(this, ex);
            }
        }

        private void OnAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;
        }

        void XPathNavigatorTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            try
            {
                //// remove the end tag we inserted when the node was expanded
                //TreeNode node = e.Node.Tag as TreeNode;
                //if (node != null)
                //{
                //   // remove it
                //   TreeNodeCollection nodes = base.Nodes;
                //   if (node.Parent != null)
                //   {
                //      nodes = node.Parent.Nodes;
                //   }
                //   nodes.Remove(node);
                //}
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }

        void XPathNavigatorTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            try
            {
                using (WaitCursorProvider provider = new WaitCursorProvider())
                {
                    XPathNavigatorTreeNode node = e.Node as XPathNavigatorTreeNode;

                    if (node == null)
                        return;

                    // for better performance opening large files, tree nodes are loaded on demand.
                    if (!node.HasExpanded)
                    {
                        // make sure we don't have to load this tree node again.
                        node.HasExpanded = true;

                        // load the child nodes of the specified xml tree node
                        this.LoadNodes(node.Navigator.SelectChildren(XPathNodeType.All), node.Nodes);

                        // load any custom child node definitions
                        this.LoadCustomChildNodes(node, this.ChildNodeDefinitions);
                    }
                }

                // // add the end tag node for the currently expanding node
                //if (node.Nodes.Count > 0)
                //{
                //   TreeNodeCollection nodes = base.Nodes;
                //   if (node.Parent != null)
                //   {
                //      nodes = node.Parent.Nodes;
                //   }

                //   var endTagNode = node.Tag as TreeNode;
                //   if (endTagNode == null)
                //   {
                //      endTagNode = new TreeNode(string.Format("</{0}>", node.Navigator.Name));
                //      node.Tag = endTagNode;
                //   }

                //   if (!nodes.Contains(endTagNode))
                //      nodes.Insert(e.Node.Index + 1, endTagNode);
                //}
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }

        void XPathNavigatorTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            try
            {
                if (!_useSyntaxHighlighting)
                {
                    e.DrawDefault = true;
                    return;
                }

                if (e.Node.IsEditing)
                {
                    e.DrawDefault = true;
                    return;
                }

                if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
                {
                    e.DrawDefault = true;
                    return;
                }

                // not sure why the TreeView doesn't take care of this...
                // I was getting requests to draw nodes that weren't even visible on the screen.
                // Obviously, this was drastically slowing down the drawing of the tree!
                if (!e.Node.IsVisible)
                    return;

                Rectangle bounds = e.Bounds;

                // I tried using the suggested TextRenderingHint.AntiAlias, but I don't think it looks right with
                // small fonts, so I'm disabling it.  Need to make this a user option in a later version.

                // this is required for sequentially rendering adjacent text, see the article
                // 'Why text appears different when drawn with GDIPlus versus GDI', section 'How to Display Adjacent Text'
                // http://support.microsoft.com/?id=307208 for more details
                // e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                string text = e.Node.Text;

                // the node isn't selected, draw it's text with syntax highlights
                bool drawn = false;

                // don't bother with syntax highlighting for text nodes
                XPathNavigatorTreeNode xpathNode = e.Node as XPathNavigatorTreeNode;
                if (xpathNode == null || xpathNode.Navigator.NodeType != XPathNodeType.Text)
                {
                    using (SolidBrush brush = new SolidBrush(this.BackColor))
                        e.Graphics.FillRectangle(brush, bounds);

                    // draw non text nodes with syntax highlights
                    drawn = this.DrawStrings(text, bounds, e.Graphics);
                }

                // if the text wasn't matched by any of our regular expressions, it's likely just a text node
                // draw it without any syntax highlights
                if (!drawn)
                {
                    e.DrawDefault = true;
                    return;
                }

                //// If the node has focus, draw the focus rectangle.
                //if ((e.State & TreeNodeStates.Focused) != 0)
                //   ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }

        #endregion

        #region INotifyPropertyChanged values

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}

