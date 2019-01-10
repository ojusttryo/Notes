using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Notes.Notes;

namespace Notes.NoteTables
{
	class SerialTable : NoteTable
	{
		enum Index
		{
			Id,
			Name,
			Season,
			SeasonButton,
			Episode,
			EpisodeButton,
			State,
			Comment
		}


		public SerialTable(Point location, string tableName):
			base(location, tableName)
		{

		}


		public override void CreateColumns()
		{
			// Add columns
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());	// Кнопка в текстовом поле смотрится красивее.
			Columns.Add(new DataGridViewTextBoxColumn());
			Columns.Add(new DataGridViewTextBoxColumn());	// Кнопка в текстовом поле смотрится красивее. 
			Columns.Add(CreateStateColumn());
			Columns.Add(new DataGridViewTextBoxColumn());

			Columns[(int)Index.Season].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			Columns[(int)Index.SeasonButton].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			Columns[(int)Index.Episode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			Columns[(int)Index.EpisodeButton].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

			// Columns settings
			Columns[(int)Index.Id].Name = "Id";
			Columns[(int)Index.Name].Name = "Name";
			Columns[(int)Index.Season].Name = "Season";
			Columns[(int)Index.SeasonButton].Name = "";
			Columns[(int)Index.Episode].Name = "Episode";
			Columns[(int)Index.EpisodeButton].Name = "";
			Columns[(int)Index.State].Name = "State";
			Columns[(int)Index.Comment].Name = "Comment";

			foreach (DataGridViewColumn column in Columns)
				column.ReadOnly = true;

			// Состояние можно редактировать напрямую.
			Columns[(int)Index.State].ReadOnly = false;

			HideColumn((int)Index.Id);
			HideColumn((int)Index.Comment);

			// Здесь обрабатывается только нажатие на кнопку + для увеличения номера серии или сезона.
			CellContentClick += delegate (object o, DataGridViewCellEventArgs e)
			{
				if (!CallCustomEvents)
					return;
				
				// Индекс строки может быть меньше нуля, если кликнули по заголовку. 
				// Столбец с кнопкой должен всегда иметь слева другой столбец, к которому он привязан (серия, сезон).
				if (e.RowIndex < 0 || e.ColumnIndex < 1)
					return;

				// При инкременте серии просто увеличиваем ее на 1. Но при инкременте сезона, номер серии должен сбрасываться в 0,
				// ведь выполняется переход на новый сезон, где еще ничего не просмотрено.
				if (Columns[e.ColumnIndex - 1].Name == "Episode")
				{
					IncrementCellValue(e.RowIndex, e.ColumnIndex - 1);
					Database.InsertOrUpdate(TableNameInDatabase, GetNoteFromSelectedRow());
				}
				else if (Columns[e.ColumnIndex - 1].Name == "Season")
				{
					IncrementCellValue(e.RowIndex, e.ColumnIndex - 1);
					SetCellValue("Episode", e.RowIndex, 0);
					Database.InsertOrUpdate(TableNameInDatabase, GetNoteFromSelectedRow());
				}
			};
		}


		private void IncrementCellValue(int rowIndex, int columnIndex)
		{
			DataGridViewCell editedCell = Rows[rowIndex].Cells[columnIndex];
			int currentCellValue = editedCell.Value.ToString().ToIntOrDefault();
			currentCellValue++;
			editedCell.Value = currentCellValue.ToString();
		}


		private void SetCellValue(string columnName, int rowIndex, int value)
		{
			if (!Columns.Contains(columnName))
				return;

			DataGridViewColumn column = Columns[columnName];
			DataGridViewCell editedCell = Rows[rowIndex].Cells[column.Index];
			editedCell.Value = value.ToString();
		}


		public override bool AddNote(Note note)
		{
			if (note is Serial)
			{
				Serial serial = note as Serial;
				
				Rows.Add(new string[] 
				{
					serial.Id.ToString(),
					serial.Name,
					serial.Season.ToString(),
					"+",
					serial.Episode.ToString(),
					"+",
					States[(int)serial.CurrentState],
					serial.Comment
				});

				return true;
			}

			return false;
		}


		public override void UpdateNote(Note note)
		{
			Serial serial = note as Serial;
			if (serial == null)
				return;

			CurrentRow.Cells[(int)Index.Name].Value =              serial.Name;
			CurrentRow.Cells[(int)Index.Season].Value =            serial.Season.ToString();
			CurrentRow.Cells[(int)Index.Episode].Value =           serial.Episode.ToString();
			CurrentRow.Cells[(int)Index.State].Value = States[(int)serial.CurrentState];
			CurrentRow.Cells[(int)Index.Comment].Value =           serial.Comment;
		}


		public override Note GetNoteFromSelectedRow()
		{
			if (CurrentRow == null)
				return null;

			Serial serial = new Serial();
			serial.Id =           CurrentRow.Cells[(int)Index.Id].Value.ToString().ToIntOrException();
			serial.Name =         CurrentRow.Cells[(int)Index.Name].Value.ToString();
			serial.Season =       CurrentRow.Cells[(int)Index.Season].Value.ToString().ToIntOrDefault();
			serial.Episode =      CurrentRow.Cells[(int)Index.Episode].Value.ToString().ToIntOrDefault();
			serial.CurrentState = CurrentRow.Cells[(int)Index.State].Value.ToString().ToNoteState();
			serial.Comment =      CurrentRow.Cells[(int)Index.Comment].Value.ToString();

			return serial;
		}


		public override void ChangeSize(Size tableSize)
		{
			Size = tableSize;		

			Columns[(int)Index.Season].Width = 50;
			Columns[(int)Index.SeasonButton].Width = 20;
			Columns[(int)Index.Episode].Width = 50;
			Columns[(int)Index.EpisodeButton].Width = 20;
			Columns[(int)Index.State].Width = 100;
			SetRemainingTableWidth((int)Index.Name);
		}


		public override string[] SearchFields
		{
			get
			{
				return new string[] { "Name" };
			}
		}
	}
}
