using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

using Notes.Notes;
using Notes.CommonUIElements;
using static Notes.Info;

namespace Notes.NoteTables
{
	class BookmarkTable : NoteTable
	{
		enum Index
		{
			Id,
			Name,
			URL,
			Login,
			Password,
			Email,
			State,
			Comment
		}


		public BookmarkTable(Point location):
			base(location, "Bookmarks")
		{
			AddBookmarksContextMenuItems((int)Index.URL);
		}


		public override void CreateColumns()
		{
			// Add columns
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(CreateStateColumn());
			Columns.Add(new DataGridViewTextBoxColumn());

			// Columns settings
			Columns[(int)Index.Id].Name       = "Id";
			Columns[(int)Index.Name].Name     = "Name";
			Columns[(int)Index.URL].Name      = "URL";
			Columns[(int)Index.Login].Name    = "Login";
			Columns[(int)Index.Password].Name = "Password";
			Columns[(int)Index.Email].Name    = "Email";
			Columns[(int)Index.State].Name    = "State";			
			Columns[(int)Index.Comment].Name  = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[(int)Index.State].ReadOnly = false;

			// Большая часть столбцов не отображается.
			HideColumn((int)Index.Id);
			HideColumn((int)Index.URL);
			HideColumn((int)Index.Login);
			HideColumn((int)Index.Password);
			HideColumn((int)Index.Email);
			HideColumn((int)Index.Comment);
		}


		public override bool AddNote(Note note)
		{
			if (note is Bookmark)
			{
				Bookmark bookmark = note as Bookmark;
				
				Rows.Add(new string[] 
				{
					bookmark.Id.ToString(),
					bookmark.Name,
					bookmark.URL,
					bookmark.Login,
					bookmark.Password,
					bookmark.Email,
					States[(int)bookmark.CurrentState],
					bookmark.Comment
				});

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			Bookmark bookmark = note as Bookmark;
			if (bookmark == null)
				return;

			CurrentRow.Cells[(int)Index.Name].Value     = bookmark.Name;
			CurrentRow.Cells[(int)Index.URL].Value      = bookmark.URL;
			CurrentRow.Cells[(int)Index.Login].Value    = bookmark.Login;
			CurrentRow.Cells[(int)Index.Password].Value = bookmark.Password;
			CurrentRow.Cells[(int)Index.Email].Value    = bookmark.Email;
			CurrentRow.Cells[(int)Index.State].Value    = States[(int)bookmark.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value  = bookmark.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			Bookmark bookmark = new Bookmark();
			bookmark.Id =           CurrentRow.Cells[(int)Index.Id].Value.ToString().ToIntOrException();
			bookmark.Name =         CurrentRow.Cells[(int)Index.Name].Value.ToString();
			bookmark.URL =          CurrentRow.Cells[(int)Index.URL].Value.ToString();
			bookmark.Login =        CurrentRow.Cells[(int)Index.Login].Value.ToString();
			bookmark.Password =     CurrentRow.Cells[(int)Index.Password].Value.ToString();
			bookmark.Email =        CurrentRow.Cells[(int)Index.Email].Value.ToString();
			bookmark.CurrentState = CurrentRow.Cells[(int)Index.State].Value.ToString().ToNoteState();
			bookmark.Comment =      CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return bookmark;
		}


		public override void ChangeSize(Size tableSize)
		{
			Size = tableSize;			

			Columns[(int)Index.State].Width = 100;
			SetRemainingTableWidth((int)Index.Name);
		}


		public override string[] SearchFields
		{
			get { return new string[] { "Name", "URL", "Email" }; }
		}
	}
}
