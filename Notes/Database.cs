using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;


namespace Notes
{
	public class Database
	{
		public static string FileName = "notes.sqlite";


		public static void Create()
		{
			if (!File.Exists(Database.FileName))
				SQLiteConnection.CreateFile(Database.FileName);

			// Многие поля дублируются, поэтому объявление перенесено сюда.
			string Id           = "Id            INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL";
			string Name         = "Name          TEXT UNIQUE NOT NULL";
			string Year         = "Year		     INTEGER NOT NULL DEFAULT 0";
			string CurrentState = "CurrentState  INTEGER NOT NULL DEFAULT 0";	// Not defined/Active/Deleted/Finished/Postponed/Waiting
			string Comment      = "Comment       TEXT NOT NULL";
			string Version      = "Version       TEXT NOT NULL";
			string Author       = "Author        TEXT NOT NULL";
			string Universe     = "Universe      TEXT NOT NULL";
			string Series       = "Series        TEXT NOT NULL";
			string Genre        = "Genre         TEXT NOT NULL";
			string PagesCount   = "PagesCount    INTEGER NOT NULL DEFAULT 0";
			string CurrentPage  = "CurrentPage   INTEGER NOT NULL DEFAULT 0";
			string Volume       = "Volume        INTEGER NOT NULL DEFAULT 0";
			string Chapter      = "Chapter       INTEGER NOT NULL DEFAULT 0";
			string URL          = "URL           TEXT NOT NULL";
			string Login        = "Login         TEXT NOT NULL";
			string Password     = "Password      TEXT NOT NULL";
			string Email        = "Email         TEXT NOT NULL";
			string Ingredients  = "Ingredients   TEXT NOT NULL";
			string Recipe       = "Recipe        TEXT NOT NULL";
			string DownloadLink = "DownloadLink  TEXT NOT NULL";
			string Address      = "Address       TEXT NOT NULL";
			string Birthday     = "Birthday      DATETIME NOT NULL";
			string Nickname     = "Nickname      TEXT NOT NULL";
			string WebPages     = "WebPages      TEXT NOT NULL";					// List of web pages, separated by ';'
			string Contacts     = "Contacts      TEXT NOT NULL";					// List of contacts (skype, etc), separated by ';'
			string Sex          = "Sex           INTEGER NOT NULL DEFAULT 0";		// 0 - not defined, 1 - male, 2 - female
			string Season       = "Season        INTEGER NOT NULL DEFAULT 0";
			string Episode      = "Episode       INTEGER NOT NULL DEFAULT 0";


			string command = string.Format("CREATE TABLE IF NOT EXISTS Films ({0}, {1}, {2}, {3}, {4});", 
				Id, Name, Year, CurrentState, Comment);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS AnimeFilms ({0}, {1}, {2}, {3}, {4});", 
				Id, Name, Year, CurrentState, Comment);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Performances ({0}, {1}, {2}, {3}, {4});", 
				Id, Name, Year, CurrentState, Comment);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Games ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, Year, CurrentState, Comment, Version);
			ExecuteNonQuery(command);
			
			command = string.Format("CREATE TABLE IF NOT EXISTS Literature ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12});", 
				Id, Name, Year, CurrentState, Comment, Author, Universe, Series, Genre, PagesCount,	CurrentPage, Volume, Chapter);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Bookmarks ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7});", 
				Id, Name, CurrentState, Comment, URL, Login, Password, Email);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Meal ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, CurrentState, Comment, Ingredients, Recipe);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Programs ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, CurrentState, Comment, DownloadLink, Version);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS People ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9});", 
				Id, Name, CurrentState, Comment, Address, Birthday, Nickname, WebPages, Contacts, Sex);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Serials ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, CurrentState, Comment, Season, Episode);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS AnimeSerials ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, CurrentState, Comment, Season, Episode);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS TVShows ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, CurrentState, Comment, Season, Episode);
			ExecuteNonQuery(command);
		}


		public static SQLiteConnection CreateConnection()
		{
			return new SQLiteConnection(string.Format("Data source={0}; Version={1}", Database.FileName, 3));
		}


		/// <summary>
		/// Подключается к БД и выполняет заданную команду (INSERT, UPDATE, DELETE).
		/// </summary>
		private static int ExecuteNonQuery(string commandText)
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(commandText))
				{
					using (SQLiteConnection connection = Database.CreateConnection())
					{
						if (command == null || command.CommandText.Length == 0)
							return 0;

						int affectedRows = -1;

						connection.Open();

						command.Connection = connection;

						if (connection.State == System.Data.ConnectionState.Open)
							affectedRows = command.ExecuteNonQuery();
						
						connection.Close();

						return affectedRows;
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", Environment.NewLine, commandText, Environment.NewLine,	ex.ToString()));
				return 0;
			}
		}


		///// <summary>
		///// Подключается к БД и считывает заметки. Результат никогда не равен null.
		///// </summary>
		//private List<Note> ExecuteReader(SQLiteCommand selectCommand)
		//{
		//	try
		//	{
		//		using (selectCommand)
		//		{
		//			using (SQLiteConnection connection = Database.CreateConnection())
		//			{
		//				if (selectCommand == null || selectCommand.CommandText.Length == 0)
		//					return new List<Note>();

		//				List<Note> notes = new List<Note>();

		//				connection.Open();

		//				selectCommand.Connection = connection;

		//				if (connection.State == System.Data.ConnectionState.Open)
		//				{
		//					using (SQLiteDataReader reader = selectCommand.ExecuteReader())
		//					{
		//						notes = ReadNotes(reader);
		//					}
		//				}

		//				connection.Close();

		//				return (notes == null) ? new List<Note>() : notes;
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Log.Error(string.Format("Can not execute select command: {0}{1}{2}{3}", 
		//			Environment.NewLine, selectCommand.CommandText, Environment.NewLine, ex.ToString()));

		//		return new List<Note>();
		//	}			
		//}
	}
}
