using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Notes.ProgramSettings;
using Notes.DB;

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

			Settings settings = new Settings();
			Database.SetSettings(settings);

			Database.CheckPassword();
			if (Database.IsPasswordProtected)
			{
				PasswordForm form = new PasswordForm(settings);
				form.ShowDialog();

				if (!Database.PasswordIsOk())
					return;
			}
			
			Database.Create();
			Database.ReadSetting();

			Application.Run(new MainForm(settings));
			
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
