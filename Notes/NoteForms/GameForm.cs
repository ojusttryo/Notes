using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Notes.Notes;
using Notes.NoteTables;

namespace Notes.NoteForms
{
	class GameForm : Form
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
		private ComboBox stateComboBox;
		private TextBox nameTextBox;
		private Button submitButton;
		private Label commentLabel;
		private Label stateLabel;
		private Label nameLabel;
		private TextBox genreTextBox;
		private Label genreLabel;

		private MainForm _mainForm;
		private Game _editedNote;

		public GameForm(MainForm parent, string title, string buttonText, Note editedNote = null)
		{
			InitializeComponent();

			_mainForm = parent;
			_editedNote = editedNote as Game;
			Text = title;
			submitButton.Text = buttonText;
			stateComboBox.Items.AddRange(NoteTable.States);
			stateComboBox.SelectedIndex = 0;

			if (_editedNote != null)
			{
				nameTextBox.Text = _editedNote.Name;
				linkRichTextBox.Text = _editedNote.DownloadLink;
				versionTextBox.Text = _editedNote.Version;
				genreTextBox.Text = _editedNote.Genre;
				loginTextBox.Text = _editedNote.Login;
				passwordTextBox.Text = _editedNote.Password;
				emailTextBox.Text = _editedNote.Email;
				stateComboBox.SelectedIndex = (int)_editedNote.CurrentState;
				commentRichTextBox.Text = _editedNote.Comment;
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
			this.stateComboBox = new System.Windows.Forms.ComboBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.submitButton = new System.Windows.Forms.Button();
			this.commentLabel = new System.Windows.Forms.Label();
			this.stateLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.genreTextBox = new System.Windows.Forms.TextBox();
			this.genreLabel = new System.Windows.Forms.Label();
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
			this.emailTextBox.Location = new System.Drawing.Point(71, 262);
			this.emailTextBox.Name = "emailTextBox";
			this.emailTextBox.Size = new System.Drawing.Size(362, 20);
			this.emailTextBox.TabIndex = 6;
			// 
			// emailLabel
			// 
			this.emailLabel.AutoSize = true;
			this.emailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.emailLabel.Location = new System.Drawing.Point(33, 265);
			this.emailLabel.Name = "emailLabel";
			this.emailLabel.Size = new System.Drawing.Size(32, 13);
			this.emailLabel.TabIndex = 54;
			this.emailLabel.Text = "Email";
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Location = new System.Drawing.Point(71, 236);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.Size = new System.Drawing.Size(362, 20);
			this.passwordTextBox.TabIndex = 5;
			// 
			// passwordLabel
			// 
			this.passwordLabel.AutoSize = true;
			this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.passwordLabel.Location = new System.Drawing.Point(12, 239);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(53, 13);
			this.passwordLabel.TabIndex = 53;
			this.passwordLabel.Text = "Password";
			// 
			// loginTextBox
			// 
			this.loginTextBox.Location = new System.Drawing.Point(71, 210);
			this.loginTextBox.Name = "loginTextBox";
			this.loginTextBox.Size = new System.Drawing.Size(362, 20);
			this.loginTextBox.TabIndex = 4;
			// 
			// loginLabel
			// 
			this.loginLabel.AutoSize = true;
			this.loginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.loginLabel.Location = new System.Drawing.Point(32, 213);
			this.loginLabel.Name = "loginLabel";
			this.loginLabel.Size = new System.Drawing.Size(33, 13);
			this.loginLabel.TabIndex = 52;
			this.loginLabel.Text = "Login";
			// 
			// linkLabel
			// 
			this.linkLabel.AutoSize = true;
			this.linkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkLabel.Location = new System.Drawing.Point(38, 96);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new System.Drawing.Size(27, 13);
			this.linkLabel.TabIndex = 51;
			this.linkLabel.Text = "Link";
			// 
			// linkRichTextBox
			// 
			this.linkRichTextBox.Location = new System.Drawing.Point(71, 93);
			this.linkRichTextBox.Name = "linkRichTextBox";
			this.linkRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.linkRichTextBox.Size = new System.Drawing.Size(362, 111);
			this.linkRichTextBox.TabIndex = 3;
			this.linkRichTextBox.Text = "";
			// 
			// commentRichTextBox
			// 
			this.commentRichTextBox.Location = new System.Drawing.Point(71, 315);
			this.commentRichTextBox.Name = "commentRichTextBox";
			this.commentRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.commentRichTextBox.Size = new System.Drawing.Size(362, 111);
			this.commentRichTextBox.TabIndex = 8;
			this.commentRichTextBox.Text = "";
			// 
			// stateComboBox
			// 
			this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.stateComboBox.FormattingEnabled = true;
			this.stateComboBox.Location = new System.Drawing.Point(71, 288);
			this.stateComboBox.Name = "stateComboBox";
			this.stateComboBox.Size = new System.Drawing.Size(155, 21);
			this.stateComboBox.TabIndex = 7;
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
			this.submitButton.Location = new System.Drawing.Point(191, 432);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 23);
			this.submitButton.TabIndex = 9;
			this.submitButton.Text = "Submit";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// commentLabel
			// 
			this.commentLabel.AutoSize = true;
			this.commentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.commentLabel.Location = new System.Drawing.Point(14, 318);
			this.commentLabel.Name = "commentLabel";
			this.commentLabel.Size = new System.Drawing.Size(51, 13);
			this.commentLabel.TabIndex = 50;
			this.commentLabel.Text = "Comment";
			// 
			// stateLabel
			// 
			this.stateLabel.AutoSize = true;
			this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stateLabel.Location = new System.Drawing.Point(33, 291);
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
			// GameForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 466);
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
			this.Controls.Add(this.stateComboBox);
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
			// Add or update note
			bool isUpdating = (_editedNote != null);

			Game game = isUpdating ? _editedNote : new Game();
			game.Name = nameTextBox.Text;
			game.Version = versionTextBox.Text;
			game.Genre = versionTextBox.Text;
			game.DownloadLink = linkRichTextBox.Text;
			game.Login = loginTextBox.Text;
			game.Password = passwordTextBox.Text;
			game.Email = emailTextBox.Text;
			game.CurrentState = (Note.State)stateComboBox.SelectedIndex;
			game.Comment = commentRichTextBox.Text;

			_mainForm.AddOrUpdateNote(game);

			Close();
		}
	}
}
