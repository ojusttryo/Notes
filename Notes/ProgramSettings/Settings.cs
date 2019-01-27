using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Notes.DB;
using Notes.NoteTables;
using static Notes.Info;

namespace Notes.ProgramSettings
{
	public class Settings
	{
		private string _defaultNotesTable;		
		private string _databasePassword;
		private string _backupEmail;
		private string _backupPassword;

		private Dictionary<string, string> _defaultNotesStates;

		public Settings()
		{
			_defaultNotesTable = GetTableNamesDB().First();
			_databasePassword = string.Empty;
			_backupEmail = string.Empty;
			_backupPassword = string.Empty;
			_defaultNotesStates = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
		}


		public string DefaultNotesTable
		{
			get { return _defaultNotesTable; }
			set
			{
				value = value.Trim();
				if (value != _defaultNotesTable && value.Length > 0)
				{
					_defaultNotesTable = value;
					Database.SaveSetting("DefaultNotesTable", value);
				}
			}
		}


		public string DatabasePassword
		{
			get { return _databasePassword; }
		}


		public string BackupEmail
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


		public string BackupPassword
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


		public void InitializeFromDatabase(string defaultNotesTable, string backupEmail, string backupPassword, Dictionary<string, string> defaultStates)
		{
			_defaultNotesTable = defaultNotesTable != "" ? defaultNotesTable : _defaultNotesTable;
			_backupEmail = backupEmail;
			_backupPassword = backupPassword;
			_defaultNotesStates = defaultStates;
		}


		/// <summary>
		/// Set password without saving to database. Use to set password for current connection.
		/// </summary>
		/// <param name="password">Current password.</param>
		public void SetCurrentPassword(string password)
		{
			_databasePassword = password;
		}


		/// <summary>
		/// Set new password, that will be saved to database.
		/// </summary>
		/// <param name="password">New password</param>
		public void SetNewPassword(string password)
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


		public void RemovePasswordProtection()
		{
			Database.ChangePassword("");
			_databasePassword = "";
		}


		public void SetDefaultState(Dictionary<string, string> defaultStates)
		{
			foreach (KeyValuePair<string, string> pair in defaultStates)
			{
				// Состояние всегда должно быть одним из элементов States.
				if (string.IsNullOrEmpty(pair.Value) || States.All(x => x != pair.Value))
					continue;

				if (!_defaultNotesStates.ContainsKey(pair.Key))
					_defaultNotesStates.Add(pair.Key, pair.Value);

				_defaultNotesStates[pair.Key] = pair.Value;
			}

			Database.SaveDefaultStates(_defaultNotesStates);
		}


		public string GetDefaultState(string tableNameInDB)
		{
			return _defaultNotesStates.ContainsKey(tableNameInDB) ? _defaultNotesStates[tableNameInDB] : States.First();			
		}
	}
}
