﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

using Notes.Notes;
using Notes.NoteTables;
using Notes.DB;
using Notes.CommonUIElements;

namespace Notes.NoteForms
{
	[ToolboxItem(true)]
	[DesignTimeVisible(true)]
	class GameForm : NoteForm
	{
		private TextBox versionTextBox;
		private Label versionLabel;
		private TextBox emailTextBox;
		private Label emailLabel;
		private TextBox passwordTextBox;
		private Label passwordLabel;
		private TextBox loginTextBox;
		private Label loginLabel;
		private Label linkLabel;
		private RichTextBox linkRichTextBox;
		private RichTextBox commentRichTextBox;
		private TextBox nameTextBox;
		private Button submitButton;
		private Label commentLabel;
		private Label stateLabel;
		private Label nameLabel;
		private TextBox genreTextBox;
		private Label playersCountLabel;
		private ComboBox playersCountComboBox;
		private StateComboBox stateComboBox;
		private Label genreLabel;

		public GameForm(MainForm mainForm, NoteTable editedTable, Mode mode):
			base (mainForm, editedTable, mode)
		{
			InitializeComponent();

			Text = GetFormText();
			submitButton.Text = GetSubmitButtonText();

			AutoCompleteStringCollection genres = new AutoCompleteStringCollection();
			genres.AddRange(Database.SelectUniqueValues("Games", "Genre").Where(x => x != "").ToArray());
			genreTextBox.AutoCompleteCustomSource = genres;

			playersCountComboBox.Items.AddRange(GameTable.PlayersCount);
			playersCountComboBox.SelectedIndex = 0;

			Game game = _editedNote as Game;
			if (game != null)
			{
				nameTextBox.Text = game.Name;
				linkRichTextBox.Text = game.DownloadLink;
				versionTextBox.Text = game.Version;
				genreTextBox.Text = game.Genre;
				loginTextBox.Text = game.Login;
				passwordTextBox.Text = game.Password;
				emailTextBox.Text = game.Email;
				playersCountComboBox.SelectedIndex = (int)game.Players;
				stateComboBox.SelectedIndex = (int)game.CurrentState;
				commentRichTextBox.Text = game.Comment;
			}

			KeyDown += delegate (object o, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Control)
					submitButton.PerformClick();
			};
		}


