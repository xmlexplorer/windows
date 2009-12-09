using System.ComponentModel;

namespace XmlExplorer
{
	public class NamespaceDefinition : INotifyPropertyChanged
	{
		private string _namespace;
		private string _oldPrefix;
		private string _newPrefix;

		public string Namespace
		{
			get { return _namespace; }
			set
			{
				_namespace = value;
				RaisePropertyChanged("Namespace");
			}
		}

		public string OldPrefix
		{
			get { return _oldPrefix; }
			set
			{
				_oldPrefix = value;
				RaisePropertyChanged("OldPrefix");
			}
		}

		public string NewPrefix
		{
			get { return _newPrefix; }
			set
			{
				_newPrefix = value;
				RaisePropertyChanged("NewPrefix");
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
