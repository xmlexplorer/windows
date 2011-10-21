using System;
using System.Windows.Forms;

namespace XmlExplorer.TreeView
{
	public class WaitCursorProvider : IDisposable
	{
		Cursor _previousCursor;

		public WaitCursorProvider()
		{
			_previousCursor = Cursor.Current;

			Cursor.Current = Cursors.WaitCursor;
		}

		#region IDisposable Members

		private bool disposed = false;

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				// If disposing equals true, dispose all managed
				// and unmanaged resources.
				if (disposing)
				{
					// Dispose managed resources.

					Cursor.Current = _previousCursor;
				}

				// Note disposing has been done.
				disposed = true;
			}
		}

		~WaitCursorProvider()
		{
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose(false) is optimal in terms of
			// readability and maintainability.
			Dispose(false);
		}

		#endregion
	}
}