		private void InitializeComponent()
		{
			this.versionTextBox = new System.Windows.Forms.TextBox();
			this.versionLabel = new System.Windows.Forms.Label();
			this.emailTextBox = new System.Windows.Forms.TextBox();
			this.emailLabel = new System.Windows.Forms.Label();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.loginTextBox = new System.Windows.Forms.TextBox();
			this.loginLabel = new System.Windows.Forms.Label();
			this.linkLabel = new System.Windows.Forms.Label();
			this.linkRichTextBox = new System.Windows.Forms.RichTextBox();
			this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.submitButton = new System.Windows.Forms.Button();
			this.commentLabel = new System.Windows.Forms.Label();
			this.stateLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.genreTextBox = new System.Windows.Forms.TextBox();
			this.genreLabel = new System.Windows.Forms.Label();
			this.playersCountLabel = new System.Windows.Forms.Label();
			this.playersCountComboBox = new System.Windows.Forms.ComboBox();
			this.stateComboBox = new StateComboBox();
			this.SuspendLayout();
			// 
			// versionTextBox
			// 
			this.versionTextBox.Location = new System.Drawing.Point(71, 41);
			this.versionTextBox.Name = "versionTextBox";
			this.versionTextBox.Size = new System.Drawing.Size(362, 20);
			this.versionTextBox.TabIndex = 1;
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.versionLabel.Location = new System.Drawing.Point(23, 44);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(42, 13);
			this.versionLabel.TabIndex = 55;
			this.versionLabel.Text = "Version";
			// 
			// emailTextBox
			// 
			this.emailTextBox.Location = new System.Drawing.Point(71, 298);
			this.emailTextBox.Name = "emailTextBox";
			this.emailTextBox.Size = new System.Drawing.Size(362, 20);
			this.emailTextBox.TabIndex = 7;
			// 
			// emailLabel
			// 
			this.emailLabel.AutoSize = true;
			this.emailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.emailLabel.Location = new System.Drawing.Point(33, 301);
			this.emailLabel.Name = "emailLabel";
			this.emailLabel.Size = new System.Drawing.Size(32, 13);
			this.emailLabel.TabIndex = 54;
			this.emailLabel.Text = "Email";
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Location = new System.Drawing.Point(71, 272);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.Size = new System.Drawing.Size(362, 20);
			this.passwordTextBox.TabIndex = 6;
			// 
			// passwordLabel
			// 
			this.passwordLabel.AutoSize = true;
			this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.passwordLabel.Location = new System.Drawing.Point(12, 275);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(53, 13);
			this.passwordLabel.TabIndex = 53;
			this.passwordLabel.Text = "Password";
			// 
			// loginTextBox
			// 
			this.loginTextBox.Location = new System.Drawing.Point(71, 246);
			this.loginTextBox.Name = "loginTextBox";
			this.loginTextBox.Size = new System.Drawing.Size(362, 20);
			this.loginTextBox.TabIndex = 5;
			// 
			// loginLabel
			// 
			this.loginLabel.AutoSize = true;
			this.loginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.loginLabel.Location = new System.Drawing.Point(32, 249);
			this.loginLabel.Name = "loginLabel";
			this.loginLabel.Size = new System.Drawing.Size(33, 13);
			this.loginLabel.TabIndex = 52;
			this.loginLabel.Text = "Login";
			// 
			// linkLabel
			// 
			this.linkLabel.AutoSize = true;
			this.linkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkLabel.Location = new System.Drawing.Point(38, 132);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new System.Drawing.Size(27, 13);
			this.linkLabel.TabIndex = 51;
			this.linkLabel.Text = "Link";
			// 
			// linkRichTextBox
			// 
			this.linkRichTextBox.Location = new System.Drawing.Point(71, 129);
			this.linkRichTextBox.Name = "linkRichTextBox";
			this.linkRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.linkRichTextBox.Size = new System.Drawing.Size(362, 111);
			this.linkRichTextBox.TabIndex = 4;
			this.linkRichTextBox.Text = "";
			// 
			// commentRichTextBox
			// 
			this.commentRichTextBox.Location = new System.Drawing.Point(71, 351);
			this.commentRichTextBox.Name = "commentRichTextBox";
			this.commentRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.commentRichTextBox.Size = new System.Drawing.Size(362, 111);
			this.commentRichTextBox.TabIndex = 9;
			this.commentRichTextBox.Text = "";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(71, 15);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(362, 20);
			this.nameTextBox.TabIndex = 0;
			// 
			// submitButton
			// 
			this.submitButton.Location = new System.Drawing.Point(191, 468);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 23);
			this.submitButton.TabIndex = 10;
			this.submitButton.Text = "Submit";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// commentLabel
			// 
			this.commentLabel.AutoSize = true;
			this.commentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.commentLabel.Location = new System.Drawing.Point(14, 354);
			this.commentLabel.Name = "commentLabel";
			this.commentLabel.Size = new System.Drawing.Size(51, 13);
			this.commentLabel.TabIndex = 50;
			this.commentLabel.Text = "Comment";
			// 
			// stateLabel
			// 
			this.stateLabel.AutoSize = true;
			this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stateLabel.Location = new System.Drawing.Point(33, 327);
			this.stateLabel.Name = "stateLabel";
			this.stateLabel.Size = new System.Drawing.Size(32, 13);
			this.stateLabel.TabIndex = 49;
			this.stateLabel.Text = "State";
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.nameLabel.Location = new System.Drawing.Point(30, 18);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(35, 13);
			this.nameLabel.TabIndex = 48;
			this.nameLabel.Text = "Name";
			// 
			// genreTextBox
			// 
			this.genreTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.genreTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.genreTextBox.Location = new System.Drawing.Point(70, 67);
			this.genreTextBox.Name = "genreTextBox";
			this.genreTextBox.Size = new System.Drawing.Size(362, 20);
			this.genreTextBox.TabIndex = 2;
			// 
			// genreLabel
			// 
			this.genreLabel.AutoSize = true;
			this.genreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.genreLabel.Location = new System.Drawing.Point(29, 70);
			this.genreLabel.Name = "genreLabel";
			this.genreLabel.Size = new System.Drawing.Size(36, 13);
			this.genreLabel.TabIndex = 57;
			this.genreLabel.Text = "Genre";
			// 
			// playersCountLabel
			// 
			this.playersCountLabel.AutoSize = true;
			this.playersCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.playersCountLabel.Location = new System.Drawing.Point(29, 102);
			this.playersCountLabel.Name = "playersCountLabel";
			this.playersCountLabel.Size = new System.Drawing.Size(31, 13);
			this.playersCountLabel.TabIndex = 58;
			this.playersCountLabel.Text = "Type";
			// 
			// playersCountComboBox
			// 
			this.playersCountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playersCountComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.playersCountComboBox.FormattingEnabled = true;
			this.playersCountComboBox.Location = new System.Drawing.Point(71, 99);
			this.playersCountComboBox.Name = "playersCountComboBox";
			this.playersCountComboBox.Size = new System.Drawing.Size(155, 21);
			this.playersCountComboBox.TabIndex = 3;
			// 
			// stateComboBox1
			// 
			this.stateComboBox.BackColor = System.Drawing.Color.White;
			this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.stateComboBox.FormattingEnabled = true;
			this.stateComboBox.Location = new System.Drawing.Point(70, 324);
			this.stateComboBox.Name = "stateComboBox1";
			this.stateComboBox.Size = new System.Drawing.Size(154, 21);
			this.stateComboBox.TabIndex = 8;
			// 
			// GameForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 498);
			this.Controls.Add(this.stateComboBox);
			this.Controls.Add(this.playersCountComboBox);
			this.Controls.Add(this.playersCountLabel);
			this.Controls.Add(this.genreTextBox);
			this.Controls.Add(this.genreLabel);
			this.Controls.Add(this.versionTextBox);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.emailTextBox);
			this.Controls.Add(this.emailLabel);
			this.Controls.Add(this.passwordTextBox);
			this.Controls.Add(this.passwordLabel);
			this.Controls.Add(this.loginTextBox);
			this.Controls.Add(this.loginLabel);
			this.Controls.Add(this.linkLabel);
			this.Controls.Add(this.linkRichTextBox);
			this.Controls.Add(this.commentRichTextBox);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.submitButton);
			this.Controls.Add(this.commentLabel);
			this.Controls.Add(this.stateLabel);
			this.Controls.Add(this.nameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "GameForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Note";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			Game game = (_editedNote != null && _editedNote is Game) ? _editedNote as Game : new Game();
			game.Name = nameTextBox.Text;
			game.Version = versionTextBox.Text;
			game.Genre = genreTextBox.Text;
			game.DownloadLink = linkRichTextBox.Text;
			game.Login = loginTextBox.Text;
			game.Password = passwordTextBox.Text;
			game.Email = emailTextBox.Text;
			game.Players = (Game.PlayersCount)playersCountComboBox.SelectedIndex;
			game.CurrentState = (Note.State)stateComboBox.SelectedIndex;
			game.Comment = commentRichTextBox.Text;

			SubmitNote(game);

			Close();
		}
	}
}
