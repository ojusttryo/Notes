using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

using Notes.CommonUIElements;
using static Notes.Info;

namespace Notes.ProgramSettings
{
	public class ViewSettingsForm : Form
	{
		private Button saveButton;
		private Panel menuPanel;
		private Label viewLabel;
		private Label label1;
		private Panel panel1;
		private Label animeSerialsStateLabel;
		private Label animeFilmsStateLabel;
		private Label gamesStateLabel;
		private Label filmsStateLabel;
		private Label desiresStateLabel;
		private Label bookmarksStateLabel;
		private Label peopleStateLabel;
		private Label performancesStateLabel;
		private Label mealStateLabel;
		private Label literatureStateLabel;
		private Label TVShowsStateLabel;
		private Label serialsStateLabel;
		private Label programsStateLabel;
		private Label regularDoingsStateLabel;
		private Label affairsStateLabel;
		private StateComboBox TVShowsComboBox;
		private StateComboBox serialsComboBox;
		private StateComboBox regularDoingsComboBox;
		private StateComboBox programsComboBox;
		private StateComboBox peopleComboBox;
		private StateComboBox performancesComboBox;
		private StateComboBox mealComboBox;
		private StateComboBox literatureComboBox;
		private StateComboBox gamesComboBox;
		private StateComboBox filmsComboBox;
		private StateComboBox desiresComboBox;
		private StateComboBox bookmarksComboBox;
		private StateComboBox animeSerialsComboBox;
		private StateComboBox animeFilmsComboBox;
		private StateComboBox affairsComboBox;
		private NotesComboBox defaultNotesComboBox;
		private Label defaultNotesLabel;

		Settings _settings;


		public ViewSettingsForm(Settings settings)
		{
			InitializeComponent();
			_settings = settings;

			if (_settings.DefaultNotesTable.Length > 0)
			{
				string tableNameUI = GetTableNameUI(_settings.DefaultNotesTable);
				defaultNotesComboBox.SelectedIndex = defaultNotesComboBox.FindString(tableNameUI);
			}

			SetDefaultState(affairsComboBox,       "Affairs");
			SetDefaultState(animeFilmsComboBox,    "AnimeFilms");
			SetDefaultState(animeSerialsComboBox,  "AnimeSerials");
			SetDefaultState(bookmarksComboBox,     "Bookmarks");
			SetDefaultState(desiresComboBox,       "Desires");
			SetDefaultState(filmsComboBox,         "Films");
			SetDefaultState(gamesComboBox,         "Games");
			SetDefaultState(literatureComboBox,    "Literature");
			SetDefaultState(mealComboBox,          "Meal");
			SetDefaultState(performancesComboBox,  "Performances");
			SetDefaultState(peopleComboBox,        "People");
			SetDefaultState(programsComboBox,      "Programs");
			SetDefaultState(regularDoingsComboBox, "RegularDoings");
			SetDefaultState(serialsComboBox,       "Serials");
			SetDefaultState(TVShowsComboBox,       "TVShows");

			KeyDown += delegate (object o, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Enter)
					saveButton.PerformClick();
				else if (e.KeyCode == Keys.Escape)
					Close();
			};
		}


