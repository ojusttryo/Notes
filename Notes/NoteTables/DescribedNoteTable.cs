using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.Notes;
using static Notes.Info;

namespace Notes.NoteTables
{
	class DescribedNoteTable : NoteTable
	{
		enum Index
		{
			Id,
			Name,
			Description,
			State,
			Comment
		}


		public DescribedNoteTable(Point location, string tableNameDB):
			base(location, tableNameDB)
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
			Columns[(int)Index.Description].Name = "Description";
			Columns[(int)Index.State].Name = "State";
			Columns[(int)Index.Comment].Name = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[(int)Index.State].ReadOnly = false;

			HideColumn((int)Index.Id);
			HideColumn((int)Index.Description);
			HideColumn((int)Index.Comment);
		}


		public override bool AddNote(Note note)
		{
			if (note is DescribedNote)
			{
				DescribedNote describedNote = note as DescribedNote;

				Rows.Add(new string[]
				{
					describedNote.Id.ToString(),
					describedNote.Name,
					describedNote.Description,
					States[(int)describedNote.CurrentState],
					describedNote.Comment
				});

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			DescribedNote describedNote = note as DescribedNote;
			if (describedNote == null)
				return;

			CurrentRow.Cells[(int)Index.Name].Value =              describedNote.Name;
			CurrentRow.Cells[(int)Index.Description].Value =       describedNote.Description;
			CurrentRow.Cells[(int)Index.State].Value = States[(int)describedNote.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value =           describedNote.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			DescribedNote describedNote = new DescribedNote();
			describedNote.Id =           CurrentRow.Cells[(int)Index.Id].Value.ToString().ToIntOrException();
			describedNote.Name =         CurrentRow.Cells[(int)Index.Name].Value.ToString();
			describedNote.Description =  CurrentRow.Cells[(int)Index.Description].Value.ToString();
			describedNote.CurrentState = CurrentRow.Cells[(int)Index.State].Value.ToString().ToNoteState();
			describedNote.Comment =      CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return describedNote;
		}


		public override void ChangeSize(Size tableSize)
		{
			Size = tableSize;

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
