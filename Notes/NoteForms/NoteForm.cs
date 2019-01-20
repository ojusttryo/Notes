using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Notes.NoteTables;
using Notes.Notes;

namespace Notes.NoteForms
{
	public class NoteForm : Form
	{
		public enum Mode
		{
			Add,
			Edit
		}

		public Mode OpenMode { get; set; }

		protected MainForm _mainForm;

		protected NoteTable _editedTable;

		protected Note _editedNote;


		private NoteForm()
		{
			// Forbidden
		}


		public NoteForm(MainForm mainForm, NoteTable editedTable, Mode mode)
		{
			_mainForm = mainForm;
			_editedTable = editedTable;
			_editedNote = (mode == Mode.Edit) ? _editedTable.GetNoteFromSelectedRow() : null;
			OpenMode = mode;

			KeyDown += delegate (object o, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Escape)
					Close();
			};
		}


		protected string GetFormText()
		{
			switch (OpenMode)
			{
				case Mode.Add:  return string.Format("{0} - Add",  _editedTable.TableName);
				case Mode.Edit: return string.Format("{0} - Edit", _editedTable.TableName);
				default: return "Note";
			}
		}


		protected string GetSubmitButtonText()
		{
			switch (OpenMode)
			{
				case Mode.Add: return "Add";
				case Mode.Edit: return "Edit";
				default: return "Submit";
			}
		}
	}
}
