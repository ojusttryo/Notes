using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;

using Notes.Notes;
using Notes.NoteTables;
using Notes.CommonUIElements;
using Notes.DB;

namespace Notes.NoteForms
{
	[ToolboxItem(true)]
	[DesignTimeVisible(true)]
	class LiteratureForm : NoteForm
	{
		private RichTextBox commentRichTextBox;
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
		private StateComboBox stateComboBox;
		private Label nameLabel;

		public LiteratureForm(MainForm mainForm, NoteTable editedTable, Mode mode):
			base (mainForm, editedTable, mode)
		{
			InitializeComponent();

			Text = GetFormText();
			submitButton.Text = GetSubmitButtonText();

			AutoCompleteStringCollection authors = new AutoCompleteStringCollection();
			authors.AddRange(Database.SelectUniqueValues("Literature", "Author").Where(x => x != "").ToArray());
			authorTextBox.AutoCompleteCustomSource = authors;

			AutoCompleteStringCollection genres = new AutoCompleteStringCollection();
			genres.AddRange(Database.SelectUniqueValues("Literature", "Genre").Where(x => x != "").ToArray());
			genreTextBox.AutoCompleteCustomSource = genres;

			AutoCompleteStringCollection universes = new AutoCompleteStringCollection();
			universes.AddRange(Database.SelectUniqueValues("Literature", "Universe").Where(x => x != "").ToArray());
			universeTextBox.AutoCompleteCustomSource = universes;

			AutoCompleteStringCollection serieses = new AutoCompleteStringCollection();
			serieses.AddRange(Database.SelectUniqueValues("Literature", "Series").Where(x => x != "").ToArray());
			seriesTextBox.AutoCompleteCustomSource = serieses;

			Literature lit = _editedNote as Literature;
			if (lit != null)
			{
				nameTextBox.Text     = lit.Name;
				authorTextBox.Text   = lit.Author;
				genreTextBox.Text    = lit.Genre;
				universeTextBox.Text = lit.Universe;
				seriesTextBox.Text   = lit.Series;
				volumeTextBox.Text  = (lit.Volume == 0) ?  "" : lit.Volume.ToString();
				chapterTextBox.Text = (lit.Chapter == 0) ? "" : lit.Chapter.ToString();
				pageTextBox.Text    = (lit.Page == 0) ?    "" : lit.Page.ToString();
				pagesTextBox.Text   = (lit.Pages == 0) ?   "" : lit.Pages.ToString();
				yearTextBox.Text    = (lit.Year == 0) ?    "" : lit.Year.ToString();
				stateComboBox.SelectedIndex = (int)lit.CurrentState;
				commentRichTextBox.Text = lit.Comment;
			}

			// При редактировании вносится только 1 том, а при добавлении можно указать сразу диапазон. 
			if (lit != null)
			{
				volumeTextBox.KeyPress += new KeyPressEventHandler(InputEventHandler.CheckNumeric);
			}
			else
			{
				volumeTextBox.KeyPress += delegate (object o, KeyPressEventArgs e)
				{
					if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
						e.Handled = true;
				};
			}
			chapterTextBox.KeyPress += new KeyPressEventHandler(InputEventHandler.CheckNumeric);
			pageTextBox.KeyPress    += new KeyPressEventHandler(InputEventHandler.CheckNumeric);
			pagesTextBox.KeyPress   += new KeyPressEventHandler(InputEventHandler.CheckNumeric);
			yearTextBox.KeyPress    += new KeyPressEventHandler(InputEventHandler.CheckNumeric);

			KeyDown += delegate (object o, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Control)
					submitButton.PerformClick();
			};
		}
		

		private void InitializeComponent()
		{
			this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
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
			this.stateComboBox = new StateComboBox();
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
			// stateComboBox
			// 
			this.stateComboBox.BackColor = System.Drawing.Color.White;
			this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.stateComboBox.FormattingEnabled = true;
			this.stateComboBox.Location = new System.Drawing.Point(273, 207);
			this.stateComboBox.Name = "stateComboBox";
			this.stateComboBox.Size = new System.Drawing.Size(155, 21);
			this.stateComboBox.TabIndex = 10;
			// 
			// LiteratureForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 382);
			this.Controls.Add(this.stateComboBox);
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
			Literature literature = (_editedNote != null && _editedNote is Literature) ? _editedNote as Literature : new Literature();

			literature.Name     = nameTextBox.Text;
			literature.Author   = authorTextBox.Text;
			literature.Genre    = genreTextBox.Text;
			literature.Universe = universeTextBox.Text;
			literature.Series   = seriesTextBox.Text;
			// Volume skipped. It's below.
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
				SubmitNote(literature);
			}
			else if (Regex.IsMatch(volume, @"\A\d+\Z", RegexOptions.CultureInvariant))
			{
				literature.Volume = Int32.Parse(volume);
				SubmitNote(literature);
			}
			else if (OpenMode == Mode.Add && Regex.IsMatch(volume, @"\A\d+-\d+\Z", RegexOptions.CultureInvariant))
			{
				MatchCollection matches = Regex.Matches(volume, @"\d+", RegexOptions.CultureInvariant);
				int firstVolume = Int32.Parse(matches[0].Value);
				int lastVolume = Int32.Parse(matches[1].Value);
				
				if (firstVolume > lastVolume)
				{
					literature.Volume = firstVolume;
					SubmitNote(literature);
				}
				else
				{
					List<Note> notes = new List<Note>();

					// Добавляем несколько книг с томами firstVolume-lastVolume.
					for (int i = firstVolume; i <= lastVolume; i++)
					{
						Literature literatureCopy = (Literature)literature.Clone();
						literatureCopy.Volume = i;
						notes.Add(literatureCopy);
					}

					_mainForm.AddNotes(notes);
				}
			}

			Close();
		}
	}
}
