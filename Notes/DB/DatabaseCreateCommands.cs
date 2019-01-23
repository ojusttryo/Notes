using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace Notes.DB
{
	public partial class Database
	{
		private static SQLiteCommand _animeFilmsInsertCommand;
		private static SQLiteCommand _animeSerialsInsertCommand;
		private static SQLiteCommand _bookmarksInsertCommand;
		private static SQLiteCommand _desiresInsertCommand;
		private static SQLiteCommand _filmsInsertCommand;
		private static SQLiteCommand _gamesInsertCommand;
		private static SQLiteCommand _literatureInsertCommand;
		private static SQLiteCommand _mealInsertCommand;
		private static SQLiteCommand _performancesInsertCommand;
		private static SQLiteCommand _peopleInsertCommand;
		private static SQLiteCommand _programsInsertCommand;
		private static SQLiteCommand _serialsInsertCommand;
		private static SQLiteCommand _TVShowsInsertCommand;		

		private static SQLiteCommand _animeFilmsUpdateCommand;
		private static SQLiteCommand _animeSerialsUpdateCommand;
		private static SQLiteCommand _bookmarksUpdateCommand;
		private static SQLiteCommand _desiresUpdateCommand;
		private static SQLiteCommand _filmsUpdateCommand;
		private static SQLiteCommand _gamesUpdateCommand;
		private static SQLiteCommand _literatureUpdateCommand;
		private static SQLiteCommand _mealUpdateCommand;
		private static SQLiteCommand _performancesUpdateCommand;
		private static SQLiteCommand _peopleUpdateCommand;
		private static SQLiteCommand _programsUpdateCommand;
		private static SQLiteCommand _serialsUpdateCommand;
		private static SQLiteCommand _TVShowsUpdateCommand;

		private static SQLiteCommand _settingsInsertOrUpdateCommand;


		private static void CreateCommands()
		{
			CreateDatedNoteInsertCommand("AnimeFilms", out _animeFilmsInsertCommand);
			CreateSerialsInsertCommand("AnimeSerials", out _animeSerialsInsertCommand);
			CreateBookmarksInsertCommand();
			CreateDesiresInsertCommand();
			CreateDatedNoteInsertCommand("Films", out _filmsInsertCommand);
			CreateGamesInsertCommand();
			CreateLiteratureInsertCommand();
			CreateMealInsertCommand();			
			CreateDatedNoteInsertCommand("Performances", out _performancesInsertCommand);
			CreatePeopleInsertCommand();
			CreateProgramsInsertCommand();
			CreateSerialsInsertCommand("Serials", out _serialsInsertCommand);
			CreateSerialsInsertCommand("TVShows", out _TVShowsInsertCommand);

			CreateDatedNoteUpdateCommand("AnimeFilms", out _animeFilmsUpdateCommand);
			CreateSerialsUpdateCommand("AnimeSerials", out _animeSerialsUpdateCommand);
			CreateBookmarksUpdateCommand();
			CreateDesiresUpdateCommand();
			CreateDatedNoteUpdateCommand("Films", out _filmsUpdateCommand);
			CreateGamesUpdateCommand();
			CreateLiteratureUpdateCommand();
			CreateMealUpdateCommand();			
			CreateDatedNoteUpdateCommand("Performances", out _performancesUpdateCommand);
			CreatePeopleUpdateCommand();
			CreateProgramsUpdateCommand();
			CreateSerialsUpdateCommand("Serials", out _serialsUpdateCommand);
			CreateSerialsUpdateCommand("TVShows", out _TVShowsUpdateCommand);

			CreateSettingsInsertOrUpdateCommand();
		}


		private static void CreateDatedNoteInsertCommand(string tableName, out SQLiteCommand _datedNoteInsertCommand)
		{
			_datedNoteInsertCommand = new SQLiteCommand();

			_datedNoteInsertCommand.CommandText = "INSERT INTO " + tableName + " (Name, CurrentState, Comment, Year) " +
				"VALUES (@Name, @CurrentState, @Comment, @Year);";

			_datedNoteInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_datedNoteInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_datedNoteInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_datedNoteInsertCommand.Parameters.Add("@Year", System.Data.DbType.Int32);
		}


		private static void CreateSerialsInsertCommand(string tableName, out SQLiteCommand _serialsInsertCommand)
		{
			_serialsInsertCommand = new SQLiteCommand();

			_serialsInsertCommand.CommandText = "INSERT INTO " + tableName + " (Name, CurrentState, Comment, Season, Episode) " +
				"VALUES (@Name, @CurrentState, @Comment, @Season, @Episode);";

			_serialsInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_serialsInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_serialsInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_serialsInsertCommand.Parameters.Add("@Season", System.Data.DbType.Int32);
			_serialsInsertCommand.Parameters.Add("@Episode", System.Data.DbType.Int32);
		}


		private static void CreateBookmarksInsertCommand()
		{
			_bookmarksInsertCommand = new SQLiteCommand();

			_bookmarksInsertCommand.CommandText = "INSERT INTO Bookmarks (Name, CurrentState, Comment, URL, Login, Password, Email) " +
				"VALUES (@Name, @CurrentState, @Comment, @URL, @Login, @Password, @Email);";

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

			_desiresInsertCommand.CommandText = "INSERT INTO Desires (Name, CurrentState, Comment, Description) " +
				"VALUES (@Name, @CurrentState, @Comment, @Description);";

			_desiresInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_desiresInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_desiresInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_desiresInsertCommand.Parameters.Add("@Description", System.Data.DbType.String);
		}


		private static void CreateGamesInsertCommand()
		{
			_gamesInsertCommand = new SQLiteCommand();

			_gamesInsertCommand.CommandText = 
				"INSERT INTO Games (Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email, Genre, PlayersCount) " +
				"VALUES (@Name, @CurrentState, @Comment, @DownloadLink, @Version, @Login, @Password, @Email, @Genre, @PlayersCount);";

			_gamesInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_gamesInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@DownloadLink", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Version", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Login", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Password", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Email", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@Genre", System.Data.DbType.String);
			_gamesInsertCommand.Parameters.Add("@PlayersCount", System.Data.DbType.Int32);
		}


		private static void CreateLiteratureInsertCommand()
		{
			_literatureInsertCommand = new SQLiteCommand();

			_literatureInsertCommand.CommandText = "INSERT INTO Literature " +
					" (Name, CurrentState, Comment, Year, Author, Genre, Universe, Series, Volume, Chapter, Page, Pages) " +
					"VALUES (@Name, @CurrentState, @Comment, @Year, @Author, @Genre, @Universe, @Series, @Volume, @Chapter, @Page, @Pages);";

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

			_mealInsertCommand.CommandText = "INSERT INTO Meal (Name, CurrentState, Comment, Ingredients, Recipe) " +
					"VALUES (@Name, @CurrentState, @Comment, @Ingredients, @Recipe);";

			_mealInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_mealInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_mealInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_mealInsertCommand.Parameters.Add("@Ingredients", System.Data.DbType.String);
			_mealInsertCommand.Parameters.Add("@Recipe", System.Data.DbType.String);
		}


		private static void CreatePeopleInsertCommand()
		{
			_peopleInsertCommand = new SQLiteCommand();

			_peopleInsertCommand.CommandText = "INSERT INTO People (Name, CurrentState, Comment, Address, Birthdate, Nickname, Contacts, Sex) " +
					"VALUES (@Name, @CurrentState, @Comment, @Address, @Birthdate, @Nickname, @Contacts, @Sex);";

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

			_programsInsertCommand.CommandText = "INSERT INTO Programs (Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email) " +
					"VALUES (@Name, @CurrentState, @Comment, @DownloadLink, @Version, @Login, @Password, @Email);";

			_programsInsertCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_programsInsertCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@DownloadLink", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@Version", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@Login", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@Password", System.Data.DbType.String);
			_programsInsertCommand.Parameters.Add("@Email", System.Data.DbType.String);
		}


		private static void CreateDatedNoteUpdateCommand(string tableName, out SQLiteCommand datedNoteUpdateCommand)
		{
			datedNoteUpdateCommand = new SQLiteCommand();

			datedNoteUpdateCommand.CommandText = "UPDATE " + tableName + " " +
				"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Year = @Year " +
				"WHERE Id = @Id;";

			datedNoteUpdateCommand.Parameters.Add("@Id", System.Data.DbType.Int32);
			datedNoteUpdateCommand.Parameters.Add("@Name", System.Data.DbType.String);
			datedNoteUpdateCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			datedNoteUpdateCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			datedNoteUpdateCommand.Parameters.Add("@Year", System.Data.DbType.Int32);
		}


		private static void CreateSerialsUpdateCommand(string tableName, out SQLiteCommand serialsUpdateCommand)
		{
			serialsUpdateCommand = new SQLiteCommand();

			serialsUpdateCommand.CommandText = "UPDATE " + tableName + " " +
				"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Season = @Season, Episode = @Episode " +
				"WHERE Id = @Id;";

			serialsUpdateCommand.Parameters.Add("@Id", System.Data.DbType.Int32);
			serialsUpdateCommand.Parameters.Add("@Name", System.Data.DbType.String);
			serialsUpdateCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			serialsUpdateCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			serialsUpdateCommand.Parameters.Add("@Season", System.Data.DbType.Int32);
			serialsUpdateCommand.Parameters.Add("@Episode", System.Data.DbType.Int32);
		}


		private static void CreateBookmarksUpdateCommand()
		{
			_bookmarksUpdateCommand = new SQLiteCommand();
			
			_bookmarksUpdateCommand.CommandText = "UPDATE Bookmarks " +
				"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, URL = @URL, Login = @Login, Password = @Password, Email = @Email " +
				"WHERE Id = @Id;";

			_bookmarksUpdateCommand.Parameters.Add("@Id", System.Data.DbType.String);
			_bookmarksUpdateCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_bookmarksUpdateCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_bookmarksUpdateCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_bookmarksUpdateCommand.Parameters.Add("@URL", System.Data.DbType.String);
			_bookmarksUpdateCommand.Parameters.Add("@Login", System.Data.DbType.String);
			_bookmarksUpdateCommand.Parameters.Add("@Password", System.Data.DbType.String);
			_bookmarksUpdateCommand.Parameters.Add("@Email", System.Data.DbType.String);
		}


		private static void CreateDesiresUpdateCommand()
		{
			_desiresUpdateCommand = new SQLiteCommand();

			_desiresUpdateCommand.CommandText = "UPDATE Desires " +
				"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Description = @Description " + 
				"WHERE Id = @Id;";

			_desiresUpdateCommand.Parameters.Add("@Id", System.Data.DbType.Int32);
			_desiresUpdateCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_desiresUpdateCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_desiresUpdateCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_desiresUpdateCommand.Parameters.Add("@Description", System.Data.DbType.String);
		}


		private static void CreateGamesUpdateCommand()
		{
			_gamesUpdateCommand = new SQLiteCommand();

			_gamesUpdateCommand.CommandText = "UPDATE Games " +
				"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, DownloadLink = @DownloadLink, " +
				"Version = @Version, Login = @Login, Password = @Password, Email = @Email, Genre = @Genre, PlayersCount = @PlayersCount " + 
				"WHERE Id = @Id;";

			_gamesUpdateCommand.Parameters.Add("@Id", System.Data.DbType.String);
			_gamesUpdateCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_gamesUpdateCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_gamesUpdateCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_gamesUpdateCommand.Parameters.Add("@DownloadLink", System.Data.DbType.String);
			_gamesUpdateCommand.Parameters.Add("@Version", System.Data.DbType.String);
			_gamesUpdateCommand.Parameters.Add("@Login", System.Data.DbType.String);
			_gamesUpdateCommand.Parameters.Add("@Password", System.Data.DbType.String);
			_gamesUpdateCommand.Parameters.Add("@Email", System.Data.DbType.String);
			_gamesUpdateCommand.Parameters.Add("@Genre", System.Data.DbType.String);
			_gamesUpdateCommand.Parameters.Add("@PlayersCount", System.Data.DbType.Int32);
		}


		private static void CreateLiteratureUpdateCommand()
		{
			_literatureUpdateCommand = new SQLiteCommand();

			_literatureUpdateCommand.CommandText = "UPDATE Literature " + 
					"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Year = @Year, Author = @Author, " +
					"Genre = @Genre, Universe = @Universe, Series = @Series, Volume = @Volume, Chapter = @Chapter, Page = @Page, Pages = @Pages " + 
					"WHERE Id = @Id;";

			_literatureUpdateCommand.Parameters.Add("@Id", System.Data.DbType.Int32);
			_literatureUpdateCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_literatureUpdateCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_literatureUpdateCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_literatureUpdateCommand.Parameters.Add("@Year", System.Data.DbType.Int32);
			_literatureUpdateCommand.Parameters.Add("@Author", System.Data.DbType.String);
			_literatureUpdateCommand.Parameters.Add("@Genre", System.Data.DbType.String);
			_literatureUpdateCommand.Parameters.Add("@Universe", System.Data.DbType.String);
			_literatureUpdateCommand.Parameters.Add("@Series", System.Data.DbType.String);
			_literatureUpdateCommand.Parameters.Add("@Volume", System.Data.DbType.Int32);
			_literatureUpdateCommand.Parameters.Add("@Chapter", System.Data.DbType.Int32);
			_literatureUpdateCommand.Parameters.Add("@Page", System.Data.DbType.Int32);
			_literatureUpdateCommand.Parameters.Add("@Pages", System.Data.DbType.Int32);
		}


		private static void CreateMealUpdateCommand()
		{
			_mealUpdateCommand = new SQLiteCommand();

			_mealUpdateCommand.CommandText = "UPDATE Meal " +
					"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Ingredients = @Ingredients, Recipe = @Recipe " +
					"WHERE Id = @Id;";

			_mealUpdateCommand.Parameters.Add("@Id", System.Data.DbType.String);
			_mealUpdateCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_mealUpdateCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_mealUpdateCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_mealUpdateCommand.Parameters.Add("@Ingredients", System.Data.DbType.String);
			_mealUpdateCommand.Parameters.Add("@Recipe", System.Data.DbType.String);
		}


		private static void CreatePeopleUpdateCommand()
		{
			_peopleUpdateCommand = new SQLiteCommand();

			_peopleUpdateCommand.CommandText = "UPDATE People " +
					"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, Address = @Address, " +
					"Birthdate = @Birthdate, Nickname = @Nickname, Contacts = @Contacts, Sex = @Sex " + 
					"WHERE Id = @Id;";

			_peopleUpdateCommand.Parameters.Add("@Id", System.Data.DbType.String);
			_peopleUpdateCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_peopleUpdateCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_peopleUpdateCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_peopleUpdateCommand.Parameters.Add("@Address", System.Data.DbType.String);
			_peopleUpdateCommand.Parameters.Add("@Birthdate", System.Data.DbType.String);
			_peopleUpdateCommand.Parameters.Add("@Nickname", System.Data.DbType.String);
			_peopleUpdateCommand.Parameters.Add("@Contacts", System.Data.DbType.String);
			_peopleUpdateCommand.Parameters.Add("@Sex", System.Data.DbType.Int32);
		}


		private static void CreateProgramsUpdateCommand()
		{
			_programsUpdateCommand = new SQLiteCommand();

			_programsUpdateCommand.CommandText = "UPDATE Programs " +
					"SET Name = @Name, CurrentState = @CurrentState, Comment = @Comment, " +
					"DownloadLink = @DownloadLink, Version = @Version, Login = @Login, Password = @Password, Email = @Email " + 
					"WHERE Id = @Id;";

			_programsUpdateCommand.Parameters.Add("@Id", System.Data.DbType.String);
			_programsUpdateCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_programsUpdateCommand.Parameters.Add("@CurrentState", System.Data.DbType.Int32);
			_programsUpdateCommand.Parameters.Add("@Comment", System.Data.DbType.String);
			_programsUpdateCommand.Parameters.Add("@DownloadLink", System.Data.DbType.String);
			_programsUpdateCommand.Parameters.Add("@Version", System.Data.DbType.String);
			_programsUpdateCommand.Parameters.Add("@Login", System.Data.DbType.String);
			_programsUpdateCommand.Parameters.Add("@Password", System.Data.DbType.String);
			_programsUpdateCommand.Parameters.Add("@Email", System.Data.DbType.String);
		}


		private static void CreateSettingsInsertOrUpdateCommand()
		{
			_settingsInsertOrUpdateCommand = new SQLiteCommand();

			_settingsInsertOrUpdateCommand.CommandText = 
				"INSERT INTO Settings (Name, Value) VALUES (@Name, @Value) " +
				"ON CONFLICT (Name) DO UPDATE SET Value = @Value";

			_settingsInsertOrUpdateCommand.Parameters.Add("@Name", System.Data.DbType.String);
			_settingsInsertOrUpdateCommand.Parameters.Add("@Value", System.Data.DbType.String);
		}
	}
}
