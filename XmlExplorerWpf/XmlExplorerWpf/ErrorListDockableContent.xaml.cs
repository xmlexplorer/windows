using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using AvalonDock;
using System.Windows.Controls.Primitives;

namespace XmlExplorer
{
    /// <summary>
    /// Interaction logic for ErrorListDockableContent.xaml
    /// </summary>
    public partial class ErrorListDockableContent : DockableContent
    {
        public ErrorListDockableContent()
        {
            InitializeComponent();

            this.DataGridErrorList.MouseDoubleClick += new MouseButtonEventHandler(DataGridErrorList_MouseDoubleClick);

            this.DataGridErrorList.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }

        public event EventHandler<EventArgs<Error>> ErrorActivated;

        void DataGridErrorList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is DataGridCell) && !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            if (dep is DataGridColumnHeader)
            {
                // do something
            }

            if (dep is DataGridCell)
            {
                // do something else - possibly navigate to the row.
                Error error = this.DataGridErrorList.SelectedItem as Error;
                if (error != null)
                {
                    if (this.ErrorActivated != null)
                    {
                        EventArgs<Error> eventArgs = new EventArgs<Error>(error);
                        this.ErrorActivated(this, eventArgs);
                    }
                }
            }
        }
    }
}
