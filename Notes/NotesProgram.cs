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

			// TODO check with PVS Studio
			// TODO import from my old sqlite database
			// TODO import bookmarks from browser export
			// TODO Переделать поиск для возможности работы с Email, Password, URL
			// TODO Придумать шифрование для пароля

			// TODO rename folders
		}
	}
}
