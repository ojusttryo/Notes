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

		private static SQLiteCommand _animeFilmsInsertCommand;
		private static SQLiteCommand _animeSerialsInsertCommand;
		private static SQLiteCommand _bookmarksInsertCommand;
		private static SQLiteCommand _desiresInsertCommand;
		private static SQLiteCommand _filmsInsertCommand;
		private static SQLiteCommand _gamesInsertCommand;
		private static SQLiteCommand _literatureInsertCommand;
		private static SQLiteCommand _mealInsertCommand;
		private static SQLiteCommand _perfomancesInsertCommand;
		private static SQLiteCommand _peopleInsertCommand;
		private static SQLiteCommand _programsInsertCommand;
		private static SQLiteCommand _serialsInsertCommand;
		private static SQLiteCommand _TVShowsInsertCommand;
		

		public static void Create()
		{
			CreateDatabase();
			CreateTables();
			CreateInsertCommands();
		}


		private static void CreateDatabase()
		{
			if (!File.Exists(Database.FileName))
				SQLiteConnection.CreateFile(Database.FileName);
		}


		private static void CreateTables()
		{
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

			SQLiteCommand command;

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS AnimeFilms ({0}, {1}, {2}, {3}, {4});", 
				Id, Name, CurrentState, Comment, Year));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS AnimeSerials ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, CurrentState, Comment, Season, Episode));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Bookmarks ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7});", 
				Id, Name, CurrentState, Comment, URL, Login, Password, Email));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Desires ({0}, {1}, {2}, {3}, {4});",
				Id, Name, CurrentState, Comment, Description));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Films ({0}, {1}, {2}, {3}, {4});", 
				Id, Name, CurrentState, Comment, Year));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Games ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9});", 
				Id, Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email, Genre));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Literature ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12});", 
				Id, Name, CurrentState, Comment, Year, Author, Genre, Universe, Series, Volume, Chapter, Page, Pages));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Meal ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, CurrentState, Comment, Ingredients, Recipe));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Performances ({0}, {1}, {2}, {3}, {4});", 
				Id, Name, CurrentState, Comment, Year));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS People ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8});", 
				Id, Name, CurrentState, Comment, Address, Birthdate, Nickname, Contacts, Sex));
			ExecuteNonQuery(command);
			
			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Programs ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8});", 
				Id, Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Serials ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, CurrentState, Comment, Season, Episode));
			ExecuteNonQuery(command);

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS TVShows ({0}, {1}, {2}, {3}, {4}, {5});", 
				Id, Name, CurrentState, Comment, Season, Episode));
			ExecuteNonQuery(command);			
		}


		private static void CreateInsertCommands()
		{
			CreateDatedNoteInsertCommand("AnimeFilms", out _animeFilmsInsertCommand);
			CreateSerialsInsertCommand("AnimeSerials", out _animeSerialsInsertCommand);
			CreateBookmarksInsertCommand();
			CreateDesiresInsertCommand();
			CreateDatedNoteInsertCommand("Films", out _filmsInsertCommand);
			CreateGamesInsertCommand();
			CreateLiteratureInsertCommand();
			CreateMealInsertCommand();			
			CreateDatedNoteInsertCommand("Perfomances", out _perfomancesInsertCommand);
			CreatePeopleInsertCommand();
			CreateProgramsInsertCommand();
			CreateSerialsInsertCommand("Serials", out _serialsInsertCommand);
			CreateSerialsInsertCommand("TVShows", out _TVShowsInsertCommand);
		}


		private static void CreateDatedNoteInsertCommand(string tableName, out SQLiteCommand _datedNoteInsertCommand)
		{
			_datedNoteInsertCommand = new SQLiteCommand();

			_datedNoteInsertCommand.CommandText = "INSERT INTO " + tableName + " (Id, Name, CurrentState, Comment, Year) " +
				"VALUES (@Id, @Name, @CurrentState, @Comment, @Year) " +
				"ON CONFLICT(Id) DO UPDATE SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Year = @Year;";

			_datedNoteInsertCommand.Parameters.Add("@Id", System.Data.DbType.Int32);
			_datedNoteInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_datedNoteInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_datedNoteInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_datedNoteInsertCommand.Parameters.Add("@Year", System.Data.DbType.Int32);
		}


		private static void CreateSerialsInsertCommand(string tableName, out SQLiteCommand _serialsInsertCommand)
		{
			_serialsInsertCommand = new SQLiteCommand();

			_serialsInsertCommand.CommandText = "INSERT INTO " + tableName + " (Id, Name, CurrentState, Comment, Season, Episode) " +
				"VALUES (@Id, @Name, @CurrentState, @Comment, @Season, @Episode) " +
				"ON CONFLICT(Id) DO UPDATE SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Season = @Season, Episode = @Episode;";

			_serialsInsertCommand.Parameters.Add("@Id", System.Data.DbType.Int32);
			_serialsInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_serialsInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_serialsInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_serialsInsertCommand.Parameters.Add("@Season", System.Data.DbType.Int32);
			_serialsInsertCommand.Parameters.Add("@Episode", System.Data.DbType.Int32);
		}


		private static void CreateBookmarksInsertCommand()
		{
			_bookmarksInsertCommand = new SQLiteCommand();
			
			_bookmarksInsertCommand.CommandText = "INSERT INTO Bookmarks (Id, Name, CurrentState, Comment, URL, Login, Password, Email) " +
				"VALUES (@Id, @Name, @CurrentState, @Comment, @URL, @Login, @Password, @Email) " +
				"ON CONFLICT(Id) DO UPDATE SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, " +
				"URL = @URL, Login = @Login, Password = @Password, Email = @Email;";

			_bookmarksInsertCommand.Parameters.Add("@Id", System.Data.DbType.String);
			_bookmarksInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_bookmarksInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_bookmarksInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_bookmarksInsertCommand.Parameters.Add("@URL", System.Data.DbType.String);
			_bookmarksInsertCommand.Parameters.Add("@Login", System.Data.DbType.String);
			_bookmarksInsertCommand.Parameters.Add("@Password", System.Data.DbType.String);
			_bookmarksInsertCommand.Parameters.Add("@Email", System.Data.DbType.String);
		}


		private static void CreateDesiresInsertCommand()
		{
			_desiresInsertCommand = new SQLiteCommand();

			_desiresInsertCommand.CommandText = "INSERT INTO Desires (Id, Name, CurrentState, Comment, Description) " +
				"VALUES (@Id, @Name, @CurrentState, @Comment, @Description) " +
				"ON CONFLICT(Id) DO UPDATE SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Description = @Description;";

			_desiresInsertCommand.Parameters.Add("@Id", System.Data.DbType.Int32);
			_desiresInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_desiresInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_desiresInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_desiresInsertCommand.Parameters.Add("@Description", System.Data.DbType.String);
		}


		private static void CreateGamesInsertCommand()
		{
			_gamesInsertCommand = new SQLiteCommand();

			_gamesInsertCommand.CommandText = "INSERT INTO Games (Id, Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email, Genre) " +
				"VALUES (@Id, @Name, @CurrentState, @Comment, @DownloadLink, @Version, @Login, @Password, @Email, @Genre) " +
				"ON CONFLICT(Id) DO UPDATE SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, " +
				"DownloadLink = @DownloadLink, Version = @Version, Login = @Login, Password = @Password, Email = @Email, Genre = @Genre;";

			_gamesInsertCommand.Parameters.Add("@Id", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_gamesInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@DownloadLink", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Version", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Login", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Password", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Email", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Genre", System.Data.DbType.String);
		}


		private static void CreateLiteratureInsertCommand()
		{
			_literatureInsertCommand = new SQLiteCommand();

			_literatureInsertCommand.CommandText = "INSERT INTO Literature " + 
					" (Id, Name, CurrentState, Comment, Year, Author, Genre, Universe, Series, Volume, Chapter, Page, Pages) " +
					"VALUES (@Id, @Name, @CurrentState, @Comment, @Year, @Author, @Genre, @Universe, @Series, @Volume, @Chapter, @Page, @Pages) " +
					"ON CONFLICT(Id) DO UPDATE " +
					"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Year = @Year, Author = @Author, " +
					"Genre = @Genre, Universe = @Universe, Series = @Series, Volume = @Volume, Chapter = @Chapter, Page = @Page, Pages = @Pages;";

			_literatureInsertCommand.Parameters.Add("@Id", System.Data.DbType.Int32);
			_literatureInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_literatureInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_literatureInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_literatureInsertCommand.Parameters.Add("@Year", System.Data.DbType.Int32);
			_literatureInsertCommand.Parameters.Add("@Author", System.Data.DbType.String);
			_literatureInsertCommand.Parameters.Add("@Genre", System.Data.DbType.String);
			_literatureInsertCommand.Parameters.Add("@Universe", System.Data.DbType.String);
			_literatureInsertCommand.Parameters.Add("@Series", System.Data.DbType.String);
			_literatureInsertCommand.Parameters.Add("@Volume", System.Data.DbType.Int32);
			_literatureInsertCommand.Parameters.Add("@Chapter", System.Data.DbType.Int32);
			_literatureInsertCommand.Parameters.Add("@Page", System.Data.DbType.Int32);
			_literatureInsertCommand.Parameters.Add("@Pages", System.Data.DbType.Int32);
		}


		private static void CreateMealInsertCommand()
		{
			_mealInsertCommand = new SQLiteCommand();

			_mealInsertCommand.CommandText = "INSERT INTO Meal (Id, Name, CurrentState, Comment, Ingredients, Recipe) " +
					"VALUES (@Id, @Name, @CurrentState, @Comment, @Ingredients, @Recipe) " +
					"ON CONFLICT(Id) DO UPDATE " + 
					"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Ingredients = @Ingredients, Recipe = @Recipe;";

			_mealInsertCommand.Parameters.Add("@Id", System.Data.DbType.String);
			_mealInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_mealInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_mealInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_mealInsertCommand.Parameters.Add("@Ingredients", System.Data.DbType.String);
			_mealInsertCommand.Parameters.Add("@Recipe", System.Data.DbType.String);
		}


		private static void CreatePeopleInsertCommand()
		{
			_peopleInsertCommand = new SQLiteCommand();

			_peopleInsertCommand.CommandText = "INSERT INTO People (Id, Name, CurrentState, Comment, Address, Birthdate, Nickname, Contacts, Sex) " +
					"VALUES (@Id, @Name, @CurrentState, @Comment, @Address, @Birthdate, @Nickname, @Contacts, @Sex) " +
					"ON CONFLICT(Id) DO UPDATE SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Address = @Address, " +
					"Birthdate = @Birthdate, Nickname = @Nickname, Contacts = @Contacts, Sex = @Sex;";

			_peopleInsertCommand.Parameters.Add("@Id", System.Data.DbType.String);
			_peopleInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_peopleInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_peopleInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_peopleInsertCommand.Parameters.Add("@Address", System.Data.DbType.String);
			_peopleInsertCommand.Parameters.Add("@Birthdate", System.Data.DbType.String);
			_peopleInsertCommand.Parameters.Add("@Nickname", System.Data.DbType.String);
			_peopleInsertCommand.Parameters.Add("@Contacts", System.Data.DbType.String);
			_peopleInsertCommand.Parameters.Add("@Sex", System.Data.DbType.Int32);
		}


		private static void CreateProgramsInsertCommand()
		{
			_programsInsertCommand = new SQLiteCommand();

			_programsInsertCommand.CommandText = "INSERT INTO Programs (Id, Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email) " +
					"VALUES (@Id, @Name, @CurrentState, @Comment, @DownloadLink, @Version, @Login, @Password, @Email) " +
					"ON CONFLICT(Id) DO UPDATE SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, " +
					"DownloadLink = @DownloadLink, Version = @Version, Login = @Login, Password = @Password, Email = @Email;";

			_programsInsertCommand.Parameters.Add("@Id", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_programsInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@DownloadLink", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@Version", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@Login", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@Password", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@Email", System.Data.DbType.String);
		}


		/// <summary>
		/// Добавляет запись в таблицу. Если запись с таким note.Id уже существует, то обновляет данные в ней.
		/// </summary>
		public static int InsertOrUpdate(string tableName, Note note)
		{
			switch (tableName)
			{
				case "AnimeFilms":   return InsertOrUpdateDatedNote(tableName, note, _animeFilmsInsertCommand);
				case "AnimeSerials": return InsertOrUpdateSerial(tableName, note, _animeSerialsInsertCommand);
				case "Bookmarks":    return InsertOrUpdateBookmark(tableName, note);
				case "Desires":      return InsertOrUpdateDesires(tableName, note);
				case "Films":        return InsertOrUpdateDatedNote(tableName, note, _filmsInsertCommand);
				case "Games":        return InsertOrUpdateGame(tableName, note);
				case "Literature":   return InsertOrUpdateLiterature(tableName, note);
				case "Meal":         return InsertOrUpdateMeal(tableName, note);				
				case "Performances": return InsertOrUpdateDatedNote(tableName, note, _perfomancesInsertCommand);
				case "People":       return InsertOrUpdatePerson(tableName, note);
				case "Programs":     return InsertOrUpdateProgram(tableName, note);
				case "Serials":      return InsertOrUpdateSerial(tableName, note, _serialsInsertCommand);
				case "TVShows":      return InsertOrUpdateSerial(tableName, note, _TVShowsInsertCommand);
				default: return 0;
			}
		}


		/// <summary>
		/// Вставляет новую заметку в БД или обновляет существующую (по Id). 
		/// В случае вставки выставляет для переданной заметки правильный Id. 
		/// </summary>
		private static int InsertOrUpdateDatedNote(string tableName, Note note, SQLiteCommand datedNoteInsertCommand)
		{
			DatedNote datedNote = note as DatedNote;
			if (datedNote == null)
			{
				Log.Error("Try to save incorrect dated note");
				return 0;
			}

			datedNote.Id = (datedNote.Id >= 0) ? datedNote.Id : (SelectMaxId(tableName) + 1);

			datedNoteInsertCommand.Parameters[0].Value = datedNote.Id;
			datedNoteInsertCommand.Parameters[1].Value = datedNote.Name;
			datedNoteInsertCommand.Parameters[2].Value = (int)datedNote.CurrentState;
			datedNoteInsertCommand.Parameters[3].Value = datedNote.Comment;
			datedNoteInsertCommand.Parameters[4].Value = datedNote.Year;

			datedNoteInsertCommand.Prepare();

			return ExecuteNonQuery(datedNoteInsertCommand);
		}


		private static int InsertOrUpdateSerial(string tableName, Note note, SQLiteCommand serialsInsertCommand)
		{
			Serial serial = note as Serial;
			if (serial == null)
			{
				Log.Error("Try to save incorrect serial note");
				return 0;
			}

			serial.Id = (serial.Id >= 0) ? serial.Id : (SelectMaxId(tableName) + 1);

			serialsInsertCommand.Parameters[0].Value = serial.Id;
			serialsInsertCommand.Parameters[1].Value = serial.Name;
			serialsInsertCommand.Parameters[2].Value = (int)serial.CurrentState;
			serialsInsertCommand.Parameters[3].Value = serial.Comment;
			serialsInsertCommand.Parameters[4].Value = serial.Season;
			serialsInsertCommand.Parameters[5].Value = serial.Episode;

			serialsInsertCommand.Prepare();

			return ExecuteNonQuery(serialsInsertCommand);
		}


		private static int InsertOrUpdateBookmark(string tableName, Note note)
		{
			Bookmark b = note as Bookmark;
			if (b == null)
			{
				Log.Error("Try to save incorrect bookmark note");
				return 0;
			}

			b.Id = (b.Id >= 0) ? b.Id : (SelectMaxId(tableName) + 1);

			_bookmarksInsertCommand.Parameters[0].Value = b.Id;
			_bookmarksInsertCommand.Parameters[1].Value = b.Name;
			_bookmarksInsertCommand.Parameters[2].Value = (int)b.CurrentState;
			_bookmarksInsertCommand.Parameters[3].Value = b.Comment;
			_bookmarksInsertCommand.Parameters[4].Value = b.URL;
			_bookmarksInsertCommand.Parameters[5].Value = b.Login;
			_bookmarksInsertCommand.Parameters[6].Value = b.Password;
			_bookmarksInsertCommand.Parameters[7].Value = b.Email;

			_bookmarksInsertCommand.Prepare();

			return ExecuteNonQuery(_bookmarksInsertCommand);
		}


		private static int InsertOrUpdateDesires(string tableName, Note note)
		{
			Desire desire = note as Desire;
			if (desire == null)
			{
				Log.Error("Try to save incorrect desire note");
				return 0;
			}

			desire.Id = (desire.Id >= 0) ? desire.Id : (SelectMaxId(tableName) + 1);

			_desiresInsertCommand.Parameters[0].Value = desire.Id;
			_desiresInsertCommand.Parameters[1].Value = desire.Name;
			_desiresInsertCommand.Parameters[2].Value = (int)desire.CurrentState;
			_desiresInsertCommand.Parameters[3].Value = desire.Comment;
			_desiresInsertCommand.Parameters[4].Value = desire.Description;

			_desiresInsertCommand.Prepare();

			return ExecuteNonQuery(_desiresInsertCommand);
		}


		private static int InsertOrUpdateGame(string tableName, Note note)
		{
			Game game = note as Game;
			if (game == null)
			{
				Log.Error("Try to save incorrect game note");
				return 0;
			}

			game.Id = (game.Id >= 0) ? game.Id : (SelectMaxId(tableName) + 1);

			_gamesInsertCommand.Parameters[0].Value = game.Id;
			_gamesInsertCommand.Parameters[1].Value = game.Name;
			_gamesInsertCommand.Parameters[2].Value = (int)game.CurrentState;
			_gamesInsertCommand.Parameters[3].Value = game.Comment;
			_gamesInsertCommand.Parameters[4].Value = game.DownloadLink;
			_gamesInsertCommand.Parameters[5].Value = game.Version;
			_gamesInsertCommand.Parameters[6].Value = game.Login;
			_gamesInsertCommand.Parameters[7].Value = game.Password;
			_gamesInsertCommand.Parameters[8].Value = game.Email;
			_gamesInsertCommand.Parameters[9].Value = game.Genre;

			_gamesInsertCommand.Prepare();

			return ExecuteNonQuery(_gamesInsertCommand);
		}
		

		private static int InsertOrUpdateLiterature(string tableName, Note note)
		{
			Literature lit = note as Literature;
			if (lit == null)
			{
				Log.Error("Try to save incorrect literature note");
				return 0;
			}

			lit.Id = (lit.Id >= 0) ? lit.Id : (SelectMaxId(tableName) + 1);

			_literatureInsertCommand.Parameters[0].Value = lit.Id;
			_literatureInsertCommand.Parameters[1].Value = lit.Name;
			_literatureInsertCommand.Parameters[2].Value = (int)lit.CurrentState;
			_literatureInsertCommand.Parameters[3].Value = lit.Comment;
			_literatureInsertCommand.Parameters[4].Value = lit.Year;
			_literatureInsertCommand.Parameters[5].Value = lit.Author;
			_literatureInsertCommand.Parameters[6].Value = lit.Genre;
			_literatureInsertCommand.Parameters[7].Value = lit.Universe;
			_literatureInsertCommand.Parameters[8].Value = lit.Series;
			_literatureInsertCommand.Parameters[9].Value = lit.Volume;
			_literatureInsertCommand.Parameters[10].Value = lit.Chapter;
			_literatureInsertCommand.Parameters[11].Value = lit.Page;
			_literatureInsertCommand.Parameters[12].Value = lit.Pages;

			_literatureInsertCommand.Prepare();

			return ExecuteNonQuery(_literatureInsertCommand);
		}


		private static int InsertOrUpdateMeal(string tableName, Note note)
		{
			Meal meal = note as Meal;
			if (meal == null)
			{
				Log.Error("Try to save incorrect meal note");
				return 0;
			}

			meal.Id = (meal.Id >= 0) ? meal.Id : (SelectMaxId(tableName) + 1);

			_mealInsertCommand.Parameters[0].Value = meal.Id;
			_mealInsertCommand.Parameters[1].Value = meal.Name;
			_mealInsertCommand.Parameters[2].Value = (int)meal.CurrentState;
			_mealInsertCommand.Parameters[3].Value = meal.Comment;
			_mealInsertCommand.Parameters[4].Value = meal.Ingredients;
			_mealInsertCommand.Parameters[5].Value = meal.Recipe;

			_mealInsertCommand.Prepare();

			return ExecuteNonQuery(_mealInsertCommand);
		}


		private static int InsertOrUpdatePerson(string tableName, Note note)
		{
			Person person = note as Person;
			if (person == null)
			{
				Log.Error("Try to save incorrect person note");
				return 0;
			}			

			person.Id = (person.Id >= 0) ? person.Id : (SelectMaxId(tableName) + 1);

			_peopleInsertCommand.Parameters[0].Value = person.Id;
			_peopleInsertCommand.Parameters[1].Value = person.Name;
			_peopleInsertCommand.Parameters[2].Value = (int)person.CurrentState;
			_peopleInsertCommand.Parameters[3].Value = person.Comment;
			_peopleInsertCommand.Parameters[4].Value = person.Address;
			_peopleInsertCommand.Parameters[5].Value = person.Birthdate;
			_peopleInsertCommand.Parameters[6].Value = person.Nickname;
			_peopleInsertCommand.Parameters[7].Value = person.Contacts;
			_peopleInsertCommand.Parameters[8].Value = person.Sex;

			_peopleInsertCommand.Prepare();

			return ExecuteNonQuery(_peopleInsertCommand);
		}


		private static int InsertOrUpdateProgram(string tableName, Note note)
		{
			Program program = note as Program;
			if (program == null)
			{
				Log.Error("Try to save incorrect program note");
				return 0;
			}

			program.Id = (program.Id >= 0) ? program.Id : (SelectMaxId(tableName) + 1);

			_programsInsertCommand.Parameters[0].Value = program.Id;
			_programsInsertCommand.Parameters[1].Value = program.Name;
			_programsInsertCommand.Parameters[2].Value = (int)program.CurrentState;
			_programsInsertCommand.Parameters[3].Value = program.Comment;
			_programsInsertCommand.Parameters[4].Value = program.DownloadLink;
			_programsInsertCommand.Parameters[5].Value = program.Version;
			_programsInsertCommand.Parameters[6].Value = program.Login;
			_programsInsertCommand.Parameters[7].Value = program.Password;
			_programsInsertCommand.Parameters[8].Value = program.Email;

			_programsInsertCommand.Prepare();

			return ExecuteNonQuery(_programsInsertCommand);
		}


		public static bool DeleteNote(string tableName, int id)
		{
			SQLiteCommand command = new SQLiteCommand(string.Format("DELETE FROM {0} WHERE Id = {1}", tableName, id));
			int deletedRows = ExecuteNonQuery(command);

			return (deletedRows == 1);
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
						InsertOrUpdateSerial("AnimeSerials", note, _animeSerialsInsertCommand);
					break;
				}
				case "serials":
				{
					foreach (Note note in SelectFromOldTable(filePath, "serials", oldStates))
						InsertOrUpdateSerial("Serials", note, _serialsInsertCommand);
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
						InsertOrUpdateDatedNote("Films", note, _filmsInsertCommand);
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
