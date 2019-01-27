using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Notes.Notes;
using static Notes.Info;

namespace Notes.NoteTables 
{
	class DatedNoteTable : NoteTable
	{
		private enum Index
		{
			Id,
			Name,
			Year,
			State,
			Comment
		}

		public DatedNoteTable(Point location, string tableNameInDB):
			base(location, tableNameInDB)
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
			Columns[(int)Index.Id].Name = "Id";
			Columns[(int)Index.Name].Name = "Name";			
			Columns[(int)Index.Year].Name = "Year";			
			Columns[(int)Index.State].Name = "State";			
			Columns[(int)Index.Comment].Name = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[(int)Index.State].ReadOnly = false;

			HideColumn((int)Index.Id);
			HideColumn((int)Index.Comment);
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

			CurrentRow.Cells[(int)Index.Name].Value =              datedNote.Name;
			CurrentRow.Cells[(int)Index.Year].Value =             (datedNote.Year == 0) ? "" : datedNote.Year.ToString();
			CurrentRow.Cells[(int)Index.State].Value = States[(int)datedNote.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value =           datedNote.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			DatedNote datedNote = new DatedNote();
			datedNote.Id =           CurrentRow.Cells[(int)Index.Id].Value.ToString().ToIntOrException();
			datedNote.Name =         CurrentRow.Cells[(int)Index.Name].Value.ToString();
			datedNote.Year =         CurrentRow.Cells[(int)Index.Year].Value.ToString().ToIntOrDefault();
			datedNote.CurrentState = CurrentRow.Cells[(int)Index.State].Value.ToString().ToNoteState();
			datedNote.Comment =      CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return datedNote;
		}


		public override void ChangeSize(Size tableSize)
		{
			Size = tableSize;		

			Columns[(int)Index.Year].Width = 40;
			Columns[(int)Index.State].Width = 100;
			SetRemainingTableWidth((int)Index.Name);
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
