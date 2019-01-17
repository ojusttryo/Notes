using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;

namespace Notes.DB
{
	public partial class Database
	{
		/// <summary>
		/// Создает базу данных, таблицы и подготавливает команды.
		/// </summary>
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

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Settings (Name TEXT PRIMARY KEY NOT NULL, Value TEXT NOT NULL);"));
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
			CreateSettingsInsertCommand();
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


		private static void CreateSettingsInsertCommand()
		{
			_settingsInsertCommand = new SQLiteCommand();

			_settingsInsertCommand.CommandText = 
				"INSERT INTO Settings (Name, Value) VALUES (@Name, @Value) " +
				"ON CONFLICT (Name) DO UPDATE SET Value = @Value";

			_settingsInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_settingsInsertCommand.Parameters.Add("@Value", System.Data.DbType.String);
		}
	}
}
