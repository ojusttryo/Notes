using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Notes.Notes;

namespace Notes.Import
{
	public class BookmarksImportForm : Form
	{
		private Button selectAllButton;
		private Button deselectAllButton;
		private Button importButton;
		private BookmarksImportTable bookmarksTable;
		private MainForm mainForm;
		private const int indent = 5;

		public BookmarksImportForm(MainForm parent)
		{
			mainForm = parent;	
			InitializeComponent();

			Point tableLocation = new Point(0, indent);
			bookmarksTable = new BookmarksImportTable(tableLocation);

			Controls.Add(bookmarksTable);

			Resize += delegate (object o, EventArgs e)
			{
				int buttonsLocationY = ClientRectangle.Height - indent - selectAllButton.Height;
				selectAllButton.Location = new Point(selectAllButton.Location.X, buttonsLocationY);
				deselectAllButton.Location = new Point(deselectAllButton.Location.X, buttonsLocationY);
				importButton.Location = new Point((ClientRectangle.Width - importButton.Width) / 2, buttonsLocationY);

				int tableWidth = this.ClientRectangle.Width;
				int tableHeight = selectAllButton.Location.Y - indent * 3;
				Size tableSize = new Size(tableWidth, tableHeight);

				bookmarksTable.ChangeSize(tableSize);
			};

			Shown += delegate (object o, EventArgs e) { OnResize(null); };

			ActiveControl = bookmarksTable;
		}


		public void AddRow(string name, string url)
		{
			bookmarksTable.Rows.Add(false, name, url);
		}

		private void InitializeComponent()
		{
			this.selectAllButton = new System.Windows.Forms.Button();
			this.deselectAllButton = new System.Windows.Forms.Button();
			this.importButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// selectAllButton
			// 
			this.selectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.selectAllButton.Location = new System.Drawing.Point(7, 332);
			this.selectAllButton.Name = "selectAllButton";
			this.selectAllButton.Size = new System.Drawing.Size(75, 23);
			this.selectAllButton.TabIndex = 0;
			this.selectAllButton.Text = "Select all";
			this.selectAllButton.UseVisualStyleBackColor = true;
			this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
			// 
			// deselectAllButton
			// 
			this.deselectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.deselectAllButton.Location = new System.Drawing.Point(88, 332);
			this.deselectAllButton.Name = "deselectAllButton";
			this.deselectAllButton.Size = new System.Drawing.Size(75, 23);
			this.deselectAllButton.TabIndex = 1;
			this.deselectAllButton.Text = "Deselect all";
			this.deselectAllButton.UseVisualStyleBackColor = true;
			this.deselectAllButton.Click += new System.EventHandler(this.deselectAllButton_Click);
			// 
			// importButton
			// 
			this.importButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.importButton.Location = new System.Drawing.Point(353, 332);
			this.importButton.Name = "importButton";
			this.importButton.Size = new System.Drawing.Size(75, 23);
			this.importButton.TabIndex = 2;
			this.importButton.Text = "Import";
			this.importButton.UseVisualStyleBackColor = true;
			this.importButton.Click += new System.EventHandler(this.importButton_Click);
			// 
			// BookmarksImportForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(784, 362);
			this.Controls.Add(this.importButton);
			this.Controls.Add(this.deselectAllButton);
			this.Controls.Add(this.selectAllButton);
			this.MinimumSize = new System.Drawing.Size(800, 400);
			this.Name = "BookmarksImportForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import bookmarks";
			this.ResumeLayout(false);

		}

		private void selectAllButton_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in bookmarksTable.Rows)
				bookmarksTable.Rows[row.Index].Cells[0].Value = true;
		}

		private void deselectAllButton_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in bookmarksTable.Rows)
				bookmarksTable.Rows[row.Index].Cells[0].Value = false;
		}

		private void importButton_Click(object sender, EventArgs e)
		{
			List<Bookmark> bookmarks = new List<Bookmark>();

			foreach (DataGridViewRow row in bookmarksTable.Rows)
			{
				DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
				if ((bool)cell.Value == true)
				{
					Bookmark b = new Bookmark();
					b.Name = row.Cells[1].Value.ToString();
					b.URL = row.Cells[2].Value.ToString();
					bookmarks.Add(b);
				}
			}

			this.Close();

			mainForm.ImportBookmarks(bookmarks);
		}
	}
}
