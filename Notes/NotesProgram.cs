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
			 * Если будет желание, можно переделать закладки и желания. Сделать их в виде дерева (как в браузере).
			 * https://www.codeproject.com/Articles/23746/TreeView-with-Columns
			 * https://www.codeproject.com/Articles/3273/ContainerListView-and-TreeListView-Writing-VS-NET
			 * https://sourceforge.net/projects/treeviewadv/
			 * 
			 * TODO:
			 * Проверить PVS Studio.
			 * Импорт из оперы и IE
			*/ 
		}
	}
}
