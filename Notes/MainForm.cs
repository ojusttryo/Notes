using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Notes.Notes;
using Notes.NoteTables;
using Notes.AddForms;

namespace Notes
{
	public partial class MainForm : Form
	{
		private NoteTable _currentNoteTable;

		private NoteTable _films;
		
		/// <summary>
		/// Ширина границы окна. Нужна для правильного позиционирования элементов.
		/// </summary>
		private int _borderWidth = 0;

		/// <summary>
		/// Высота заголовка главного окна. Нужна для правильного позиционирования элементов.
		/// </summary>
		private int _titleHeight = 0;


		public MainForm()
		{
			InitializeComponent();

			_borderWidth = (this.Width - this.ClientRectangle.Width) / 2;
			_titleHeight = this.Height - this.ClientRectangle.Height - _borderWidth * 2;

			BackColor = Color.White;
			Resize += new EventHandler(MainForm_Resize);

			Point noteTableLocation = new Point(this.ClientRectangle.Location.X, this.ClientRectangle.Location.Y + menu.Height);

			_films = new DatedNoteTable(noteTableLocation, "Films");
			_currentNoteTable = _films;

			Controls.Add(_currentNoteTable);

			this.OnResize(null);
		}


		/// <summary>
		/// Обновление размеров и позиций графических элементов при любом изменении размера окна. 
		/// </summary>
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

		private void addButton_Click(object sender, EventArgs e)
		{
			switch (_currentNoteTable.TableNameInDatabase)
			{
				case "Films": new AddDatedNoteForm(this, "Add film", "Add").ShowDialog(); break;
					
				default: break;
			}
		}


		private void deleteButton_Click(object sender, EventArgs e)
		{
			// Обработка для пустой таблицы.
			DataGridViewRow currentRow = _currentNoteTable.CurrentRow;
			if (currentRow == null)
				return;

			// Подразумевается, что пользователь будет в основном менять состояние заметки на Deleted, а не физически удалять из базы.
			// Поэтому здесь можно спросить подтверждение. Это должно происходить довольно редко и не будет напрягать.
			DialogResult answer = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
			if (answer == DialogResult.No)
				return;

			int id = Int32.Parse(currentRow.Cells[0].Value.ToString());		// Там всегда корректный Id. Пользователь не имеет доступа к ячейке.

			bool deleted = Database.DeleteNote(_currentNoteTable.TableNameInDatabase, id);
			if (!deleted)
				MessageBox.Show("Unknown error. Cannot delete note.");
			else
				_currentNoteTable.Rows.RemoveAt(currentRow.Index);
		}


		private void editButton_Click(object sender, EventArgs e)
		{
			switch (_currentNoteTable.TableNameInDatabase)
			{
				case "Films": new AddDatedNoteForm(this, "Edit film", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
					
				default: break;
			}
		}



		/// <summary>
		/// Добавляет или обновляет заметку.
		/// </summary>
		/// <param name="note"></param>
		public void AddOrUpdateNote(Note note)
		{
			// Заметка должна добавиться как в базу данных, так и в таблицу. Если куда-то не удалось, то вообще не добавлять.
			int result = Database.InsertOrUpdate(_currentNoteTable.TableNameInDatabase, note);
			switch (result)
			{
				case 0: break;     // Ничего не добавлено. Если из-за исключения, то это в данный момент не проверяется. Можно посмотреть в логе.
				case 1: _currentNoteTable.AddNote(note); break;
				case 2: _currentNoteTable.UpdateNote(note); break;
				default: break;
			}
		}
	}
}
