using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.Notes;

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


		public LiteratureTable(Point location, string tableName):
			base(location, tableName)
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

			// Многие поля просто не влезут в таблицу на экране. Их лучше не отображать.
			Columns[(int)Index.Id].Visible       = false;
			Columns[(int)Index.Universe].Visible = false;
			Columns[(int)Index.Chapter].Visible  = false;
			Columns[(int)Index.Page].Visible     = false;
			Columns[(int)Index.Pages].Visible    = false;
			Columns[(int)Index.Year].Visible     = false;
			Columns[(int)Index.Comment].Visible  = false;
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
			literature.Id = Int32.Parse(CurrentRow.Cells[(int)Index.Id].Value.ToString());
			literature.Name           = CurrentRow.Cells[(int)Index.Name].Value.ToString();
			literature.Author         = CurrentRow.Cells[(int)Index.Author].Value.ToString();
			literature.Genre          = CurrentRow.Cells[(int)Index.Genre].Value.ToString();
			literature.Universe       = CurrentRow.Cells[(int)Index.Universe].Value.ToString();
			literature.Series         = CurrentRow.Cells[(int)Index.Series].Value.ToString();
			literature.Volume        = (CurrentRow.Cells[(int)Index.Volume].Value.ToString() == string.Empty) ? 0 : 
				            Int32.Parse(CurrentRow.Cells[(int)Index.Volume].Value.ToString());
			literature.Chapter       = (CurrentRow.Cells[(int)Index.Chapter].Value.ToString() == string.Empty) ? 0 : 
				            Int32.Parse(CurrentRow.Cells[(int)Index.Chapter].Value.ToString());
			literature.Page          = (CurrentRow.Cells[(int)Index.Page].Value.ToString() == string.Empty) ? 0 : 
				            Int32.Parse(CurrentRow.Cells[(int)Index.Page].Value.ToString());
			literature.Pages         = (CurrentRow.Cells[(int)Index.Pages].Value.ToString() == string.Empty) ? 0 : 
				            Int32.Parse(CurrentRow.Cells[(int)Index.Pages].Value.ToString());
			literature.Year          = (CurrentRow.Cells[(int)Index.Year].Value.ToString() == string.Empty) ? 0 : 
				            Int32.Parse(CurrentRow.Cells[(int)Index.Year].Value.ToString());
			literature.CurrentState = (Note.State)
				          GetStateIndex(CurrentRow.Cells[(int)Index.State].Value.ToString());
			literature.Comment        = CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return literature;
		}


		public override void ChangeSize(Size tableSize)
		{
			this.Size = tableSize;
			
			int scrollbarWidth = VerticalScrollBar.Visible ? VerticalScrollBar.Width : 0;

			Columns[(int)Index.Id].Width   = 0;
			
			Columns[(int)Index.Author].Width = (int)(tableSize.Width * 0.2);
			Columns[(int)Index.Genre].Width = 120;
			Columns[(int)Index.Universe].Width = 0;
			Columns[(int)Index.Series].Width = (int)(tableSize.Width * 0.2);
			Columns[(int)Index.Volume].Width = 45;
			Columns[(int)Index.Chapter].Width = 0;
			Columns[(int)Index.Page].Width = 0;
			Columns[(int)Index.Pages].Width = 0;
			Columns[(int)Index.Year].Width = 0;
			Columns[(int)Index.State].Width = 100;
			Columns[(int)Index.Comment].Width = 0;

			Columns[(int)Index.Name].Width = (tableSize.Width - 
				Columns[(int)Index.Author].Width -
				Columns[(int)Index.Genre].Width - 
				Columns[(int)Index.Series].Width - 
				Columns[(int)Index.Volume].Width - 
				Columns[(int)Index.State].Width - 
				scrollbarWidth);
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
