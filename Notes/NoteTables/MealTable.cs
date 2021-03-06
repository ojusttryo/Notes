﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.Notes;
using static Notes.Info;

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

			HideColumn((int)Index.Id);
			HideColumn((int)Index.Ingredients);
			HideColumn((int)Index.Recipe);
			HideColumn((int)Index.Comment);
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

			CurrentRow.Cells[(int)Index.Name].Value =              meal.Name;
			CurrentRow.Cells[(int)Index.Ingredients].Value =       meal.Ingredients;
			CurrentRow.Cells[(int)Index.Recipe].Value =            meal.Recipe;
			CurrentRow.Cells[(int)Index.State].Value = States[(int)meal.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value =           meal.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			Meal meal = new Meal();
			meal.Id =           CurrentRow.Cells[(int)Index.Id].Value.ToString().ToIntOrException();
			meal.Name =         CurrentRow.Cells[(int)Index.Name].Value.ToString();
			meal.Ingredients =  CurrentRow.Cells[(int)Index.Ingredients].Value.ToString();
			meal.Recipe =       CurrentRow.Cells[(int)Index.Recipe].Value.ToString();
			meal.CurrentState = CurrentRow.Cells[(int)Index.State].Value.ToString().ToNoteState();
			meal.Comment =      CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return meal;
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
				return new string[] { "Name", "Ingredients" };
			}
		}
	}
}
