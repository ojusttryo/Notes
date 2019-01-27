using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

using Notes.CommonUIElements;

namespace Notes.ProgramSettings
{
	public class SecuritySettingsForm : Form
	{
		private Panel panel2;
		private TextBox newPasswordTextBox;
		private Label label2;
		private Panel panel1;
		private TextBox backupEmailTextBox;
		private Label emailLabel;
		private Label passwordLabel;
		private TextBox backupPasswordTextBox;
		private Button saveButton;
		private Label backupLabel;
		private Label securityLabel;
		private Button removeProtectionButton;
		private Settings _settings;

		public SecuritySettingsForm(Settings settings)
		{
			InitializeComponent();
			_settings = settings;

			backupEmailTextBox.Text = _settings.BackupEmail;
			backupPasswordTextBox.Text = _settings.BackupPassword;

			newPasswordTextBox.KeyPress += new KeyPressEventHandler(InputEventHandler.CheckPasword);

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
			this.panel2 = new System.Windows.Forms.Panel();
			this.newPasswordTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.backupEmailTextBox = new System.Windows.Forms.TextBox();
			this.emailLabel = new System.Windows.Forms.Label();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.backupPasswordTextBox = new System.Windows.Forms.TextBox();
			this.saveButton = new System.Windows.Forms.Button();
			this.backupLabel = new System.Windows.Forms.Label();
			this.securityLabel = new System.Windows.Forms.Label();
			this.removeProtectionButton = new System.Windows.Forms.Button();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.removeProtectionButton);
			this.panel2.Controls.Add(this.newPasswordTextBox);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Location = new System.Drawing.Point(9, 135);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(422, 49);
			this.panel2.TabIndex = 8;
			// 
			// newPasswordTextBox
			// 
			this.newPasswordTextBox.Location = new System.Drawing.Point(98, 12);
			this.newPasswordTextBox.Name = "newPasswordTextBox";
			this.newPasswordTextBox.PasswordChar = '*';
			this.newPasswordTextBox.Size = new System.Drawing.Size(183, 20);
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
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.backupEmailTextBox);
			this.panel1.Controls.Add(this.emailLabel);
			this.panel1.Controls.Add(this.passwordLabel);
			this.panel1.Controls.Add(this.backupPasswordTextBox);
			this.panel1.Location = new System.Drawing.Point(10, 32);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(422, 69);
			this.panel1.TabIndex = 6;
			// 
			// backupEmailTextBox
			// 
			this.backupEmailTextBox.Location = new System.Drawing.Point(96, 12);
			this.backupEmailTextBox.Name = "backupEmailTextBox";
			this.backupEmailTextBox.Size = new System.Drawing.Size(297, 20);
			this.backupEmailTextBox.TabIndex = 2;
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
			// backupPasswordTextBox
			// 
			this.backupPasswordTextBox.Location = new System.Drawing.Point(96, 38);
			this.backupPasswordTextBox.Name = "backupPasswordTextBox";
			this.backupPasswordTextBox.PasswordChar = '*';
			this.backupPasswordTextBox.Size = new System.Drawing.Size(297, 20);
			this.backupPasswordTextBox.TabIndex = 3;
			// 
			// saveButton
			// 
			this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.saveButton.Location = new System.Drawing.Point(186, 198);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 10;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// backupLabel
			// 
			this.backupLabel.AutoSize = true;
			this.backupLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.backupLabel.Location = new System.Drawing.Point(12, 9);
			this.backupLabel.Name = "backupLabel";
			this.backupLabel.Size = new System.Drawing.Size(54, 16);
			this.backupLabel.TabIndex = 9;
			this.backupLabel.Text = "Backup";
			// 
			// securityLabel
			// 
			this.securityLabel.AutoSize = true;
			this.securityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.securityLabel.Location = new System.Drawing.Point(12, 111);
			this.securityLabel.Name = "securityLabel";
			this.securityLabel.Size = new System.Drawing.Size(53, 16);
			this.securityLabel.TabIndex = 7;
			this.securityLabel.Text = "Access";
			// 
			// removeProtectionButton
			// 
			this.removeProtectionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.removeProtectionButton.Location = new System.Drawing.Point(287, 10);
			this.removeProtectionButton.Name = "removeProtectionButton";
			this.removeProtectionButton.Size = new System.Drawing.Size(107, 23);
			this.removeProtectionButton.TabIndex = 11;
			this.removeProtectionButton.Text = "Remove protection";
			this.removeProtectionButton.UseVisualStyleBackColor = true;
			this.removeProtectionButton.Click += new System.EventHandler(this.removeProtectionButton_Click);
			// 
			// SecuritySettingsForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 230);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.backupLabel);
			this.Controls.Add(this.securityLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "SecuritySettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings - Security";
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			// Backup
			_settings.BackupEmail = backupEmailTextBox.Text;
			_settings.BackupPassword = backupPasswordTextBox.Text;

			// Access
			string password = newPasswordTextBox.Text.Trim();
			_settings.SetNewPassword(password);

			Close();
		}

		private void removeProtectionButton_Click(object sender, EventArgs e)
		{
			_settings.RemovePasswordProtection();
		}
	}
}
