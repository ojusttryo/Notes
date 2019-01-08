﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.Notes;



namespace Notes.NoteTables
{
	class GameTable : NoteTable
	{
		enum Index
		{
			Id,
			Name,
			Version,
			Genre,
			DownloadLink,
			Login,
			Password,
			Email,
			State,
			Comment
		}
		

		public GameTable(Point location):
			base(location, "Games")
		{

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
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(CreateStateColumn());
			Columns.Add(new DataGridViewTextBoxColumn());

			// Columns settings
			Columns[(int)Index.Id].Name           = "Id";
			Columns[(int)Index.Name].Name         = "Name";
			Columns[(int)Index.Version].Name      = "Version";
			Columns[(int)Index.Genre].Name        = "Genre";
			Columns[(int)Index.DownloadLink].Name = "Download link";
			Columns[(int)Index.Login].Name        = "Login";
			Columns[(int)Index.Password].Name     = "Password";
			Columns[(int)Index.Email].Name        = "Email";
			Columns[(int)Index.State].Name        = "State";			
			Columns[(int)Index.Comment].Name      = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[(int)Index.State].ReadOnly = false;

			// Большая часть столбцов не отображается.
			Columns[(int)Index.Id].Visible = false;
			Columns[(int)Index.DownloadLink].Visible = false;
			Columns[(int)Index.Login].Visible = false;
			Columns[(int)Index.Password].Visible = false;
			Columns[(int)Index.Email].Visible = false;
			Columns[(int)Index.Comment].Visible = false;
		}


		public override bool AddNote(Note note)
		{			
			if (note is Program)
			{
				Game game = note as Game;
				
				Rows.Add(new string[] 
				{
					game.Id.ToString(),
					game.Name,
					game.Version,
					game.Genre,
					game.DownloadLink,
					game.Login,
					game.Password,
					game.Email,
					States[(int)game.CurrentState],
					game.Comment
				});

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			Game game = note as Game;
			if (game == null)
				return;

			CurrentRow.Cells[(int)Index.Name].Value         = game.Name;
			CurrentRow.Cells[(int)Index.Version].Value      = game.Version;
			CurrentRow.Cells[(int)Index.Genre].Value        = game.Genre;
			CurrentRow.Cells[(int)Index.DownloadLink].Value = game.DownloadLink;
			CurrentRow.Cells[(int)Index.Login].Value        = game.Login;
			CurrentRow.Cells[(int)Index.Password].Value     = game.Password;
			CurrentRow.Cells[(int)Index.Email].Value        = game.Email;
			CurrentRow.Cells[(int)Index.State].Value        = States[(int)game.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value      = game.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;
			
			Game game = new Game();
			game.Id = Int32.Parse(CurrentRow.Cells[(int)Index.Id].Value.ToString());
			game.Name =           CurrentRow.Cells[(int)Index.Name].Value.ToString();
			game.Version =        CurrentRow.Cells[(int)Index.Version].Value.ToString();
			game.Genre =          CurrentRow.Cells[(int)Index.Genre].Value.ToString();
			game.DownloadLink =   CurrentRow.Cells[(int)Index.DownloadLink].Value.ToString();
			game.Login =          CurrentRow.Cells[(int)Index.Login].Value.ToString();
			game.Password =       CurrentRow.Cells[(int)Index.Password].Value.ToString();
			game.Email =          CurrentRow.Cells[(int)Index.Email].Value.ToString();
			game.CurrentState = (Note.State)GetStateIndex(CurrentRow.Cells[(int)Index.State].Value.ToString());
			game.Comment =        CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return game;
		}


		public override void ChangeSize(Size tableSize)
		{
			int scrollbarWidth = VerticalScrollBar.Visible ? VerticalScrollBar.Width : 0;

			Width = tableSize.Width;
			Height = tableSize.Height;			

			Columns[(int)Index.Id].Width = 0;
			Columns[(int)Index.Name].Width = 0;
			Columns[(int)Index.Version].Width = 100;
			Columns[(int)Index.Genre].Width = 200;
			Columns[(int)Index.DownloadLink].Width = 0;
			Columns[(int)Index.Login].Width = 0;
			Columns[(int)Index.Password].Width = 0;
			Columns[(int)Index.Email].Width = 0;
			Columns[(int)Index.State].Width = 100;
			Columns[(int)Index.Comment].Width = 0;
			Columns[(int)Index.Name].Width = this.Width - 
				Columns[(int)Index.Version].Width -
				Columns[(int)Index.State].Width - 
				Columns[(int)Index.Genre].Width - 
				scrollbarWidth;
		}


		public override string[] SearchFields
		{
			get { return new string[] { "Name", "Genre", "Email" }; }
		}
	}
}