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
			PlayersCount,
			State,
			Comment
		}


		public static string[] PlayersCount = { "Not defined", "Singleplayer", "Multiplayer", "Mixed" };
		

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
			Columns[(int)Index.PlayersCount].Name = "Type";
			Columns[(int)Index.State].Name        = "State";			
			Columns[(int)Index.Comment].Name      = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[(int)Index.State].ReadOnly = false;

			HideColumn((int)Index.Id);
			HideColumn((int)Index.DownloadLink);
			HideColumn((int)Index.Login);
			HideColumn((int)Index.Password);
			HideColumn((int)Index.Email);
			HideColumn((int)Index.Comment);
		}


		public override bool AddNote(Note note)
		{			
			if (note is Game)
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
					PlayersCount[(int)game.Players],
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
			CurrentRow.Cells[(int)Index.PlayersCount].Value = PlayersCount[(int)game.Players];
			CurrentRow.Cells[(int)Index.State].Value        = States[(int)game.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value      = game.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;
			
			Game game = new Game();
			game.Id =           CurrentRow.Cells[(int)Index.Id].Value.ToString().ToIntOrException();
			game.Name =         CurrentRow.Cells[(int)Index.Name].Value.ToString();
			game.Version =      CurrentRow.Cells[(int)Index.Version].Value.ToString();
			game.Genre =        CurrentRow.Cells[(int)Index.Genre].Value.ToString();
			game.DownloadLink = CurrentRow.Cells[(int)Index.DownloadLink].Value.ToString();
			game.Login =        CurrentRow.Cells[(int)Index.Login].Value.ToString();
			game.Password =     CurrentRow.Cells[(int)Index.Password].Value.ToString();
			game.Email =        CurrentRow.Cells[(int)Index.Email].Value.ToString();
			game.Players =      CurrentRow.Cells[(int)Index.PlayersCount].Value.ToString().ToPlayersCount();
			game.CurrentState = CurrentRow.Cells[(int)Index.State].Value.ToString().ToNoteState();
			game.Comment =      CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return game;
		}


		public override void ChangeSize(Size tableSize)
		{
			Size = tableSize;		

			Columns[(int)Index.Version].Width = 100;
			Columns[(int)Index.Genre].Width = 170;
			Columns[(int)Index.PlayersCount].Width = 80;
			Columns[(int)Index.State].Width = 100;
			SetRemainingTableWidth((int)Index.Name);
		}


		public override string[] SearchFields
		{
			get { return new string[] { "Name", "Genre", "Email" }; }
		}
	}
}
