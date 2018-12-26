using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notes.NoteTables 
{
	class DatedNoteTable : NoteTable
	{
		public DatedNoteTable(Point location)
		{
			Initialize();
			Location = location;
		}


		public override void Initialize()
		{
			// add columns
			DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
			comboBoxColumn.Items.AddRange("Not selected", "Active", "Deleted", "Finished", "Postponed", "Waiting");
			comboBoxColumn.DefaultCellStyle.BackColor = Color.White;
			comboBoxColumn.DefaultCellStyle.SelectionBackColor = Color.White;
			comboBoxColumn.FlatStyle = FlatStyle.Flat;

			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(comboBoxColumn);
			Columns.Add(new DataGridViewTextBoxColumn());

			// columns settings
			Columns[0].Name = "Name";			
			Columns[0].ReadOnly = true;
			Columns[1].Name = "Year";			
			Columns[1].ReadOnly = true;
			Columns[2].Name = "State";			
			Columns[2].ReadOnly = false;
			Columns[3].Name = "Comment";			
			Columns[3].ReadOnly = true;

			// appearence and interraction
			this.AllowUserToAddRows = false;
			this.AllowUserToDeleteRows = false;
			this.DefaultCellStyle.SelectionBackColor = Color.LightCyan;
			this.DefaultCellStyle.SelectionForeColor = Color.Black;
			this.BackgroundColor = Color.White;
			this.RowHeadersVisible = false;
			this.BorderStyle = BorderStyle.None;
			this.ScrollBars = ScrollBars.Vertical;

			// test rows
			string[] row = { "Note1", "1990", "Postponed", "Some comment" };
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
			Rows.Add(row);
		}


		public override void ChangeSize(Size tableSize)
		{
			this.MinimumSize = tableSize;
			this.Size = tableSize;
			this.MaximumSize = tableSize;

			int scrollbarWidth = VerticalScrollBar.Visible ? VerticalScrollBar.Width : 0;

			Columns[0].Width = (int)(tableSize.Width * 0.4);
			Columns[1].Width = 40;
			Columns[2].Width = 100;
			Columns[3].Width = (tableSize.Width - Columns[0].Width - Columns[1].Width - Columns[2].Width - scrollbarWidth);
		}
	}
}
