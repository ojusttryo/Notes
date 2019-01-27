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
			CreateCommands();
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
			string PlayersCount = "PlayersCount  INTEGER NOT NULL DEFAULT 0";		// 0 - not defined, 1 - singleplayer, 2 - multiplayer

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

			command = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS Games ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10});", 
				Id, Name, CurrentState, Comment, DownloadLink, Version, Login, Password, Email, Genre, PlayersCount));
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
	}
}
