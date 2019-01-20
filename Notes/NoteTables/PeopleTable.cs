using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.Notes;

namespace Notes.NoteTables
{
	class PeopleTable : NoteTable
	{
		enum Index
		{
			Id,
			Name,
			Nickname,
			Birthdate,
			Address,
			Contacts,
			Sex,
			State,
			Comment
		}


		public static string[] Sex = { "Not selected", "Male", "Female" };


		public PeopleTable(Point location):
			base(location, "People", "People")
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
			Columns[(int)Index.Id].Name = "Id";
			Columns[(int)Index.Name].Name = "Name";			
			Columns[(int)Index.Nickname].Name = "Nickname";
			Columns[(int)Index.Birthdate].Name = "Birthdate";
			Columns[(int)Index.Address].Name = "Address";
			Columns[(int)Index.Contacts].Name = "Contacts";
			Columns[(int)Index.Sex].Name = "Sex";
			Columns[(int)Index.State].Name = "State";			
			Columns[(int)Index.Comment].Name = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[(int)Index.State].ReadOnly = false;

			HideColumn((int)Index.Id);
			HideColumn((int)Index.Address);
			HideColumn((int)Index.Contacts);
			HideColumn((int)Index.Comment);		
		}


		public override bool AddNote(Note note)
		{
			if (note is Person)
			{
				Person person = note as Person;
				
				Rows.Add(new string[] 
				{
					person.Id.ToString(),
					person.Name,
					person.Nickname,
					person.Birthdate,
					person.Address,
					person.Contacts,
					Sex[(int)person.Sex],
					States[(int)person.CurrentState],
					person.Comment
				});

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			Person person = note as Person;
			if (person == null)
				return;

			CurrentRow.Cells[(int)Index.Name].Value = person.Name;
			CurrentRow.Cells[(int)Index.Nickname].Value = person.Nickname;
			CurrentRow.Cells[(int)Index.Birthdate].Value = person.Birthdate;
			CurrentRow.Cells[(int)Index.Address].Value = person.Address;
			CurrentRow.Cells[(int)Index.Contacts].Value = person.Contacts;
			CurrentRow.Cells[(int)Index.Sex].Value = Sex[(int)person.Sex];
			CurrentRow.Cells[(int)Index.State].Value = States[(int)person.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value = person.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			Person person = new Person();
			person.Id =           CurrentRow.Cells[(int)Index.Id].Value.ToString().ToIntOrException();
			person.Name =         CurrentRow.Cells[(int)Index.Name].Value.ToString();
			person.Nickname =     CurrentRow.Cells[(int)Index.Nickname].Value.ToString();
			person.Birthdate =    CurrentRow.Cells[(int)Index.Birthdate].Value.ToString();
			person.Address =      CurrentRow.Cells[(int)Index.Address].Value.ToString();
			person.Contacts =     CurrentRow.Cells[(int)Index.Contacts].Value.ToString();
			person.Sex =          CurrentRow.Cells[(int)Index.Sex].Value.ToString().ToSex();
			person.CurrentState = CurrentRow.Cells[(int)Index.State].Value.ToString().ToNoteState();
			person.Comment =      CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return person;
		}


		public override void ChangeSize(Size tableSize)
		{
			Size = tableSize;

			Columns[(int)Index.Nickname].Width = (int)(tableSize.Width * 0.30);
			Columns[(int)Index.Birthdate].Width = 65;
			Columns[(int)Index.Sex].Width = 80;
			Columns[(int)Index.State].Width = 100;
			SetRemainingTableWidth((int)Index.Name);
		}


		public override string[] SearchFields
		{
			get
			{
				return new string[] { "Name", "Nickname", "Address" };
			}
		}
	}
}
