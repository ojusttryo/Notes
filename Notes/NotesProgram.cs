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
			 * 
			 * Если будет желание, можно переделать Bookmarks и Desires. Сделать их в виде дерева (как в браузере).
			 * https://www.codeproject.com/Articles/23746/TreeView-with-Columns
			 * https://www.codeproject.com/Articles/3273/ContainerListView-and-TreeListView-Writing-VS-NET
			 * https://sourceforge.net/projects/treeviewadv/
			 * Так можно будет создавать подкатегории. К примеру, здоровье, спорт, программы.
			 * 
			 * Также неплохо было б избавиться от лишних библиотек, если найдутся нормальные стандартные средства для парсинга json и html.
			 * 
			 * Добавить новую настройку - что отображать для состояния All. Выбирать через чекбоксы нужные состояния. 
			 * Удобно, к примеру, для сокрытия удаленных.
			 * 
			 * К Desires добавить приоритеты, стоимость. В иерархическом виде (если сделать в виде дерева)
			 * будет удобно смотреть, насколько что дорого. Например, велосипед со всеми аксессуарами.
			 * 
			 * Добавить Problems. Типа какие проблемы у меня висят и что нужно для их решения.
			 * 
			 * Добавить к фильмам и книгам мои оценки, чтоб советовать.
			 * 
			 * Добавить Minds. Мысли, идеи, лайфхаки и прочее. Можно назвать типа Useful или еще как.
			 * 
			 * Сделать ссылки на Bookmarks. Типа {ref=id}. 
			 * Можно через "Правая кнопка - Вставить ссылку - Выбор ссылки из сохраненных".
			 * Можно сделать проверку текста после сохранения. Если в тексте есть корректные ссылки, искать их среди Bookmarks
			 * и выделять синим с возможностью клика.
			 * 
			 * Переработать регулярные дела. Или убрать. На ПК ими пользоваться не удобно. Нужна или синхронизация с телефоном или другой формат.
			*/ 
		}
	}
}
