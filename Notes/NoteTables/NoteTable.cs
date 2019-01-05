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


		protected static DataGridViewComboBoxColumn _stateColumn = null;


		static NoteTable()
		{
			_stateColumn = new DataGridViewComboBoxColumn();
			_stateColumn.Items.AddRange(States);
			_stateColumn.DefaultCellStyle.BackColor = Color.White;
			_stateColumn.DefaultCellStyle.SelectionBackColor = Color.White;
			_stateColumn.FlatStyle = FlatStyle.Flat;
		}


		protected NoteTable()
		{
			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;

			DefaultCellStyle.SelectionBackColor = Color.LightCyan;
			DefaultCellStyle.SelectionForeColor = Color.Black;
			BackgroundColor = Color.White;

			RowHeadersVisible = false;

			BorderStyle = BorderStyle.None;

			ScrollBars = ScrollBars.Vertical;

			CellValueChanged += delegate(object o, DataGridViewCellEventArgs e) 
			{
				// TODO: обработка изменения состояния и кнопок плюс/минус для серий и сезонов.
			};
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


		public abstract void Initialize();

		public abstract bool AddNote(Note note);

		public abstract void UpdateNote(Note note);

		public abstract Note GetNoteFromSelectedRow();

		public abstract void ChangeSize(Size tableSize);

		public abstract string[] SearchFields { get; }
	}
}
