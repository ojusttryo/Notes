using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.Notes;
using Notes;
using Notes.DB;

namespace Notes.NoteTables
{
	public abstract class NoteTable : DataGridView
	{
		public string TableNameInDatabase { get; protected set; }


		public string TableName { get; protected set; }


		public static string[] States = { "Not selected", "Active", "Deleted", "Finished", "Postponed", "Waiting" };


		/// <summary>
		/// Свойство указывает на необходимость вызова не дефолтных событий (например, реакция на изменение состояния).
		/// </summary>
		public bool CallCustomEvents { get; set; }


		private NoteTable()
		{
			// Forbidden
		}


		protected NoteTable(Point location, string tableNameInDB, string tableName)
		{
			Location = location;
			TableNameInDatabase = tableNameInDB;
			TableName = tableName;

			Initialize();
			CreateColumns();

			LoadNotes();
		}


		private void LoadNotes()
		{
			List<Note> notes = Database.SelectNotes(TableNameInDatabase);
			foreach (Note note in notes)
				AddNote(note);
		}


		public void ReloadNotes()
		{
			Rows.Clear();
			LoadNotes();
		}


		private void Initialize()
		{
			CallCustomEvents = true;

			MultiSelect = true;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
			AllowUserToResizeRows = false;

			EditMode = DataGridViewEditMode.EditOnEnter;

			DefaultCellStyle.SelectionBackColor = Color.LightCyan;
			DefaultCellStyle.SelectionForeColor = Color.Black;
			BackgroundColor = Color.White;

			RowHeadersVisible = false;

			BorderStyle = BorderStyle.None;
			AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;

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
					case "State": Database.Update(TableNameInDatabase, GetNoteFromSelectedRow()); break;
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


			this.ContextMenu = new ContextMenu();
			MenuItem deleteItem = new MenuItem("Delete");
			deleteItem.Click += delegate (object o, EventArgs e) { DeleteSelectedNotes(); };
			this.ContextMenu.MenuItems.Add(deleteItem);


			CellMouseDown += delegate (object o, DataGridViewCellMouseEventArgs e)
			{
				Point cursorPosition = this.PointToClient(Cursor.Position);

				// Если были выбраны одни строки, а правый клик выполнен на другой строке, то надо удалить только ее.
				// Так что убираем выделение и ставим новое.
				HitTestInfo hitInfo = HitTest(cursorPosition.X, cursorPosition.Y);
				if (hitInfo != null && hitInfo.RowIndex >= 0 && hitInfo.ColumnIndex >= 0)
				{
					// Только когда нет ни контрола, ни шифта - т.е. ничего не делаем, если выбираются строки для удаления.
					if (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.None)
					{
						DataGridViewRow row = Rows[hitInfo.RowIndex];
						if (!row.Selected)
						{
							this.ClearSelection();
							row.Cells[1].Selected = true;
							row.Selected = true;
						}
					}
				}

				if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.Button == MouseButtons.Right)
					this.ContextMenu.Show(this, cursorPosition);
			};
		}


		public void DeleteSelectedNotes()
		{
			if (SelectedRows == null)
				return;

			// Подразумевается, что пользователь будет в основном менять состояние заметки на Deleted, а не физически удалять из базы.
			// Поэтому здесь можно спросить подтверждение. Это должно происходить довольно редко и не будет напрягать.
			DialogResult answer = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
			if (answer == DialogResult.No)
				return;
			
			List<int> identifiers = new List<int>();
			foreach (DataGridViewRow row in SelectedRows)
				identifiers.Add(row.Cells[0].Value.ToString().ToIntOrException());

			bool deleted = Database.DeleteNotes(TableNameInDatabase, identifiers);
			if (!deleted)
			{
				MessageBox.Show("Unknown error. Cannot delete notes.");
				return;
			}

			foreach (DataGridViewRow row in SelectedRows)
				Rows.RemoveAt(row.Index);

			OnResize(null);
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
