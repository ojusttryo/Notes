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
			
			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
			AllowUserToResizeColumns = false;
			AllowUserToResizeRows = false;

			EditMode = DataGridViewEditMode.EditOnEnter;

			DefaultCellStyle.SelectionBackColor = Color.White;
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
