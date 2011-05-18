using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlExplorer.TreeView
{
	public class ChildNodeDefinitionCollection : List<ChildNodeDefinition>
	{
		public void Serialize(string fileName)
		{
			using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
			{
				Serialize(stream);
			}
		}

		public void Serialize(FileStream stream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ChildNodeDefinitionCollection));

			serializer.Serialize(stream, this);
		}

		public void Import(string fileName)
		{
			ChildNodeDefinitionCollection otherChildNodes = Deserialize(fileName);

			if (otherChildNodes == null)
				return;

			foreach (var otherChildNode in otherChildNodes)
			{
				if (!this.Contains(otherChildNode))
					this.Add(otherChildNode);
			}
		}

		public static ChildNodeDefinitionCollection Deserialize(string fileName)
		{
			using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return Deserialize(stream);
			}
		}

		private static ChildNodeDefinitionCollection Deserialize(FileStream stream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ChildNodeDefinitionCollection));

			return serializer.Deserialize(stream) as ChildNodeDefinitionCollection;
		}

		public override string ToString()
		{
			return this.Count + " definitions";
		}
	}
}
