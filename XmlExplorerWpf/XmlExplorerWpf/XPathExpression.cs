using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace XmlExplorer
{
    public class XPathExpression : INotifyPropertyChanged
    {
        private string name;
        private string expression;

        public string Expression
        {
            get { return expression; }
            set
            {
                expression = value;
                RaisePropertyChanged("Expression");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
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

    public class XPathExpressionList : ObservableCollection<XPathExpression>
    {

    }
}
