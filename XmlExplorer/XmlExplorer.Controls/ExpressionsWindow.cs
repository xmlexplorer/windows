using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections;
using System.Windows.Forms.Design;
using System.Drawing.Design;

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

			this.toolStripSpringTextBoxFilter.TextChanged += this.OnToolStripSpringTextBoxFilterTextChanged;
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

		public event EventHandler ExpressionsLaunched;

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
			this.listViewExpressions.AutoResizeColumns();
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

			this.listViewExpressions.AutoResizeColumns();
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

		public void LoadExpressions(string filterText)
		{
			try
			{
				this.listViewExpressions.BeginUpdate();
				this.listViewExpressions.Items.Clear();

				if (_expressions == null)
					return;

				string lowerSearchText = null;
				if (!string.IsNullOrEmpty(filterText))
					lowerSearchText = filterText.ToLower();

				foreach (XPathExpression expression in _expressions)
				{
					if (string.IsNullOrEmpty(filterText) || this.IsMatchingExpression(expression, lowerSearchText))
					{
						XPathExpressionListViewItem item = new XPathExpressionListViewItem(expression);

						this.listViewExpressions.Items.Add(item);
					}
				}

				this.listViewExpressions.AutoResizeColumns();
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

		public void UpdateExpressionFilter(string filterText)
		{
			this.LoadExpressions(filterText);
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

		private void DeleteSelectedExpressions()
		{
			ArrayList selectedItems = new ArrayList(this.listViewExpressions.SelectedItems);

			string message = string.Format("Are you sure you want to delete the selected expression{0}?", selectedItems.Count == 1 ? "" : "s");

			var result = MessageBox.Show(this, message, "Delete Expressions", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (result != System.Windows.Forms.DialogResult.Yes)
				return;

			foreach (var selectedItem in selectedItems)
			{
				XPathExpressionListViewItem item = selectedItem as XPathExpressionListViewItem;
				if (item != null)
				{
					if (_expressions.Contains(item.XPathExpression))
						_expressions.Remove(item.XPathExpression);
				}

				item.Remove();
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

		private void OnToolStripSpringTextBoxFilterTextChanged(object sender, EventArgs e)
		{
			try
			{
				this.UpdateExpressionFilter(this.toolStripSpringTextBoxFilter.Text);
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
				if (e.CancelEdit || e.Label == null)
					return;

				XPathExpressionListViewItem item = this.listViewExpressions.Items[e.Item] as XPathExpressionListViewItem;

				if (item == null)
					return;

				item.XPathExpression.Name = e.Label;

				item.Text = e.Label;

				this.listViewExpressions.AutoResizeColumns();
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

				bool selected = this.listViewExpressions.SelectedItems.Count > 0;

				this.toolStripButtonDelete.Enabled = selected;
				this.toolStripButtonLaunchXpath.Enabled = selected;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void toolStripButtonLaunchXpath_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.ExpressionsLaunched != null)
					this.ExpressionsLaunched(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void toolStripButtonEditExpressions_Click(object sender, EventArgs e)
		{
			try
			{
				CollectionEditor.EditValue(this, this, "Expressions");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void toolStripButtonDelete_Click(object sender, EventArgs e)
		{
			try
			{
				this.DeleteSelectedExpressions();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessageBox.Show(this, ex.ToString());
			}
		}

		#endregion
	}

	class CollectionEditor : IWindowsFormsEditorService, IServiceProvider, ITypeDescriptorContext
	{
		private readonly IWin32Window owner;
		private readonly object component;
		private readonly PropertyDescriptor property;

		public static void EditValue(IWin32Window owner, object component, string propertyName)
		{
			PropertyDescriptor prop = TypeDescriptor.GetProperties(component)[propertyName];
			if (prop == null)
				throw new ArgumentException("propertyName");
			UITypeEditor editor = (UITypeEditor)prop.GetEditor(typeof(UITypeEditor));

			CollectionEditor ctx = new CollectionEditor(owner, component, prop);

			if (editor != null && editor.GetEditStyle(ctx) == UITypeEditorEditStyle.Modal)
			{
				object value = prop.GetValue(component);
				value = editor.EditValue(ctx, ctx, value);
				if (!prop.IsReadOnly)
				{
					prop.SetValue(component, value);
				}
			}
		}

		private CollectionEditor(IWin32Window owner, object component, PropertyDescriptor property)
		{
			this.owner = owner;
			this.component = component;
			this.property = property;
		}

		#region IWindowsFormsEditorService Members

		public void CloseDropDown()
		{
			throw new NotImplementedException();
		}

		public void DropDownControl(System.Windows.Forms.Control control)
		{
			throw new NotImplementedException();
		}

		public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.Form dialog)
		{
			return dialog.ShowDialog(owner);
		}

		#endregion

		#region IServiceProvider Members

		public object GetService(Type serviceType)
		{
			return serviceType == typeof(IWindowsFormsEditorService) ? this : null;
		}

		#endregion

		#region ITypeDescriptorContext Members

		IContainer ITypeDescriptorContext.Container
		{
			get { return null; }
		}

		object ITypeDescriptorContext.Instance
		{
			get { return component; }
		}

		void ITypeDescriptorContext.OnComponentChanged()
		{
		}

		bool ITypeDescriptorContext.OnComponentChanging()
		{
			return true;
		}

		PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
		{
			get { return property; }
		}

		#endregion
	}
}