		private void InitializeComponent()
		{
			this.saveButton = new System.Windows.Forms.Button();
			this.defaultNotesLabel = new System.Windows.Forms.Label();
			this.menuPanel = new System.Windows.Forms.Panel();
			this.defaultNotesComboBox = new CommonUIElements.NotesComboBox();
			this.viewLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.TVShowsComboBox = new CommonUIElements.StateComboBox();
			this.serialsComboBox = new CommonUIElements.StateComboBox();
			this.regularDoingsComboBox = new CommonUIElements.StateComboBox();
			this.programsComboBox = new CommonUIElements.StateComboBox();
			this.peopleComboBox = new CommonUIElements.StateComboBox();
			this.performancesComboBox = new CommonUIElements.StateComboBox();
			this.mealComboBox = new CommonUIElements.StateComboBox();
			this.literatureComboBox = new CommonUIElements.StateComboBox();
			this.gamesComboBox = new CommonUIElements.StateComboBox();
			this.filmsComboBox = new CommonUIElements.StateComboBox();
			this.desiresComboBox = new CommonUIElements.StateComboBox();
			this.bookmarksComboBox = new CommonUIElements.StateComboBox();
			this.animeSerialsComboBox = new CommonUIElements.StateComboBox();
			this.animeFilmsComboBox = new CommonUIElements.StateComboBox();
			this.affairsComboBox = new CommonUIElements.StateComboBox();
			this.affairsStateLabel = new System.Windows.Forms.Label();
			this.regularDoingsStateLabel = new System.Windows.Forms.Label();
			this.animeFilmsStateLabel = new System.Windows.Forms.Label();
			this.literatureStateLabel = new System.Windows.Forms.Label();
			this.animeSerialsStateLabel = new System.Windows.Forms.Label();
			this.TVShowsStateLabel = new System.Windows.Forms.Label();
			this.mealStateLabel = new System.Windows.Forms.Label();
			this.bookmarksStateLabel = new System.Windows.Forms.Label();
			this.gamesStateLabel = new System.Windows.Forms.Label();
			this.serialsStateLabel = new System.Windows.Forms.Label();
			this.desiresStateLabel = new System.Windows.Forms.Label();
			this.performancesStateLabel = new System.Windows.Forms.Label();
			this.programsStateLabel = new System.Windows.Forms.Label();
			this.filmsStateLabel = new System.Windows.Forms.Label();
			this.peopleStateLabel = new System.Windows.Forms.Label();
			this.menuPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// saveButton
			// 
			this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.saveButton.Location = new System.Drawing.Point(188, 324);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 12;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// defaultNotesLabel
			// 
			this.defaultNotesLabel.AutoSize = true;
			this.defaultNotesLabel.Location = new System.Drawing.Point(6, 17);
			this.defaultNotesLabel.Name = "defaultNotesLabel";
			this.defaultNotesLabel.Size = new System.Drawing.Size(35, 13);
			this.defaultNotesLabel.TabIndex = 9;
			this.defaultNotesLabel.Text = "Notes";
			// 
			// menuPanel
			// 
			this.menuPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.menuPanel.Controls.Add(this.defaultNotesComboBox);
			this.menuPanel.Controls.Add(this.defaultNotesLabel);
			this.menuPanel.Location = new System.Drawing.Point(9, 32);
			this.menuPanel.Name = "menuPanel";
			this.menuPanel.Size = new System.Drawing.Size(422, 46);
			this.menuPanel.TabIndex = 7;
			// 
			// defaultNotesComboBox
			// 
			this.defaultNotesComboBox.BackColor = System.Drawing.Color.White;
			this.defaultNotesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.defaultNotesComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.defaultNotesComboBox.FormattingEnabled = true;
			this.defaultNotesComboBox.Location = new System.Drawing.Point(93, 14);
			this.defaultNotesComboBox.Name = "defaultNotesComboBox";
			this.defaultNotesComboBox.Size = new System.Drawing.Size(103, 21);
			this.defaultNotesComboBox.TabIndex = 10;
			// 
			// viewLabel
			// 
			this.viewLabel.AutoSize = true;
			this.viewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.viewLabel.Location = new System.Drawing.Point(12, 9);
			this.viewLabel.Name = "viewLabel";
			this.viewLabel.Size = new System.Drawing.Size(86, 16);
			this.viewLabel.TabIndex = 6;
			this.viewLabel.Text = "Default notes";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(12, 87);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Default state";
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.TVShowsComboBox);
			this.panel1.Controls.Add(this.serialsComboBox);
			this.panel1.Controls.Add(this.regularDoingsComboBox);
			this.panel1.Controls.Add(this.programsComboBox);
			this.panel1.Controls.Add(this.peopleComboBox);
			this.panel1.Controls.Add(this.performancesComboBox);
			this.panel1.Controls.Add(this.mealComboBox);
			this.panel1.Controls.Add(this.literatureComboBox);
			this.panel1.Controls.Add(this.gamesComboBox);
			this.panel1.Controls.Add(this.filmsComboBox);
			this.panel1.Controls.Add(this.desiresComboBox);
			this.panel1.Controls.Add(this.bookmarksComboBox);
			this.panel1.Controls.Add(this.animeSerialsComboBox);
			this.panel1.Controls.Add(this.animeFilmsComboBox);
			this.panel1.Controls.Add(this.affairsComboBox);
			this.panel1.Controls.Add(this.affairsStateLabel);
			this.panel1.Controls.Add(this.regularDoingsStateLabel);
			this.panel1.Controls.Add(this.animeFilmsStateLabel);
			this.panel1.Controls.Add(this.literatureStateLabel);
			this.panel1.Controls.Add(this.animeSerialsStateLabel);
			this.panel1.Controls.Add(this.TVShowsStateLabel);
			this.panel1.Controls.Add(this.mealStateLabel);
			this.panel1.Controls.Add(this.bookmarksStateLabel);
			this.panel1.Controls.Add(this.gamesStateLabel);
			this.panel1.Controls.Add(this.serialsStateLabel);
			this.panel1.Controls.Add(this.desiresStateLabel);
			this.panel1.Controls.Add(this.performancesStateLabel);
			this.panel1.Controls.Add(this.programsStateLabel);
			this.panel1.Controls.Add(this.filmsStateLabel);
			this.panel1.Controls.Add(this.peopleStateLabel);
			this.panel1.Location = new System.Drawing.Point(9, 110);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(423, 201);
			this.panel1.TabIndex = 14;
			// 
			// TVShowsComboBox
			// 
			this.TVShowsComboBox.BackColor = System.Drawing.Color.White;
			this.TVShowsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TVShowsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.TVShowsComboBox.FormattingEnabled = true;
			this.TVShowsComboBox.Location = new System.Drawing.Point(303, 149);
			this.TVShowsComboBox.Name = "TVShowsComboBox";
			this.TVShowsComboBox.Size = new System.Drawing.Size(103, 21);
			this.TVShowsComboBox.TabIndex = 52;
			// 
			// serialsComboBox
			// 
			this.serialsComboBox.BackColor = System.Drawing.Color.White;
			this.serialsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.serialsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.serialsComboBox.FormattingEnabled = true;
			this.serialsComboBox.Location = new System.Drawing.Point(303, 126);
			this.serialsComboBox.Name = "serialsComboBox";
			this.serialsComboBox.Size = new System.Drawing.Size(103, 21);
			this.serialsComboBox.TabIndex = 51;
			// 
			// regularDoingsComboBox
			// 
			this.regularDoingsComboBox.BackColor = System.Drawing.Color.White;
			this.regularDoingsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.regularDoingsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.regularDoingsComboBox.FormattingEnabled = true;
			this.regularDoingsComboBox.Location = new System.Drawing.Point(303, 103);
			this.regularDoingsComboBox.Name = "regularDoingsComboBox";
			this.regularDoingsComboBox.Size = new System.Drawing.Size(103, 21);
			this.regularDoingsComboBox.TabIndex = 50;
			// 
			// programsComboBox
			// 
			this.programsComboBox.BackColor = System.Drawing.Color.White;
			this.programsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.programsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.programsComboBox.FormattingEnabled = true;
			this.programsComboBox.Location = new System.Drawing.Point(303, 80);
			this.programsComboBox.Name = "programsComboBox";
			this.programsComboBox.Size = new System.Drawing.Size(103, 21);
			this.programsComboBox.TabIndex = 49;
			// 
			// peopleComboBox
			// 
			this.peopleComboBox.BackColor = System.Drawing.Color.White;
			this.peopleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.peopleComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.peopleComboBox.FormattingEnabled = true;
			this.peopleComboBox.Location = new System.Drawing.Point(303, 57);
			this.peopleComboBox.Name = "peopleComboBox";
			this.peopleComboBox.Size = new System.Drawing.Size(103, 21);
			this.peopleComboBox.TabIndex = 48;
			// 
			// performancesComboBox
			// 
			this.performancesComboBox.BackColor = System.Drawing.Color.White;
			this.performancesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.performancesComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.performancesComboBox.FormattingEnabled = true;
			this.performancesComboBox.Location = new System.Drawing.Point(303, 34);
			this.performancesComboBox.Name = "performancesComboBox";
			this.performancesComboBox.Size = new System.Drawing.Size(103, 21);
			this.performancesComboBox.TabIndex = 47;
			// 
			// mealComboBox
			// 
			this.mealComboBox.BackColor = System.Drawing.Color.White;
			this.mealComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mealComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.mealComboBox.FormattingEnabled = true;
			this.mealComboBox.Location = new System.Drawing.Point(303, 11);
			this.mealComboBox.Name = "mealComboBox";
			this.mealComboBox.Size = new System.Drawing.Size(103, 21);
			this.mealComboBox.TabIndex = 46;
			// 
			// literatureComboBox
			// 
			this.literatureComboBox.BackColor = System.Drawing.Color.White;
			this.literatureComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.literatureComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.literatureComboBox.FormattingEnabled = true;
			this.literatureComboBox.Location = new System.Drawing.Point(93, 172);
			this.literatureComboBox.Name = "literatureComboBox";
			this.literatureComboBox.Size = new System.Drawing.Size(103, 21);
			this.literatureComboBox.TabIndex = 45;
			// 
			// gamesComboBox
			// 
			this.gamesComboBox.BackColor = System.Drawing.Color.White;
			this.gamesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.gamesComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.gamesComboBox.FormattingEnabled = true;
			this.gamesComboBox.Location = new System.Drawing.Point(93, 149);
			this.gamesComboBox.Name = "gamesComboBox";
			this.gamesComboBox.Size = new System.Drawing.Size(103, 21);
			this.gamesComboBox.TabIndex = 44;
			// 
			// filmsComboBox
			// 
			this.filmsComboBox.BackColor = System.Drawing.Color.White;
			this.filmsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.filmsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.filmsComboBox.FormattingEnabled = true;
			this.filmsComboBox.Location = new System.Drawing.Point(93, 126);
			this.filmsComboBox.Name = "filmsComboBox";
			this.filmsComboBox.Size = new System.Drawing.Size(103, 21);
			this.filmsComboBox.TabIndex = 43;
			// 
			// desiresComboBox
			// 
			this.desiresComboBox.BackColor = System.Drawing.Color.White;
			this.desiresComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.desiresComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.desiresComboBox.FormattingEnabled = true;
			this.desiresComboBox.Location = new System.Drawing.Point(93, 103);
			this.desiresComboBox.Name = "desiresComboBox";
			this.desiresComboBox.Size = new System.Drawing.Size(103, 21);
			this.desiresComboBox.TabIndex = 42;
			// 
			// bookmarksComboBox
			// 
			this.bookmarksComboBox.BackColor = System.Drawing.Color.White;
			this.bookmarksComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.bookmarksComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.bookmarksComboBox.FormattingEnabled = true;
			this.bookmarksComboBox.Location = new System.Drawing.Point(93, 80);
			this.bookmarksComboBox.Name = "bookmarksComboBox";
			this.bookmarksComboBox.Size = new System.Drawing.Size(103, 21);
			this.bookmarksComboBox.TabIndex = 41;
			// 
			// animeSerialsComboBox
			// 
			this.animeSerialsComboBox.BackColor = System.Drawing.Color.White;
			this.animeSerialsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.animeSerialsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.animeSerialsComboBox.FormattingEnabled = true;
			this.animeSerialsComboBox.Location = new System.Drawing.Point(93, 57);
			this.animeSerialsComboBox.Name = "animeSerialsComboBox";
			this.animeSerialsComboBox.Size = new System.Drawing.Size(103, 21);
			this.animeSerialsComboBox.TabIndex = 40;
			// 
			// animeFilmsComboBox
			// 
			this.animeFilmsComboBox.BackColor = System.Drawing.Color.White;
			this.animeFilmsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.animeFilmsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.animeFilmsComboBox.FormattingEnabled = true;
			this.animeFilmsComboBox.Location = new System.Drawing.Point(93, 34);
			this.animeFilmsComboBox.Name = "animeFilmsComboBox";
			this.animeFilmsComboBox.Size = new System.Drawing.Size(103, 21);
			this.animeFilmsComboBox.TabIndex = 39;
			// 
			// affairsComboBox
			// 
			this.affairsComboBox.BackColor = System.Drawing.Color.White;
			this.affairsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.affairsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.affairsComboBox.FormattingEnabled = true;
			this.affairsComboBox.Location = new System.Drawing.Point(93, 11);
			this.affairsComboBox.Name = "affairsComboBox";
			this.affairsComboBox.Size = new System.Drawing.Size(103, 21);
			this.affairsComboBox.TabIndex = 38;
			// 
			// affairsStateLabel
			// 
			this.affairsStateLabel.AutoSize = true;
			this.affairsStateLabel.Location = new System.Drawing.Point(6, 14);
			this.affairsStateLabel.Name = "affairsStateLabel";
			this.affairsStateLabel.Size = new System.Drawing.Size(36, 13);
			this.affairsStateLabel.TabIndex = 35;
			this.affairsStateLabel.Text = "Affairs";
			// 
			// regularDoingsStateLabel
			// 
			this.regularDoingsStateLabel.AutoSize = true;
			this.regularDoingsStateLabel.Location = new System.Drawing.Point(216, 106);
			this.regularDoingsStateLabel.Name = "regularDoingsStateLabel";
			this.regularDoingsStateLabel.Size = new System.Drawing.Size(78, 13);
			this.regularDoingsStateLabel.TabIndex = 37;
			this.regularDoingsStateLabel.Text = "Regular doings";
			// 
			// animeFilmsStateLabel
			// 
			this.animeFilmsStateLabel.AutoSize = true;
			this.animeFilmsStateLabel.Location = new System.Drawing.Point(6, 37);
			this.animeFilmsStateLabel.Name = "animeFilmsStateLabel";
			this.animeFilmsStateLabel.Size = new System.Drawing.Size(59, 13);
			this.animeFilmsStateLabel.TabIndex = 9;
			this.animeFilmsStateLabel.Text = "Anime films";
			// 
			// literatureStateLabel
			// 
			this.literatureStateLabel.AutoSize = true;
			this.literatureStateLabel.Location = new System.Drawing.Point(6, 175);
			this.literatureStateLabel.Name = "literatureStateLabel";
			this.literatureStateLabel.Size = new System.Drawing.Size(51, 13);
			this.literatureStateLabel.TabIndex = 22;
			this.literatureStateLabel.Text = "Literature";
			// 
			// animeSerialsStateLabel
			// 
			this.animeSerialsStateLabel.AutoSize = true;
			this.animeSerialsStateLabel.Location = new System.Drawing.Point(6, 60);
			this.animeSerialsStateLabel.Name = "animeSerialsStateLabel";
			this.animeSerialsStateLabel.Size = new System.Drawing.Size(68, 13);
			this.animeSerialsStateLabel.TabIndex = 11;
			this.animeSerialsStateLabel.Text = "Anime serials";
			// 
			// TVShowsStateLabel
			// 
			this.TVShowsStateLabel.AutoSize = true;
			this.TVShowsStateLabel.Location = new System.Drawing.Point(216, 152);
			this.TVShowsStateLabel.Name = "TVShowsStateLabel";
			this.TVShowsStateLabel.Size = new System.Drawing.Size(54, 13);
			this.TVShowsStateLabel.TabIndex = 33;
			this.TVShowsStateLabel.Text = "TV shows";
			// 
			// mealStateLabel
			// 
			this.mealStateLabel.AutoSize = true;
			this.mealStateLabel.Location = new System.Drawing.Point(216, 14);
			this.mealStateLabel.Name = "mealStateLabel";
			this.mealStateLabel.Size = new System.Drawing.Size(30, 13);
			this.mealStateLabel.TabIndex = 23;
			this.mealStateLabel.Text = "Meal";
			// 
			// bookmarksStateLabel
			// 
			this.bookmarksStateLabel.AutoSize = true;
			this.bookmarksStateLabel.Location = new System.Drawing.Point(6, 83);
			this.bookmarksStateLabel.Name = "bookmarksStateLabel";
			this.bookmarksStateLabel.Size = new System.Drawing.Size(60, 13);
			this.bookmarksStateLabel.TabIndex = 14;
			this.bookmarksStateLabel.Text = "Bookmarks";
			// 
			// gamesStateLabel
			// 
			this.gamesStateLabel.AutoSize = true;
			this.gamesStateLabel.Location = new System.Drawing.Point(6, 152);
			this.gamesStateLabel.Name = "gamesStateLabel";
			this.gamesStateLabel.Size = new System.Drawing.Size(40, 13);
			this.gamesStateLabel.TabIndex = 19;
			this.gamesStateLabel.Text = "Games";
			// 
			// serialsStateLabel
			// 
			this.serialsStateLabel.AutoSize = true;
			this.serialsStateLabel.Location = new System.Drawing.Point(216, 129);
			this.serialsStateLabel.Name = "serialsStateLabel";
			this.serialsStateLabel.Size = new System.Drawing.Size(38, 13);
			this.serialsStateLabel.TabIndex = 32;
			this.serialsStateLabel.Text = "Serials";
			// 
			// desiresStateLabel
			// 
			this.desiresStateLabel.AutoSize = true;
			this.desiresStateLabel.Location = new System.Drawing.Point(6, 106);
			this.desiresStateLabel.Name = "desiresStateLabel";
			this.desiresStateLabel.Size = new System.Drawing.Size(42, 13);
			this.desiresStateLabel.TabIndex = 15;
			this.desiresStateLabel.Text = "Desires";
			// 
			// performancesStateLabel
			// 
			this.performancesStateLabel.AutoSize = true;
			this.performancesStateLabel.Location = new System.Drawing.Point(216, 37);
			this.performancesStateLabel.Name = "performancesStateLabel";
			this.performancesStateLabel.Size = new System.Drawing.Size(72, 13);
			this.performancesStateLabel.TabIndex = 26;
			this.performancesStateLabel.Text = "Performances";
			// 
			// programsStateLabel
			// 
			this.programsStateLabel.AutoSize = true;
			this.programsStateLabel.Location = new System.Drawing.Point(216, 83);
			this.programsStateLabel.Name = "programsStateLabel";
			this.programsStateLabel.Size = new System.Drawing.Size(51, 13);
			this.programsStateLabel.TabIndex = 29;
			this.programsStateLabel.Text = "Programs";
			// 
			// filmsStateLabel
			// 
			this.filmsStateLabel.AutoSize = true;
			this.filmsStateLabel.Location = new System.Drawing.Point(6, 129);
			this.filmsStateLabel.Name = "filmsStateLabel";
			this.filmsStateLabel.Size = new System.Drawing.Size(30, 13);
			this.filmsStateLabel.TabIndex = 18;
			this.filmsStateLabel.Text = "Films";
			// 
			// peopleStateLabel
			// 
			this.peopleStateLabel.AutoSize = true;
			this.peopleStateLabel.Location = new System.Drawing.Point(216, 60);
			this.peopleStateLabel.Name = "peopleStateLabel";
			this.peopleStateLabel.Size = new System.Drawing.Size(40, 13);
			this.peopleStateLabel.TabIndex = 27;
			this.peopleStateLabel.Text = "People";
			// 
			// ViewSettingsForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 358);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.viewLabel);
			this.Controls.Add(this.menuPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "ViewSettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings - View";
			this.menuPanel.ResumeLayout(false);
			this.menuPanel.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}


