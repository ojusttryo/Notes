using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

using Notes.NoteTables;
using Notes.Notes;

namespace Notes.NoteForms
{
	[ToolboxItem(true)]
	[DesignTimeVisible(true)]
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
				case Mode.Add:  return string.Format("{0} - Add",  _editedTable.TableNameUI);
				case Mode.Edit: return string.Format("{0} - Edit", _editedTable.TableNameUI);
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


		protected void SubmitNote(Note note)
		{
			switch (OpenMode)
			{
				case Mode.Add:  _mainForm.AddNote(note); break;
				case Mode.Edit: _mainForm.UpdateNote(note); break;
			}
		}
	}
}
