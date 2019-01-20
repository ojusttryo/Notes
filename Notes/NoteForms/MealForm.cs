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
	class MealForm : NoteForm
	{
		private RichTextBox commentRichTextBox;
		private ComboBox stateComboBox;
		private TextBox nameTextBox;
		private Button submitButton;
		private Label commentLabel;
		private Label stateLabel;
		private RichTextBox ingredientsRichTextBox;
		private Label ingredientsLabel;
		private RichTextBox recipeRichTextBox;
		private Label recipeLabel;
		private Label nameLabel;
		

		public MealForm(MainForm mainForm, NoteTable editedTable, Mode mode):
			base (mainForm, editedTable, mode)
		{
			InitializeComponent();

			Text = GetFormText();
			submitButton.Text = GetSubmitButtonText();
			stateComboBox.Items.AddRange(NoteTable.States);
			stateComboBox.SelectedIndex = 0;

			Meal meal = _editedNote as Meal;
			if (meal != null)
			{
				nameTextBox.Text = meal.Name;
				ingredientsRichTextBox.Text = meal.Ingredients;
				recipeRichTextBox.Text = meal.Recipe;
				stateComboBox.SelectedIndex = (int)meal.CurrentState;
				commentRichTextBox.Text = meal.Comment;
			}

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
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.submitButton = new System.Windows.Forms.Button();
			this.commentLabel = new System.Windows.Forms.Label();
			this.stateLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.ingredientsRichTextBox = new System.Windows.Forms.RichTextBox();
			this.ingredientsLabel = new System.Windows.Forms.Label();
			this.recipeRichTextBox = new System.Windows.Forms.RichTextBox();
			this.recipeLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// commentRichTextBox
			// 
			this.commentRichTextBox.Location = new System.Drawing.Point(66, 249);
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
			this.stateComboBox.Location = new System.Drawing.Point(66, 222);
			this.stateComboBox.Name = "stateComboBox";
			this.stateComboBox.Size = new System.Drawing.Size(155, 21);
			this.stateComboBox.TabIndex = 3;
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
			this.submitButton.Location = new System.Drawing.Point(421, 366);
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
			this.commentLabel.Location = new System.Drawing.Point(11, 252);
			this.commentLabel.Name = "commentLabel";
			this.commentLabel.Size = new System.Drawing.Size(51, 13);
			this.commentLabel.TabIndex = 12;
			this.commentLabel.Text = "Comment";
			// 
			// stateLabel
			// 
			this.stateLabel.AutoSize = true;
			this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stateLabel.Location = new System.Drawing.Point(30, 225);
			this.stateLabel.Name = "stateLabel";
			this.stateLabel.Size = new System.Drawing.Size(32, 13);
			this.stateLabel.TabIndex = 10;
			this.stateLabel.Text = "State";
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.nameLabel.Location = new System.Drawing.Point(27, 24);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(35, 13);
			this.nameLabel.TabIndex = 6;
			this.nameLabel.Text = "Name";
			// 
			// ingredientsRichTextBox
			// 
			this.ingredientsRichTextBox.Location = new System.Drawing.Point(66, 47);
			this.ingredientsRichTextBox.Name = "ingredientsRichTextBox";
			this.ingredientsRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.ingredientsRichTextBox.Size = new System.Drawing.Size(362, 169);
			this.ingredientsRichTextBox.TabIndex = 1;
			this.ingredientsRichTextBox.Text = "";
			// 
			// ingredientsLabel
			// 
			this.ingredientsLabel.AutoSize = true;
			this.ingredientsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ingredientsLabel.Location = new System.Drawing.Point(3, 50);
			this.ingredientsLabel.Name = "ingredientsLabel";
			this.ingredientsLabel.Size = new System.Drawing.Size(59, 13);
			this.ingredientsLabel.TabIndex = 15;
			this.ingredientsLabel.Text = "Ingredients";
			// 
			// recipeRichTextBox
			// 
			this.recipeRichTextBox.Location = new System.Drawing.Point(493, 21);
			this.recipeRichTextBox.Name = "recipeRichTextBox";
			this.recipeRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.recipeRichTextBox.Size = new System.Drawing.Size(386, 339);
			this.recipeRichTextBox.TabIndex = 2;
			this.recipeRichTextBox.Text = "";
			// 
			// recipeLabel
			// 
			this.recipeLabel.AutoSize = true;
			this.recipeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.recipeLabel.Location = new System.Drawing.Point(446, 24);
			this.recipeLabel.Name = "recipeLabel";
			this.recipeLabel.Size = new System.Drawing.Size(41, 13);
			this.recipeLabel.TabIndex = 17;
			this.recipeLabel.Text = "Recipe";
			// 
			// MealForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(904, 400);
			this.Controls.Add(this.recipeLabel);
			this.Controls.Add(this.recipeRichTextBox);
			this.Controls.Add(this.ingredientsLabel);
			this.Controls.Add(this.ingredientsRichTextBox);
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
			this.Name = "MealForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Meal";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			Meal meal = (_editedNote != null && _editedNote is Meal) ? _editedNote as Meal : new Meal();
			meal.Name = nameTextBox.Text;
			meal.Ingredients = ingredientsRichTextBox.Text;
			meal.Recipe = recipeRichTextBox.Text;
			meal.CurrentState = (Note.State)stateComboBox.SelectedIndex;
			meal.Comment = commentRichTextBox.Text;

			_mainForm.AddOrUpdateNote(meal);

			Close();
		}
	}
}
