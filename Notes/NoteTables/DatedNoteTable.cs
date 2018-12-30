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
		public DatedNoteTable(Point location, string tableName)
		{
			Initialize();

			Location = location;
			TableNameInDatabase = tableName;

			SetNotes();
		}


		public override void Initialize()
		{
			// Add columns
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(_stateColumn);
			Columns.Add(new DataGridViewTextBoxColumn());

			// Columns settings
			Columns[0].Name = "Id";
			Columns[0].ReadOnly = true;
			Columns[0].Visible = false;			// TODO after test set to false
			Columns[1].Name = "Name";			
			Columns[1].ReadOnly = true;
			Columns[2].Name = "Year";			
			Columns[2].ReadOnly = true;
			Columns[3].Name = "State";			
			Columns[3].ReadOnly = false;
			Columns[4].Name = "Comment";			
			Columns[4].ReadOnly = true;
			

			//// Test rows
			//string[] row = { "Note1", "1990", "Postponed", "Some comment" };
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
			//Rows.Add(row);
		}


		private void SetNotes()
		{
			List<Note> notes = Database.GetNotes(TableNameInDatabase);

			foreach (Note note in notes)
				AddNote(note);
		}


		public override bool AddNote(Note note)
		{
			if (note is DatedNote)
			{
				DatedNote datedNote = note as DatedNote;
				
				Rows.Add(new string[] { datedNote.Id.ToString(), datedNote.Name, datedNote.Year.ToString(), "", datedNote.Comment });
				Rows[Rows.Count - 1].Cells[3].Value = NoteTable.States[(int)datedNote.CurrentState];

				return true;
			}

			return false;
		}


		public override void ChangeSize(Size tableSize)
		{
			this.Size = tableSize;
			
			int scrollbarWidth = VerticalScrollBar.Visible ? VerticalScrollBar.Width : 0;

			Columns[0].Width = 0;
			Columns[1].Width = (int)(tableSize.Width * 0.4);
			Columns[2].Width = 40;
			Columns[3].Width = 100;
			Columns[4].Width = (tableSize.Width - Columns[0].Width - Columns[1].Width - Columns[2].Width - Columns[3].Width - scrollbarWidth);
		}
	}
}
