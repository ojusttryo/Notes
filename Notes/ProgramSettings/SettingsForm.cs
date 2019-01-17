using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.NoteTables;

namespace Notes.ProgramSettings
{
	public class SettingsForm : Form
	{
		private Label viewLabel;
		private Label securityLabel;
		private Label emailLabel;
		private Label passwordLabel;
		private ComboBox initialNotesComboBox;
		private Label initialNotesLabel;
		private TextBox backupEmailTextBox;
		private TextBox backupPasswordTextBox;
		private Panel menuPanel;
		private TextBox newPasswordTextBox;
		private Label label2;
		private Button saveButton;
		private Panel panel1;
		private Panel panel2;
		private ComboBox initialStateComboBox;
		private Label initialStateLabel;
		private Label backupLabel;

		private Settings _settings;


		public SettingsForm(Settings settings)
		{
			InitializeComponent();
			_settings = settings;
			
			initialNotesComboBox.Items.Add("Anime films");
			initialNotesComboBox.Items.Add("Anime serials");
			initialNotesComboBox.Items.Add("Bookmarks");
			initialNotesComboBox.Items.Add("Desires");
			initialNotesComboBox.Items.Add("Films");
			initialNotesComboBox.Items.Add("Games");
			initialNotesComboBox.Items.Add("Literature");
			initialNotesComboBox.Items.Add("Meal");
			initialNotesComboBox.Items.Add("Performances");
			initialNotesComboBox.Items.Add("People");
			initialNotesComboBox.Items.Add("Programs");
			initialNotesComboBox.Items.Add("Serials");
			initialNotesComboBox.Items.Add("TV shows");
			initialNotesComboBox.SelectedIndex = 0;
			if (_settings.InitialNotesTable.Length > 0)
				initialNotesComboBox.SelectedIndex = initialNotesComboBox.FindString(_settings.InitialNotesTable);

			initialStateComboBox.Items.AddRange(NoteTable.States);
			initialStateComboBox.Items[0] = "All";
			initialStateComboBox.SelectedIndex = 0;
			if (_settings.InitialNotesState.Length > 0)
				initialStateComboBox.SelectedIndex = initialStateComboBox.FindString(_settings.InitialNotesState);

			backupEmailTextBox.Text = _settings.BackupEmail;
			backupPasswordTextBox.Text = _settings.BackupPassword;

			KeyDown += delegate (object o, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Enter)
					saveButton.PerformClick();
				else if (e.KeyCode == Keys.Escape)
					Close();
			};
		}


