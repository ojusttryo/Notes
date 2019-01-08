using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Notes.Notes;

namespace Notes.NoteTables 
{
	class DatedNoteTable : NoteTable
	{
		public DatedNoteTable(Point location, string tableName):
			base(location, tableName)
		{

		}


		public override void CreateColumns()
		{
			// Add columns
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(CreateStateColumn());
			Columns.Add(new DataGridViewTextBoxColumn());

			// Columns settings
			Columns[0].Name = "Id";
			Columns[1].Name = "Name";			
			Columns[2].Name = "Year";			
			Columns[3].Name = "State";			
			Columns[4].Name = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[3].ReadOnly = false;

			// Id не отображается.
			Columns[0].Visible = false;
		}


		public override bool AddNote(Note note)
		{
			if (note is DatedNote)
			{
				DatedNote datedNote = note as DatedNote;
				
				Rows.Add(new string[] 
				{
					datedNote.Id.ToString(),
					datedNote.Name,
					(datedNote.Year == 0) ? "" : datedNote.Year.ToString(),
					States[(int)datedNote.CurrentState],
					datedNote.Comment
				});

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			DatedNote datedNote = note as DatedNote;
			if (datedNote == null)
				return;

			CurrentRow.Cells[1].Value = datedNote.Name;
			CurrentRow.Cells[2].Value = (datedNote.Year == 0) ? "" : datedNote.Year.ToString();
			CurrentRow.Cells[3].Value = States[(int)datedNote.CurrentState];
			CurrentRow.Cells[4].Value = datedNote.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			DatedNote datedNote = new DatedNote();
			datedNote.Id = Int32.Parse(CurrentRow.Cells[0].Value.ToString());
			datedNote.Name = CurrentRow.Cells[1].Value.ToString();
			datedNote.Year = (CurrentRow.Cells[2].Value.ToString() == string.Empty) ? 0 : Int32.Parse(CurrentRow.Cells[2].Value.ToString());
			datedNote.CurrentState = (Note.State)GetStateIndex(CurrentRow.Cells[3].Value.ToString());
			datedNote.Comment = CurrentRow.Cells[4].Value.ToString();

			return datedNote;
		}


		public override void ChangeSize(Size tableSize)
		{
			int scrollbarWidth = VerticalScrollBar.Visible ? VerticalScrollBar.Width : 0;

			Width = tableSize.Width;
			Height = tableSize.Height;			

			Columns[0].Width = 0;
			Columns[1].Width = (int)(tableSize.Width * 0.4);
			Columns[2].Width = 40;
			Columns[3].Width = 100;
			Columns[4].Width = this.Width - Columns[1].Width - Columns[2].Width - Columns[3].Width - scrollbarWidth;
		}


		public override string[] SearchFields
		{
			get
			{
				return new string[] { "Name" };
			}
		}
	}
}
