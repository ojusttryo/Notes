namespace Notes
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.animeFilmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.animeSerialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bookmarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.desiresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.literatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.performancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.peopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.programsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.serialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TVshowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.allStatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.activeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.finishedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.postponedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.waitingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.allSexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.maleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.femaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchTextBox = new System.Windows.Forms.TextBox();
			this.searchComboBox = new System.Windows.Forms.ComboBox();
			this.settingsButton = new System.Windows.Forms.Button();
			this.editButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.searchButton = new System.Windows.Forms.Button();
			this.menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// menu
			// 
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.categoryToolStripMenuItem,
            this.stateToolStripMenuItem,
            this.sexToolStripMenuItem});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(784, 24);
			this.menu.TabIndex = 6;
			this.menu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem,
            this.importToolStripMenuItem,
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// backupToolStripMenuItem
			// 
			this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
			this.backupToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.backupToolStripMenuItem.Text = "Backup";
			this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
			// 
			// importToolStripMenuItem
			// 
			this.importToolStripMenuItem.Name = "importToolStripMenuItem";
			this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.importToolStripMenuItem.Text = "Import...";
			this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// categoryToolStripMenuItem
			// 
			this.categoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.animeFilmsToolStripMenuItem,
            this.animeSerialsToolStripMenuItem,
            this.bookmarksToolStripMenuItem,
            this.desiresToolStripMenuItem,
            this.filmsToolStripMenuItem,
            this.gamesToolStripMenuItem,
            this.literatureToolStripMenuItem,
            this.mealToolStripMenuItem,
            this.performancesToolStripMenuItem,
            this.peopleToolStripMenuItem,
            this.programsToolStripMenuItem,
            this.serialsToolStripMenuItem,
            this.TVshowsToolStripMenuItem});
			this.categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
			this.categoryToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
			this.categoryToolStripMenuItem.Text = "Category";
			// 
			// animeFilmsToolStripMenuItem
			// 
			this.animeFilmsToolStripMenuItem.Name = "animeFilmsToolStripMenuItem";
			this.animeFilmsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.animeFilmsToolStripMenuItem.Text = "Anime films";
			this.animeFilmsToolStripMenuItem.Click += new System.EventHandler(this.animeFilmsToolStripMenuItem_Click);
			// 
			// animeSerialsToolStripMenuItem
			// 
			this.animeSerialsToolStripMenuItem.Name = "animeSerialsToolStripMenuItem";
			this.animeSerialsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.animeSerialsToolStripMenuItem.Text = "Anime serials";
			this.animeSerialsToolStripMenuItem.Click += new System.EventHandler(this.animeSerialsToolStripMenuItem_Click);
			// 
			// bookmarksToolStripMenuItem
			// 
			this.bookmarksToolStripMenuItem.Name = "bookmarksToolStripMenuItem";
			this.bookmarksToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.bookmarksToolStripMenuItem.Text = "Bookmarks";
			this.bookmarksToolStripMenuItem.Click += new System.EventHandler(this.bookmarksToolStripMenuItem_Click);
			// 
			// desiresToolStripMenuItem
			// 
			this.desiresToolStripMenuItem.Name = "desiresToolStripMenuItem";
			this.desiresToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.desiresToolStripMenuItem.Text = "Desires";
			this.desiresToolStripMenuItem.Click += new System.EventHandler(this.desiresToolStripMenuItem_Click);
			// 
			// filmsToolStripMenuItem
			// 
			this.filmsToolStripMenuItem.Name = "filmsToolStripMenuItem";
			this.filmsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.filmsToolStripMenuItem.Text = "Films";
			this.filmsToolStripMenuItem.Click += new System.EventHandler(this.filmsToolStripMenuItem_Click);
			// 
			// gamesToolStripMenuItem
			// 
			this.gamesToolStripMenuItem.Name = "gamesToolStripMenuItem";
			this.gamesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.gamesToolStripMenuItem.Text = "Games";
			this.gamesToolStripMenuItem.Click += new System.EventHandler(this.gamesToolStripMenuItem_Click);
			// 
			// literatureToolStripMenuItem
			// 
			this.literatureToolStripMenuItem.Name = "literatureToolStripMenuItem";
			this.literatureToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.literatureToolStripMenuItem.Text = "Literature";
			this.literatureToolStripMenuItem.Click += new System.EventHandler(this.literatureToolStripMenuItem_Click);
			// 
			// mealToolStripMenuItem
			// 
			this.mealToolStripMenuItem.Name = "mealToolStripMenuItem";
			this.mealToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.mealToolStripMenuItem.Text = "Meal";
			this.mealToolStripMenuItem.Click += new System.EventHandler(this.mealToolStripMenuItem_Click);
			// 
			// performancesToolStripMenuItem
			// 
			this.performancesToolStripMenuItem.Name = "performancesToolStripMenuItem";
			this.performancesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.performancesToolStripMenuItem.Text = "Performances";
			this.performancesToolStripMenuItem.Click += new System.EventHandler(this.performancesToolStripMenuItem_Click);
			// 
			// peopleToolStripMenuItem
			// 
			this.peopleToolStripMenuItem.Name = "peopleToolStripMenuItem";
			this.peopleToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.peopleToolStripMenuItem.Text = "People";
			this.peopleToolStripMenuItem.Click += new System.EventHandler(this.peopleToolStripMenuItem_Click);
			// 
			// programsToolStripMenuItem
			// 
			this.programsToolStripMenuItem.Name = "programsToolStripMenuItem";
			this.programsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.programsToolStripMenuItem.Text = "Programs";
			this.programsToolStripMenuItem.Click += new System.EventHandler(this.programsToolStripMenuItem_Click);
			// 
			// serialsToolStripMenuItem
			// 
			this.serialsToolStripMenuItem.Name = "serialsToolStripMenuItem";
			this.serialsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.serialsToolStripMenuItem.Text = "Serials";
			this.serialsToolStripMenuItem.Click += new System.EventHandler(this.serialsToolStripMenuItem_Click);
			// 
			// TVshowsToolStripMenuItem
			// 
			this.TVshowsToolStripMenuItem.Name = "TVshowsToolStripMenuItem";
			this.TVshowsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.TVshowsToolStripMenuItem.Text = "TV shows";
			this.TVshowsToolStripMenuItem.Click += new System.EventHandler(this.TVshowsToolStripMenuItem_Click);
			// 
			// stateToolStripMenuItem
			// 
			this.stateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allStatesToolStripMenuItem,
            this.activeToolStripMenuItem,
            this.deletedToolStripMenuItem,
            this.finishedToolStripMenuItem,
            this.postponedToolStripMenuItem,
            this.waitingToolStripMenuItem});
			this.stateToolStripMenuItem.Name = "stateToolStripMenuItem";
			this.stateToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.stateToolStripMenuItem.Text = "State";
			// 
			// allStatesToolStripMenuItem
			// 
			this.allStatesToolStripMenuItem.Name = "allStatesToolStripMenuItem";
			this.allStatesToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.allStatesToolStripMenuItem.Text = "All";
			this.allStatesToolStripMenuItem.Click += new System.EventHandler(this.StateToolStripMenuItem_Click);
			// 
			// activeToolStripMenuItem
			// 
			this.activeToolStripMenuItem.Name = "activeToolStripMenuItem";
			this.activeToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.activeToolStripMenuItem.Text = "Active";
			this.activeToolStripMenuItem.Click += new System.EventHandler(this.StateToolStripMenuItem_Click);
			// 
			// deletedToolStripMenuItem
			// 
			this.deletedToolStripMenuItem.Name = "deletedToolStripMenuItem";
			this.deletedToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.deletedToolStripMenuItem.Text = "Deleted";
			this.deletedToolStripMenuItem.Click += new System.EventHandler(this.StateToolStripMenuItem_Click);
			// 
			// finishedToolStripMenuItem
			// 
			this.finishedToolStripMenuItem.Name = "finishedToolStripMenuItem";
			this.finishedToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.finishedToolStripMenuItem.Text = "Finished";
			this.finishedToolStripMenuItem.Click += new System.EventHandler(this.StateToolStripMenuItem_Click);
			// 
			// postponedToolStripMenuItem
			// 
			this.postponedToolStripMenuItem.Name = "postponedToolStripMenuItem";
			this.postponedToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.postponedToolStripMenuItem.Text = "Postponed";
			this.postponedToolStripMenuItem.Click += new System.EventHandler(this.StateToolStripMenuItem_Click);
			// 
			// waitingToolStripMenuItem
			// 
			this.waitingToolStripMenuItem.Name = "waitingToolStripMenuItem";
			this.waitingToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.waitingToolStripMenuItem.Text = "Waiting";
			this.waitingToolStripMenuItem.Click += new System.EventHandler(this.StateToolStripMenuItem_Click);
			// 
			// sexToolStripMenuItem
			// 
			this.sexToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allSexToolStripMenuItem,
            this.maleToolStripMenuItem,
            this.femaleToolStripMenuItem});
			this.sexToolStripMenuItem.Name = "sexToolStripMenuItem";
			this.sexToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
			this.sexToolStripMenuItem.Text = "Sex";
			// 
			// allSexToolStripMenuItem
			// 
			this.allSexToolStripMenuItem.Name = "allSexToolStripMenuItem";
			this.allSexToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.allSexToolStripMenuItem.Text = "All";
			this.allSexToolStripMenuItem.Click += new System.EventHandler(this.SexToolStripMenuItem1_Click);
			// 
			// maleToolStripMenuItem
			// 
			this.maleToolStripMenuItem.Name = "maleToolStripMenuItem";
			this.maleToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.maleToolStripMenuItem.Text = "Male";
			this.maleToolStripMenuItem.Click += new System.EventHandler(this.SexToolStripMenuItem1_Click);
			// 
			// femaleToolStripMenuItem
			// 
			this.femaleToolStripMenuItem.Name = "femaleToolStripMenuItem";
			this.femaleToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.femaleToolStripMenuItem.Text = "Female";
			this.femaleToolStripMenuItem.Click += new System.EventHandler(this.SexToolStripMenuItem1_Click);
			// 
			// searchTextBox
			// 
			this.searchTextBox.Location = new System.Drawing.Point(128, 29);
			this.searchTextBox.Name = "searchTextBox";
			this.searchTextBox.Size = new System.Drawing.Size(432, 20);
			this.searchTextBox.TabIndex = 4;
			// 
			// searchComboBox
			// 
			this.searchComboBox.BackColor = System.Drawing.Color.White;
			this.searchComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.searchComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.searchComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.searchComboBox.FormattingEnabled = true;
			this.searchComboBox.Location = new System.Drawing.Point(566, 28);
			this.searchComboBox.Name = "searchComboBox";
			this.searchComboBox.Size = new System.Drawing.Size(188, 21);
			this.searchComboBox.TabIndex = 5;
			// 
			// settingsButton
			// 
			this.settingsButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.settingsButton.FlatAppearance.BorderSize = 0;
			this.settingsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.settingsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.settingsButton.Image = global::Notes.Properties.Resources.settings_20x20;
			this.settingsButton.Location = new System.Drawing.Point(97, 25);
			this.settingsButton.Name = "settingsButton";
			this.settingsButton.Size = new System.Drawing.Size(24, 24);
			this.settingsButton.TabIndex = 3;
			this.settingsButton.UseVisualStyleBackColor = true;
			this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
			// 
			// editButton
			// 
			this.editButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.editButton.FlatAppearance.BorderSize = 0;
			this.editButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.editButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.editButton.Image = global::Notes.Properties.Resources.edit_20x20;
			this.editButton.Location = new System.Drawing.Point(68, 25);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(24, 24);
			this.editButton.TabIndex = 2;
			this.editButton.UseVisualStyleBackColor = true;
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.deleteButton.FlatAppearance.BorderSize = 0;
			this.deleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.deleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteButton.Image = global::Notes.Properties.Resources.delete_20х20;
			this.deleteButton.Location = new System.Drawing.Point(39, 25);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(24, 24);
			this.deleteButton.TabIndex = 1;
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// addButton
			// 
			this.addButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.addButton.FlatAppearance.BorderSize = 0;
			this.addButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.addButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addButton.Image = global::Notes.Properties.Resources.add_20х20;
			this.addButton.Location = new System.Drawing.Point(10, 25);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(24, 24);
			this.addButton.TabIndex = 0;
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// searchButton
			// 
			this.searchButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.searchButton.FlatAppearance.BorderSize = 0;
			this.searchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.searchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.searchButton.Image = global::Notes.Properties.Resources.search_20x20;
			this.searchButton.Location = new System.Drawing.Point(755, 25);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(24, 24);
			this.searchButton.TabIndex = 6;
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 462);
			this.Controls.Add(this.searchComboBox);
			this.Controls.Add(this.searchTextBox);
			this.Controls.Add(this.settingsButton);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.menu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menu;
			this.MinimumSize = new System.Drawing.Size(800, 500);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Notes";
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.Button settingsButton;
		private System.Windows.Forms.MenuStrip menu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem categoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem allStatesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem activeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deletedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem finishedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem postponedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem waitingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem animeFilmsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem animeSerialsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bookmarksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filmsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gamesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem literatureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem performancesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem peopleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem programsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem serialsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TVshowsToolStripMenuItem;
		private System.Windows.Forms.TextBox searchTextBox;
		private System.Windows.Forms.ComboBox searchComboBox;
		private System.Windows.Forms.ToolStripMenuItem mealToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sexToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem allSexToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem maleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem femaleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem desiresToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
	}
}

