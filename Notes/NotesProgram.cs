using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Notes.ProgramSettings;

namespace Notes
{
	static class NotesProgram
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Database.CheckPassword();
			if (Database.IsPasswordProtected)
			{
				PasswordForm form = new PasswordForm();
				form.ShowDialog();

				if (!Database.PasswordIsOk())
					return;
			}
			
			Database.Create();
			Settings.LoadFromDatabase();

			Application.Run(new MainForm());
			
			/*
			 * TODO:
			 * Проверить PVS Studio.
			 * Для закладок можно сделать меню при нажатии правой кнопки на закладке для открытия ссылки в браузере.
			 * Импорт из оперы и IE
			 * Перенести чтение настроек в БД одним запросом?
			*/ 
		}
	}
}
