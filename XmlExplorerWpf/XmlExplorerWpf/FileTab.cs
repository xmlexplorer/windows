using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace XmlExplorer
{
    public class FileTab
    {
        private string _name;

        public FileTab()
        {
        }

		public FileTab(FileInfo fileInfo, object document, XmlNamespaceManager namespaceManager)
        {
            this.FileInfo = fileInfo;
			this.Document = document;
			this.XmlNamespaceManager = namespaceManager;
        }

		public FileTab(string name, object document, XmlNamespaceManager namespaceManager)
        {
            _name = name;

            if (_name == null)
                _name = "Untitled";

            this.Document = document;
			this.XmlNamespaceManager = namespaceManager;
        }

        public object Document { get; set; }
        public FileInfo FileInfo { get; set; }
		public XmlNamespaceManager XmlNamespaceManager { get; set; }

        public string Name
        {
            get
            {
                try
                {
                    if (this.FileInfo == null)
                        return _name;

                    return this.FileInfo.Name;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                return null;
            }
        }

        public void Save(bool formatting)
        {
            using (FileStream stream = new FileStream(this.FileInfo.FullName, FileMode.Create, FileAccess.Write, FileShare.None))
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
                XPathNavigator navigator = this.Document as XPathNavigator;
                if (navigator != null)
                {
                    navigator.WriteSubtree(writer);
                }
                else
                {
                    XPathNodeIterator iterator = this.Document as XPathNodeIterator;
                    if (iterator != null)
                    {
                        foreach (XPathNavigator node in iterator)
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

                writer.Flush();
            }
        }
    }
}
