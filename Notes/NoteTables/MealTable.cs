using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.Notes;

namespace Notes.NoteTables
{
	public class MealTable : NoteTable
	{
		enum Index
		{
			Id,
			Name,
			Ingredients,
			Recipe,
			State,
			Comment
		}


		public MealTable(Point location):
			base(location, "Meal")
		{

		}


		public override void CreateColumns()
		{
			// Add columns
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(CreateStateColumn());
			Columns.Add(new DataGridViewTextBoxColumn());

			// Columns settings
			Columns[(int)Index.Id].Name = "Id";
			Columns[(int)Index.Name].Name = "Name";			
			Columns[(int)Index.Ingredients].Name = "Ingredients";
			Columns[(int)Index.Recipe].Name = "Recipe";			
			Columns[(int)Index.State].Name = "State";			
			Columns[(int)Index.Comment].Name = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[(int)Index.State].ReadOnly = false;

			// Часть столбцов скрыты
			Columns[(int)Index.Id].Visible = false;
			Columns[(int)Index.Ingredients].Visible = false;
			Columns[(int)Index.Recipe].Visible = false;
		}


		public override bool AddNote(Note note)
		{
			if (note is Meal)
			{
				Meal meal = note as Meal;
				
				Rows.Add(new string[] 
				{
					meal.Id.ToString(),
					meal.Name,
					meal.Ingredients,
					meal.Recipe,
					States[(int)meal.CurrentState],
					meal.Comment
				});

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			Meal meal = note as Meal;
			if (meal == null)
				return;

			CurrentRow.Cells[(int)Index.Name].Value = meal.Name;
			CurrentRow.Cells[(int)Index.Ingredients].Value = meal.Ingredients;
			CurrentRow.Cells[(int)Index.Recipe].Value = meal.Recipe;
			CurrentRow.Cells[(int)Index.State].Value = States[(int)meal.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value = meal.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			Meal meal = new Meal();
			meal.Id = Int32.Parse(CurrentRow.Cells[(int)Index.Id].Value.ToString());
			meal.Name           = CurrentRow.Cells[(int)Index.Name].Value.ToString();
			meal.Ingredients    = CurrentRow.Cells[(int)Index.Ingredients].Value.ToString();
			meal.Recipe         = CurrentRow.Cells[(int)Index.Recipe].Value.ToString();
			meal.CurrentState = (Note.State)GetStateIndex(CurrentRow.Cells[(int)Index.State].Value.ToString());
			meal.Comment        = CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return meal;
		}


		public override void ChangeSize(Size tableSize)
		{
			int scrollbarWidth = VerticalScrollBar.Visible ? VerticalScrollBar.Width : 0;

			Width = tableSize.Width - scrollbarWidth;
			Height = tableSize.Height;			

			Columns[(int)Index.Id].Width = 0;
			Columns[(int)Index.Name].Width = (int)(tableSize.Width * 0.5);
			Columns[(int)Index.Ingredients].Width = 0;
			Columns[(int)Index.Recipe].Width = 0;
			Columns[(int)Index.State].Width = 100;
			Columns[(int)Index.Comment].Width = this.Width - Columns[(int)Index.Name].Width - Columns[(int)Index.State].Width - scrollbarWidth;
		}


		public override string[] SearchFields
		{
			get
			{
				return new string[] { "Name", "Ingredients" };
			}
		}
	}
}