		private void InitializeComponent()
		{
			this.viewLabel = new System.Windows.Forms.Label();
			this.securityLabel = new System.Windows.Forms.Label();
			this.backupLabel = new System.Windows.Forms.Label();
			this.emailLabel = new System.Windows.Forms.Label();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.initialNotesComboBox = new System.Windows.Forms.ComboBox();
			this.initialNotesLabel = new System.Windows.Forms.Label();
			this.backupEmailTextBox = new System.Windows.Forms.TextBox();
			this.backupPasswordTextBox = new System.Windows.Forms.TextBox();
			this.menuPanel = new System.Windows.Forms.Panel();
			this.initialStateComboBox = new System.Windows.Forms.ComboBox();
			this.initialStateLabel = new System.Windows.Forms.Label();
			this.newPasswordTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.saveButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.menuPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// viewLabel
			// 
			this.viewLabel.AutoSize = true;
			this.viewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.viewLabel.Location = new System.Drawing.Point(13, 10);
			this.viewLabel.Name = "viewLabel";
			this.viewLabel.Size = new System.Drawing.Size(37, 16);
			this.viewLabel.TabIndex = 0;
			this.viewLabel.Text = "View";
			// 
			// securityLabel
			// 
			this.securityLabel.AutoSize = true;
			this.securityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.securityLabel.Location = new System.Drawing.Point(13, 212);
			this.securityLabel.Name = "securityLabel";
			this.securityLabel.Size = new System.Drawing.Size(56, 16);
			this.securityLabel.TabIndex = 1;
			this.securityLabel.Text = "Security";
			// 
			// backupLabel
			// 
			this.backupLabel.AutoSize = true;
			this.backupLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.backupLabel.Location = new System.Drawing.Point(13, 110);
			this.backupLabel.Name = "backupLabel";
			this.backupLabel.Size = new System.Drawing.Size(54, 16);
			this.backupLabel.TabIndex = 2;
			this.backupLabel.Text = "Backup";
			// 
			// emailLabel
			// 
			this.emailLabel.AutoSize = true;
			this.emailLabel.Location = new System.Drawing.Point(5, 15);
			this.emailLabel.Name = "emailLabel";
			this.emailLabel.Size = new System.Drawing.Size(32, 13);
			this.emailLabel.TabIndex = 3;
			this.emailLabel.Text = "Email";
			// 
			// passwordLabel
			// 
			this.passwordLabel.AutoSize = true;
			this.passwordLabel.Location = new System.Drawing.Point(5, 41);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(53, 13);
			this.passwordLabel.TabIndex = 4;
			this.passwordLabel.Text = "Password";
			// 
			// initialNotesComboBox
			// 
			this.initialNotesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.initialNotesComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.initialNotesComboBox.FormattingEnabled = true;
			this.initialNotesComboBox.Location = new System.Drawing.Point(97, 13);
			this.initialNotesComboBox.Name = "initialNotesComboBox";
			this.initialNotesComboBox.Size = new System.Drawing.Size(297, 21);
			this.initialNotesComboBox.TabIndex = 0;
			// 
			// initialNotesLabel
			// 
			this.initialNotesLabel.AutoSize = true;
			this.initialNotesLabel.Location = new System.Drawing.Point(6, 17);
			this.initialNotesLabel.Name = "initialNotesLabel";
			this.initialNotesLabel.Size = new System.Drawing.Size(60, 13);
			this.initialNotesLabel.TabIndex = 9;
			this.initialNotesLabel.Text = "Initial notes";
			// 
			// backupEmailTextBox
			// 
			this.backupEmailTextBox.Location = new System.Drawing.Point(96, 12);
			this.backupEmailTextBox.Name = "backupEmailTextBox";
			this.backupEmailTextBox.Size = new System.Drawing.Size(297, 20);
			this.backupEmailTextBox.TabIndex = 2;
			// 
			// backupPasswordTextBox
			// 
			this.backupPasswordTextBox.Location = new System.Drawing.Point(96, 38);
			this.backupPasswordTextBox.Name = "backupPasswordTextBox";
			this.backupPasswordTextBox.PasswordChar = '*';
			this.backupPasswordTextBox.Size = new System.Drawing.Size(297, 20);
			this.backupPasswordTextBox.TabIndex = 3;
			// 
			// menuPanel
			// 
			this.menuPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.menuPanel.Controls.Add(this.initialStateComboBox);
			this.menuPanel.Controls.Add(this.initialStateLabel);
			this.menuPanel.Controls.Add(this.initialNotesComboBox);
			this.menuPanel.Controls.Add(this.initialNotesLabel);
			this.menuPanel.Location = new System.Drawing.Point(10, 33);
			this.menuPanel.Name = "menuPanel";
			this.menuPanel.Size = new System.Drawing.Size(422, 68);
			this.menuPanel.TabIndex = 0;
			// 
			// initialStateComboBox
			// 
			this.initialStateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.initialStateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.initialStateComboBox.FormattingEnabled = true;
			this.initialStateComboBox.Location = new System.Drawing.Point(97, 39);
			this.initialStateComboBox.Name = "initialStateComboBox";
			this.initialStateComboBox.Size = new System.Drawing.Size(297, 21);
			this.initialStateComboBox.TabIndex = 1;
			// 
			// initialStateLabel
			// 
			this.initialStateLabel.AutoSize = true;
			this.initialStateLabel.Location = new System.Drawing.Point(6, 42);
			this.initialStateLabel.Name = "initialStateLabel";
			this.initialStateLabel.Size = new System.Drawing.Size(57, 13);
			this.initialStateLabel.TabIndex = 11;
			this.initialStateLabel.Text = "Initial state";
			// 
			// newPasswordTextBox
			// 
			this.newPasswordTextBox.Location = new System.Drawing.Point(98, 12);
			this.newPasswordTextBox.Name = "newPasswordTextBox";
			this.newPasswordTextBox.PasswordChar = '*';
			this.newPasswordTextBox.Size = new System.Drawing.Size(296, 20);
			this.newPasswordTextBox.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "New password";
			// 
			// saveButton
			// 
			this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.saveButton.Location = new System.Drawing.Point(187, 299);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 5;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.backupEmailTextBox);
			this.panel1.Controls.Add(this.emailLabel);
			this.panel1.Controls.Add(this.passwordLabel);
			this.panel1.Controls.Add(this.backupPasswordTextBox);
			this.panel1.Location = new System.Drawing.Point(11, 133);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(422, 69);
			this.panel1.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.newPasswordTextBox);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Location = new System.Drawing.Point(10, 236);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(422, 49);
			this.panel2.TabIndex = 2;
			// 
			// SettingsForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 333);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.viewLabel);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.backupLabel);
			this.Controls.Add(this.securityLabel);
			this.Controls.Add(this.menuPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.menuPanel.ResumeLayout(false);
			this.menuPanel.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			// View
			_settings.InitialNotesTable = initialNotesComboBox.GetItemText(initialNotesComboBox.SelectedItem);
			_settings.InitialNotesState = initialStateComboBox.GetItemText(initialStateComboBox.SelectedItem);

			// Backup
			_settings.BackupEmail = backupEmailTextBox.Text;
			_settings.BackupPassword = backupPasswordTextBox.Text;

			// Security
			string password = newPasswordTextBox.Text.Trim();
			_settings.SetNewPassword(password);

			Close();
		}
	}
}
