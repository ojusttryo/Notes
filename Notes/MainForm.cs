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
			// Кнопки
			const int borderWidth = 15;
			int newButtonPointX = this.Width - searchButton.Width - borderWidth;
			foreach (Control c in Controls)
			{
				if (c is Button)
					c.Location = new Point(newButtonPointX, c.Location.Y);
			}

			// Таблица. Она зависит от расположения кнопок, потому выполняется после них.
			if (_currentNoteTable != null)
			{
				int tableWidth = searchButton.Location.X;
				int tableHeight = this.Height - menu.Location.Y - menu.Height - 40;	// Не знаю, что там еще на 40 пикселей висит. Может заголовок.
				Size noteTableSize = new Size(tableWidth, tableHeight);

				_currentNoteTable.ChangeSize(noteTableSize);
			}
		}
	}
}
