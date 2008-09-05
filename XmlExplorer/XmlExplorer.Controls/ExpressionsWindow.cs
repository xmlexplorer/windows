using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;

namespace XmlExplorer.Controls
{
    public partial class ExpressionsWindow : DockContent
    {
        #region Variables

        private XPathExpressionLibrary _expressions;

        #endregion

        #region Constructors

        public ExpressionsWindow()
        {
            InitializeComponent();

            _expressions = new XPathExpressionLibrary();

            this.listViewExpressions.ItemActivate += this.OnListViewExpressionsItemActivate;
            this.listViewExpressions.AfterLabelEdit += this.OnListViewExpressionsAfterLabelEdit;
            this.listViewExpressions.SelectedIndexChanged += this.OnListViewExpressionsSelectedIndexChanged;
            this.listViewExpressions.KeyDown += this.OnListViewExpressionsKeyDown;
            this.listViewExpressions.MouseUp += this.OnListViewExpressionsMouseUp;

            this.textBoxSearchExpressions.TextChanged += this.OnTextBoxSearchExpressionsTextChanged;
        }

        #endregion

        #region Properties

        public XPathExpressionLibrary Expressions
        {
            get
            {
                return _expressions;
            }

            set
            {
                _expressions = value;

                this.LoadExpressions();
            }
        }

        public List<String> SelectedExpressions
        {
            get
            {
                List<String> selectedExpressions = new List<string>();

                foreach (ListViewItem item in this.listViewExpressions.SelectedItems)
                {
                    XPathExpressionListViewItem expressionItem = item as XPathExpressionListViewItem;

                    if (expressionItem == null)
                        continue;

                    selectedExpressions.Add(expressionItem.XPathExpression.Expression);
                }

                return selectedExpressions;
            }
        }

        #endregion

        #region Events

        public event EventHandler SelectedExpressionChanged;

        public event EventHandler ExpressionsActivated;

        #endregion

        #region Methods

        public void AddOrEditXPathExpression(string expression)
        {
            XPathExpression xpathExpression = _expressions.Find(expression);

            if (xpathExpression != null)
            {
                // edit
                this.EditXPathExpression(xpathExpression);
            }
            else
            {
                // add
                this.AddXPathExpression(expression);
            }
        }

        public void AddXPathExpression(string expression)
        {
            XPathExpression xpathExpression = new XPathExpression();
            xpathExpression.Expression = expression;
            _expressions.Add(xpathExpression);
            XPathExpressionListViewItem item = new XPathExpressionListViewItem(xpathExpression);
            this.listViewExpressions.Items.Add(item);
            this.UpdateXPathExpressionTool(expression);
            this.AutoSizeListViewColumns(this.listViewExpressions);
            //this.toolStripButtonXPathExpression.ToolTipText = "Edit expression";
        }

        public void EditXPathExpression(XPathExpression xpathExpression)
        {
            using (XPathExpressionDialog dialog = new XPathExpressionDialog(xpathExpression))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
            }

            foreach (ListViewItem item in this.listViewExpressions.Items)
            {
                XPathExpressionListViewItem expressionItem = item as XPathExpressionListViewItem;
                if (expressionItem == null)
                    continue;

                if (expressionItem.XPathExpression == xpathExpression)
                {
                    expressionItem.Initialize();
                    //this.toolStripTextBoxXpath.Text = xpathExpression.Expression;
                    break;
                }
            }

            this.AutoSizeListViewColumns(this.listViewExpressions);
        }

        private void UpdateXPathExpressionTool(string text)
        {
            if (_expressions.Contains(text))
            {
                //this.toolStripButtonXPathExpression.Image = Properties.Resources.star;
                //this.toolStripButtonXPathExpression.ToolTipText = "Edit expression";
            }
            else
            {
                //this.toolStripButtonXPathExpression.Image = Properties.Resources.unstarred;
                //this.toolStripButtonXPathExpression.ToolTipText = "Add expression to library";
            }
        }

        public void LoadExpressions()
        {
            this.LoadExpressions(null);
        }