		private void SetDefaultState(StateComboBox comboBox, string tableNameDB)
		{
			comboBox.SelectedIndex = comboBox.FindString(_settings.GetDefaultState(tableNameDB));
		}

		private string GetSelectedText(StateComboBox comboBox)
		{
			return comboBox.GetItemText(comboBox.SelectedItem);
		}


		private void saveButton_Click(object sender, EventArgs e)
		{
			string tableNameUI = defaultNotesComboBox.GetItemText(defaultNotesComboBox.SelectedItem);
			_settings.DefaultNotesTable = GetTableNameDB(tableNameUI);

			Dictionary<string, string> defaultStates = new Dictionary<string, string>();
			defaultStates.Add("Affairs",       GetSelectedText(affairsComboBox));
			defaultStates.Add("AnimeFilms",    GetSelectedText(animeFilmsComboBox));
			defaultStates.Add("AnimeSerials",  GetSelectedText(animeSerialsComboBox));
			defaultStates.Add("Bookmarks",     GetSelectedText(bookmarksComboBox));
			defaultStates.Add("Desires",       GetSelectedText(desiresComboBox));
			defaultStates.Add("Films",         GetSelectedText(filmsComboBox));
			defaultStates.Add("Games",         GetSelectedText(gamesComboBox));
			defaultStates.Add("Literature",    GetSelectedText(literatureComboBox));
			defaultStates.Add("Meal",          GetSelectedText(mealComboBox));
			defaultStates.Add("Performances",  GetSelectedText(performancesComboBox));
			defaultStates.Add("People",        GetSelectedText(peopleComboBox));
			defaultStates.Add("Programs",      GetSelectedText(programsComboBox));
			defaultStates.Add("RegularDoings", GetSelectedText(regularDoingsComboBox));
			defaultStates.Add("Serials",       GetSelectedText(serialsComboBox));
			defaultStates.Add("TVShows",       GetSelectedText(TVShowsComboBox));

			_settings.SetDefaultState(defaultStates);

			Close();
		}
	}
}
