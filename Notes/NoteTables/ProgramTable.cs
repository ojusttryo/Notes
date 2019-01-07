using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.Notes;

namespace Notes.NoteTables
{
	class ProgramTable : NoteTable
	{
		enum Index
		{
			Id,
			Name,
			Version,
			DownloadLink,
			Login,
			Password,
			Email,
			State,
			Comment
		}
		

		public ProgramTable(Point location):
			base(location, "Programs")
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
			Columns.Add(CreateStateColumn());
			Columns.Add(new DataGridViewTextBoxColumn());

			// Columns settings
			Columns[(int)Index.Id].Name           = "Id";
			Columns[(int)Index.Name].Name         = "Name";
			Columns[(int)Index.Version].Name      = "Version";
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
		}


		public override bool AddNote(Note note)
		{			
			if (note is Program)
			{
				Program program = note as Program;
				
				Rows.Add(new string[] 
				{
					program.Id.ToString(),
					program.Name,
					program.Version,
					program.DownloadLink,
					program.Login,
					program.Password,
					program.Email,
					States[(int)program.CurrentState],
					program.Comment
				});

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			Program program = note as Program;
			if (program == null)
				return;

			CurrentRow.Cells[(int)Index.Name].Value         = program.Name;
			CurrentRow.Cells[(int)Index.Version].Value      = program.Version;
			CurrentRow.Cells[(int)Index.DownloadLink].Value = program.DownloadLink;
			CurrentRow.Cells[(int)Index.Login].Value        = program.Login;
			CurrentRow.Cells[(int)Index.Password].Value     = program.Password;
			CurrentRow.Cells[(int)Index.Email].Value        = program.Email;
			CurrentRow.Cells[(int)Index.State].Value        = States[(int)program.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value      = program.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;
			
			Program program = new Program();
			program.Id = Int32.Parse(CurrentRow.Cells[(int)Index.Id].Value.ToString());
			program.Name =           CurrentRow.Cells[(int)Index.Name].Value.ToString();
			program.Version =        CurrentRow.Cells[(int)Index.Version].Value.ToString();
			program.DownloadLink =   CurrentRow.Cells[(int)Index.DownloadLink].Value.ToString();
			program.Login =          CurrentRow.Cells[(int)Index.Login].Value.ToString();
			program.Password =       CurrentRow.Cells[(int)Index.Password].Value.ToString();
			program.Email =          CurrentRow.Cells[(int)Index.Email].Value.ToString();
			program.CurrentState = (Note.State)GetStateIndex(CurrentRow.Cells[(int)Index.State].Value.ToString());
			program.Comment =        CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return program;
		}


		public override void ChangeSize(Size tableSize)
		{
			int scrollbarWidth = VerticalScrollBar.Visible ? VerticalScrollBar.Width : 0;

			Width = tableSize.Width - scrollbarWidth;
			Height = tableSize.Height;			

			Columns[(int)Index.Id].Width = 0;
			Columns[(int)Index.Name].Width = (int)(this.Width * 0.4);
			Columns[(int)Index.Version].Width = 100;
			Columns[(int)Index.DownloadLink].Width = 0;
			Columns[(int)Index.Login].Width = 0;
			Columns[(int)Index.Password].Width = 0;
			Columns[(int)Index.Email].Width = 0;
			Columns[(int)Index.State].Width = 100;
			Columns[(int)Index.Comment].Width = this.Width - 
				Columns[(int)Index.Name].Width - 
				Columns[(int)Index.Version].Width -
				Columns[(int)Index.State].Width - 
				scrollbarWidth;
		}


		public override string[] SearchFields
		{
			get { return new string[] { "Name", "Email" }; }
		}
	}
}
