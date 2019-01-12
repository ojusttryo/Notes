using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Notes.Notes;
using Notes.NoteTables;

namespace Notes.NoteForms
{
	class LiteratureForm : Form
	{
		private RichTextBox commentRichTextBox;
		private ComboBox stateComboBox;
		private TextBox yearTextBox;
		private TextBox nameTextBox;
		private Button submitButton;
		private Label commentLabel;
		private Label stateLabel;
		private Label yearLabel;
		private TextBox authorTextBox;
		private Label authorLabel;
		private TextBox genreTextBox;
		private Label genreLabel;
		private TextBox universeTextBox;
		private Label universeLabel;
		private TextBox seriesTextBox;
		private Label seriesLabel;
		private Label volumeLabel;
		private Label chapterLabel;
		private TextBox volumeTextBox;
		private TextBox chapterTextBox;
		private TextBox pagesTextBox;
		private TextBox pageTextBox;
		private Label pagesLabel;
		private Label pageLabel;
		private Label nameLabel;


		private MainForm _mainForm;
		private Literature _literature;

		public LiteratureForm(MainForm parent, string title, string buttonText, Note editedNote = null)
		{
			InitializeComponent();

			_mainForm = parent;
			_literature = editedNote as Literature;
			Text = title;
			submitButton.Text = buttonText;

			AutoCompleteStringCollection authors = new AutoCompleteStringCollection();
			authors.AddRange(Database.SelectUniqueValues("Literature", "Author").ToArray());
			authorTextBox.AutoCompleteCustomSource = authors;

			AutoCompleteStringCollection genres = new AutoCompleteStringCollection();
			genres.AddRange(Database.SelectUniqueValues("Literature", "Genre").ToArray());
			genreTextBox.AutoCompleteCustomSource = genres;

			AutoCompleteStringCollection universes = new AutoCompleteStringCollection();
			universes.AddRange(Database.SelectUniqueValues("Literature", "Universe").ToArray());
			universeTextBox.AutoCompleteCustomSource = universes;

			AutoCompleteStringCollection serieses = new AutoCompleteStringCollection();
			serieses.AddRange(Database.SelectUniqueValues("Literature", "Series").ToArray());
			seriesTextBox.AutoCompleteCustomSource = serieses;

			stateComboBox.Items.AddRange(NoteTable.States);
			stateComboBox.SelectedIndex = 0;

			if (_literature != null)
			{
				nameTextBox.Text     = _literature.Name;
				authorTextBox.Text   = _literature.Author;
				genreTextBox.Text    = _literature.Genre;
				universeTextBox.Text = _literature.Universe;
				seriesTextBox.Text   = _literature.Series;
				volumeTextBox.Text  = (_literature.Volume == 0) ?  "" : _literature.Volume.ToString();
				chapterTextBox.Text = (_literature.Chapter == 0) ? "" : _literature.Chapter.ToString();
				pageTextBox.Text    = (_literature.Page == 0) ?    "" : _literature.Page.ToString();
				pagesTextBox.Text   = (_literature.Pages == 0) ?   "" : _literature.Pages.ToString();
				yearTextBox.Text    = (_literature.Year == 0) ?    "" : _literature.Year.ToString();
				stateComboBox.SelectedIndex = (int)_literature.CurrentState;
				commentRichTextBox.Text = _literature.Comment;
			}

			// Делаю возможность при добавлении новых книг вводить диапазон значений для добавления нескольких томов за раз.
			if (_literature != null)
			{
				volumeTextBox.KeyPress += new KeyPressEventHandler(MainForm.CheckNumericInput);
			}
			else
			{
				volumeTextBox.KeyPress += delegate (object o, KeyPressEventArgs e)
				{
					if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
						e.Handled = true;
				};
			}
			chapterTextBox.KeyPress += new KeyPressEventHandler(MainForm.CheckNumericInput);
			pageTextBox.KeyPress    += new KeyPressEventHandler(MainForm.CheckNumericInput);
			pagesTextBox.KeyPress   += new KeyPressEventHandler(MainForm.CheckNumericInput);
			yearTextBox.KeyPress    += new KeyPressEventHandler(MainForm.CheckNumericInput);

			KeyDown += delegate (object o, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Control)
					submitButton.PerformClick();
			};
		}
		

