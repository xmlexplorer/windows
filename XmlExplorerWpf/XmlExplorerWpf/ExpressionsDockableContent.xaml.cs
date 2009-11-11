using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AvalonDock;

namespace XmlExplorer
{
    /// <summary>
    /// Interaction logic for ExpressionsDockableContent.xaml
    /// </summary>
    public partial class ExpressionsDockableContent : DockableContent
    {
        public event EventHandler<EventArgs<XPathExpression>> ExpressionActivated;

        XPathExpressionList expressions;

        public XPathExpressionList Expressions
        {
            get
            {
                return expressions;
            }

            set
            {
                expressions = value;
                this.DataContext = expressions;
            }
        }

        public ExpressionsDockableContent()
        {
            InitializeComponent();
        }

        public void AddOrRemoveExpression(string expression)
        {
            bool existsInExpressions = this.Expressions.Any(x => x.Expression == expression);

            if (existsInExpressions)
                this.RemoveExpression(expression);
            else
                this.AddExpression(expression);
        }

        public void AddExpression(string expression)
        {
            this.Expressions.Add(
                new XPathExpression()
                {
                    Expression = expression,
                });
        }

        public void RemoveExpression(string expression)
        {
            this.Expressions.RemoveAll(x => x.Expression == expression);
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (this.ExpressionActivated == null)
                    return;

                DependencyObject source = (DependencyObject)e.OriginalSource;
                var row = TryFindParent<DataGridRow>(source);

                if (row == null)
                    return;

                XPathExpression expression = row.DataContext as XPathExpression;
                if (expression == null)
                    return;

                this.ExpressionActivated(this, new EventArgs<XPathExpression>(expression));

                e.Handled = true;
            }
            catch (Exception ex)
            {
                App.HandleException(ex);
            }
        }

        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                //use recursion to proceed with next level
                return TryFindParent<T>(parentObject);
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject GetParentObject(DependencyObject child)
        {
            if (child == null) return null;

            //handle content elements separately
            ContentElement contentElement = child as ContentElement;
            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                FrameworkContentElement fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            FrameworkElement frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                DependencyObject parent = frameworkElement.Parent;
                if (parent != null) return parent;
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }
    }
}
