using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

using Notes.Notes;
using Notes.NoteTables;
using Notes.NoteForms;
using Notes.Import;
using Notes.ProgramSettings;
using Notes.DB;
using static Notes.Info;

namespace Notes
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// Вертикальный и горизонтальный отступ между элементами на форме.
		/// </summary>
		private const int _indentBetweenElements = 5;

		private Dictionary<string, NoteTable> _noteTables;

		private NoteTable _currentNoteTable;

		private ToolStripMenuItem lastClickedStateItem = null;

		private Settings _settings;


		public MainForm(Settings settings)
		{
			_settings = settings;

			InitializeComponent();
			CreateNoteTables();			
			SetEventHandlers();
			SwitchToTable(_settings.DefaultNotesTable);
		}


		private void CreateNoteTables()
		{
			Point noteTableLocation = new Point(ClientRectangle.Location.X, addButton.Location.Y + addButton.Height + _indentBetweenElements);

			_noteTables = new Dictionary<string, NoteTable>();
			_noteTables.Add("Affairs",       new AffairTable(noteTableLocation));
			_noteTables.Add("AnimeFilms",    new DatedNoteTable(noteTableLocation, "AnimeFilms"));
			_noteTables.Add("AnimeSerials",  new SerialTable(noteTableLocation, "AnimeSerials"));
			_noteTables.Add("Bookmarks",     new BookmarkTable(noteTableLocation));
			_noteTables.Add("Desires",       new DescribedNoteTable(noteTableLocation, "Desires"));
			_noteTables.Add("Films",         new DatedNoteTable(noteTableLocation, "Films"));
			_noteTables.Add("Games",         new GameTable(noteTableLocation));
			_noteTables.Add("Literature",    new LiteratureTable(noteTableLocation));
			_noteTables.Add("Meal",          new MealTable(noteTableLocation));
			_noteTables.Add("Performances",  new DatedNoteTable(noteTableLocation, "Performances"));
			_noteTables.Add("People",        new PeopleTable(noteTableLocation));			
			_noteTables.Add("Programs",      new ProgramTable(noteTableLocation));
			_noteTables.Add("RegularDoings", new DescribedNoteTable(noteTableLocation, "RegularDoings"));			
			_noteTables.Add("Serials",       new SerialTable(noteTableLocation, "Serials"));			
			_noteTables.Add("TVShows",       new SerialTable(noteTableLocation, "TVShows"));		
		}
		

		private void SetEventHandlers()
		{
			KeyDown += new KeyEventHandler(MainForm_KeyDown);

			searchTextBox.KeyPress += delegate (object o, KeyPressEventArgs e)
			{
				// Поиск по нажатию Enter
				if (e.KeyChar == (char)13)
				{
					e.Handled = true;
					search();
				}
			};

			Resize += new EventHandler(MainForm_Resize);

			// Без этого не отображается вертикальный скролл бар в таблице при первом открытии.
			Shown += delegate (object o, EventArgs e) 
			{
				OnResize(null);
			};

			// Когда пользователь крутит колесико, то значит хочет прокрутить таблицу. Больше тут просто нечего. 
			// Поэтому сразу фокус на нее.
			MouseWheel += delegate (object o, MouseEventArgs e)
			{
				if (ActiveControl != _currentNoteTable)
					ActiveControl = _currentNoteTable;
			};
		}
		

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			DataGridViewCell currentCell = _currentNoteTable.CurrentCell;
			bool currentCellIsIsNotHeader = (currentCell != null && currentCell.ColumnIndex >= 0 && currentCell.RowIndex >= 0);
			bool tableIsActive = (_currentNoteTable == ActiveControl);

			if (e.Control && e.KeyCode == Keys.F)
				ActiveControl = searchTextBox;
			else if (e.Control && e.KeyCode == Keys.E)
				editButton.PerformClick();
			else if (e.KeyCode == Keys.Add)
				addButton.PerformClick();
			else if (e.KeyCode == Keys.Delete && tableIsActive)
				deleteButton.PerformClick();
			else if (e.KeyCode == Keys.Enter && currentCellIsIsNotHeader && tableIsActive)
				editButton.PerformClick();
		}
		

		private void MainForm_Resize(object sender, EventArgs e)
		{
			searchButton.Location = new Point(ClientRectangle.Width - searchButton.Width - _indentBetweenElements * 2, searchButton.Location.Y);
			searchComboBox.Location = new Point(searchButton.Location.X - searchComboBox.Width - _indentBetweenElements, searchComboBox.Location.Y);
			searchTextBox.Location = new Point(editButton.Location.X + editButton.Width + _indentBetweenElements, searchTextBox.Location.Y);
			searchTextBox.Width = searchComboBox.Location.X - editButton.Location.X - editButton.Width - 2 * _indentBetweenElements;

			const int maxSearchTextBoxWidth = 400;
			if (searchTextBox.Width > maxSearchTextBoxWidth)
			{
				searchTextBox.Width = maxSearchTextBoxWidth;
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


		public void AddNote(Note note)
		{
			List<Note> notes = new List<Note>();
			notes.Add(note);
			AddNotes(notes);
		}


		public void AddNotes(List<Note> notes)
		{
			bool successful = Database.Insert(_currentNoteTable.TableNameDB, notes);
			if (successful)
			{
				foreach (Note note in notes)
					_currentNoteTable.AddNote(note);
				OnResize(null);
			}
		}


		public void UpdateNote(Note note)
		{
			bool successful = Database.Update(_currentNoteTable.TableNameDB, note);
			if (successful)
				_currentNoteTable.UpdateNote(note);
		}
		

		#region File menu

		private void backupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string email = _settings.BackupEmail;
			string password = _settings.BackupPassword;
			string smtpAddress = Regex.Replace(email, @"\A.+@", @"smtp.");

			try
			{
				MailAddress from = new MailAddress(email, "Notes");
				MailAddress to = new MailAddress(email);
				using (MailMessage message = new MailMessage(from, to))
				{
					message.Subject = "Backup";
					message.Body = "";
					message.Attachments.Add(new Attachment("notes.sqlite"));
					using (SmtpClient smtp = new SmtpClient(smtpAddress, 587))
					{
						smtp.Credentials = new NetworkCredential(email, password);
						smtp.EnableSsl = true;
						smtp.Send(message);

						MessageBox.Show("Backup sent");
					}
				}				
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Cannot send backup:{0}{1}", Environment.NewLine, ex.ToString()));
				MessageBox.Show(string.Format("Cannot send backup:{0}{1}", Environment.NewLine, ex.ToString()));
			}
		}


		private void importToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Firefox bookmarks|*.json|Opera bookmarks|*.html|IE bookmarks|*.htm|Database|MEDIA.sqlite";
			dialog.Title = "Select old file";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				string extension = Path.GetExtension(dialog.FileName);

				bool isOldDB = dialog.SafeFileName.EqualsInvariantCI("MEDIA.sqlite");
				bool isJson = Path.GetExtension(dialog.FileName).EqualsInvariantCI(".json");
				bool isHtm = Path.GetExtension(dialog.FileName).EqualsInvariantCI(".htm");
				bool isHtml = Path.GetExtension(dialog.FileName).EqualsInvariantCI(".html");

				if (isOldDB)
					ImportOldDatabase(dialog.FileName);
				else if (isJson || isHtml || isHtm)
					ShowImportingBookmarks(dialog.FileName);
			}
		}


		private void ImportOldDatabase(string filePath)
		{
			Database.Import(filePath, "books");
			Database.Import(filePath, "films");
			Database.Import(filePath, "anime");
			Database.Import(filePath, "serials");

			_noteTables["Literature"].ReloadNotes();
			_noteTables["Films"].ReloadNotes();
			_noteTables["AnimeSerials"].ReloadNotes();
			_noteTables["Serials"].ReloadNotes();
		}


		private void ShowImportingBookmarks(string fileName)
		{
			BookmarksImport import = new BookmarksImport(fileName);
			BookmarksImportForm importForm = new BookmarksImportForm(this);
			List<Bookmark> bookmarks = import.ImportBookmarks();
			bookmarks = Database.CheckForDuplicates(bookmarks);
			foreach (Bookmark b in bookmarks)
				importForm.AddRow(b.Name, b.URL);
			importForm.ShowDialog();
		}


		public void ImportBookmarks(List<Bookmark> bookmarks)
		{
			List<Note> notes = new List<Note>(bookmarks);
			SwitchToTable("Bookmarks");
			AddNotes(notes);
			_noteTables["Bookmarks"].ReloadNotes();			
		}


		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion


		#region Category menu

		private void notesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			if (item == null)
				return;

			SwitchToTable(GetTableNameDB(item.Text));
		}


		private void SwitchToTable(string tableNameDB)
		{
			ReplaceNoteTableInControls(tableNameDB);
			SetSexMenuVisibleProperty(tableNameDB);
			UpdateSearchComboBox();
			SwitchToState();
			OnResize(null);
		}


		private void ReplaceNoteTableInControls(string tableNameDB)
		{
			if (_currentNoteTable != null)
				Controls.Remove(_currentNoteTable);

			_currentNoteTable = _noteTables[tableNameDB];

			// При переключении вкладок нужно чтоб у открытой всегда был правильный порядок.
			// Когда добавляем новую запись в конец, таблица все еще считается отсортированной, поэтому нужно всегда вызвать метод,
			// а не проверять условия предыдущей сортировки.
			_currentNoteTable.Sort(_currentNoteTable.Columns[1], ListSortDirection.Ascending);

			Controls.Add(_currentNoteTable);
		}


		private void SetSexMenuVisibleProperty(string tableNameDB)
		{
			// There is additional menu for People - Sex.
			sexToolStripMenuItem.Visible = (tableNameDB == "People");
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


		private void SwitchToState()
		{
			switch (_settings.GetDefaultState(_currentNoteTable.TableNameDB))
			{
				case "Active":    activeToolStripMenuItem.PerformClick(); break;
				case "Deleted":   deletedToolStripMenuItem.PerformClick(); break;
				case "Finished":  finishedToolStripMenuItem.PerformClick(); break;
				case "Postponed": postponedToolStripMenuItem.PerformClick(); break;
				case "Waiting":   waitingToolStripMenuItem.PerformClick(); break;
				default:          allStatesToolStripMenuItem.PerformClick(); break;
			}
		}

		#endregion


		#region State menu

		private void StateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// При выборе элемента меню, должны отображаться все заметки с этим состоянием.
			// Если ранее был выполнен поиск, его результаты игнорируются. Выборка делается по всей таблице.
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			if (item != null && stateToolStripMenuItem.DropDownItems.Contains(item))
			{
				if (item.Text == "All")
					ShowAllNotes();
				else
					ShowNotesForClickedMenuItem(item);

				Text = string.Format("{0} - {1}", GetTableNameUI(_currentNoteTable.TableNameDB), item.Text);
			}
			searchTextBox.Text = "";
			lastClickedStateItem = item;
		}


		private void ShowAllNotes()
		{
			foreach (DataGridViewRow row in _currentNoteTable.Rows)
				row.Visible = true;
		}


		private void ShowNotesForClickedMenuItem(ToolStripMenuItem item)
		{
			if (_currentNoteTable.Columns.Contains("State"))
			{
				DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn)_currentNoteTable.Columns["State"];
				if (column == null)
					return;

				int stateIndex = stateToolStripMenuItem.DropDownItems.IndexOf(item);

				foreach (DataGridViewRow row in _currentNoteTable.Rows)
				{
					DataGridViewComboBoxCell stateCell = row.Cells[column.Index] as DataGridViewComboBoxCell;
					row.Visible = (stateCell != null && column.Items.IndexOf(stateCell.Value) == stateIndex);
				}
			}
		}

		#endregion


		#region Sex menu

		private void SexToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			const string sexColumnName = "Sex";
			if (!_currentNoteTable.Columns.Contains(sexColumnName))
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
				// Если нужно выбрать все, то ничего не делаем. Все уже выбрано нажатием на меню состояния.	
				if (item.Text == "All")
					return;
				
				DataGridViewColumn column = _currentNoteTable.Columns[sexColumnName];
				foreach (DataGridViewRow row in _currentNoteTable.Rows)
				{
					// Строки, скрытые по выборке состояния, не нужно изменять.
					if (row.Visible == false)
						continue;
						
					DataGridViewCell sexCell = row.Cells[column.Index];
					int sexIndex = sexToolStripMenuItem.DropDownItems.IndexOf(item);
					row.Visible = (sexCell != null && sexCell.Value.ToString() == PeopleTable.Sex[sexIndex]);
				}
			}
		}

		#endregion


		#region Buttons action

		private void addButton_Click(object sender, EventArgs e)
		{
			switch (_currentNoteTable.TableNameDB)
			{
				case "Affairs":       new AffairForm(       this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				case "AnimeFilms":    new DatedNoteForm(    this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				case "AnimeSerials":  new SerialForm(       this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				case "Bookmarks":     new BookmarkForm(     this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				case "Desires":       new DescribedNoteForm(this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				case "Films":         new DatedNoteForm(    this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				case "Games":         new GameForm(         this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				case "Literature":    new LiteratureForm(   this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				case "Meal":          new MealForm(         this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				case "Performances":  new DatedNoteForm(    this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;				
				case "People":        new PersonForm(       this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;				
				case "Programs":      new ProgramForm(      this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				case "RegularDoings": new DescribedNoteForm(this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;				
				case "Serials":       new SerialForm(       this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;				
				case "TVShows":       new SerialForm(       this, _currentNoteTable, NoteForm.Mode.Add).ShowDialog(); break;
				
				default: break;
			}
		}


		private void deleteButton_Click(object sender, EventArgs e)
		{
			_currentNoteTable.DeleteSelectedNotes();
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

			switch (_currentNoteTable.TableNameDB)
			{
				case "Affairs":       new AffairForm(       this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				case "AnimeFilms":    new DatedNoteForm(    this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				case "AnimeSerials":  new SerialForm(       this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				case "Bookmarks":     new BookmarkForm(     this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				case "Desires":       new DescribedNoteForm(this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				case "Films":         new DatedNoteForm(    this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				case "Games":         new GameForm(         this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				case "Literature":    new LiteratureForm(   this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				case "Meal":          new MealForm(         this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				case "Performances":  new DatedNoteForm(    this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;				
				case "People":        new PersonForm(       this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;				
				case "Programs":      new ProgramForm(      this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				case "RegularDoings": new DescribedNoteForm(this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;				
				case "Serials":       new SerialForm(       this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;				
				case "TVShows":       new SerialForm(       this, _currentNoteTable, NoteForm.Mode.Edit).ShowDialog(); break;
				
				default: break;
			}

			_currentNoteTable.CallCustomEvents = true;
		}


		private void searchButton_Click(object sender, EventArgs e)
		{
			search();
		}


		private void search()
		{
			// Можно сделать через запрос к базе. Но как по мне, лучше не перезаписывать данные в таблице новым запросом, а просто скрывать лишнее.

			// Убираю выделение строк, чтоб не оставались выделеными скрытые строки. Например, в случае удаления.
			_currentNoteTable.ClearSelection();

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

		#endregion


		#region Settings menu

		private void viewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ViewSettingsForm viewForm = new ViewSettingsForm(_settings);
			viewForm.ShowDialog();
		}


		private void securityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SecuritySettingsForm securityForm = new SecuritySettingsForm(_settings);
			securityForm.ShowDialog();
		}

		#endregion
	}
}
