using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

using Notes.Notes;
using Notes.NoteTables;

namespace Notes.NoteForms
{
	[ToolboxItem(true)]
	[DesignTimeVisible(true)]
	class PersonForm : NoteForm
	{
		private RichTextBox commentRichTextBox;
		private ComboBox stateComboBox;
		private TextBox birthdateTextBox;
		private TextBox nameTextBox;
		private Button submitButton;
		private Label commentLabel;
		private Label stateLabel;
		private Label birthdateLabel;
		private Label sexLabel;
		private TextBox addressTextBox;
		private Label addressLabel;
		private Label contactsLabel;
		private TextBox nicknameTextBox;
		private Label nicknameLabel;
		private RichTextBox contactsRichTextBox;
		private Label nameLabel;
		private ComboBox sexComboBox;

		public PersonForm(MainForm mainForm, NoteTable editedTable, Mode mode):
			base (mainForm, editedTable, mode)
		{
			InitializeComponent();
						
			Text = GetFormText();
			submitButton.Text = GetSubmitButtonText();
			stateComboBox.Items.AddRange(NoteTable.States);
			stateComboBox.SelectedIndex = 0;
			sexComboBox.Items.AddRange(PeopleTable.Sex);
			sexComboBox.SelectedIndex = 0;
			
			Person person = _editedNote as Person;
			if (person != null)
			{
				nameTextBox.Text = person.Name;
				nicknameTextBox.Text = person.Nickname;
				sexComboBox.SelectedIndex = (int)person.Sex;
				birthdateTextBox.Text = person.Birthdate;
				addressTextBox.Text = person.Address;
				contactsRichTextBox.Text = person.Contacts;
				stateComboBox.SelectedIndex = (int)person.CurrentState;
				commentRichTextBox.Text = person.Comment;
			}

			birthdateTextBox.KeyPress += new KeyPressEventHandler(CheckDateInput);

			KeyDown += delegate (object o, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Control)
					submitButton.PerformClick();
			};
		}


		public void CheckDateInput(object sender, KeyPressEventArgs e)
		{
			TextBox birthdateTextBox = (TextBox)sender;
			if (birthdateTextBox == null)
				return;

			// TODO Можно в будущем сделать проверку на ввод. Но пока не решил, как же лучше. 
			// Может, \d{2}\.\d{2}\.\d{4}?
			// Но не хочется жестко фиксировать.
		}
		

