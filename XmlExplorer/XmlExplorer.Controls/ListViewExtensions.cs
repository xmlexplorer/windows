using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XmlExplorer.Controls
{
    public static class ListViewExtensions
    {
        public static void AutoResizeColumns(this ListView listView)
        {
            listView.BeginUpdate();

            try
            {
                foreach (ColumnHeader column in listView.Columns)
                {
                    column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                    int columnContentWidth = column.Width;

                    column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                    int headerWidth = column.Width;

                    column.Width = Math.Max(columnContentWidth, headerWidth);
                }
            }
            finally
            {
                listView.EndUpdate();
            }
        }
    }
}
