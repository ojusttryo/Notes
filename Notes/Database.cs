using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

using Notes.Notes;

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
			string Name         = "Name          TEXT NOT NULL";
			string Year         = "Year		     INTEGER NOT NULL DEFAULT 0";
			string CurrentState = "CurrentState  INTEGER NOT NULL DEFAULT 0";	// Not defined/Active/Deleted/Finished/Postponed/Waiting
			string Comment      = "Comment       TEXT NOT NULL";
			string Version      = "Version       TEXT NOT NULL";
			string Author       = "Author        TEXT NOT NULL";
			string Universe     = "Universe      TEXT NOT NULL";
			string Series       = "Series        TEXT NOT NULL";
			string Genre        = "Genre         TEXT NOT NULL";
			string Pages        = "Pages         INTEGER NOT NULL DEFAULT 0";
			string Page         = "Page          INTEGER NOT NULL DEFAULT 0";
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
			
			command = string.Format("CREATE TABLE IF NOT EXISTS Literature ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12});", 
				Id, Name, Author, Genre, Universe, Series, Volume, Chapter, Page, Pages, Year, CurrentState, Comment);
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

			command = string.Format("CREATE TABLE IF NOT EXISTS Games ({0}, {1}, {2}, {3}, {4}, {5});", 
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


		/// <summary>
		/// Добавляет запись в таблицу. Если запись с таким note.Id уже существует, то обновляет данные в ней.
		/// </summary>
		public static int InsertOrUpdate(string tableName, Note note)
		{
			Log.Info(string.Format("Insert or update note with Id = {0} in {1}", note.Id, tableName));

			switch (tableName)
			{
				case "Films":        return InsertOrUpdateDatedNote(tableName, note);
				case "AnimeFilms":   return InsertOrUpdateDatedNote(tableName, note);
				case "Performances": return InsertOrUpdateDatedNote(tableName, note);
																			
				case "Literature":   return InsertOrUpdateLiterature(tableName, note);

				case "Bookmarks":    break;

				case "Meal":         break;

				case "Programs":     break;
				case "Games":        break;

				case "People":       break;

				case "Serials":      break;
				case "AnimeSerials": break;
				case "TVShows":      break;

				default: return 0;
			}

			return 0;
		}


		/// <summary>
		/// Вставляет новую заметку в БД или обновляет существующую (по Id). 
		/// В случае вставки выставляет для переданной заметки правильный Id. 
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="note"></param>
		/// <returns></returns>
		private static int InsertOrUpdateDatedNote(string tableName, Note note)
		{
			DatedNote datedNote = note as DatedNote;
			if (datedNote == null)
			{
				Log.Error("Try to save incorrect dated note");
				return 0;
			}

			note.Id = (note.Id >= 0) ? note.Id : (SelectMaxId(tableName) + 1);
			string commandText = string.Format(
				"INSERT INTO {0} (Id, Name, Year, CurrentState, Comment) " + 
				"VALUES ({1}, \"{2}\", {3}, {4}, \"{5}\") " + 
				"ON CONFLICT(Id) DO UPDATE SET Name = \"{2}\", Year = {3}, CurrentState = {4}, Comment = \"{5}\";", 
				tableName, note.Id, datedNote.Name, datedNote.Year, (int)datedNote.CurrentState, datedNote.Comment);

			return ExecuteNonQuery(commandText);
		}


		private static int InsertOrUpdateLiterature(string tableName, Note note)
		{
			Literature lit = note as Literature;
			if (lit == null)
			{
				Log.Error("Try to save incorrect literature note");
				return 0;
			}

			note.Id = (note.Id >= 0) ? note.Id : (SelectMaxId(tableName) + 1);
			string commandText = string.Format(
				"INSERT INTO {0} (Id,    Name,  Author,   Genre, Universe,  Series, Volume, Chapter, Page, Pages, Year, CurrentState,  Comment) " + 
				"VALUES         ({1}, \"{2}\", \"{3}\", \"{4}\",  \"{5}\", \"{6}\",    {7},     {8},  {9},  {10}, {11},         {12}, \"{13}\") " + 
				"ON CONFLICT(Id) DO UPDATE " + 
				"SET Name = \"{2}\", Author = \"{3}\", Genre = \"{4}\", Universe = \"{5}\", Series = \"{6}\", " + 
				"Volume = {7}, Chapter = {8}, Page = {9}, Pages = {10}, Year = {11}, CurrentState = {12}, Comment = \"{13}\";", 
				tableName, note.Id, lit.Name, lit.Author, lit.Genre, lit.Universe, lit.Series, 
				lit.Volume, lit.Chapter, lit.Page, lit.Pages, lit.Year, (int)lit.CurrentState, lit.Comment);

			return ExecuteNonQuery(commandText);
		}


		public static bool DeleteNote(string tableName, int id)
		{
			string commandText = string.Format("DELETE FROM {0} WHERE Id = {1}", tableName, id);
			int deletedRows = ExecuteNonQuery(commandText);

			return (deletedRows == 1);
		}


		// TODO: remove if not used
		public static void DeleteLastNote(string tableName)
		{
			string commandText = string.Format("DELETE FROM {0} ORDER BY Id DESC LIMIT 1");
			ExecuteNonQuery(commandText);
		}


		/// <summary>
		/// Получить максимальный Id в таблице.
		/// </summary>
		/// <param name="tableName">Имя таблицы.</param>
		/// <returns>Id записи или -1, если записей нет или произошла ошибка.</returns>
		private static int SelectMaxId(string tableName)
		{
			int maxId = -1;

			try
			{
				using (SQLiteCommand command = new SQLiteCommand(string.Format("SELECT MAX(Id) FROM {0}", tableName)))
				{
					using (SQLiteConnection connection = CreateConnection())
					{
						if (command == null)
							return maxId;

						connection.Open();
						command.Connection = connection;
						if (connection.State == System.Data.ConnectionState.Open)
						{
							using (SQLiteDataReader reader = command.ExecuteReader())
							{
								if (reader.Read())
									maxId = reader.GetInt32(0);
							}
						}

						connection.Close();
						command.Connection = null;
					}
				}

				return maxId;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not select max Id from: {0}{1}{2}", tableName, Environment.NewLine, ex.ToString()));
				return maxId;
			}
		}


		public static List<string> SelectUniqueValues(string tableName, string fieldName)
		{
			List<string> values = new List<string>();

			try
			{
				using (SQLiteCommand command = new SQLiteCommand(string.Format("SELECT DISTINCT {0} FROM {1}", fieldName, tableName)))
				{
					using (SQLiteConnection connection = CreateConnection())
					{
						if (command == null)
							return values;

						connection.Open();
						command.Connection = connection;
						if (connection.State == System.Data.ConnectionState.Open)
						{
							using (SQLiteDataReader reader = command.ExecuteReader())
							{
								if (reader.Read())
									values.Add(reader.GetString(0));
							}
						}

						connection.Close();
						command.Connection = null;
					}
				}

				return values;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not select unique {0} values from {1}: {2}{3}", fieldName, tableName, Environment.NewLine, ex.ToString()));
				return values;
			}
		}


		public static SQLiteConnection CreateConnection()
		{
			return new SQLiteConnection(string.Format("Data source={0}; Version={1}", Database.FileName, 3));
		}


		/// <summary>
		/// Подключается к БД и выполняет заданную команду (INSERT, UPDATE, DELETE).
		/// </summary>
		/// <param name="commandText">Текст команды.</param>
		/// <returns>Количество вставленных/обновленных строк.</returns>
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


		public static List<Note> GetNotes(string tableName)
		{
			string commandText = string.Format("SELECT * FROM {0}", tableName);
            SQLiteCommand command = new SQLiteCommand(commandText);
			List<Note> notes = ExecuteReader(command, tableName);

			return notes;
		}


		/// <summary>
		/// Подключается к БД и считывает заметки. Результат никогда не равен null.
		/// </summary>
		private static List<Note> ExecuteReader(SQLiteCommand selectCommand, string tableName)
		{
			try
			{
				using (selectCommand)
				{
					using (SQLiteConnection connection = Database.CreateConnection())
					{
						if (selectCommand == null || selectCommand.CommandText.Length == 0)
							return new List<Note>();

						List<Note> notes = new List<Note>();

						connection.Open();

						selectCommand.Connection = connection;

						if (connection.State == System.Data.ConnectionState.Open)
						{
							using (SQLiteDataReader reader = selectCommand.ExecuteReader())
							{
								switch (tableName)
								{
									case "Films":        notes = ReadDatedNotes(reader); break;
									case "AnimeFilms":   notes = ReadDatedNotes(reader); break;
									case "Performances": notes = ReadDatedNotes(reader); break;
																			
									case "Literature":   notes = ReadLiterature(reader); break;

									case "Bookmarks":    notes = ReadBookmarks(reader); break;

									case "Meal":         notes = ReadMeal(reader); break;

									case "Programs":     notes = ReadPrograms(reader); break;
									case "Games":        notes = ReadPrograms(reader); break;

									case "People":       notes = ReadPeople(reader); break;

									case "Serials":      notes = ReadSerials(reader); break;
									case "AnimeSerials": notes = ReadSerials(reader); break;
									case "TVShows":      notes = ReadSerials(reader); break;

									default: break;
								}
							}
						}

						connection.Close();

						return (notes == null) ? new List<Note>() : notes;
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute select command: {0}{1}{2}{3}",
					Environment.NewLine, selectCommand.CommandText, Environment.NewLine, ex.ToString()));

				return new List<Note>();
			}
		}


		private static List<Note> ReadDatedNotes(SQLiteDataReader reader)
		{
			try
			{
				List<Note> notes = new List<Note>();

				while (reader.Read())
				{
					DatedNote datedNote = new DatedNote();

					datedNote.Id = reader.GetInt32(0);
					datedNote.Name = reader.GetString(1);
					datedNote.Year = reader.GetInt32(2);
					datedNote.CurrentState = (Note.State)reader.GetInt32(3);
					datedNote.Comment = reader.GetString(4);					

					notes.Add(datedNote);
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not read dated notes: {0}{1}",
					Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
		}


		private static List<Note> ReadPrograms(SQLiteDataReader reader)
		{
			return null;
		}


		private static List<Note> ReadLiterature(SQLiteDataReader reader)
		{
			try
			{
				List<Note> notes = new List<Note>();

				while (reader.Read())
				{
					Literature literature = new Literature();

					literature.Id = reader.GetInt32(0);
					literature.Name = reader.GetString(1);
					literature.Author = reader.GetString(2);
					literature.Genre = reader.GetString(3);
					literature.Universe = reader.GetString(4);
					literature.Series = reader.GetString(5);
					literature.Volume = reader.GetInt32(6);
					literature.Chapter = reader.GetInt32(7);
					literature.Page = reader.GetInt32(8);
					literature.Pages = reader.GetInt32(9);
					literature.Year = reader.GetInt32(10);
					literature.CurrentState = (Note.State)reader.GetInt32(11);
					literature.Comment = reader.GetString(12);					

					notes.Add(literature);
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not read literature notes: {0}{1}",
					Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
		}


		private static List<Note> ReadBookmarks(SQLiteDataReader reader)
		{
			return null;
		}


		private static List<Note> ReadMeal(SQLiteDataReader reader)
		{
			return null;
		}


		private static List<Note> ReadPeople(SQLiteDataReader reader)
		{
			return null;
		}

		private static List<Note> ReadSerials(SQLiteDataReader reader)
		{
			return null;
		}
	}
}
