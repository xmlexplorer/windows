using System.Xml.Schema;

namespace XmlExplorer.TreeView
{
    public class Error
    {
        public XmlSeverityType Category { get; set; }
        public int DefaultOrder { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public object SourceObject { get; set; }
    }
}
