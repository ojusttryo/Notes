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
		}


		public abstract void Initialize();

		public abstract bool AddNote(Note note);

		public abstract void ChangeSize(Size tableSize);
	}
}
