using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Notes.Notes;
using Notes.NoteTables;

namespace Notes.NoteForms
{
	public class SerialForm : Form
	{
		private RichTextBox commentRichTextBox;
		private ComboBox stateComboBox;
		private TextBox seasonTextBox;
		private TextBox nameTextBox;
		private Button submitButton;
		private Label commentLabel;
		private Label stateLabel;
		private Label seasonLabel;
		private TextBox episodeTextBox;
		private Label episodeLabel;
		private Label nameLabel;

		private MainForm _mainForm;
		private Serial _editedNote;

		public SerialForm(MainForm parent, string title, string buttonText, Note editedNote = null)
		{
			InitializeComponent();

			_mainForm = parent;
			_editedNote = editedNote as Serial;
			Text = title;
			submitButton.Text = buttonText;
			stateComboBox.Items.AddRange(NoteTable.States);
			stateComboBox.SelectedIndex = 0;

			if (_editedNote != null)
			{
				nameTextBox.Text = _editedNote.Name;
				seasonTextBox.Text = _editedNote.Season.ToString();
				episodeTextBox.Text = _editedNote.Episode.ToString();
				stateComboBox.SelectedIndex = (int)_editedNote.CurrentState;
				commentRichTextBox.Text = _editedNote.Comment;
			}

			seasonTextBox.KeyPress +=  new KeyPressEventHandler(MainForm.CheckNumericInput);
			episodeTextBox.KeyPress += new KeyPressEventHandler(MainForm.CheckNumericInput);

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
			this.seasonTextBox = new System.Windows.Forms.TextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.submitButton = new System.Windows.Forms.Button();
			this.commentLabel = new System.Windows.Forms.Label();
			this.stateLabel = new System.Windows.Forms.Label();
			this.seasonLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.episodeTextBox = new System.Windows.Forms.TextBox();
			this.episodeLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// commentRichTextBox
			// 
			this.commentRichTextBox.Location = new System.Drawing.Point(66, 74);
			this.commentRichTextBox.Name = "commentRichTextBox";
			this.commentRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.commentRichTextBox.Size = new System.Drawing.Size(362, 111);
			this.commentRichTextBox.TabIndex = 4;
			this.commentRichTextBox.Text = "";
			// 
			// stateComboBox
			// 
			this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.stateComboBox.FormattingEnabled = true;
			this.stateComboBox.Location = new System.Drawing.Point(273, 47);
			this.stateComboBox.Name = "stateComboBox";
			this.stateComboBox.Size = new System.Drawing.Size(155, 21);
			this.stateComboBox.TabIndex = 3;
			// 
			// seasonTextBox
			// 
			this.seasonTextBox.Location = new System.Drawing.Point(66, 47);
			this.seasonTextBox.MaxLength = 9;
			this.seasonTextBox.Name = "seasonTextBox";
			this.seasonTextBox.Size = new System.Drawing.Size(59, 20);
			this.seasonTextBox.TabIndex = 1;
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
			this.submitButton.Location = new System.Drawing.Point(190, 191);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 23);
			this.submitButton.TabIndex = 5;
			this.submitButton.Text = "Submit";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// commentLabel
			// 
			this.commentLabel.AutoSize = true;
			this.commentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.commentLabel.Location = new System.Drawing.Point(9, 77);
			this.commentLabel.Name = "commentLabel";
			this.commentLabel.Size = new System.Drawing.Size(51, 13);
			this.commentLabel.TabIndex = 12;
			this.commentLabel.Text = "Comment";
			// 
			// stateLabel
			// 
			this.stateLabel.AutoSize = true;
			this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stateLabel.Location = new System.Drawing.Point(239, 51);
			this.stateLabel.Name = "stateLabel";
			this.stateLabel.Size = new System.Drawing.Size(32, 13);
			this.stateLabel.TabIndex = 10;
			this.stateLabel.Text = "State";
			// 
			// seasonLabel
			// 
			this.seasonLabel.AutoSize = true;
			this.seasonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.seasonLabel.Location = new System.Drawing.Point(17, 52);
			this.seasonLabel.Name = "seasonLabel";
			this.seasonLabel.Size = new System.Drawing.Size(43, 13);
			this.seasonLabel.TabIndex = 8;
			this.seasonLabel.Text = "Season";
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
			// episodeTextBox
			// 
			this.episodeTextBox.Location = new System.Drawing.Point(177, 47);
			this.episodeTextBox.MaxLength = 9;
			this.episodeTextBox.Name = "episodeTextBox";
			this.episodeTextBox.Size = new System.Drawing.Size(59, 20);
			this.episodeTextBox.TabIndex = 2;
			// 
			// episodeLabel
			// 
			this.episodeLabel.AutoSize = true;
			this.episodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.episodeLabel.Location = new System.Drawing.Point(131, 50);
			this.episodeLabel.Name = "episodeLabel";
			this.episodeLabel.Size = new System.Drawing.Size(45, 13);
			this.episodeLabel.TabIndex = 15;
			this.episodeLabel.Text = "Episode";
			// 
			// SerialForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 224);
			this.Controls.Add(this.episodeTextBox);
			this.Controls.Add(this.episodeLabel);
			this.Controls.Add(this.commentRichTextBox);
			this.Controls.Add(this.stateComboBox);
			this.Controls.Add(this.seasonTextBox);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.submitButton);
			this.Controls.Add(this.commentLabel);
			this.Controls.Add(this.stateLabel);
			this.Controls.Add(this.seasonLabel);
			this.Controls.Add(this.nameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "SerialForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Note";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			// Add or update note
			bool isUpdating = (_editedNote != null);

			Serial serial = isUpdating ? _editedNote : new Serial();
			serial.Name = nameTextBox.Text;
			serial.Season = (seasonTextBox.Text.Trim().Length == 0) ? 0 : Int32.Parse(seasonTextBox.Text.Trim());
			serial.Episode = (episodeTextBox.Text.Trim().Length == 0) ? 0 : Int32.Parse(episodeTextBox.Text.Trim());
			serial.CurrentState = (Note.State)stateComboBox.SelectedIndex;
			serial.Comment = commentRichTextBox.Text;

			_mainForm.AddOrUpdateNote(serial);

			Close();
		}
	}
}
