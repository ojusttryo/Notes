using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.Notes;

namespace Notes.NoteTables
{
	public abstract class NoteTable : DataGridView
	{
		public string TableNameInDatabase { get; protected set; }


		public static string[] States = { "Not selected", "Active", "Deleted", "Finished", "Postponed", "Waiting" };


		/// <summary>
		/// Свойство указывает на необходимость вызова не дефолтных событий (например, реакция на изменение состояния).
		/// </summary>
		public bool CallCustomEvents { get; set; }


		private NoteTable()
		{
			// Forbidden
		}


		protected NoteTable(Point location, string tableName)
		{
			Location = location;
			TableNameInDatabase = tableName;

			Initialize();
			CreateColumns();

			List<Note> notes = Database.GetNotes(TableNameInDatabase);
			foreach (Note note in notes)
				AddNote(note);
		}


		private void Initialize()
		{
			CallCustomEvents = true;

			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;

			EditMode = DataGridViewEditMode.EditOnEnter;

			DefaultCellStyle.SelectionBackColor = Color.LightCyan;
			DefaultCellStyle.SelectionForeColor = Color.Black;
			BackgroundColor = Color.White;

			RowHeadersVisible = false;

			BorderStyle = BorderStyle.None;

			ScrollBars = ScrollBars.Vertical;


			// События для изменения заметки через главную форму. Можно менять только некоторые столбцы. Остальные заблокированы.
			// Первое событие нужно, т.к. без него не происходит моментальный вызов второго. Только после выбора какого-то другого элемента в таблице.
			// https://stackoverflow.com/questions/5652957/what-event-catches-a-change-of-value-in-a-combobox-in-a-datagridviewcell
			CurrentCellDirtyStateChanged += delegate (object o, EventArgs e)
			{
				if (!CallCustomEvents)
					return;

				if (IsCurrentCellDirty)
					CommitEdit(DataGridViewDataErrorContexts.Commit);
			};
			CellValueChanged += delegate (object o, DataGridViewCellEventArgs e) 
			{
				if (!CallCustomEvents)
					return;

				switch (Columns[e.ColumnIndex].Name)
				{
					case "State": Database.InsertOrUpdate(TableNameInDatabase, GetNoteFromSelectedRow()); break;
					// TODO: обработка	 кнопок плюс/минус для серий и сезонов.
					default: break;
				}
			};

			// Двойное нажатие на клетку таблицы должно вызывать меню редактирования текущей заметки.
			CellDoubleClick += delegate (object o, DataGridViewCellEventArgs e)
			{
				if (!CallCustomEvents || e.RowIndex < 0)
					return;

				if (Columns[e.ColumnIndex].Name == "" && e.ColumnIndex > 0)
				{
					// При двойном нажатии на ячейке из столбца с кнопками +, меню редактирования не вызывается. 
					// Хорошо было б вызвать повторно еще нажатие на кнопку, но код ниже пока не работает. В принципе, не критично.

					// Двойной клик по кнопке + вызывает только одиночный инкремент. Нужно вызвать еще. 
					//if (Columns[e.ColumnIndex - 1].Name == "Season" || Columns[e.ColumnIndex - 1].Name == "Season")
					//	OnCellClick(new DataGridViewCellEventArgs(e.ColumnIndex - 1, e.RowIndex));
				}
				else
				{
					MainForm mainForm = this.Parent as MainForm;
					if (mainForm != null)
						mainForm.StartCurrentNoteEditing();
				}
			};
		}


		protected void HideColumn(int index)
		{
			Columns[index].Visible = false;
			Columns[index].Width = 0;
		}


		protected DataGridViewComboBoxColumn CreateStateColumn()
		{
			DataGridViewComboBoxColumn stateColumn = new DataGridViewComboBoxColumn();
			stateColumn.Items.AddRange(States);
			stateColumn.DefaultCellStyle.BackColor = Color.White;
			stateColumn.DefaultCellStyle.SelectionBackColor = Color.White;
			stateColumn.FlatStyle = FlatStyle.Flat;

			return stateColumn;
		}


		protected void SetRemainingTableWidth(int columnIndex)
		{
			Columns[columnIndex].Width = 0;

			int scrollbarWidth = VerticalScrollBar.Visible ? VerticalScrollBar.Width : 0;			

			int columnsWidth = 0;
			foreach (DataGridViewColumn column in Columns)
			{
				if (column.Visible)
					columnsWidth += column.Width;
			}

			// Columns[columnIndex] минимум 5 пикселей, даже если ставим 0. Нужно не потерять их.
			Columns[columnIndex].Width = (this.Width - columnsWidth - scrollbarWidth + Columns[columnIndex].Width);
		}



		public abstract void CreateColumns();

		public abstract bool AddNote(Note note);

		public abstract void UpdateNote(Note note);

		public abstract Note GetNoteFromSelectedRow();

		public abstract void ChangeSize(Size tableSize);

		public abstract string[] SearchFields { get; }
	}
}
