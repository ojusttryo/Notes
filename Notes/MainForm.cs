using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;

using Notes.Notes;
using Notes.NoteTables;
using Notes.AddForms;

namespace Notes
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// Ширина границы окна. Нужна для правильного позиционирования элементов.
		/// </summary>
		private int _borderWidth = 0;

		/// <summary>
		/// Высота заголовка главного окна. Нужна для правильного позиционирования элементов.
		/// </summary>
		private int _titleHeight = 0;
		
		/// <summary>
		/// Вертикальный и горизонтальный отступ между элементами на форме.
		/// </summary>
		const int _indentBetweenElements = 5;

		private NoteTable _currentNoteTable;

		private NoteTable _films;

		private NoteTable _animeFilms;

		private NoteTable _performances;


		public MainForm()
		{
			InitializeComponent();

			_borderWidth = (this.Width - this.ClientRectangle.Width) / 2;
			_titleHeight = this.Height - this.ClientRectangle.Height - _borderWidth * 2;

			BackColor = Color.White;
			Resize += new EventHandler(MainForm_Resize);

			Point noteTableLocation = new Point(ClientRectangle.Location.X, addButton.Location.Y + addButton.Height + _indentBetweenElements);

			_animeFilms   = new DatedNoteTable(noteTableLocation, "AnimeFilms");
			_films        = new DatedNoteTable(noteTableLocation, "Films");
			_performances = new DatedNoteTable(noteTableLocation, "Performances");
			SwitchToTable(_films);
		}


		private void SwitchToTable(NoteTable table)
		{
			if (_currentNoteTable != null)
				Controls.Remove(_currentNoteTable);

			_currentNoteTable = table;
			Controls.Add(_currentNoteTable);

			UpdateSearchComboBox();
			ShowAllRows();
			OnResize(null);
		}


		private void ShowAllRows()
		{
			if (_currentNoteTable != null)
			{
				foreach (DataGridViewRow row in _currentNoteTable.Rows)
					row.Visible = true;
			}
		}


		private void UpdateSearchComboBox()
		{
			searchComboBox.Items.Clear();
			if (_currentNoteTable != null)
			{
				searchComboBox.Items.AddRange(_currentNoteTable.SearchFields);
				searchComboBox.SelectedIndex = 0;

				// Ширина выпадающего списка.
				int maxItemWidth = 0;
				Label label = new Label();
				foreach (object item in searchComboBox.Items)
				{
					label.Text = item.ToString();
					if (label.PreferredWidth > maxItemWidth)
						maxItemWidth = label.PreferredWidth;
				}

				searchComboBox.Width = maxItemWidth + 20;	// 20 - скролбар + небольшой отступ
			}
		}


		/// <summary>
		/// Обновление размеров и позиций графических элементов при любом изменении размера окна. 
		/// </summary>
		private void MainForm_Resize(object sender, EventArgs e)
		{
			searchButton.Location   = new Point(ClientRectangle.Width - searchButton.Width - _indentBetweenElements * 2, searchButton.Location.Y);
			searchComboBox.Location = new Point(searchButton.Location.X - searchComboBox.Width - _indentBetweenElements, searchComboBox.Location.Y);
			searchTextBox.Location  = new Point(settingsButton.Location.X + settingsButton.Width + _indentBetweenElements, searchTextBox.Location.Y);
			searchTextBox.Width = searchComboBox.Location.X - settingsButton.Location.X - settingsButton.Width - 2  * _indentBetweenElements;
			if (searchTextBox.Width > 400)
			{
				searchTextBox.Width = 400;
				searchComboBox.Location = new Point(searchTextBox.Location.X + searchTextBox.Width + _indentBetweenElements, searchComboBox.Location.Y);
				searchButton.Location = new Point(searchComboBox.Location.X + searchComboBox.Width + _indentBetweenElements, searchButton.Location.Y);
			}
			
			// Таблица
			if (_currentNoteTable != null)
			{
				int tableWidth = this.Width;
				int tableHeight = this.ClientRectangle.Height;
				Size noteTableSize = new Size(tableWidth, tableHeight);

				_currentNoteTable.ChangeSize(noteTableSize);
			}
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			switch (_currentNoteTable.TableNameInDatabase)
			{
				case "AnimeFilms":   new AddDatedNoteForm(this, "Add anime film",  "Add").ShowDialog(); break;
				case "Films":        new AddDatedNoteForm(this, "Add film",        "Add").ShowDialog(); break;
				case "Performances": new AddDatedNoteForm(this, "Add performance", "Add").ShowDialog(); break;
					
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
			// Временно запрещается вызов событий, чтоб не было повторного сохранения изменений в таблице.
			_currentNoteTable.CallCustomEvents = false;

			switch (_currentNoteTable.TableNameInDatabase)
			{
				case "AnimeFilms":   new AddDatedNoteForm(this, "Edit anime film",  "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Films":        new AddDatedNoteForm(this, "Edit film",        "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Performances": new AddDatedNoteForm(this, "Edit performance", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;

				default: break;
			}

			_currentNoteTable.CallCustomEvents = true;
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
				// Ничего не добавлено. Если из-за исключения, то это в данный момент не проверяется. Можно посмотреть в логе.
				case 0: break;
				// Добавлена или обновлена 1 строка. В MySQL при обновлении будет результат 2, но в SQLite возвращает также 1.
				case 1:
				{
					if (_currentNoteTable.CurrentRow != null && Int32.Parse(_currentNoteTable.CurrentRow.Cells[0].Value.ToString()) == note.Id)
						_currentNoteTable.UpdateNote(note);
					else
						_currentNoteTable.AddNote(note);
					break;
				}
				default: break;
			}
		}

		private void searchButton_Click(object sender, EventArgs e)
		{
			// Можно сделать через запрос к базе. Но как по мне, лучше не перезаписывать данные в таблице новым запросом, а просто скрывать лишнее.

			string fieldName = searchComboBox.GetItemText(searchComboBox.SelectedItem);
			if (!_currentNoteTable.Columns.Contains(fieldName))
				return;

			// При пустом запросе отображаются все строки.
			if (searchTextBox.Text == string.Empty)
			{
				foreach (DataGridViewRow row in _currentNoteTable.Rows)
					row.Visible = true;
			}

			int columnIndex = _currentNoteTable.Columns[fieldName].Index;
			foreach (DataGridViewRow row in _currentNoteTable.Rows)
			{
				if (!Regex.IsMatch(row.Cells[columnIndex].Value.ToString(), searchTextBox.Text, Constant.CommonRegexOptions))
					row.Visible = false;
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void animeFilmsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable(_animeFilms);
		}

		private void animeSerialsToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void bookmarksToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void filmsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable(_films);
		}

		private void gamesToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void literatureToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void performancesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable(_performances);
		}

		private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void programsToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void serialsToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void TVshowsToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
	}
}
