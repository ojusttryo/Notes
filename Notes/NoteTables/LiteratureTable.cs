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
	class LiteratureTable : DatedNoteTable
	{
		enum Index
		{
			Id = 0,
			Name,
			Author, 
			Genre,
			Universe,
			Series,
			Volume,
			Chapter,
			Page,
			Pages,
			Year,
			State,
			Comment
		}


		public LiteratureTable(Point location):
			base(location, "Literature")
		{

		}


		public override void CreateColumns()
		{
			// Чтоб не запутаться, вставлю рядом названия.
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(new DataGridViewTextBoxColumn());	
			Columns.Add(CreateStateColumn());				
			Columns.Add(new DataGridViewTextBoxColumn());	

			Columns[(int)Index.Id].Name       = "Id";
			Columns[(int)Index.Name].Name     = "Name";
			Columns[(int)Index.Author].Name   = "Author";
			Columns[(int)Index.Genre].Name    = "Genre";
			Columns[(int)Index.Universe].Name = "Universe";
			Columns[(int)Index.Series].Name   = "Series";
			Columns[(int)Index.Volume].Name   = "Volume";
			Columns[(int)Index.Chapter].Name  = "Chapter";
			Columns[(int)Index.Page].Name     = "Page";
			Columns[(int)Index.Pages].Name    = "Pages";	
			Columns[(int)Index.Year].Name     = "Year";
			Columns[(int)Index.State].Name    = "State";
			Columns[(int)Index.Comment].Name  = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[(int)Index.State].ReadOnly = false;

			HideColumn((int)Index.Id);
			HideColumn((int)Index.Universe);
			HideColumn((int)Index.Chapter);
			HideColumn((int)Index.Page);
			HideColumn((int)Index.Pages);
			HideColumn((int)Index.Year);
			HideColumn((int)Index.Comment);
		}


		public override bool AddNote(Note note)
		{
			if (note is Literature)
			{
				Literature literature = note as Literature;
				
				Rows.Add(new string[] 
				{
					literature.Id.ToString(),
					literature.Name,
					literature.Author,
					literature.Genre,
					literature.Universe,
					literature.Series,
					(literature.Volume == 0)  ? "" : literature.Volume.ToString(),
					(literature.Chapter == 0) ? "" : literature.Chapter.ToString(),
					(literature.Page == 0)    ? "" : literature.Page.ToString(),
					(literature.Pages == 0)   ? "" : literature.Pages.ToString(),
					(literature.Year == 0)    ? "" : literature.Year.ToString(),
					States[(int)literature.CurrentState],
					literature.Comment
				});

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			Literature literature = note as Literature;
			if (literature == null)
				return;

			CurrentRow.Cells[(int)Index.Name].Value     = literature.Name;
			CurrentRow.Cells[(int)Index.Author].Value   = literature.Author;
			CurrentRow.Cells[(int)Index.Genre].Value    = literature.Genre;
			CurrentRow.Cells[(int)Index.Universe].Value = literature.Universe;
			CurrentRow.Cells[(int)Index.Series].Value   = literature.Series;
			CurrentRow.Cells[(int)Index.Volume].Value  = (literature.Volume == 0)  ? "" : literature.Volume.ToString();
			CurrentRow.Cells[(int)Index.Chapter].Value = (literature.Chapter == 0) ? "" : literature.Chapter.ToString();
			CurrentRow.Cells[(int)Index.Page].Value    = (literature.Page == 0)    ? "" : literature.Page.ToString();
			CurrentRow.Cells[(int)Index.Pages].Value   = (literature.Pages == 0)   ? "" : literature.Pages.ToString();
			CurrentRow.Cells[(int)Index.Year].Value    = (literature.Year == 0)    ? "" : literature.Year.ToString();
			CurrentRow.Cells[(int)Index.State].Value = States[(int)literature.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value = literature.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			Literature literature = new Literature();
			literature.Id =           CurrentRow.Cells[(int)Index.Id].Value.ToString().ToIntOrException();
			literature.Name =         CurrentRow.Cells[(int)Index.Name].Value.ToString();
			literature.Author =       CurrentRow.Cells[(int)Index.Author].Value.ToString();
			literature.Genre =        CurrentRow.Cells[(int)Index.Genre].Value.ToString();
			literature.Universe =     CurrentRow.Cells[(int)Index.Universe].Value.ToString();
			literature.Series =       CurrentRow.Cells[(int)Index.Series].Value.ToString();
			literature.Volume =       CurrentRow.Cells[(int)Index.Volume].Value.ToString().ToIntOrDefault();
			literature.Chapter =      CurrentRow.Cells[(int)Index.Chapter].Value.ToString().ToIntOrDefault();
			literature.Page =         CurrentRow.Cells[(int)Index.Page].Value.ToString().ToIntOrDefault();
			literature.Pages =        CurrentRow.Cells[(int)Index.Pages].Value.ToString().ToIntOrDefault();
			literature.Year =         CurrentRow.Cells[(int)Index.Year].Value.ToString().ToIntOrDefault();
			literature.CurrentState = CurrentRow.Cells[(int)Index.State].Value.ToString().ToNoteState();
			literature.Comment =      CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return literature;
		}


		public override void ChangeSize(Size tableSize)
		{
			Size = tableSize;

			Columns[(int)Index.Author].Width = (int)(Width * 0.2);
			Columns[(int)Index.Genre].Width = 120;
			Columns[(int)Index.Series].Width = (int)(Width * 0.2);
			Columns[(int)Index.Volume].Width = 45;
			Columns[(int)Index.State].Width = 100;
			SetRemainingTableWidth((int)Index.Name);
		}


		public override string[] SearchFields
		{
			get
			{
				return new string[] { "Name", "Author", "Genre", "Universe", "Series" };
			}
		}
	}
}
