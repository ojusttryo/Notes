using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;

namespace Notes.CommonUIElements
{
	[ToolboxItem(true)]
	[DesignTimeVisible(true)]
	public class TableBase : DataGridView
	{
		public TableBase()
		{
			MultiSelect = true;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
			AllowUserToResizeRows = false;

			EditMode = DataGridViewEditMode.EditOnEnter;

			DefaultCellStyle.SelectionBackColor = Color.LightCyan;
			DefaultCellStyle.SelectionForeColor = Color.Black;
			BackgroundColor = Color.White;

			RowHeadersVisible = false;

			BorderStyle = BorderStyle.None;
			AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;

			ScrollBars = ScrollBars.Vertical;

			this.ContextMenu = new ContextMenu();
		}


		protected void AddBookmarksContextMenuItems(int URLIndex)
		{
			// В openItem.Click и ContextMenu.Popup используется SelectedRows[0] вместо CurrentRow, т.к. невозможно установить
			// в качестве CurrentRow ту, на которую нажали правой кнопкой мыши.

			MenuItem copyLinkItem = new MenuItem("Copy link");
			copyLinkItem.Click += delegate (object o, EventArgs e)
			{
				// Скопировать ссылку можно только если выбрана 1 строка.
				if (SelectedRows == null || SelectedRows.Count != 1)
					return;

				string uri = SelectedRows[0].Cells[URLIndex].Value.ToString();
				Clipboard.SetText(uri);
			};
			this.ContextMenu.MenuItems.Add(copyLinkItem);

			// Меню для открытия закладки из заметки в браузере.
			MenuItem openInBrowserItem = new MenuItem("Open in default browser");
			openInBrowserItem.Click += delegate (object o, EventArgs e) 
			{
				// Правильность uri проверяем при открытии меню (ContextMenu.Popup). Здесь считаем, что все ОК.
				string uri = SelectedRows[0].Cells[URLIndex].Value.ToString();
				Process.Start(uri);
			};
			this.ContextMenu.MenuItems.Add(openInBrowserItem);
			
			// Пункт меню "Open in default browser" отображается только при наличии правильной ссылки, которую поймет браузер.
			// Пункт меню "Copy link" отображается, только если строка не пустая.
			this.ContextMenu.Popup += delegate (object o, EventArgs e)
			{
				openInBrowserItem.Visible = false;
				copyLinkItem.Visible = false;

				if (SelectedRows == null || SelectedRows.Count != 1)
					return;

				string uri = SelectedRows[0].Cells[URLIndex].Value.ToString();
				Uri uriResult;
				bool isValidUri = Uri.TryCreate(uri, UriKind.Absolute, out uriResult)
					&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
				
				openInBrowserItem.Visible = isValidUri;
				copyLinkItem.Visible = uri.Length > 0;									
			};
		}
	}




}
