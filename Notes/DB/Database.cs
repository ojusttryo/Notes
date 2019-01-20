using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

using Notes.ProgramSettings;

namespace Notes.DB
{
	public partial class Database
	{
		private static Settings _settings;

		public static string FileName = "notes.sqlite";

		public static bool IsPasswordProtected { get; private set; }


		public static void SetSettings(Settings settings)
		{
			_settings = settings;
		}


		/// <summary>
		/// Create connection to SQLite database. Filename specified only if you use database from old notes program.
		/// </summary>
		/// <returns>Prepared connection to SQLite database. Not opened.</returns>
		public static SQLiteConnection CreateConnection(string fileName = null)
		{
			if (fileName == null)
			{
				SQLiteConnection connection = new SQLiteConnection(string.Format("Data source=\"{0}\"; Version={1}", Database.FileName, 3));
				if (IsPasswordProtected)
					connection.SetPassword(_settings.DatabasePassword);
				return connection;
			}
			else
			{
				// Just for old database file. It has no password protection.
				return new SQLiteConnection(string.Format("Data source=\"{0}\"; Version={1}", fileName, 3));
			}
		}


		/// <summary>
		/// Подключается к БД и выполняет заданную команду (INSERT, UPDATE, DELETE).
		/// </summary>
		/// <param name="command">Команда, инициализированная текстом.</param>
		/// <returns>Количество вставленных/обновленных строк.</returns>
		private static int ExecuteNonQuery(SQLiteCommand command)
		{
			if (command == null || command.CommandText.Length == 0)
				return 0;

			try
			{
				using (SQLiteConnection connection = Database.CreateConnection())
				{
					int affectedRows = -1;
					
					connection.Open();
					command.Connection = connection;

					if (connection.State == System.Data.ConnectionState.Open)
						affectedRows = command.ExecuteNonQuery();
						
					connection.Close();
					command.Connection = null;

					return affectedRows;
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", 
					Environment.NewLine, (command != null) ? command.CommandText : "", 
					Environment.NewLine, ex.ToString()));
				return 0;
			}
		}


		public static bool DeleteNote(string tableName, int id)
		{
			SQLiteCommand command = new SQLiteCommand(string.Format("DELETE FROM {0} WHERE Id = {1}", tableName, id));
			int deletedRows = ExecuteNonQuery(command);

			return (deletedRows == 1);
		}
		

		/// <summary>
		/// Save one value from settings.
		/// </summary>
		public static void SaveSetting(string name, string value)
		{
			_settingsInsertOrUpdateCommand.Parameters[0].Value = name;
			_settingsInsertOrUpdateCommand.Parameters[1].Value = value;

			_settingsInsertOrUpdateCommand.Prepare();
				
			ExecuteNonQuery(_settingsInsertOrUpdateCommand);
		}


		/// <summary>
		/// Read all settings from database and set it to Settings.
		/// </summary>
		public static void ReadSetting()
		{
			try
			{
				using (SQLiteConnection con = CreateConnection())
				{
					using (SQLiteCommand command = new SQLiteCommand("SELECT Value FROM Settings WHERE Name = @Name;"))
					{
						con.Open();
						command.Connection = con;
						command.Parameters.Add("@Name", System.Data.DbType.String);
												
						command.Parameters[0].Value = "InitialNotesTable";
						command.Prepare();
						string initialNotesTable = ReadSingleValue(command);

						command.Parameters[0].Value = "InitialNotesState";
						command.Prepare();
						string initialNotesState = ReadSingleValue(command);

						command.Parameters[0].Value = "BackupEmail";
						command.Prepare();
						string backupEmail = ReadSingleValue(command);

						command.Parameters[0].Value = "BackupPassword";
						command.Prepare();
						string backupPassword = ReadSingleValue(command);

						command.Connection = null;

						_settings.InitializeFromDatabase(initialNotesTable, initialNotesState, backupEmail, backupPassword);
					}

					con.Close();
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Cannot read settings: {0}{1}", Environment.NewLine, ex.ToString()));
			}
		}


		private static string ReadSingleValue(SQLiteCommand command)
		{
			using (SQLiteDataReader reader = command.ExecuteReader())
			{
				return (reader.Read() && reader.FieldCount > 0) ? reader.GetString(0) : string.Empty;
			}
		}
	}
}
