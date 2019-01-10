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
			string Birthdate    = "Birthdate     TEXT NOT NULL";
			string Nickname     = "Nickname      TEXT NOT NULL";
			string Contacts     = "Contacts      TEXT NOT NULL";
			string Sex          = "Sex           INTEGER NOT NULL DEFAULT 0";		// 0 - not defined, 1 - male, 2 - female
			string Season       = "Season        INTEGER NOT NULL DEFAULT 0";
			string Episode      = "Episode       INTEGER NOT NULL DEFAULT 0";
			string Description  = "Description   TEXT NOT NULL";

		
			string command = string.Format("CREATE TABLE IF NOT EXISTS Films ({0}, {1}, {2}, {3}, {4});", 
				Id, Name, CurrentState, Comment, Year);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS AnimeFilms ({0}, {1}, {2}, {3}, {4});", 
				Id, Name, CurrentState, Comment, Year);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Performances ({0}, {1}, {2}, {3}, {4});", 
				Id, Name, CurrentState, Comment, Year);
			ExecuteNonQuery(command);
			
			command = string.Format("CREATE TABLE IF NOT EXISTS Literature ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12});", 
				Id, Name, CurrentState, Comment, Year, Author, Genre, Universe, Series, Volume, Chapter, Page, Pages);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Bookmarks ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7});", 
				Id, Name, CurrentState, Comment, URL, Login, Password, Email);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Meal ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, CurrentState, Comment, Ingredients, Recipe);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Programs ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8});", 
				Id, Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email);
			ExecuteNonQuery(command);

			command = string.Format("CREATE TABLE IF NOT EXISTS Games ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9});", 
				Id, Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email, Genre);
			ExecuteNonQuery(command);
			
			command = string.Format("CREATE TABLE IF NOT EXISTS People ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8});", 
				Id, Name, CurrentState, Comment, Address, Birthdate, Nickname, Contacts, Sex);
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

			command = string.Format("CREATE TABLE IF NOT EXISTS Desires ({0}, {1}, {2}, {3}, {4});",
				Id, Name, CurrentState, Comment, Description);
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

				case "Bookmarks":    return InsertOrUpdateBookmark(tableName, note);

				case "Meal":         return InsertOrUpdateMeal(tableName, note);

				case "Programs":     return InsertOrUpdateProgram(tableName, note);

				case "Games":        return InsertOrUpdateGame(tableName, note);

				case "People":       return InsertOrUpdatePerson(tableName, note);

				case "Serials":      return InsertOrUpdateSerial(tableName, note);
				case "AnimeSerials": return InsertOrUpdateSerial(tableName, note);
				case "TVShows":      return InsertOrUpdateSerial(tableName, note);

				case "Desires":      return InsertOrUpdateDesires(tableName, note);

				default: return 0;
			}
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
				"INSERT INTO {0} (Id, Name, CurrentState, Comment, Year) " + 
				"VALUES ({1}, \"{2}\", {3}, \"{4}\", {5}) " + 
				"ON CONFLICT(Id) DO UPDATE SET Name = \"{2}\", CurrentState = {3}, Comment = \"{4}\", Year = {5};", 
				tableName, note.Id, datedNote.Name, (int)datedNote.CurrentState, datedNote.Comment, datedNote.Year);

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
			// Id, Name, CurrentState, Comment, Year, Author, Genre, Universe, Series, Volume, Chapter, Page, Pages);
			string commandText = string.Format(
				"INSERT INTO {0} (Id, Name, CurrentState, Comment, Year, Author, Genre, Universe, Series, Volume, Chapter, Page, Pages) " + 
				"VALUES ({1}, \"{2}\", {3}, \"{4}\", {5}, \"{6}\", \"{7}\", \"{8}\", \"{9}\", {10}, {11}, {12}, {13}) " + 
				"ON CONFLICT(Id) DO UPDATE " +
				"SET Name = \"{2}\", CurrentState = {3}, Comment = \"{4}\", Year = {5}, Author = \"{6}\", " + 
				"Genre = \"{7}\", Universe = \"{8}\", Series = \"{9}\", Volume = {10}, Chapter = {11}, Page = {12}, Pages = {13};", 
				tableName, note.Id, lit.Name, (int)lit.CurrentState, lit.Comment, lit.Year, lit.Author,
				lit.Genre, lit.Universe, lit.Series, lit.Volume, lit.Chapter, lit.Page, lit.Pages);

			return ExecuteNonQuery(commandText);
		}


		private static int InsertOrUpdateBookmark(string tableName, Note note)
		{
			Bookmark b = note as Bookmark;
			if (b == null)
			{
				Log.Error("Try to save incorrect bookmark note");
				return 0;
			}

			note.Id = (note.Id >= 0) ? note.Id : (SelectMaxId(tableName) + 1);

			string commandText = string.Format(
				"INSERT INTO {0} (Id, Name, CurrentState, Comment, URL, Login, Password, Email) " + 
				"VALUES ({1}, \"{2}\", {3}, \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\") " + 
				"ON CONFLICT(Id) DO UPDATE SET Name = \"{2}\", CurrentState = {3}, Comment = \"{4}\", " +
				"URL = \"{5}\", Login = \"{6}\", Password = \"{7}\", Email = \"{8}\";", 
				tableName, note.Id, b.Name, (int)b.CurrentState, b.Comment, b.URL, b.Login, b.Password, b.Email);

			return ExecuteNonQuery(commandText);
		}


		private static int InsertOrUpdateMeal(string tableName, Note note)
		{
			Meal meal = note as Meal;
			if (meal == null)
			{
				Log.Error("Try to save incorrect meal note");
				return 0;
			}

			note.Id = (note.Id >= 0) ? note.Id : (SelectMaxId(tableName) + 1);

			string commandText = string.Format(
				"INSERT INTO {0} (Id, Name, CurrentState, Comment, Ingredients, Recipe) " + 
				"VALUES ({1}, \"{2}\", {3}, \"{4}\", \"{5}\", \"{6}\") " + 
				"ON CONFLICT(Id) DO UPDATE SET Name = \"{2}\", CurrentState = {3}, Comment = \"{4}\", Ingredients = \"{5}\", Recipe = \"{6}\";",
				tableName, note.Id, meal.Name, (int)meal.CurrentState, meal.Comment, meal.Ingredients, meal.Recipe);

			return ExecuteNonQuery(commandText);
		}


		private static int InsertOrUpdateProgram(string tableName, Note note)
		{
			Program program = note as Program;
			if (program == null)
			{
				Log.Error("Try to save incorrect program note");
				return 0;
			}

			note.Id = (note.Id >= 0) ? note.Id : (SelectMaxId(tableName) + 1);

			string commandText = string.Format(
				"INSERT INTO {0} (Id, Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email) " + 
				"VALUES ({1}, \"{2}\", {3}, \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\", \"{9}\") " + 
				"ON CONFLICT(Id) DO UPDATE SET Name = \"{2}\", CurrentState = {3}, Comment = \"{4}\", " +
				"DownloadLink = \"{5}\", Version = \"{6}\", Login = \"{7}\", Password = \"{8}\", Email = \"{9}\";", 
				tableName, note.Id, program.Name, (int)program.CurrentState, program.Comment, program.DownloadLink, 
				program.Version, program.Login, program.Password, program.Email);

			return ExecuteNonQuery(commandText);
		}


		private static int InsertOrUpdateGame(string tableName, Note note)
		{
			Game game = note as Game;
			if (game == null)
			{
				Log.Error("Try to save incorrect program note");
				return 0;
			}

			note.Id = (note.Id >= 0) ? note.Id : (SelectMaxId(tableName) + 1);

			string commandText = string.Format(
				"INSERT INTO {0} (Id, Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email, Genre) " + 
				"VALUES ({1}, \"{2}\", {3}, \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\", \"{9}\", \"{10}\") " + 
				"ON CONFLICT(Id) DO UPDATE SET Name = \"{2}\", CurrentState = {3}, Comment = \"{4}\", " +
				"DownloadLink = \"{5}\", Version = \"{6}\", Login = \"{7}\", Password = \"{8}\", Email = \"{9}\", Genre = \"{10}\";", 
				tableName, note.Id, game.Name, (int)game.CurrentState, game.Comment, game.DownloadLink, 
				game.Version, game.Login, game.Password, game.Email, game.Genre);

			return ExecuteNonQuery(commandText);
		}


		private static int InsertOrUpdatePerson(string tableName, Note note)
		{
			Person person = note as Person;
			if (person == null)
			{
				Log.Error("Try to save incorrect person");
				return 0;
			}			

			note.Id = (note.Id >= 0) ? note.Id : (SelectMaxId(tableName) + 1);

			string commandText = string.Format(
				"INSERT INTO {0} (Id, Name, CurrentState, Comment, Address, Birthdate, Nickname, Contacts, Sex) " + 
				"VALUES ({1}, \"{2}\", {3}, \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\", {9}) " + 
				"ON CONFLICT(Id) DO UPDATE SET Name = \"{2}\", CurrentState = {3}, Comment = \"{4}\", Address = \"{5}\", " +
				"Birthdate = \"{6}\", Nickname = \"{7}\", Contacts = \"{8}\", Sex = {9};", 
				tableName, note.Id, person.Name, (int)person.CurrentState, person.Comment, person.Address, 
				person.Birthdate, person.Nickname, person.Contacts, (int)person.Sex);

			return ExecuteNonQuery(commandText);
		}


		private static int InsertOrUpdateSerial(string tableName, Note note)
		{
			Serial serial = note as Serial;
			if (serial == null)
			{
				Log.Error("Try to save incorrect serial note");
				return 0;
			}

			note.Id = (note.Id >= 0) ? note.Id : (SelectMaxId(tableName) + 1);

			string commandText = string.Format(
				"INSERT INTO {0} (Id, Name, CurrentState, Comment, Season, Episode) " + 
				"VALUES ({1}, \"{2}\", {3}, \"{4}\", {5}, {6}) " + 
				"ON CONFLICT(Id) DO UPDATE SET Name = \"{2}\", CurrentState = {3}, Comment = \"{4}\", Season = {5}, Episode = {6};", 
				tableName, note.Id, serial.Name, (int)serial.CurrentState, serial.Comment, serial.Season, serial.Episode);

			return ExecuteNonQuery(commandText);
		}


		private static int InsertOrUpdateDesires(string tableName, Note note)
		{
			Desire desire = note as Desire;
			if (desire == null)
			{
				Log.Error("Try to save incorrect desire note");
				return 0;
			}

			note.Id = (note.Id >= 0) ? note.Id : (SelectMaxId(tableName) + 1);

			string commandText = string.Format(
				"INSERT INTO {0} (Id, Name, CurrentState, Comment, Description) " +
				"VALUES ({1}, \"{2}\", {3}, \"{4}\", \"{5}\") " +
				"ON CONFLICT(Id) DO UPDATE SET Name = \"{2}\", CurrentState = {3}, Comment = \"{4}\", Description = \"{5}\";",
				tableName, note.Id, desire.Name, (int)desire.CurrentState, desire.Comment, desire.Description);

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
				// Предполагаю, исключение может возникать при попытке добавить новую запись в таблицу. Когда еще нет никаких max id.
				// Можно игнорировать.
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
								while (reader.Read())
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


		public static SQLiteConnection CreateConnection(string fileName = null)
		{
			if (fileName == null)
				return new SQLiteConnection(string.Format("Data source=\"{0}\"; Version={1}", Database.FileName, 3));
			else
				return new SQLiteConnection(string.Format("Data source=\"{0}\"; Version={1}", fileName, 3));
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
			string commandText = string.Format("SELECT * FROM {0} ORDER BY Name", tableName);
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

									case "Games":        notes = ReadGames(reader); break;

									case "People":       notes = ReadPeople(reader); break;

									case "Serials":      notes = ReadSerials(reader); break;
									case "AnimeSerials": notes = ReadSerials(reader); break;
									case "TVShows":      notes = ReadSerials(reader); break;

									case "Desires":      notes = ReadDesires(reader); break;

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
					datedNote.CurrentState = (Note.State)reader.GetInt32(2);
					datedNote.Comment = reader.GetString(3);
					datedNote.Year = reader.GetInt32(4);

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
			try
			{
				List<Note> notes = new List<Note>();

				while (reader.Read())
				{
					Program program = new Program();

					program.Id = reader.GetInt32(0);
					program.Name = reader.GetString(1);
					program.CurrentState = (Note.State)reader.GetInt32(2);
					program.Comment = reader.GetString(3);
					program.DownloadLink = reader.GetString(4);
					program.Version = reader.GetString(5);
					program.Login = reader.GetString(6);
					program.Password = reader.GetString(7);
					program.Email = reader.GetString(8);

					notes.Add(program);
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not read program: {0}{1}",
					Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
		}


		private static List<Note> ReadGames(SQLiteDataReader reader)
		{
			try
			{
				List<Note> notes = new List<Note>();

				while (reader.Read())
				{
					Game game = new Game();

					game.Id = reader.GetInt32(0);
					game.Name = reader.GetString(1);
					game.CurrentState = (Note.State)reader.GetInt32(2);
					game.Comment = reader.GetString(3);
					game.DownloadLink = reader.GetString(4);
					game.Version = reader.GetString(5);
					game.Login = reader.GetString(6);
					game.Password = reader.GetString(7);
					game.Email = reader.GetString(8);
					game.Genre = reader.GetString(9);

					notes.Add(game);
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not read game: {0}{1}",
					Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
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
					literature.CurrentState = (Note.State)reader.GetInt32(2);
					literature.Comment = reader.GetString(3);
					literature.Year = reader.GetInt32(4);
					literature.Author = reader.GetString(5);
					literature.Genre = reader.GetString(6);
					literature.Universe = reader.GetString(7);
					literature.Series = reader.GetString(8);
					literature.Volume = reader.GetInt32(9);
					literature.Chapter = reader.GetInt32(10);
					literature.Page = reader.GetInt32(11);
					literature.Pages = reader.GetInt32(12);				

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
			try
			{
				List<Note> notes = new List<Note>();

				while (reader.Read())
				{
					Bookmark bookmark = new Bookmark();

					bookmark.Id = reader.GetInt32(0);
					bookmark.Name = reader.GetString(1);
					bookmark.CurrentState = (Note.State)reader.GetInt32(2);
					bookmark.Comment = reader.GetString(3);
					bookmark.URL = reader.GetString(4);
					bookmark.Login = reader.GetString(5);
					bookmark.Password = reader.GetString(6);
					bookmark.Email = reader.GetString(7);				

					notes.Add(bookmark);
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not read bookmark: {0}{1}",
					Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
		}


		private static List<Note> ReadMeal(SQLiteDataReader reader)
		{
			try
			{
				List<Note> notes = new List<Note>();

				while (reader.Read())
				{
					Meal meal = new Meal();

					meal.Id = reader.GetInt32(0);
					meal.Name = reader.GetString(1);
					meal.CurrentState = (Note.State)reader.GetInt32(2);
					meal.Comment = reader.GetString(3);
					meal.Ingredients = reader.GetString(4);
					meal.Recipe = reader.GetString(5);		

					notes.Add(meal);
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not read meal: {0}{1}",
					Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
		}


		private static List<Note> ReadPeople(SQLiteDataReader reader)
		{
			try
			{
				List<Note> notes = new List<Note>();

				while (reader.Read())
				{
					Person person = new Person();

					person.Id = reader.GetInt32(0);
					person.Name = reader.GetString(1);					
					person.CurrentState = (Note.State)reader.GetInt32(2);
					person.Comment = reader.GetString(3);
					person.Address = reader.GetString(4);
					person.Birthdate = reader.GetString(5);
					person.Nickname = reader.GetString(6);
					person.Contacts = reader.GetString(7);
					person.Sex = (Person.PSex)reader.GetInt32(8);

					notes.Add(person);
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not read people: {0}{1}",
					Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
		}

		private static List<Note> ReadSerials(SQLiteDataReader reader)
		{
			try
			{
				List<Note> notes = new List<Note>();

				while (reader.Read())
				{
					Serial serial = new Serial();

					serial.Id = reader.GetInt32(0);
					serial.Name = reader.GetString(1);
					serial.CurrentState = (Note.State)reader.GetInt32(2);
					serial.Comment = reader.GetString(3);
					serial.Season = reader.GetInt32(4);
					serial.Episode = reader.GetInt32(5);					

					notes.Add(serial);
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not read serial notes: {0}{1}",
					Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
		}


		private static List<Note> ReadDesires(SQLiteDataReader reader)
		{
			try
			{
				List<Note> notes = new List<Note>();

				while (reader.Read())
				{
					Desire desire = new Desire();

					desire.Id = reader.GetInt32(0);
					desire.Name = reader.GetString(1);
					desire.CurrentState = (Note.State)reader.GetInt32(2);
					desire.Comment = reader.GetString(3);
					desire.Description = reader.GetString(4);

					notes.Add(desire);
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not read desire notes: {0}{1}",
					Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
		}


		public static void Export(string filePath, string tableName)
		{
			Dictionary<int, Note.State> oldStates = new Dictionary<int, Note.State>();
			oldStates.Add(1, Note.State.Active);
			oldStates.Add(4, Note.State.Deleted);
			oldStates.Add(2, Note.State.Finished);
			oldStates.Add(3, Note.State.Postponed);
			oldStates.Add(0, Note.State.Waiting);

			switch (tableName)
			{
				case "anime":
				{
					foreach (Note note in SelectFromOldTable(filePath, "anime", oldStates))
						InsertOrUpdateSerial("AnimeSerials", note);
					break;
				}
				case "serials":
				{
					foreach (Note note in SelectFromOldTable(filePath, "serials", oldStates))
						InsertOrUpdateSerial("Serials", note);
					break;
				}
				case "books":
				{
					foreach (Note note in SelectFromOldTable(filePath, "books", oldStates))
						InsertOrUpdateLiterature("Literature", note);
					break;
				}
				case "films":
				{
					foreach (Note note in SelectFromOldTable(filePath, "films", oldStates))
						InsertOrUpdateDatedNote("Films", note);
					break;
				}
			}		
		}


		private static List<Note> SelectFromOldTable(string filePath, string tableName, Dictionary<int, Note.State> oldStates)
		{
			List<Note> notes = new List<Note>();
			
			try
			{
				string commandText = string.Format("SELECT * FROM {0}", tableName);			
				using (SQLiteCommand selectCommand = new SQLiteCommand(commandText))
				{
					using (SQLiteConnection connection = Database.CreateConnection(filePath))
					{
						connection.Open();
						selectCommand.Connection = connection;

						if (connection.State == System.Data.ConnectionState.Open)
						{
							using (SQLiteDataReader reader = selectCommand.ExecuteReader())
							{
								switch (tableName)
								{
									case "anime": return ReadOldSerials(reader, oldStates);
									case "serials": return ReadOldSerials(reader, oldStates);
									case "books": return ReadOldLiterature(reader, oldStates);
									case "films": return ReadOldFilms(reader, oldStates);
								}								
							}
						}

						connection.Close();
					}
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not export anime:{0}{1}", Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
		}


		private static List<Note> ReadOldSerials(SQLiteDataReader reader, Dictionary<int, Note.State> oldStates)
		{
			List<Note> notes = new List<Note>();

			while (reader.Read())
			{
				Serial serial = new Serial();

				serial.Name = reader.GetString(1);
				serial.Season = reader.GetInt32(2);
				serial.Episode = reader.GetInt32(3);
				serial.CurrentState = oldStates[reader.GetInt32(4)];
				serial.Comment = reader.GetString(5);

				notes.Add(serial);
			}

			return notes;
		}


		private static List<Note> ReadOldLiterature(SQLiteDataReader reader, Dictionary<int, Note.State> oldStates)
		{
			List<Note> notes = new List<Note>();

			while (reader.Read())
			{
				Literature literature = new Literature();

				literature.Name = reader.GetString(1);
				literature.Year = reader.GetInt32(2);
				literature.Author = reader.GetString(3);
				literature.Series = reader.GetString(4);
				literature.CurrentState = oldStates[reader.GetInt32(5)];
				literature.Comment = reader.GetString(6);

				notes.Add(literature);
			}

			return notes;
		}


		private static List<Note> ReadOldFilms(SQLiteDataReader reader, Dictionary<int, Note.State> oldStates)
		{
			List<Note> notes = new List<Note>();

			while (reader.Read())
			{
				DatedNote film = new DatedNote();

				film.Name = reader.GetString(1);
				film.Year = reader.GetInt32(2);
				film.CurrentState = oldStates[reader.GetInt32(3)];
				film.Comment = reader.GetString(4);

				notes.Add(film);
			}

			return notes;
		}
	}
}
