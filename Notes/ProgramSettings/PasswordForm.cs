using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notes.ProgramSettings
{
	public class PasswordForm : Form
	{
		private TextBox passwordTextBox;
		private Button submitButton;
		private Label wrongPasswordLabel;
		private Label label1;


		public PasswordForm()
		{
			InitializeComponent();
			wrongPasswordLabel.Visible = false;

			KeyPress += delegate (object o, KeyPressEventArgs e)
			{
				// TODO: запрет ввода определенных символов.
			};

			KeyDown += delegate (object o, KeyEventArgs e)
			{
				wrongPasswordLabel.Visible = false;

				if (e.KeyCode == Keys.Enter)
					submitButton.PerformClick();
			};
		}

		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.submitButton = new System.Windows.Forms.Button();
			this.wrongPasswordLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter password:";
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Location = new System.Drawing.Point(101, 25);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '*';
			this.passwordTextBox.Size = new System.Drawing.Size(222, 20);
			this.passwordTextBox.TabIndex = 0;
			// 
			// submitButton
			// 
			this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.submitButton.Location = new System.Drawing.Point(131, 69);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 23);
			this.submitButton.TabIndex = 1;
			this.submitButton.Text = "Submit";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// wrongPasswordLabel
			// 
			this.wrongPasswordLabel.AutoSize = true;
			this.wrongPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.wrongPasswordLabel.ForeColor = System.Drawing.Color.Red;
			this.wrongPasswordLabel.Location = new System.Drawing.Point(100, 45);
			this.wrongPasswordLabel.Name = "wrongPasswordLabel";
			this.wrongPasswordLabel.Size = new System.Drawing.Size(75, 12);
			this.wrongPasswordLabel.TabIndex = 2;
			this.wrongPasswordLabel.Text = "Wrong password";
			// 
			// PasswordForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(335, 104);
			this.Controls.Add(this.wrongPasswordLabel);
			this.Controls.Add(this.submitButton);
			this.Controls.Add(this.passwordTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PasswordForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Password";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			Settings.SetCurrentPassword(passwordTextBox.Text);

			if (Database.PasswordIsOk())
				Close();
			else
				wrongPasswordLabel.Visible = true;
		}
	}
}
