using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Notes.Import
{
	public class BookmarksImportTable : DataGridView
	{
		public BookmarksImportTable(Point location)
		{
			Location = location;

			MultiSelect = true;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			
			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
			AllowUserToResizeColumns = false;
			AllowUserToResizeRows = false;

			EditMode = DataGridViewEditMode.EditOnEnter;

			DefaultCellStyle.SelectionBackColor = Color.LightCyan;
			DefaultCellStyle.SelectionForeColor = Color.Black;
			BackgroundColor = Color.White;

			RowHeadersVisible = false;
			ColumnHeadersVisible = false;

			BorderStyle = BorderStyle.None;
			AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;

			ScrollBars = ScrollBars.Vertical;

			Columns.Add(new DataGridViewCheckBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());

			Columns[1].ReadOnly = true;
			Columns[2].ReadOnly = true;


			this.ContextMenu = new ContextMenu();
			MenuItem selectItem = new MenuItem("Select");
			selectItem.Click += delegate (object o, EventArgs e)
			{
				if (SelectedRows == null || SelectedRows.Count == 0)
					return;

				foreach (DataGridViewRow row in SelectedRows)
					row.Cells[0].Value = true;
			};
			this.ContextMenu.MenuItems.Add(selectItem);

			MenuItem deselectItem = new MenuItem("Deselect");
			deselectItem.Click += delegate (object o, EventArgs e)
			{
				if (SelectedRows == null || SelectedRows.Count == 0)
					return;

				foreach (DataGridViewRow row in SelectedRows)
					row.Cells[0].Value = false;
			};
			this.ContextMenu.MenuItems.Add(deselectItem);

			CellMouseDown += delegate (object o, DataGridViewCellMouseEventArgs e)
			{
				Point cursorPosition = this.PointToClient(Cursor.Position);

				// Если были выбраны одни строки, а правый клик выполнен на другой строке, то надо удалить только ее.
				// Так что убираем выделение и ставим новое.
				HitTestInfo hitInfo = HitTest(cursorPosition.X, cursorPosition.Y);
				if (hitInfo != null && hitInfo.RowIndex >= 0 && hitInfo.ColumnIndex >= 0)
				{
					// Только когда нет ни контрола, ни шифта - т.е. ничего не делаем, если выбираются строки для удаления.
					if (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.None)
					{
						DataGridViewRow row = Rows[hitInfo.RowIndex];
						if (!row.Selected)
						{
							this.ClearSelection();
							row.Cells[1].Selected = true;
							row.Selected = true;
						}
					}
				}

				if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.Button == MouseButtons.Right)
					this.ContextMenu.Show(this, cursorPosition);
			};
		}


		public void ChangeSize(Size size)
		{
			this.Size = size;
			int scrollBarWidth = (VerticalScrollBar.Visible) ? VerticalScrollBar.Width : 0;
			Columns[0].Width = 25;
			Columns[1].Width = (int)(this.Width * 0.4);
			Columns[2].Width = this.Width - Columns[0].Width - Columns[1].Width - scrollBarWidth;
		}
	}
}
