using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Notes.Notes;
using static Notes.Info;

namespace Notes.NoteTables
{
	public class AffairTable : NoteTable
	{
		enum Index
		{
			Id,
			Name,
			Description,
			IsDateSet,
			Date,
			State,			
			Comment
		}


		public AffairTable(Point location):
			base(location, "Affairs")
		{

		}


		public override void CreateColumns()
		{
			// Add columns
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewCheckBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(CreateStateColumn());
			Columns.Add(new DataGridViewTextBoxColumn());

			// Columns settings
			Columns[(int)Index.Id].Name = "Id";
			Columns[(int)Index.Name].Name = "Name";
			Columns[(int)Index.Description].Name = "Description";
			Columns[(int)Index.IsDateSet].Name = "IsDateSet";
			Columns[(int)Index.Date].Name = "Date";
			Columns[(int)Index.State].Name = "State";
			Columns[(int)Index.Comment].Name = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[(int)Index.State].ReadOnly = false;

			HideColumn((int)Index.Id);
			HideColumn((int)Index.IsDateSet);
			HideColumn((int)Index.Description);
			HideColumn((int)Index.Comment);
		}


		public override bool AddNote(Note note)
		{
			if (note is Affair)
			{
				Affair affair = note as Affair;

				Rows.Add(
					affair.Id.ToString(),
					affair.Name,
					affair.Description,
					affair.IsDateSet,
					affair.IsDateSet ? affair.GetDate().Replace(' ', '.') : "",
					States[(int)affair.CurrentState],
					affair.Comment
				);

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			Affair affair = note as Affair;
			if (affair == null)
				return;

			CurrentRow.Cells[(int)Index.Name].Value =              affair.Name;
			CurrentRow.Cells[(int)Index.Description].Value =       affair.Description;
			CurrentRow.Cells[(int)Index.IsDateSet].Value =         affair.IsDateSet;
			CurrentRow.Cells[(int)Index.Date].Value =              affair.IsDateSet ? affair.GetDate().Replace(' ', '.') : "";
			CurrentRow.Cells[(int)Index.State].Value = States[(int)affair.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value =           affair.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			Affair affair = new Affair();
			affair.Id =              CurrentRow.Cells[(int)Index.Id].Value.ToString().ToIntOrException();
			affair.Name =            CurrentRow.Cells[(int)Index.Name].Value.ToString();
			affair.Description =     CurrentRow.Cells[(int)Index.Description].Value.ToString();
			affair.IsDateSet = (bool)CurrentRow.Cells[(int)Index.IsDateSet].Value;
			if (affair.IsDateSet)
				affair.SetDate(      CurrentRow.Cells[(int)Index.Date].Value.ToString().Replace('.', ' '));
			affair.CurrentState =    CurrentRow.Cells[(int)Index.State].Value.ToString().ToNoteState();
			affair.Comment =         CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return affair;
		}


		public override void ChangeSize(Size tableSize)
		{
			Size = tableSize;

			Columns[(int)Index.Date].Width = 80;
			Columns[(int)Index.State].Width = 100;
			SetRemainingTableWidth((int)Index.Name);
		}


		public override string[] SearchFields
		{
			get
			{
				return new string[] { "Name", "Description" };
			}
		}
	}
}
