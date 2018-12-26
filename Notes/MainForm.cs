using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Notes.NoteTables;

namespace Notes
{
	public partial class MainForm : Form
	{
		private NoteTable _currentNoteTable;

		private NoteTable _films;

		private NoteTable _games;


		public MainForm()
		{
			InitializeComponent();

			BackColor = Color.White;
			Resize += new EventHandler(MainForm_Resize);

			Point noteTableLocation = new Point(0, menu.Height);

			_films = new DatedNoteTable(noteTableLocation);
			_games = new DatedNoteTable(noteTableLocation);
			_currentNoteTable = _films;

			Controls.Add(_currentNoteTable);

			this.OnResize(null);
		}


		private void MainForm_Resize(object sender, EventArgs e)
		{
			Size noteTableSize = new Size(searchButton.Location.X, this.Height - menu.Height);

			if (_currentNoteTable != null)
				_currentNoteTable.ChangeSize(noteTableSize);
		}
	}
}
