using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Notes.Notes;
using Notes.NoteTables;

namespace Notes.AddForms
{
	// TODO: переименовать класс и кнопку, т.к. используется для добавления и изменения.
	public class AddDatedNoteForm : Form
	{
		private Label yearLabel;
		private Label stateLabel;
		private Label commentLabel;
		private Button addButton;
		private TextBox nameTextBox;
		private TextBox yearTextBox;
		private ComboBox stateComboBox;
		private RichTextBox commentRichTextBox;
		private Label nameLabel;

		private MainForm _mainForm;
		private DatedNote _editedNote;

		public AddDatedNoteForm(MainForm parent, string title, string buttonText, Note editedNote = null)
		{
			InitializeComponent();

			_mainForm = parent;
			_editedNote = editedNote as DatedNote;
			Text = title;
			addButton.Text = buttonText;
			stateComboBox.Items.AddRange(NoteTable.States);
			stateComboBox.SelectedIndex = (editedNote == null) ? 0 : (int)editedNote.CurrentState;

			if (_editedNote != null)
			{
				nameTextBox.Text = _editedNote.Name;
				yearTextBox.Text = _editedNote.Year.ToString();
				stateComboBox.SelectedValue = NoteTable.States[(int)_editedNote.CurrentState];
				commentRichTextBox.Text = _editedNote.Comment;
			}
		}


		private void InitializeComponent()
		{
			this.nameLabel = new System.Windows.Forms.Label();
			this.yearLabel = new System.Windows.Forms.Label();
			this.stateLabel = new System.Windows.Forms.Label();
			this.commentLabel = new System.Windows.Forms.Label();
			this.addButton = new System.Windows.Forms.Button();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.yearTextBox = new System.Windows.Forms.TextBox();
			this.stateComboBox = new System.Windows.Forms.ComboBox();
			this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.nameLabel.Location = new System.Drawing.Point(25, 24);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(35, 13);
			this.nameLabel.TabIndex = 0;
			this.nameLabel.Text = "Name";
			// 
			// yearLabel
			// 
			this.yearLabel.AutoSize = true;
			this.yearLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.yearLabel.Location = new System.Drawing.Point(31, 52);
			this.yearLabel.Name = "yearLabel";
			this.yearLabel.Size = new System.Drawing.Size(29, 13);
			this.yearLabel.TabIndex = 1;
			this.yearLabel.Text = "Year";
			// 
			// stateLabel
			// 
			this.stateLabel.AutoSize = true;
			this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stateLabel.Location = new System.Drawing.Point(214, 52);
			this.stateLabel.Name = "stateLabel";
			this.stateLabel.Size = new System.Drawing.Size(32, 13);
			this.stateLabel.TabIndex = 2;
			this.stateLabel.Text = "State";
			// 
			// commentLabel
			// 
			this.commentLabel.AutoSize = true;
			this.commentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.commentLabel.Location = new System.Drawing.Point(9, 77);
			this.commentLabel.Name = "commentLabel";
			this.commentLabel.Size = new System.Drawing.Size(51, 13);
			this.commentLabel.TabIndex = 3;
			this.commentLabel.Text = "Comment";
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(186, 191);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(75, 23);
			this.addButton.TabIndex = 4;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(66, 21);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(362, 20);
			this.nameTextBox.TabIndex = 0;
			// 
			// yearTextBox
			// 
			this.yearTextBox.Location = new System.Drawing.Point(66, 47);
			this.yearTextBox.Name = "yearTextBox";
			this.yearTextBox.Size = new System.Drawing.Size(142, 20);
			this.yearTextBox.TabIndex = 1;
			// 
			// stateComboBox
			// 
			this.stateComboBox.FormattingEnabled = true;
			this.stateComboBox.Location = new System.Drawing.Point(252, 47);
			this.stateComboBox.Name = "stateComboBox";
			this.stateComboBox.Size = new System.Drawing.Size(176, 21);
			this.stateComboBox.TabIndex = 2;
			// 
			// commentRichTextBox
			// 
			this.commentRichTextBox.Location = new System.Drawing.Point(66, 74);
			this.commentRichTextBox.Name = "commentRichTextBox";
			this.commentRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.commentRichTextBox.Size = new System.Drawing.Size(362, 111);
			this.commentRichTextBox.TabIndex = 3;
			this.commentRichTextBox.Text = "";
			// 
			// AddDatedNoteForm
			// 
			this.ClientSize = new System.Drawing.Size(442, 224);
			this.Controls.Add(this.commentRichTextBox);
			this.Controls.Add(this.stateComboBox);
			this.Controls.Add(this.yearTextBox);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.commentLabel);
			this.Controls.Add(this.stateLabel);
			this.Controls.Add(this.yearLabel);
			this.Controls.Add(this.nameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AddDatedNoteForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add note";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void addButton_Click(object sender, EventArgs e)
		{
			nameTextBox.ForeColor = System.Drawing.Color.Black;
			nameLabel.ForeColor   = System.Drawing.Color.Black;
			yearTextBox.ForeColor = System.Drawing.Color.Black;
			yearLabel.ForeColor   = System.Drawing.Color.Black;

			// Check if input is OK
			uint n = 0;
			if (nameTextBox.Text.Trim().Length == 0)
			{
				nameTextBox.ForeColor = System.Drawing.Color.Red;
				nameLabel.ForeColor = System.Drawing.Color.Red;
				return;
			}
			if (UInt32.TryParse(yearTextBox.Text.Trim(), out n) == false)
			{
				yearTextBox.ForeColor = System.Drawing.Color.Red;
				yearLabel.ForeColor = System.Drawing.Color.Red;
				return;
			}

			// Add or update note
			bool isUpdating = (_editedNote != null);

			DatedNote datedNote = isUpdating ? _editedNote    : new DatedNote();
			datedNote.Name = nameTextBox.Text;
			datedNote.Year = (int)n;
			datedNote.CurrentState = (Note.State)stateComboBox.SelectedIndex;
			datedNote.Comment = commentRichTextBox.Text;

			_mainForm.AddOrUpdateNote(datedNote);

			Close();
		}
	}
}
