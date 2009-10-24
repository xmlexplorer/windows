using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace XmlExplorer
{
    public class Error
    {
        public XmlSeverityType Category { get; set; }
        public int DefaultOrder { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        //public int Line { get; set; }
        //public int Column { get; set; }
        //public ValidationEventArgs ValidationEventArgs { get; set; }
        public object SourceObject { get; set; }
    }
}
