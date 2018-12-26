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
			this.searchButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.editButton = new System.Windows.Forms.Button();
			this.settingsButton = new System.Windows.Forms.Button();
			this.menu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.activeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.finishedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.postponedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.waitingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.animeFilmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.animeSerialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bookmarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.literatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.performanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.personToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.serialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.teleshowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// searchButton
			// 
			this.searchButton.Location = new System.Drawing.Point(734, 23);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(50, 50);
			this.searchButton.TabIndex = 0;
			this.searchButton.Text = "Search";
			this.searchButton.UseVisualStyleBackColor = true;
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(734, 69);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(50, 50);
			this.addButton.TabIndex = 2;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = true;
			// 
			// removeButton
			// 
			this.removeButton.Location = new System.Drawing.Point(734, 114);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(50, 50);
			this.removeButton.TabIndex = 3;
			this.removeButton.Text = "Remove";
			this.removeButton.UseVisualStyleBackColor = true;
			// 
			// editButton
			// 
			this.editButton.Location = new System.Drawing.Point(734, 162);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(50, 50);
			this.editButton.TabIndex = 4;
			this.editButton.Text = "Edit";
			this.editButton.UseVisualStyleBackColor = true;
			// 
			// settingsButton
			// 
			this.settingsButton.Location = new System.Drawing.Point(734, 209);
			this.settingsButton.Name = "settingsButton";
			this.settingsButton.Size = new System.Drawing.Size(50, 50);
			this.settingsButton.TabIndex = 5;
			this.settingsButton.Text = "Settings";
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
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.closeToolStripMenuItem.Text = "Close";
			// 
			// categoryToolStripMenuItem
			// 
			this.categoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.animeFilmToolStripMenuItem,
            this.animeSerialToolStripMenuItem,
            this.bookmarkToolStripMenuItem,
            this.filmToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.literatureToolStripMenuItem,
            this.performanceToolStripMenuItem,
            this.personToolStripMenuItem,
            this.programToolStripMenuItem,
            this.serialToolStripMenuItem,
            this.teleshowToolStripMenuItem});
			this.categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
			this.categoryToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
			this.categoryToolStripMenuItem.Text = "Category";
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
			this.allToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.allToolStripMenuItem.Text = "All";
			// 
			// activeToolStripMenuItem
			// 
			this.activeToolStripMenuItem.Name = "activeToolStripMenuItem";
			this.activeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.activeToolStripMenuItem.Text = "Active";
			// 
			// deletedToolStripMenuItem
			// 
			this.deletedToolStripMenuItem.Name = "deletedToolStripMenuItem";
			this.deletedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.deletedToolStripMenuItem.Text = "Deleted";
			// 
			// finishedToolStripMenuItem
			// 
			this.finishedToolStripMenuItem.Name = "finishedToolStripMenuItem";
			this.finishedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.finishedToolStripMenuItem.Text = "Finished";
			// 
			// postponedToolStripMenuItem
			// 
			this.postponedToolStripMenuItem.Name = "postponedToolStripMenuItem";
			this.postponedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.postponedToolStripMenuItem.Text = "Postponed";
			// 
			// waitingToolStripMenuItem
			// 
			this.waitingToolStripMenuItem.Name = "waitingToolStripMenuItem";
			this.waitingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.waitingToolStripMenuItem.Text = "Waiting";
			// 
			// animeFilmToolStripMenuItem
			// 
			this.animeFilmToolStripMenuItem.Name = "animeFilmToolStripMenuItem";
			this.animeFilmToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.animeFilmToolStripMenuItem.Text = "Anime film";
			// 
			// animeSerialToolStripMenuItem
			// 
			this.animeSerialToolStripMenuItem.Name = "animeSerialToolStripMenuItem";
			this.animeSerialToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.animeSerialToolStripMenuItem.Text = "Anime serial";
			// 
			// bookmarkToolStripMenuItem
			// 
			this.bookmarkToolStripMenuItem.Name = "bookmarkToolStripMenuItem";
			this.bookmarkToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.bookmarkToolStripMenuItem.Text = "Bookmark";
			// 
			// filmToolStripMenuItem
			// 
			this.filmToolStripMenuItem.Name = "filmToolStripMenuItem";
			this.filmToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.filmToolStripMenuItem.Text = "Film";
			// 
			// gameToolStripMenuItem
			// 
			this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
			this.gameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.gameToolStripMenuItem.Text = "Game";
			// 
			// literatureToolStripMenuItem
			// 
			this.literatureToolStripMenuItem.Name = "literatureToolStripMenuItem";
			this.literatureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.literatureToolStripMenuItem.Text = "Literature";
			// 
			// performanceToolStripMenuItem
			// 
			this.performanceToolStripMenuItem.Name = "performanceToolStripMenuItem";
			this.performanceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.performanceToolStripMenuItem.Text = "Performance";
			// 
			// personToolStripMenuItem
			// 
			this.personToolStripMenuItem.Name = "personToolStripMenuItem";
			this.personToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.personToolStripMenuItem.Text = "Person";
			// 
			// programToolStripMenuItem
			// 
			this.programToolStripMenuItem.Name = "programToolStripMenuItem";
			this.programToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.programToolStripMenuItem.Text = "Program";
			// 
			// serialToolStripMenuItem
			// 
			this.serialToolStripMenuItem.Name = "serialToolStripMenuItem";
			this.serialToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.serialToolStripMenuItem.Text = "Serial";
			// 
			// teleshowToolStripMenuItem
			// 
			this.teleshowToolStripMenuItem.Name = "teleshowToolStripMenuItem";
			this.teleshowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.teleshowToolStripMenuItem.Text = "Teleshow";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 462);
			this.Controls.Add(this.settingsButton);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.removeButton);
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
		private System.Windows.Forms.Button removeButton;
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
		private System.Windows.Forms.ToolStripMenuItem animeFilmToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem animeSerialToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bookmarkToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filmToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem literatureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem performanceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem personToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem serialToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem teleshowToolStripMenuItem;
	}
}

