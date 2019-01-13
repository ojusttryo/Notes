using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
			Database.Create();
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
			
			/*
			 * TODO:
			 * Проверить PVS Studio.
			 * Окно для выбора импортируемых закладок (чекбокс, имя, ссылка) с кнопками "выбрать все", "отменить все", "инвертировать".
			 * Добавить пароль и шифрование некоторых полей. 
			 * Для закладок можно сделать меню при нажатии правой кнопки на закладке для открытия ссылки в браузере.
			 * Фото людей?
			 * Бэкап файла БД (например, сохранение в 7z архив и отправка на почту, введенную в настройках).
			 * Экспорт отображаемых заметок: в тхт файл с форматированием, в cvs формат для Excel
			 * Сохранение последней открытой вкладки
			 * Быстрые клавиши для вызова добавления, удаления и редактирования
			 * При наведении на строку с заметкой выводить подсказку со всеми полями? 
			*/ 
		}
	}
}
