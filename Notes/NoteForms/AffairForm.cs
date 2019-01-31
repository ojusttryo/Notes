using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;

using Notes.NoteTables;
using Notes.Notes;

namespace Notes.NoteForms
{
	public class AffairForm : NoteForm
	{
		private CommonUIElements.StateComboBox stateComboBox;
		private RichTextBox descriptionRichTextBox;
		private RichTextBox commentRichTextBox;
		private TextBox nameTextBox;
		private Button submitButton;
		private Label commentLabel;
		private Label stateLabel;
		private Label descriptionLabel;
		private CheckBox setDateCheckBox;
		private DateTimePicker datePicker;
		private Label nameLabel;


		public AffairForm(MainForm mainForm, NoteTable editedTable, Mode mode):
			base(mainForm, editedTable, mode)
		{
			InitializeComponent();
			
			Text = GetFormText();
			submitButton.Text = GetSubmitButtonText();
			datePicker.Visible = false;

			Affair affair = _editedNote as Affair;
			if (affair != null)
			{
				nameTextBox.Text = affair.Name;
				descriptionRichTextBox.Text = affair.Description;
				setDateCheckBox.Checked = affair.IsDateSet;
				datePicker.Value = affair.Date;
				stateComboBox.SelectedIndex = (int)affair.CurrentState;
				commentRichTextBox.Text = affair.Comment;
			}

			KeyDown += delegate (object o, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Control)
					submitButton.PerformClick();
			};			
		}


		private void InitializeComponent()
		{
			this.stateComboBox = new CommonUIElements.StateComboBox();
			this.descriptionRichTextBox = new System.Windows.Forms.RichTextBox();
			this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.submitButton = new System.Windows.Forms.Button();
			this.commentLabel = new System.Windows.Forms.Label();
			this.stateLabel = new System.Windows.Forms.Label();
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.setDateCheckBox = new System.Windows.Forms.CheckBox();
			this.datePicker = new System.Windows.Forms.DateTimePicker();
			this.SuspendLayout();
			// 
			// stateComboBox
			// 
			this.stateComboBox.BackColor = System.Drawing.Color.White;
			this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.stateComboBox.FormattingEnabled = true;
			this.stateComboBox.Location = new System.Drawing.Point(70, 226);
			this.stateComboBox.Name = "stateComboBox";
			this.stateComboBox.Size = new System.Drawing.Size(154, 21);
			this.stateComboBox.TabIndex = 4;
			// 
			// descriptionRichTextBox
			// 
			this.descriptionRichTextBox.Location = new System.Drawing.Point(70, 40);
			this.descriptionRichTextBox.Name = "descriptionRichTextBox";
			this.descriptionRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.descriptionRichTextBox.Size = new System.Drawing.Size(362, 151);
			this.descriptionRichTextBox.TabIndex = 1;
			this.descriptionRichTextBox.Text = "";
			// 
			// commentRichTextBox
			// 
			this.commentRichTextBox.Location = new System.Drawing.Point(70, 253);
			this.commentRichTextBox.Name = "commentRichTextBox";
			this.commentRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.commentRichTextBox.Size = new System.Drawing.Size(362, 111);
			this.commentRichTextBox.TabIndex = 5;
			this.commentRichTextBox.Text = "";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(70, 12);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(362, 20);
			this.nameTextBox.TabIndex = 0;
			// 
			// submitButton
			// 
			this.submitButton.Location = new System.Drawing.Point(194, 370);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 23);
			this.submitButton.TabIndex = 6;
			this.submitButton.Text = "Submit";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// commentLabel
			// 
			this.commentLabel.AutoSize = true;
			this.commentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.commentLabel.Location = new System.Drawing.Point(13, 256);
			this.commentLabel.Name = "commentLabel";
			this.commentLabel.Size = new System.Drawing.Size(51, 13);
			this.commentLabel.TabIndex = 21;
			this.commentLabel.Text = "Comment";
			// 
			// stateLabel
			// 
			this.stateLabel.AutoSize = true;
			this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stateLabel.Location = new System.Drawing.Point(32, 229);
			this.stateLabel.Name = "stateLabel";
			this.stateLabel.Size = new System.Drawing.Size(32, 13);
			this.stateLabel.TabIndex = 20;
			this.stateLabel.Text = "State";
			// 
			// descriptionLabel
			// 
			this.descriptionLabel.AutoSize = true;
			this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.descriptionLabel.Location = new System.Drawing.Point(4, 43);
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
			this.descriptionLabel.TabIndex = 19;
			this.descriptionLabel.Text = "Description";
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.nameLabel.Location = new System.Drawing.Point(29, 15);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(35, 13);
			this.nameLabel.TabIndex = 18;
			this.nameLabel.Text = "Name";
			// 
			// setDateCheckBox
			// 
			this.setDateCheckBox.AutoSize = true;
			this.setDateCheckBox.Location = new System.Drawing.Point(70, 198);
			this.setDateCheckBox.Name = "setDateCheckBox";
			this.setDateCheckBox.Size = new System.Drawing.Size(66, 17);
			this.setDateCheckBox.TabIndex = 2;
			this.setDateCheckBox.Text = "Set date";
			this.setDateCheckBox.UseVisualStyleBackColor = true;
			this.setDateCheckBox.CheckedChanged += new System.EventHandler(this.setDateCheckBox_CheckedChanged);
			// 
			// whenPicker
			// 
			this.datePicker.CustomFormat = "dd.MM.yyyy";
			this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.datePicker.Location = new System.Drawing.Point(175, 197);
			this.datePicker.Name = "whenPicker";
			this.datePicker.Size = new System.Drawing.Size(94, 20);
			this.datePicker.TabIndex = 3;
			// 
			// AffairForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 402);
			this.Controls.Add(this.datePicker);
			this.Controls.Add(this.setDateCheckBox);
			this.Controls.Add(this.stateComboBox);
			this.Controls.Add(this.descriptionRichTextBox);
			this.Controls.Add(this.commentRichTextBox);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.submitButton);
			this.Controls.Add(this.commentLabel);
			this.Controls.Add(this.stateLabel);
			this.Controls.Add(this.descriptionLabel);
			this.Controls.Add(this.nameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "AffairForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Note";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void setDateCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			datePicker.Visible = setDateCheckBox.Checked;
		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			DateTime d = datePicker.Value;

			Affair affair = (_editedNote != null && _editedNote is Affair) ? _editedNote as Affair : new Affair();
			affair.Name = nameTextBox.Text;
			affair.Description = descriptionRichTextBox.Text;
			affair.IsDateSet = setDateCheckBox.Checked;
			affair.Date = datePicker.Value;
			affair.CurrentState = (Note.State)stateComboBox.SelectedIndex;
			affair.Comment = commentRichTextBox.Text;

			SubmitNote(affair);

			Close();
		}
	}
}
