using System.IO;
namespace XmlExplorer.Controls
{
	public interface IXmlViewer
	{
		void CollapseAll();
		void ExpandAll();

		void Save(bool formatting);
		void SaveAs(bool formatting);

		FileInfo FileInfo { get; set; }

		void Reload();
	}
}