		private void InitializeComponent()
		{
			this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
			this.stateComboBox = new System.Windows.Forms.ComboBox();
			this.birthdateTextBox = new System.Windows.Forms.TextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.submitButton = new System.Windows.Forms.Button();
			this.commentLabel = new System.Windows.Forms.Label();
			this.stateLabel = new System.Windows.Forms.Label();
			this.birthdateLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.sexLabel = new System.Windows.Forms.Label();
			this.addressTextBox = new System.Windows.Forms.TextBox();
			this.addressLabel = new System.Windows.Forms.Label();
			this.contactsLabel = new System.Windows.Forms.Label();
			this.nicknameTextBox = new System.Windows.Forms.TextBox();
			this.nicknameLabel = new System.Windows.Forms.Label();
			this.contactsRichTextBox = new System.Windows.Forms.RichTextBox();
			this.sexComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// commentRichTextBox
			// 
			this.commentRichTextBox.Location = new System.Drawing.Point(66, 246);
			this.commentRichTextBox.Name = "commentRichTextBox";
			this.commentRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.commentRichTextBox.Size = new System.Drawing.Size(362, 111);
			this.commentRichTextBox.TabIndex = 7;
			this.commentRichTextBox.Text = "";
			// 
			// stateComboBox
			// 
			this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.stateComboBox.FormattingEnabled = true;
			this.stateComboBox.Location = new System.Drawing.Point(66, 219);
			this.stateComboBox.Name = "stateComboBox";
			this.stateComboBox.Size = new System.Drawing.Size(155, 21);
			this.stateComboBox.TabIndex = 6;
			// 
			// birthdateTextBox
			// 
			this.birthdateTextBox.Location = new System.Drawing.Point(288, 73);
			this.birthdateTextBox.MaxLength = 10;
			this.birthdateTextBox.Name = "birthdateTextBox";
			this.birthdateTextBox.Size = new System.Drawing.Size(140, 20);
			this.birthdateTextBox.TabIndex = 3;
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
			this.submitButton.Location = new System.Drawing.Point(190, 363);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 23);
			this.submitButton.TabIndex = 8;
			this.submitButton.Text = "Submit";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// commentLabel
			// 
			this.commentLabel.AutoSize = true;
			this.commentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.commentLabel.Location = new System.Drawing.Point(9, 249);
			this.commentLabel.Name = "commentLabel";
			this.commentLabel.Size = new System.Drawing.Size(51, 13);
			this.commentLabel.TabIndex = 12;
			this.commentLabel.Text = "Comment";
			// 
			// stateLabel
			// 
			this.stateLabel.AutoSize = true;
			this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stateLabel.Location = new System.Drawing.Point(28, 222);
			this.stateLabel.Name = "stateLabel";
			this.stateLabel.Size = new System.Drawing.Size(32, 13);
			this.stateLabel.TabIndex = 10;
			this.stateLabel.Text = "State";
			// 
			// birthdateLabel
			// 
			this.birthdateLabel.AutoSize = true;
			this.birthdateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.birthdateLabel.Location = new System.Drawing.Point(227, 76);
			this.birthdateLabel.Name = "birthdateLabel";
			this.birthdateLabel.Size = new System.Drawing.Size(55, 13);
			this.birthdateLabel.TabIndex = 8;
			this.birthdateLabel.Text = "Birdthdate";
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
			// sexLabel
			// 
			this.sexLabel.AutoSize = true;
			this.sexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.sexLabel.Location = new System.Drawing.Point(35, 76);
			this.sexLabel.Name = "sexLabel";
			this.sexLabel.Size = new System.Drawing.Size(25, 13);
			this.sexLabel.TabIndex = 15;
			this.sexLabel.Text = "Sex";
			// 
			// addressTextBox
			// 
			this.addressTextBox.Location = new System.Drawing.Point(66, 99);
			this.addressTextBox.Name = "addressTextBox";
			this.addressTextBox.Size = new System.Drawing.Size(362, 20);
			this.addressTextBox.TabIndex = 4;
			// 
			// addressLabel
			// 
			this.addressLabel.AutoSize = true;
			this.addressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.addressLabel.Location = new System.Drawing.Point(15, 102);
			this.addressLabel.Name = "addressLabel";
			this.addressLabel.Size = new System.Drawing.Size(45, 13);
			this.addressLabel.TabIndex = 17;
			this.addressLabel.Text = "Address";
			// 
			// contactsLabel
			// 
			this.contactsLabel.AutoSize = true;
			this.contactsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.contactsLabel.Location = new System.Drawing.Point(11, 128);
			this.contactsLabel.Name = "contactsLabel";
			this.contactsLabel.Size = new System.Drawing.Size(49, 13);
			this.contactsLabel.TabIndex = 19;
			this.contactsLabel.Text = "Contacts";
			// 
			// nicknameTextBox
			// 
			this.nicknameTextBox.Location = new System.Drawing.Point(66, 47);
			this.nicknameTextBox.Name = "nicknameTextBox";
			this.nicknameTextBox.Size = new System.Drawing.Size(362, 20);
			this.nicknameTextBox.TabIndex = 1;
			// 
			// nicknameLabel
			// 
			this.nicknameLabel.AutoSize = true;
			this.nicknameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.nicknameLabel.Location = new System.Drawing.Point(5, 50);
			this.nicknameLabel.Name = "nicknameLabel";
			this.nicknameLabel.Size = new System.Drawing.Size(55, 13);
			this.nicknameLabel.TabIndex = 21;
			this.nicknameLabel.Text = "Nickname";
			// 
			// contactsRichTextBox
			// 
			this.contactsRichTextBox.Location = new System.Drawing.Point(66, 125);
			this.contactsRichTextBox.Name = "contactsRichTextBox";
			this.contactsRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.contactsRichTextBox.Size = new System.Drawing.Size(362, 88);
			this.contactsRichTextBox.TabIndex = 5;
			this.contactsRichTextBox.Text = "";
			// 
			// sexComboBox
			// 
			this.sexComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.sexComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.sexComboBox.FormattingEnabled = true;
			this.sexComboBox.Location = new System.Drawing.Point(66, 72);
			this.sexComboBox.Name = "sexComboBox";
			this.sexComboBox.Size = new System.Drawing.Size(155, 21);
			this.sexComboBox.TabIndex = 2;
			// 
			// PersonForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 395);
			this.Controls.Add(this.sexComboBox);
			this.Controls.Add(this.contactsRichTextBox);
			this.Controls.Add(this.nicknameTextBox);
			this.Controls.Add(this.nicknameLabel);
			this.Controls.Add(this.contactsLabel);
			this.Controls.Add(this.addressTextBox);
			this.Controls.Add(this.addressLabel);
			this.Controls.Add(this.sexLabel);
			this.Controls.Add(this.commentRichTextBox);
			this.Controls.Add(this.stateComboBox);
			this.Controls.Add(this.birthdateTextBox);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.submitButton);
			this.Controls.Add(this.commentLabel);
			this.Controls.Add(this.stateLabel);
			this.Controls.Add(this.birthdateLabel);
			this.Controls.Add(this.nameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Name = "PersonForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Note";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			Person person = (_editedNote != null && _editedNote is Person) ? _editedNote as Person : new Person();
			person.Name = nameTextBox.Text;
			person.Nickname = nicknameTextBox.Text;
			person.Sex = (Person.PSex)sexComboBox.SelectedIndex;
			person.Birthdate = birthdateTextBox.Text;
			person.Address = addressTextBox.Text;
			person.Contacts = contactsRichTextBox.Text;
			person.CurrentState = (Note.State)stateComboBox.SelectedIndex;
			person.Comment = commentRichTextBox.Text;

			_mainForm.AddOrUpdateNote(person);

			Close();
		}
	}
}
