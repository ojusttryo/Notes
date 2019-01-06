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


		static NoteTable()
		{

		}


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

			DefaultCellStyle.SelectionBackColor = Color.LightCyan;
			DefaultCellStyle.SelectionForeColor = Color.Black;
			BackgroundColor = Color.White;

			RowHeadersVisible = false;

			BorderStyle = BorderStyle.None;

			ScrollBars = ScrollBars.Vertical;
			VerticalScrollBar.Visible = true;

			// События для изменения заметки через главную форму. Можно менять только некоторые столбцы.
			// Первое событие нужно, т.к. без него не происходит моментальный вызов второго. Только после выбора какого-то другого элемента в таблице.
			// https://stackoverflow.com/questions/5652957/what-event-catches-a-change-of-value-in-a-combobox-in-a-datagridviewcell
			CurrentCellDirtyStateChanged += delegate(object o, EventArgs e)
			{
				if (!CallCustomEvents)
					return;

				if (IsCurrentCellDirty)
					CommitEdit(DataGridViewDataErrorContexts.Commit);
			};
			CellValueChanged += delegate(object o, DataGridViewCellEventArgs e) 
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
				MainForm mainForm = this.Parent as MainForm;
				if (mainForm != null)
					mainForm.StartCurrentNoteEditing();
			};
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


		/// <summary>
		/// Получить индекс состояния. 
		/// </summary>
		/// <param name="state">Строка состояния, как в интерфейсе.</param>
		/// <returns>Индекс строки состояния или -1, если не найден.</returns>
		protected int GetStateIndex(string state)
		{
			for (int i = 0; i < States.Length; i++)
			{
				if (state == States[i])
					return i;
			}

			return -1;
		}


		public abstract void CreateColumns();

		public abstract bool AddNote(Note note);

		public abstract void UpdateNote(Note note);

		public abstract Note GetNoteFromSelectedRow();

		public abstract void ChangeSize(Size tableSize);

		public abstract string[] SearchFields { get; }
	}
}
