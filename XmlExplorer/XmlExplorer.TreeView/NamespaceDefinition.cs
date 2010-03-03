using System.ComponentModel;

namespace XmlExplorer.TreeView
{
    public class NamespaceDefinition : INotifyPropertyChanged
    {
        private string _namespace;
        private string _prefix;

        public string Namespace
        {
            get { return _namespace; }
            set
            {
                _namespace = value;
                RaisePropertyChanged("Namespace");
            }
        }

        public string Prefix
        {
            get { return _prefix; }
            set
            {
                _prefix = value;
                RaisePropertyChanged("Prefix");
            }
        }

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
