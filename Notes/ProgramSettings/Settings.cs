using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Notes.ProgramSettings
{
	public class Settings
	{
		private static string _initialNotesTable;		
		private static string _initialNotesState;
		private static string _databasePassword;
		private static string _backupEmail;
		private static string _backupPassword;


		static Settings()
		{
			_initialNotesTable = string.Empty;
			_initialNotesState = string.Empty;
			_databasePassword = string.Empty;
			_backupEmail = string.Empty;
			_backupPassword = string.Empty;
		}


		public static string InitialNotesTable
		{
			get { return _initialNotesTable; }
			set
			{
				value = value.Trim();
				if (value != _initialNotesTable && value.Length > 0)
				{
					_initialNotesTable = value;
					Database.SaveSetting("InitialNotesTable", value);
				}
			}
		}
		
		public static string InitialNotesState
		{
			get { return _initialNotesState; }
			set
			{
				value = value.Trim();
				if (value != _initialNotesState && value.Length > 0)
				{
					_initialNotesState = value;
					Database.SaveSetting("InitialNotesState", value);
				}
			}
		}

		public static string DatabasePassword
		{
			get { return _databasePassword; }
		}

		public static string BackupEmail
		{
			get { return _backupEmail; }
			set
			{
				value = value.Trim();
				if (value != _backupEmail && value.Length > 0)
				{
					_backupEmail = value;
					Database.SaveSetting("BackupEmail", value);
				}
			}
		}

		public static string BackupPassword
		{
			get { return _backupPassword; }
			set
			{
				value = value.Trim();
				if (value != _backupPassword && value.Length > 0)
				{
					_backupPassword = value;
					Database.SaveSetting("BackupPassword", value);
				}
			}
		}


		public static void LoadFromDatabase()
		{
			_initialNotesTable = Database.ReadSetting("InitialNotesTable");
			_initialNotesState = Database.ReadSetting("InitialNotesState");
			_backupEmail = Database.ReadSetting("BackupEmail");
			_backupPassword = Database.ReadSetting("BackupPassword");
		}


		public static void SetCurrentPassword(string password)
		{
			_databasePassword = password;
		}


		public static void SetNewPassword(string password)
		{
			// В пароле не должно быть пробельных символов.
			password = Regex.Replace(password, @"\s+", "");

			// Для пароля значение может быть и пустое, но тогда будет просто его сброс. Пока что это не нужно.
			if (password != _databasePassword && password.Length > 0)
			{
				// Смена пароля в Settings только после смены в БД, т.к. нет возможности подключиться с новым паролем до его установки.
				Database.ChangePassword(password);
				_databasePassword = password;
			}
		}
	}
}