		private void InitializeComponent()
		{
			this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
			this.stateComboBox = new System.Windows.Forms.ComboBox();
			this.yearTextBox = new System.Windows.Forms.TextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.submitButton = new System.Windows.Forms.Button();
			this.commentLabel = new System.Windows.Forms.Label();
			this.stateLabel = new System.Windows.Forms.Label();
			this.yearLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.authorTextBox = new System.Windows.Forms.TextBox();
			this.authorLabel = new System.Windows.Forms.Label();
			this.genreTextBox = new System.Windows.Forms.TextBox();
			this.genreLabel = new System.Windows.Forms.Label();
			this.universeTextBox = new System.Windows.Forms.TextBox();
			this.universeLabel = new System.Windows.Forms.Label();
			this.seriesTextBox = new System.Windows.Forms.TextBox();
			this.seriesLabel = new System.Windows.Forms.Label();
			this.volumeLabel = new System.Windows.Forms.Label();
			this.chapterLabel = new System.Windows.Forms.Label();
			this.volumeTextBox = new System.Windows.Forms.TextBox();
			this.chapterTextBox = new System.Windows.Forms.TextBox();
			this.pagesTextBox = new System.Windows.Forms.TextBox();
			this.pageTextBox = new System.Windows.Forms.TextBox();
			this.pagesLabel = new System.Windows.Forms.Label();
			this.pageLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// commentRichTextBox
			// 
			this.commentRichTextBox.Location = new System.Drawing.Point(66, 232);
			this.commentRichTextBox.Name = "commentRichTextBox";
			this.commentRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.commentRichTextBox.Size = new System.Drawing.Size(362, 111);
			this.commentRichTextBox.TabIndex = 11;
			this.commentRichTextBox.Text = "";
			// 
			// stateComboBox
			// 
			this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.stateComboBox.FormattingEnabled = true;
			this.stateComboBox.Location = new System.Drawing.Point(273, 205);
			this.stateComboBox.Name = "stateComboBox";
			this.stateComboBox.Size = new System.Drawing.Size(155, 21);
			this.stateComboBox.TabIndex = 10;
			// 
			// yearTextBox
			// 
			this.yearTextBox.Location = new System.Drawing.Point(66, 205);
			this.yearTextBox.MaxLength = 9;
			this.yearTextBox.Name = "yearTextBox";
			this.yearTextBox.Size = new System.Drawing.Size(155, 20);
			this.yearTextBox.TabIndex = 9;
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(66, 21);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(362, 20);
			this.nameTextBox.TabIndex = 0;
			// 
			// submitButton
			// 
			this.submitButton.Location = new System.Drawing.Point(190, 349);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 23);
			this.submitButton.TabIndex = 12;
			this.submitButton.Text = "Submit";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// commentLabel
			// 
			this.commentLabel.AutoSize = true;
			this.commentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.commentLabel.Location = new System.Drawing.Point(9, 235);
			this.commentLabel.Name = "commentLabel";
			this.commentLabel.Size = new System.Drawing.Size(51, 13);
			this.commentLabel.TabIndex = 12;
			this.commentLabel.Text = "Comment";
			// 
			// stateLabel
			// 
			this.stateLabel.AutoSize = true;
			this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stateLabel.Location = new System.Drawing.Point(237, 210);
			this.stateLabel.Name = "stateLabel";
			this.stateLabel.Size = new System.Drawing.Size(32, 13);
			this.stateLabel.TabIndex = 10;
			this.stateLabel.Text = "State";
			// 
			// yearLabel
			// 
			this.yearLabel.AutoSize = true;
			this.yearLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.yearLabel.Location = new System.Drawing.Point(31, 210);
			this.yearLabel.Name = "yearLabel";
			this.yearLabel.Size = new System.Drawing.Size(29, 13);
			this.yearLabel.TabIndex = 8;
			this.yearLabel.Text = "Year";
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.nameLabel.Location = new System.Drawing.Point(25, 24);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(35, 13);
			this.nameLabel.TabIndex = 6;
			this.nameLabel.Text = "Name";
			// 
			// authorTextBox
			// 
			this.authorTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.authorTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.authorTextBox.Location = new System.Drawing.Point(66, 47);
			this.authorTextBox.Name = "authorTextBox";
			this.authorTextBox.Size = new System.Drawing.Size(362, 20);
			this.authorTextBox.TabIndex = 1;
			// 
			// authorLabel
			// 
			this.authorLabel.AutoSize = true;
			this.authorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.authorLabel.Location = new System.Drawing.Point(22, 50);
			this.authorLabel.Name = "authorLabel";
			this.authorLabel.Size = new System.Drawing.Size(38, 13);
			this.authorLabel.TabIndex = 15;
			this.authorLabel.Text = "Author";
			// 
			// genreTextBox
			// 
			this.genreTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.genreTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.genreTextBox.Location = new System.Drawing.Point(66, 73);
			this.genreTextBox.Name = "genreTextBox";
			this.genreTextBox.Size = new System.Drawing.Size(362, 20);
			this.genreTextBox.TabIndex = 2;
			// 
			// genreLabel
			// 
			this.genreLabel.AutoSize = true;
			this.genreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.genreLabel.Location = new System.Drawing.Point(24, 76);
			this.genreLabel.Name = "genreLabel";
			this.genreLabel.Size = new System.Drawing.Size(36, 13);
			this.genreLabel.TabIndex = 17;
			this.genreLabel.Text = "Genre";
			// 
			// universeTextBox
			// 
			this.universeTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.universeTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.universeTextBox.Location = new System.Drawing.Point(66, 99);
			this.universeTextBox.Name = "universeTextBox";
			this.universeTextBox.Size = new System.Drawing.Size(362, 20);
			this.universeTextBox.TabIndex = 3;
			// 
			// universeLabel
			// 
			this.universeLabel.AutoSize = true;
			this.universeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.universeLabel.Location = new System.Drawing.Point(11, 102);
			this.universeLabel.Name = "universeLabel";
			this.universeLabel.Size = new System.Drawing.Size(49, 13);
			this.universeLabel.TabIndex = 19;
			this.universeLabel.Text = "Universe";
			// 
			// seriesTextBox
			// 
			this.seriesTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.seriesTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.seriesTextBox.Location = new System.Drawing.Point(66, 125);
			this.seriesTextBox.Name = "seriesTextBox";
			this.seriesTextBox.Size = new System.Drawing.Size(362, 20);
			this.seriesTextBox.TabIndex = 4;
			// 
			// seriesLabel
			// 
			this.seriesLabel.AutoSize = true;
			this.seriesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.seriesLabel.Location = new System.Drawing.Point(24, 128);
			this.seriesLabel.Name = "seriesLabel";
			this.seriesLabel.Size = new System.Drawing.Size(36, 13);
			this.seriesLabel.TabIndex = 21;
			this.seriesLabel.Text = "Series";
			// 
			// volumeLabel
			// 
			this.volumeLabel.AutoSize = true;
			this.volumeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.volumeLabel.Location = new System.Drawing.Point(18, 156);
			this.volumeLabel.Name = "volumeLabel";
			this.volumeLabel.Size = new System.Drawing.Size(42, 13);
			this.volumeLabel.TabIndex = 23;
			this.volumeLabel.Text = "Volume";
			// 
			// chapterLabel
			// 
			this.chapterLabel.AutoSize = true;
			this.chapterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.chapterLabel.Location = new System.Drawing.Point(225, 156);
			this.chapterLabel.Name = "chapterLabel";
			this.chapterLabel.Size = new System.Drawing.Size(44, 13);
			this.chapterLabel.TabIndex = 25;
			this.chapterLabel.Text = "Chapter";
			// 
			// volumeTextBox
			// 
			this.volumeTextBox.Location = new System.Drawing.Point(66, 153);
			this.volumeTextBox.MaxLength = 9;
			this.volumeTextBox.Name = "volumeTextBox";
			this.volumeTextBox.Size = new System.Drawing.Size(155, 20);
			this.volumeTextBox.TabIndex = 5;
			// 
			// chapterTextBox
			// 
			this.chapterTextBox.Location = new System.Drawing.Point(273, 153);
			this.chapterTextBox.MaxLength = 9;
			this.chapterTextBox.Name = "chapterTextBox";
			this.chapterTextBox.Size = new System.Drawing.Size(155, 20);
			this.chapterTextBox.TabIndex = 6;
			// 
			// pagesTextBox
			// 
			this.pagesTextBox.Location = new System.Drawing.Point(273, 179);
			this.pagesTextBox.MaxLength = 9;
			this.pagesTextBox.Name = "pagesTextBox";
			this.pagesTextBox.Size = new System.Drawing.Size(155, 20);
			this.pagesTextBox.TabIndex = 8;
			// 
			// pageTextBox
			// 
			this.pageTextBox.Location = new System.Drawing.Point(66, 179);
			this.pageTextBox.MaxLength = 9;
			this.pageTextBox.Name = "pageTextBox";
			this.pageTextBox.Size = new System.Drawing.Size(155, 20);
			this.pageTextBox.TabIndex = 7;
			// 
			// pagesLabel
			// 
			this.pagesLabel.AutoSize = true;
			this.pagesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pagesLabel.Location = new System.Drawing.Point(232, 182);
			this.pagesLabel.Name = "pagesLabel";
			this.pagesLabel.Size = new System.Drawing.Size(37, 13);
			this.pagesLabel.TabIndex = 29;
			this.pagesLabel.Text = "Pages";
			// 
			// pageLabel
			// 
			this.pageLabel.AutoSize = true;
			this.pageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pageLabel.Location = new System.Drawing.Point(28, 182);
			this.pageLabel.Name = "pageLabel";
			this.pageLabel.Size = new System.Drawing.Size(32, 13);
			this.pageLabel.TabIndex = 28;
			this.pageLabel.Text = "Page";
			// 
			// LiteratureForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 382);
			this.Controls.Add(this.pagesTextBox);
			this.Controls.Add(this.pageTextBox);
			this.Controls.Add(this.pagesLabel);
			this.Controls.Add(this.pageLabel);
			this.Controls.Add(this.chapterTextBox);
			this.Controls.Add(this.volumeTextBox);
			this.Controls.Add(this.chapterLabel);
			this.Controls.Add(this.volumeLabel);
			this.Controls.Add(this.seriesTextBox);
			this.Controls.Add(this.seriesLabel);
			this.Controls.Add(this.universeTextBox);
			this.Controls.Add(this.universeLabel);
			this.Controls.Add(this.genreTextBox);
			this.Controls.Add(this.genreLabel);
			this.Controls.Add(this.authorTextBox);
			this.Controls.Add(this.authorLabel);
			this.Controls.Add(this.commentRichTextBox);
			this.Controls.Add(this.stateComboBox);
			this.Controls.Add(this.yearTextBox);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.submitButton);
			this.Controls.Add(this.commentLabel);
			this.Controls.Add(this.stateLabel);
			this.Controls.Add(this.yearLabel);
			this.Controls.Add(this.nameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "LiteratureForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Note";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			// Add or update note
			bool isUpdating = (_literature != null);

			Literature literature = isUpdating ? _literature : new Literature();

			literature.Name     = nameTextBox.Text;
			literature.Author   = authorTextBox.Text;
			literature.Genre    = genreTextBox.Text;
			literature.Universe = universeTextBox.Text;
			literature.Series   = seriesTextBox.Text;
			// Volume below
			literature.Chapter = (chapterTextBox.Text.Trim().Length == 0) ? 0 : Int32.Parse(chapterTextBox.Text.Trim());
			literature.Page    = (pageTextBox.Text.Trim().Length == 0) ?    0 : Int32.Parse(pageTextBox.Text.Trim());
			literature.Pages   = (pagesTextBox.Text.Trim().Length == 0) ?   0 : Int32.Parse(pagesTextBox.Text.Trim());
			literature.Year    = (yearTextBox.Text.Trim().Length == 0) ?    0 : Int32.Parse(yearTextBox.Text.Trim());
			literature.CurrentState = (Note.State)stateComboBox.SelectedIndex;
			literature.Comment = commentRichTextBox.Text;

			// Set volume and submit literature
			string volume = volumeTextBox.Text.Trim();
			if (volume.Length == 0)
			{
				literature.Volume = 0;
				_mainForm.AddOrUpdateNote(literature);
			}
			else if (Regex.IsMatch(volume, @"\A\d+\Z", RegexOptions.CultureInvariant))
			{
				literature.Volume = Int32.Parse(volume);
				_mainForm.AddOrUpdateNote(literature);
			}
			else if (!isUpdating && Regex.IsMatch(volume, @"\A\d+-\d+\Z", RegexOptions.CultureInvariant))
			{
				MatchCollection matches = Regex.Matches(volume, @"\d+", RegexOptions.CultureInvariant);
				int firstVolume = Int32.Parse(matches[0].Value);
				int lastVolume = Int32.Parse(matches[1].Value);
				if (firstVolume > lastVolume)
				{
					literature.Volume = firstVolume;
					_mainForm.AddOrUpdateNote(literature);
				}
				else
				{
					// Добавляем несколько книг с томами firstVolume-lastVolume.
					for (int i = firstVolume; i <= lastVolume; i++)
					{
						Literature literatureCopy = (Literature)literature.Clone();
						literatureCopy.Volume = i;
						_mainForm.AddOrUpdateNote(literatureCopy);
					}
				}
			}

			Close();
		}
	}
}
