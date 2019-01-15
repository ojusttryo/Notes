using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.NoteTables;

namespace Notes.Settings
{
	public class SettingsForm : Form
	{
		private Label viewLabel;
		private Label securityLabel;
		private Label emailLabel;
		private Label passwordLabel;
		private ComboBox openOnStartComboBox;
		private Label label1;
		private TextBox backupEmailTextBox;
		private TextBox backupPasswordTextBox;
		private Panel menuPanel;
		private TextBox newPasswordTextBox;
		private Label label2;
		private Button button1;
		private Panel panel1;
		private Panel panel2;
		private Label backupLabel;


		public SettingsForm()
		{
			InitializeComponent();
			
			openOnStartComboBox.Items.Add("Last");
			openOnStartComboBox.Items.Add("Anime films");
			openOnStartComboBox.Items.Add("Anime serials");
			openOnStartComboBox.Items.Add("Bookmarks");
			openOnStartComboBox.Items.Add("Desires");
			openOnStartComboBox.Items.Add("Films");
			openOnStartComboBox.Items.Add("Games");
			openOnStartComboBox.Items.Add("Literature");
			openOnStartComboBox.Items.Add("Meal");
			openOnStartComboBox.Items.Add("Performances");
			openOnStartComboBox.Items.Add("People");
			openOnStartComboBox.Items.Add("Programs");
			openOnStartComboBox.Items.Add("Serials");
			openOnStartComboBox.Items.Add("TV shows");

			openOnStartComboBox.SelectedIndex = 0;


		}


		private void InitializeComponent()
		{
			this.viewLabel = new System.Windows.Forms.Label();
			this.securityLabel = new System.Windows.Forms.Label();
			this.backupLabel = new System.Windows.Forms.Label();
			this.emailLabel = new System.Windows.Forms.Label();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.openOnStartComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.backupEmailTextBox = new System.Windows.Forms.TextBox();
			this.backupPasswordTextBox = new System.Windows.Forms.TextBox();
			this.menuPanel = new System.Windows.Forms.Panel();
			this.newPasswordTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
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
			this.securityLabel.Location = new System.Drawing.Point(13, 191);
			this.securityLabel.Name = "securityLabel";
			this.securityLabel.Size = new System.Drawing.Size(56, 16);
			this.securityLabel.TabIndex = 1;
			this.securityLabel.Text = "Security";
			// 
			// backupLabel
			// 
			this.backupLabel.AutoSize = true;
			this.backupLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.backupLabel.Location = new System.Drawing.Point(13, 89);
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
			// openOnStartComboBox
			// 
			this.openOnStartComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.openOnStartComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.openOnStartComboBox.FormattingEnabled = true;
			this.openOnStartComboBox.Location = new System.Drawing.Point(97, 13);
			this.openOnStartComboBox.Name = "openOnStartComboBox";
			this.openOnStartComboBox.Size = new System.Drawing.Size(297, 21);
			this.openOnStartComboBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Open on start";
			// 
			// backupEmailTextBox
			// 
			this.backupEmailTextBox.Location = new System.Drawing.Point(96, 12);
			this.backupEmailTextBox.Name = "backupEmailTextBox";
			this.backupEmailTextBox.Size = new System.Drawing.Size(297, 20);
			this.backupEmailTextBox.TabIndex = 1;
			// 
			// backupPasswordTextBox
			// 
			this.backupPasswordTextBox.Location = new System.Drawing.Point(96, 38);
			this.backupPasswordTextBox.Name = "backupPasswordTextBox";
			this.backupPasswordTextBox.Size = new System.Drawing.Size(297, 20);
			this.backupPasswordTextBox.TabIndex = 2;
			// 
			// menuPanel
			// 
			this.menuPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.menuPanel.Controls.Add(this.openOnStartComboBox);
			this.menuPanel.Controls.Add(this.label1);
			this.menuPanel.Location = new System.Drawing.Point(10, 33);
			this.menuPanel.Name = "menuPanel";
			this.menuPanel.Size = new System.Drawing.Size(422, 49);
			this.menuPanel.TabIndex = 12;
			// 
			// newPasswordTextBox
			// 
			this.newPasswordTextBox.Location = new System.Drawing.Point(98, 12);
			this.newPasswordTextBox.Name = "newPasswordTextBox";
			this.newPasswordTextBox.Size = new System.Drawing.Size(296, 20);
			this.newPasswordTextBox.TabIndex = 3;
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
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.Location = new System.Drawing.Point(187, 278);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Save";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.backupEmailTextBox);
			this.panel1.Controls.Add(this.emailLabel);
			this.panel1.Controls.Add(this.passwordLabel);
			this.panel1.Controls.Add(this.backupPasswordTextBox);
			this.panel1.Location = new System.Drawing.Point(11, 112);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(422, 69);
			this.panel1.TabIndex = 16;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.newPasswordTextBox);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Location = new System.Drawing.Point(10, 215);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(422, 49);
			this.panel2.TabIndex = 13;
			// 
			// SettingsForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(444, 313);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.viewLabel);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.backupLabel);
			this.Controls.Add(this.securityLabel);
			this.Controls.Add(this.menuPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
	}
}
