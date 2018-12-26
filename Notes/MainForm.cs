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


		private int _borderWidth = 0;

		private int _titleHeight = 0;


		public MainForm()
		{
			InitializeComponent();

			_borderWidth = (this.Width - this.ClientRectangle.Width) / 2;
			_titleHeight = this.Height - this.ClientRectangle.Height - _borderWidth * 2;

			BackColor = Color.White;
			Resize += new EventHandler(MainForm_Resize);

			Point noteTableLocation = new Point(this.ClientRectangle.Location.X, this.ClientRectangle.Location.Y + menu.Height);

			_films = new DatedNoteTable(noteTableLocation);
			_games = new DatedNoteTable(noteTableLocation);
			_currentNoteTable = _films;

			Controls.Add(_currentNoteTable);

			this.OnResize(null);
		}


		private void MainForm_Resize(object sender, EventArgs e)
		{
			// Кнопки
			int newButtonPointX = this.Width - searchButton.Width - _borderWidth * 2;
			foreach (Control c in Controls)
			{
				if (c is Button)
					c.Location = new Point(newButtonPointX, c.Location.Y);
			}

			// Таблица
			if (_currentNoteTable != null)
			{
				int tableWidth = this.ClientRectangle.Width - searchButton.Width;
				int tableHeight = this.ClientRectangle.Height - menu.Height;
				Size noteTableSize = new Size(tableWidth, tableHeight);

				_currentNoteTable.ChangeSize(noteTableSize);
			}
		}
	}
}
