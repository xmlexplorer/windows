namespace XmlExplorer.TreeView
{
	public class ChildNodeDefinition
	{
		private string idXpath = "@id";
		private string childXpath = "//*[@id='{0}']";

		public string IdXpath
		{
			get
			{
				return this.idXpath;
			}
			set
			{
				this.idXpath = value;
			}
		}

		public string ChildXpath
		{
			get
			{
				return this.childXpath;
			}
			set
			{
				this.childXpath = value;
			}
		}

		public override string ToString()
		{
			return this.idXpath;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			return this.GetHashCode() == obj.GetHashCode();
		}

		public override int GetHashCode()
		{
			string values = this.idXpath + "|" + this.childXpath;

			return values.GetHashCode();
		}
	}
}
