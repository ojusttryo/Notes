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
	class DesireTable : NoteTable
	{
		enum Index
		{
			Id,
			Name,
			Description,
			State,
			Comment
		}


		public DesireTable(Point location):
			base(location, "Desires")
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
			if (note is Desire)
			{
				Desire desire = note as Desire;

				Rows.Add(new string[]
				{
					desire.Id.ToString(),
					desire.Name,
					desire.Description,
					States[(int)desire.CurrentState],
					desire.Comment
				});

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			Desire desire = note as Desire;
			if (desire == null)
				return;

			CurrentRow.Cells[(int)Index.Name].Value =              desire.Name;
			CurrentRow.Cells[(int)Index.Description].Value =       desire.Description;
			CurrentRow.Cells[(int)Index.State].Value = States[(int)desire.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value =           desire.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			Desire desire = new Desire();
			desire.Id =           CurrentRow.Cells[(int)Index.Id].Value.ToString().ToIntOrException();
			desire.Name =         CurrentRow.Cells[(int)Index.Name].Value.ToString();
			desire.Description =  CurrentRow.Cells[(int)Index.Description].Value.ToString();
			desire.CurrentState = CurrentRow.Cells[(int)Index.State].Value.ToString().ToNoteState();
			desire.Comment =      CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return desire;
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
