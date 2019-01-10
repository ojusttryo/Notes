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
using Notes.NoteForms;

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

		private Dictionary<string, NoteTable> _noteTables;

		private NoteTable _currentNoteTable;


		private ToolStripMenuItem lastClickedStateItem = null;


		public MainForm()
		{
			InitializeComponent();			

			_borderWidth = (this.Width - this.ClientRectangle.Width) / 2;
			_titleHeight = this.Height - this.ClientRectangle.Height - _borderWidth * 2;

			BackColor = Color.White;
			Resize += new EventHandler(MainForm_Resize);

			Point noteTableLocation = new Point(ClientRectangle.Location.X, addButton.Location.Y + addButton.Height + _indentBetweenElements);

			_noteTables = new Dictionary<string, NoteTable>();
			_noteTables.Add("AnimeFilms", new DatedNoteTable(noteTableLocation, "AnimeFilms"));
			_noteTables.Add("Films", new DatedNoteTable(noteTableLocation, "Films"));
			_noteTables.Add("Performances", new DatedNoteTable(noteTableLocation, "Performances"));
			_noteTables.Add("Literature", new LiteratureTable(noteTableLocation));
			_noteTables.Add("Bookmarks", new BookmarkTable(noteTableLocation));
			_noteTables.Add("Meal", new MealTable(noteTableLocation));
			_noteTables.Add("Programs", new ProgramTable(noteTableLocation));
			_noteTables.Add("Games", new GameTable(noteTableLocation));
			_noteTables.Add("People", new PeopleTable(noteTableLocation));
			_noteTables.Add("Serials", new SerialTable(noteTableLocation, "Serials"));
			_noteTables.Add("AnimeSerials", new SerialTable(noteTableLocation, "AnimeSerials"));
			_noteTables.Add("TVShows", new SerialTable(noteTableLocation, "TVShows"));
			_noteTables.Add("Desires", new DesireTable(noteTableLocation));

			SwitchToTable("Desires");

			// Без этого не отображается вертикальный скролл бар в таблице при первом открытии.
			this.Shown += delegate (object o, EventArgs e) { OnResize(null); };
		}


		private void SwitchToTable(string tableName, string title = null)
		{
			sexToolStripMenuItem.Visible = (tableName == "People");

			this.Text = (string.IsNullOrEmpty(title)) ? tableName : title;

			if (_currentNoteTable != null)
				Controls.Remove(_currentNoteTable);

			_currentNoteTable = _noteTables[tableName];
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

				searchComboBox.Width = maxItemWidth + 20;   // 20 - скролбар + небольшой отступ
			}
		}


		/// <summary>
		/// Обновление размеров и позиций графических элементов при любом изменении размера окна. 
		/// </summary>
		private void MainForm_Resize(object sender, EventArgs e)
		{
			searchButton.Location = new Point(ClientRectangle.Width - searchButton.Width - _indentBetweenElements * 2, searchButton.Location.Y);
			searchComboBox.Location = new Point(searchButton.Location.X - searchComboBox.Width - _indentBetweenElements, searchComboBox.Location.Y);
			searchTextBox.Location = new Point(settingsButton.Location.X + settingsButton.Width + _indentBetweenElements, searchTextBox.Location.Y);
			searchTextBox.Width = searchComboBox.Location.X - settingsButton.Location.X - settingsButton.Width - 2 * _indentBetweenElements;
			if (searchTextBox.Width > 400)
			{
				searchTextBox.Width = 400;
				searchComboBox.Location = new Point(searchTextBox.Location.X + searchTextBox.Width + _indentBetweenElements, searchComboBox.Location.Y);
				searchButton.Location = new Point(searchComboBox.Location.X + searchComboBox.Width + _indentBetweenElements, searchButton.Location.Y);
			}

			// Таблица
			if (_currentNoteTable != null)
			{
				int tableWidth = this.ClientRectangle.Width;
				int tableHeight = this.ClientRectangle.Height - addButton.Location.Y - addButton.Height - _indentBetweenElements;
				Size noteTableSize = new Size(tableWidth, tableHeight);

				_currentNoteTable.ChangeSize(noteTableSize);
			}
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			switch (_currentNoteTable.TableNameInDatabase)
			{
				case "AnimeFilms":   new DatedNoteForm( this, "Add anime film", "Add").ShowDialog(); break;
				case "Films":        new DatedNoteForm( this, "Add film", "Add").ShowDialog(); break;
				case "Performances": new DatedNoteForm( this, "Add performance", "Add").ShowDialog(); break;
				case "Literature":   new LiteratureForm(this, "Add literature", "Add").ShowDialog(); break;
				case "Bookmarks":    new BookmarkForm(  this, "Add bookmark", "Add").ShowDialog(); break;
				case "Meal":         new MealForm(      this, "Add meal", "Add").ShowDialog(); break;
				case "Programs":     new ProgramForm(   this, "Add program", "Add").ShowDialog(); break;
				case "Games":        new GameForm(      this, "Add game", "Add").ShowDialog(); break;
				case "People":       new PersonForm(    this, "Add person", "Add").ShowDialog(); break;
				case "Serials":      new SerialForm(    this, "Add serial", "Add").ShowDialog(); break;
				case "AnimeSerials": new SerialForm(    this, "Add anime serial", "Add").ShowDialog(); break;
				case "TVShows":      new SerialForm(    this, "Add TV show", "Add").ShowDialog(); break;
				case "Desires":      new DesireForm(    this, "Add desire", "Add").ShowDialog(); break;
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

			int id = Int32.Parse(currentRow.Cells[0].Value.ToString());     // Там всегда корректный Id. Пользователь не имеет доступа к ячейке.

			bool deleted = Database.DeleteNote(_currentNoteTable.TableNameInDatabase, id);
			if (!deleted)
				MessageBox.Show("Unknown error. Cannot delete note.");
			else
				_currentNoteTable.Rows.RemoveAt(currentRow.Index);

			OnResize(null);
		}


		private void editButton_Click(object sender, EventArgs e)
		{
			StartCurrentNoteEditing();
		}


		public void StartCurrentNoteEditing()
		{
			if (_currentNoteTable.CurrentRow == null)
				return;

			// Временно запрещается вызов событий, чтоб не было повторного сохранения изменений в таблице.
			_currentNoteTable.CallCustomEvents = false;

			switch (_currentNoteTable.TableNameInDatabase)
			{
				case "AnimeFilms":   new DatedNoteForm( this, "Edit anime film", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Films":        new DatedNoteForm( this, "Edit film", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Performances": new DatedNoteForm( this, "Edit performance", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Literature":   new LiteratureForm(this, "Edit literature", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Bookmarks":    new BookmarkForm(  this, "Edit bookmark", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Meal":         new MealForm(      this, "Edit meal", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Programs":     new ProgramForm(   this, "Edit program", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Games":        new GameForm(      this, "Edit game", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "People":       new PersonForm(    this, "Edit person", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Serials":      new SerialForm(    this, "Edit serial", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "AnimeSerials": new SerialForm(    this, "Edit anime serial", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "TVShows":      new SerialForm(    this, "Edit TV show", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
				case "Desires":      new DesireForm(    this, "Edit desires", "Edit", _currentNoteTable.GetNoteFromSelectedRow()).ShowDialog(); break;
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
					{
						_currentNoteTable.UpdateNote(note);
					}
					else
					{
						_currentNoteTable.AddNote(note);
						OnResize(null);
					}
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

			// Сделал возможность поиска сразу по нескольким подстрокам, разделенным пробелом. Все подстроки должны содержаться в искомой строке.
			int columnIndex = _currentNoteTable.Columns[fieldName].Index;
			string[] searchSubstrings = searchTextBox.Text.Split(' ').Where(x => x.Trim().Length > 0).ToArray();			
			foreach (DataGridViewRow row in _currentNoteTable.Rows)
			{
				string rowValue = row.Cells[columnIndex].Value.ToString();
				// From https://stackoverflow.com/questions/444798/case-insensitive-containsstring
				CultureInfo culture = CultureInfo.InvariantCulture;
				row.Visible = searchSubstrings.All(x => culture.CompareInfo.IndexOf(rowValue, x.Trim(), CompareOptions.IgnoreCase) >= 0);
			}

			OnResize(null);
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void animeFilmsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("AnimeFilms", "Anime films");
		}

		private void animeSerialsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("AnimeSerials", "Anime serials");
		}

		private void bookmarksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("Bookmarks");
		}

		private void desiresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("Desires");
		}

		private void filmsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("Films");
		}

		private void gamesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("Games");
		}

		private void literatureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("Literature");
		}

		private void mealToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("Meal");
		}

		private void performancesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("Performances");
		}

		private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("People");
		}

		private void programsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("Programs");
		}

		private void serialsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("Serials");
		}

		private void TVshowsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SwitchToTable("TVShows", "TV shows");
		}


		public static void CheckNumericInput(object sender, KeyPressEventArgs e)
		{
			// From https://ourcodeworld.com/articles/read/507/how-to-allow-only-numbers-inside-a-textbox-in-winforms-c-sharp
			// Verify that the pressed key isn't CTRL or any non-numeric digit
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
				e.Handled = true;
		}


		private void StateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// При выборе элемента меню, должны отображаться все заметки с этим состоянием.
			// Если ранее был выполнен поиск, его результаты игнорируются. Выборка делается по всей таблице.
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			if (item != null && stateToolStripMenuItem.DropDownItems.Contains(item))
			{
				int stateIndex = stateToolStripMenuItem.DropDownItems.IndexOf(item);
				// Первый пункт меню выбирает все заметки.
				if (stateIndex == 0)
				{
					foreach (DataGridViewRow row in _currentNoteTable.Rows)
						row.Visible = true;
				}
				else if (_currentNoteTable.Columns.Contains("State"))
				{
					DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn)_currentNoteTable.Columns["State"];
					if (column == null)
						return;

					foreach (DataGridViewRow row in _currentNoteTable.Rows)
					{
						DataGridViewComboBoxCell stateCell = row.Cells[column.Index] as DataGridViewComboBoxCell;
						row.Visible = (stateCell != null && column.Items.IndexOf(stateCell.Value) == stateIndex);
					}
				}
			}
			searchTextBox.Text = "";

			lastClickedStateItem = item;
		}


		private void settingsButton_Click(object sender, EventArgs e)
		{
		


		}

		private void SexToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			const string sexColumn = "Sex";
			if (!_currentNoteTable.Columns.Contains(sexColumn))
				return;

			// Сначала отображаем все заметки, выбранные предыдущим кликом на меню состояния.
			if (lastClickedStateItem != null)
				lastClickedStateItem.PerformClick();
			else
				allStatesToolStripMenuItem.PerformClick();

			// Затем уже выбираем заметки по полу.
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			if (item != null && sexToolStripMenuItem.DropDownItems.Contains(item))
			{
				int sexIndex = sexToolStripMenuItem.DropDownItems.IndexOf(item);
				// Нулевой элемент - любой пол.
				if (sexIndex == 0)
				{
					// Ничего не нужно делать. Все уже выбрано состоянием.						
				}
				else
				{
					DataGridViewColumn column = _currentNoteTable.Columns[sexColumn];

					foreach (DataGridViewRow row in _currentNoteTable.Rows)
					{
						// Строки, скрытые по выборке состояния, не нужно изменять.
						if (row.Visible == false)
							continue;

						DataGridViewCell sexCell = row.Cells[column.Index];
						row.Visible = (sexCell != null && sexCell.Value.ToString() == PeopleTable.Sex[sexIndex]);
					}
				}
			}
		}
	}
}