        public void LoadExpressions(string searchText)
        {
            try
            {
                this.listViewExpressions.BeginUpdate();
                this.listViewExpressions.Items.Clear();

                if (_expressions == null)
                    return;

                string lowerSearchText = null;
                if (!string.IsNullOrEmpty(searchText))
                    lowerSearchText = searchText.ToLower();

                foreach (XPathExpression expression in _expressions)
                {
                    if (string.IsNullOrEmpty(searchText) || this.IsMatchingExpression(expression, lowerSearchText))
                    {
                        XPathExpressionListViewItem item = new XPathExpressionListViewItem(expression);

                        this.listViewExpressions.Items.Add(item);
                    }
                }

                this.AutoSizeListViewColumns(this.listViewExpressions);
            }
            finally
            {
                this.listViewExpressions.EndUpdate();
            }
        }

        public bool IsMatchingExpression(XPathExpression expression, string lowerSearchText)
        {
            if (!string.IsNullOrEmpty(expression.Name))
                if (expression.Name.ToLower().Contains(lowerSearchText))
                    return true;

            if (!string.IsNullOrEmpty(expression.Expression))
                if (expression.Expression.ToLower().Contains(lowerSearchText))
                    return true;

            return false;
        }

        public void UpdateExpressionSearch(string searchText)
        {
            this.LoadExpressions(searchText);
        }

        public void DeleteSelectedExpressionItems(bool skipConfirmation)
        {
            DialogResult result = DialogResult.Yes;

            if (!skipConfirmation)
            {
                string message = "";
                string caption = "";

                int count = this.listViewExpressions.SelectedItems.Count;
                if (count > 1)
                {
                    message = string.Format("Are you sure you want to delete these {0} expressions?", count);
                    caption = "Confirm multiple expression delete";
                }
                else
                {
                    message = "Are you sure you want to delete this expression?";
                    caption = "Confirm expression delete";
                }
                result = MessageBox.Show(this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            }

            if (result != DialogResult.Yes)
                return;

            foreach (ListViewItem item in this.listViewExpressions.SelectedItems)
            {
                XPathExpressionListViewItem expressionItem = item as XPathExpressionListViewItem;
                if (expressionItem != null)
                {
                    _expressions.Remove(expressionItem.XPathExpression);
                }

                item.Remove();
            }

            //string text = this.toolStripTextBoxXpath.Text;

            //this.UpdateXPathExpressionTool(text);
        }

        private void AutoSizeListViewColumns(ListView listView)
        {
            foreach (ColumnHeader header in listView.Columns)
            {
                header.Width = -1;
                int width = header.Width;
                header.Width = -2;
                if (width > header.Width)
                    header.Width = width;
            }
        }

        #endregion

        #region Event Handlers

        private void OnListViewExpressionsMouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.SelectedExpressionChanged != null)
                    this.SelectedExpressionChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        private void OnTextBoxSearchExpressionsTextChanged(object sender, EventArgs e)
        {
            try
            {
                this.UpdateExpressionSearch(this.textBoxSearchExpressions.Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        private void OnListViewExpressionsItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (this.ExpressionsActivated != null)
                    this.ExpressionsActivated(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        private void OnListViewExpressionsAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            try
            {
                if (e.CancelEdit)
                    return;

                XPathExpressionListViewItem item = this.listViewExpressions.Items[e.Item] as XPathExpressionListViewItem;

                if (item == null)
                    return;

                item.XPathExpression.Name = e.Label;

                this.AutoSizeListViewColumns(this.listViewExpressions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        private void OnListViewExpressionsKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.listViewExpressions.SelectedItems.Count < 1)
                    return;

                XPathExpressionListViewItem item = this.listViewExpressions.SelectedItems[0] as XPathExpressionListViewItem;

                if (item == null)
                    return;

                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        this.DeleteSelectedExpressionItems(e.Shift);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        private void OnListViewExpressionsSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedExpressionChanged != null)
                    this.SelectedExpressionChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(this, ex.ToString());
            }
        }

        #endregion
    }
}
