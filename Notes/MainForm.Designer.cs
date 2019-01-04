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
			this.editButton = new System.Windows.Forms.Button();
			this.settingsButton = new System.Windows.Forms.Button();
			this.menu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.animeFilmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.animeSerialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bookmarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.literatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.performancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.peopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.programsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.serialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TVshowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.activeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.finishedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.postponedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.waitingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.searchButton = new System.Windows.Forms.Button();
			this.menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// editButton
			// 
			this.editButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.editButton.FlatAppearance.BorderSize = 0;
			this.editButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.editButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.editButton.Image = global::Notes.Properties.Resources.edit_40x40;
			this.editButton.Location = new System.Drawing.Point(734, 174);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(50, 50);
			this.editButton.TabIndex = 4;
			this.editButton.UseVisualStyleBackColor = true;
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// settingsButton
			// 
			this.settingsButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.settingsButton.FlatAppearance.BorderSize = 0;
			this.settingsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.settingsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.settingsButton.Image = global::Notes.Properties.Resources.settings_40x40;
			this.settingsButton.Location = new System.Drawing.Point(734, 224);
			this.settingsButton.Name = "settingsButton";
			this.settingsButton.Size = new System.Drawing.Size(50, 50);
			this.settingsButton.TabIndex = 5;
			this.settingsButton.UseVisualStyleBackColor = true;
			// 
			// menu
			// 
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.categoryToolStripMenuItem,
            this.stateToolStripMenuItem});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(784, 24);
			this.menu.TabIndex = 6;
			this.menu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.closeToolStripMenuItem.Text = "Close";
			// 
			// categoryToolStripMenuItem
			// 
			this.categoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.animeFilmsToolStripMenuItem,
            this.animeSerialsToolStripMenuItem,
            this.bookmarksToolStripMenuItem,
            this.filmsToolStripMenuItem,
            this.gamesToolStripMenuItem,
            this.literatureToolStripMenuItem,
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
			// 
			// animeSerialsToolStripMenuItem
			// 
			this.animeSerialsToolStripMenuItem.Name = "animeSerialsToolStripMenuItem";
			this.animeSerialsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.animeSerialsToolStripMenuItem.Text = "Anime serials";
			// 
			// bookmarksToolStripMenuItem
			// 
			this.bookmarksToolStripMenuItem.Name = "bookmarksToolStripMenuItem";
			this.bookmarksToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.bookmarksToolStripMenuItem.Text = "Bookmarks";
			// 
			// filmsToolStripMenuItem
			// 
			this.filmsToolStripMenuItem.Name = "filmsToolStripMenuItem";
			this.filmsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.filmsToolStripMenuItem.Text = "Films";
			// 
			// gamesToolStripMenuItem
			// 
			this.gamesToolStripMenuItem.Name = "gamesToolStripMenuItem";
			this.gamesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.gamesToolStripMenuItem.Text = "Games";
			// 
			// literatureToolStripMenuItem
			// 
			this.literatureToolStripMenuItem.Name = "literatureToolStripMenuItem";
			this.literatureToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.literatureToolStripMenuItem.Text = "Literature";
			// 
			// performancesToolStripMenuItem
			// 
			this.performancesToolStripMenuItem.Name = "performancesToolStripMenuItem";
			this.performancesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.performancesToolStripMenuItem.Text = "Performances";
			// 
			// peopleToolStripMenuItem
			// 
			this.peopleToolStripMenuItem.Name = "peopleToolStripMenuItem";
			this.peopleToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.peopleToolStripMenuItem.Text = "People";
			// 
			// programsToolStripMenuItem
			// 
			this.programsToolStripMenuItem.Name = "programsToolStripMenuItem";
			this.programsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.programsToolStripMenuItem.Text = "Programs";
			// 
			// serialsToolStripMenuItem
			// 
			this.serialsToolStripMenuItem.Name = "serialsToolStripMenuItem";
			this.serialsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.serialsToolStripMenuItem.Text = "Serials";
			// 
			// TVshowsToolStripMenuItem
			// 
			this.TVshowsToolStripMenuItem.Name = "TVshowsToolStripMenuItem";
			this.TVshowsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.TVshowsToolStripMenuItem.Text = "TV shows";
			// 
			// stateToolStripMenuItem
			// 
			this.stateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.activeToolStripMenuItem,
            this.deletedToolStripMenuItem,
            this.finishedToolStripMenuItem,
            this.postponedToolStripMenuItem,
            this.waitingToolStripMenuItem});
			this.stateToolStripMenuItem.Name = "stateToolStripMenuItem";
			this.stateToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.stateToolStripMenuItem.Text = "State";
			// 
			// allToolStripMenuItem
			// 
			this.allToolStripMenuItem.Name = "allToolStripMenuItem";
			this.allToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.allToolStripMenuItem.Text = "All";
			// 
			// activeToolStripMenuItem
			// 
			this.activeToolStripMenuItem.Name = "activeToolStripMenuItem";
			this.activeToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.activeToolStripMenuItem.Text = "Active";
			// 
			// deletedToolStripMenuItem
			// 
			this.deletedToolStripMenuItem.Name = "deletedToolStripMenuItem";
			this.deletedToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.deletedToolStripMenuItem.Text = "Deleted";
			// 
			// finishedToolStripMenuItem
			// 
			this.finishedToolStripMenuItem.Name = "finishedToolStripMenuItem";
			this.finishedToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.finishedToolStripMenuItem.Text = "Finished";
			// 
			// postponedToolStripMenuItem
			// 
			this.postponedToolStripMenuItem.Name = "postponedToolStripMenuItem";
			this.postponedToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.postponedToolStripMenuItem.Text = "Postponed";
			// 
			// waitingToolStripMenuItem
			// 
			this.waitingToolStripMenuItem.Name = "waitingToolStripMenuItem";
			this.waitingToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.waitingToolStripMenuItem.Text = "Waiting";
			// 
			// deleteButton
			// 
			this.deleteButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.deleteButton.FlatAppearance.BorderSize = 0;
			this.deleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.deleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteButton.Image = global::Notes.Properties.Resources.delete_40х40;
			this.deleteButton.Location = new System.Drawing.Point(734, 124);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(50, 50);
			this.deleteButton.TabIndex = 3;
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
			this.addButton.Image = global::Notes.Properties.Resources.add_40х40;
			this.addButton.Location = new System.Drawing.Point(734, 73);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(50, 50);
			this.addButton.TabIndex = 2;
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
			this.searchButton.Image = global::Notes.Properties.Resources.search_40x40;
			this.searchButton.Location = new System.Drawing.Point(734, 23);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(50, 50);
			this.searchButton.TabIndex = 0;
			this.searchButton.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 462);
			this.Controls.Add(this.settingsButton);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.menu);
			this.MainMenuStrip = this.menu;
			this.MinimumSize = new System.Drawing.Size(800, 500);
			this.Name = "MainForm";
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
		private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
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
	}
}

