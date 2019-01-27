using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

using Notes.Notes;

namespace Notes.DB
{
	public partial class Database
	{
		public static bool Insert(string tableNameDB, List<Note> notes)
		{
			switch (tableNameDB)
			{
				case "AnimeFilms":   return InsertDatedNotes(notes, _animeFilmsInsertCommand);
				case "AnimeSerials": return InsertSerials(notes, _animeSerialsInsertCommand);
				case "Bookmarks":    return InsertBookmarks(notes);
				case "Desires":      return InsertDesires(notes);
				case "Films":        return InsertDatedNotes(notes, _filmsInsertCommand);
				case "Games":        return InsertGames(notes);
				case "Literature":   return InsertLiterature(notes);
				case "Meal":         return InsertMeal(notes);
				case "Performances": return InsertDatedNotes(notes, _performancesInsertCommand);
				case "People":       return InsertPeople(notes);
				case "Programs":     return InsertPrograms(notes);
				case "Serials":      return InsertSerials(notes, _serialsInsertCommand);
				case "TVShows":      return InsertSerials(notes, _TVShowsInsertCommand);
				default: return false;
			}
		}


		private static bool InsertDatedNotes(List<Note> notes, SQLiteCommand datedNoteInsertCommand)
		{
			bool inserted = false;

			try
			{
				using (SQLiteConnection connection = Database.CreateConnection())
				{	
					connection.Open();
					datedNoteInsertCommand.Connection = connection;

					if (connection.State == System.Data.ConnectionState.Open)
					{
						using (SQLiteTransaction transaction = connection.BeginTransaction())
						{
							foreach (Note note in notes)
							{
								DatedNote datedNote = note as DatedNote;
								if (datedNote == null)
								{
									Log.Error("Try to insert incorrect note");
									continue;
								}

								datedNoteInsertCommand.Parameters[0].Value = datedNote.Name;
								datedNoteInsertCommand.Parameters[1].Value = (int)datedNote.CurrentState;
								datedNoteInsertCommand.Parameters[2].Value = datedNote.Comment;
								datedNoteInsertCommand.Parameters[3].Value = datedNote.Year;

								datedNoteInsertCommand.Prepare();
								datedNoteInsertCommand.ExecuteNonQuery();

								datedNote.Id = (int)connection.LastInsertRowId;

								inserted = true;
							}

							transaction.Commit();
						}
					}

					connection.Close();
					datedNoteInsertCommand.Connection = null;
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", 
					Environment.NewLine, (datedNoteInsertCommand != null) ? datedNoteInsertCommand.CommandText : "", 
					Environment.NewLine, ex.ToString()));

				return false;
			}

			return inserted;
		}


		private static bool InsertSerials(List<Note> notes, SQLiteCommand serialsInsertCommand)
		{
			bool inserted = false;

			try
			{
				using (SQLiteConnection connection = Database.CreateConnection())
				{	
					connection.Open();
					serialsInsertCommand.Connection = connection;

					if (connection.State == System.Data.ConnectionState.Open)
					{
						using (SQLiteTransaction transaction = connection.BeginTransaction())
						{
							foreach (Note note in notes)
							{
								Serial serial = note as Serial;
								if (serial == null)
								{
									Log.Error("Try to insert incorrect note");
									continue;
								}

								serialsInsertCommand.Parameters[0].Value = serial.Name;
								serialsInsertCommand.Parameters[1].Value = (int)serial.CurrentState;
								serialsInsertCommand.Parameters[2].Value = serial.Comment;
								serialsInsertCommand.Parameters[3].Value = serial.Season;
								serialsInsertCommand.Parameters[4].Value = serial.Episode;

								serialsInsertCommand.Prepare();
								serialsInsertCommand.ExecuteNonQuery();

								serial.Id = (int)connection.LastInsertRowId;

								inserted = true;
							}

							transaction.Commit();
						}
					}

					connection.Close();
					serialsInsertCommand.Connection = null;
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", 
					Environment.NewLine, (serialsInsertCommand != null) ? serialsInsertCommand.CommandText : "", 
					Environment.NewLine, ex.ToString()));

				return false;
			}

			return inserted;
		}


		private static bool InsertBookmarks(List<Note> notes)
		{
			bool inserted = false;

			try
			{
				using (SQLiteConnection connection = Database.CreateConnection())
				{	
					connection.Open();
					_bookmarksInsertCommand.Connection = connection;

					if (connection.State == System.Data.ConnectionState.Open)
					{
						using (SQLiteTransaction transaction = connection.BeginTransaction())
						{
							foreach (Note note in notes)
							{
								Bookmark b = note as Bookmark;
								if (b == null)
								{
									Log.Error("Try to insert incorrect note");
									continue;
								}

								_bookmarksInsertCommand.Parameters[0].Value = b.Name;
								_bookmarksInsertCommand.Parameters[1].Value = (int)b.CurrentState;
								_bookmarksInsertCommand.Parameters[2].Value = b.Comment;
								_bookmarksInsertCommand.Parameters[3].Value = b.URL;
								_bookmarksInsertCommand.Parameters[4].Value = b.Login;
								_bookmarksInsertCommand.Parameters[5].Value = b.Password;
								_bookmarksInsertCommand.Parameters[6].Value = b.Email;

								_bookmarksInsertCommand.Prepare();
								_bookmarksInsertCommand.ExecuteNonQuery();

								b.Id = (int)connection.LastInsertRowId;

								inserted = true;
							}

							transaction.Commit();
						}
					}

					connection.Close();
					_bookmarksInsertCommand.Connection = null;
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", 
					Environment.NewLine, (_bookmarksInsertCommand != null) ? _bookmarksInsertCommand.CommandText : "", 
					Environment.NewLine, ex.ToString()));

				return false;
			}

			return inserted;
		}


		private static bool InsertDesires(List<Note> notes)
		{
			bool inserted = false;

			try
			{
				using (SQLiteConnection connection = Database.CreateConnection())
				{	
					connection.Open();
					_desiresInsertCommand.Connection = connection;

					if (connection.State == System.Data.ConnectionState.Open)
					{
						using (SQLiteTransaction transaction = connection.BeginTransaction())
						{
							foreach (Note note in notes)
							{
								Desire desire = note as Desire;
								if (desire == null)
								{
									Log.Error("Try to insert incorrect note");
									continue;
								}

								_desiresInsertCommand.Parameters[0].Value = desire.Name;
								_desiresInsertCommand.Parameters[1].Value = (int)desire.CurrentState;
								_desiresInsertCommand.Parameters[2].Value = desire.Comment;
								_desiresInsertCommand.Parameters[3].Value = desire.Description;

								_desiresInsertCommand.Prepare();
								_desiresInsertCommand.ExecuteNonQuery();

								desire.Id = (int)connection.LastInsertRowId;

								inserted = true;
							}

							transaction.Commit();
						}
					}

					connection.Close();
					_desiresInsertCommand.Connection = null;
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", 
					Environment.NewLine, (_desiresInsertCommand != null) ? _desiresInsertCommand.CommandText : "", 
					Environment.NewLine, ex.ToString()));

				return false;
			}

			return inserted;
		}


		private static bool InsertGames(List<Note> notes)
		{
			bool inserted = false;

			try
			{
				using (SQLiteConnection connection = Database.CreateConnection())
				{	
					connection.Open();
					_gamesInsertCommand.Connection = connection;

					if (connection.State == System.Data.ConnectionState.Open)
					{
						using (SQLiteTransaction transaction = connection.BeginTransaction())
						{
							foreach (Note note in notes)
							{
								Game game = note as Game;
								if (game == null)
								{
									Log.Error("Try to insert incorrect note");
									continue;
								}

								_gamesInsertCommand.Parameters[0].Value = game.Name;
								_gamesInsertCommand.Parameters[1].Value = (int)game.CurrentState;
								_gamesInsertCommand.Parameters[2].Value = game.Comment;
								_gamesInsertCommand.Parameters[3].Value = game.DownloadLink;
								_gamesInsertCommand.Parameters[4].Value = game.Version;
								_gamesInsertCommand.Parameters[5].Value = game.Login;
								_gamesInsertCommand.Parameters[6].Value = game.Password;
								_gamesInsertCommand.Parameters[7].Value = game.Email;
								_gamesInsertCommand.Parameters[8].Value = game.Genre;
								_gamesInsertCommand.Parameters[9].Value = (int)game.Players;

								_gamesInsertCommand.Prepare();
								_gamesInsertCommand.ExecuteNonQuery();

								game.Id = (int)connection.LastInsertRowId;

								inserted = true;
							}

							transaction.Commit();
						}
					}

					connection.Close();
					_gamesInsertCommand.Connection = null;
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", 
					Environment.NewLine, (_gamesInsertCommand != null) ? _gamesInsertCommand.CommandText : "", 
					Environment.NewLine, ex.ToString()));

				return false;
			}

			return inserted;
		}
		

		private static bool InsertLiterature(List<Note> notes)
		{
			bool inserted = false;

			try
			{
				using (SQLiteConnection connection = Database.CreateConnection())
				{	
					connection.Open();
					_literatureInsertCommand.Connection = connection;

					if (connection.State == System.Data.ConnectionState.Open)
					{
						using (SQLiteTransaction transaction = connection.BeginTransaction())
						{
							foreach (Note note in notes)
							{
								Literature lit = note as Literature;
								if (lit == null)
								{
									Log.Error("Try to insert incorrect note");
									continue;
								}

								_literatureInsertCommand.Parameters[0].Value = lit.Name;
								_literatureInsertCommand.Parameters[1].Value = (int)lit.CurrentState;
								_literatureInsertCommand.Parameters[2].Value = lit.Comment;
								_literatureInsertCommand.Parameters[3].Value = lit.Year;
								_literatureInsertCommand.Parameters[4].Value = lit.Author;
								_literatureInsertCommand.Parameters[5].Value = lit.Genre;
								_literatureInsertCommand.Parameters[6].Value = lit.Universe;
								_literatureInsertCommand.Parameters[7].Value = lit.Series;
								_literatureInsertCommand.Parameters[8].Value = lit.Volume;
								_literatureInsertCommand.Parameters[9].Value = lit.Chapter;
								_literatureInsertCommand.Parameters[10].Value = lit.Page;
								_literatureInsertCommand.Parameters[11].Value = lit.Pages;

								_literatureInsertCommand.Prepare();
								_literatureInsertCommand.ExecuteNonQuery();

								lit.Id = (int)connection.LastInsertRowId;

								inserted = true;
							}

							transaction.Commit();
						}
					}

					connection.Close();
					_literatureInsertCommand.Connection = null;
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", 
					Environment.NewLine, (_literatureInsertCommand != null) ? _literatureInsertCommand.CommandText : "", 
					Environment.NewLine, ex.ToString()));

				return false;
			}

			return inserted;
		}


		private static bool InsertMeal(List<Note> notes)
		{
			bool inserted = false;

			try
			{
				using (SQLiteConnection connection = Database.CreateConnection())
				{	
					connection.Open();
					_mealInsertCommand.Connection = connection;

					if (connection.State == System.Data.ConnectionState.Open)
					{
						using (SQLiteTransaction transaction = connection.BeginTransaction())
						{
							foreach (Note note in notes)
							{
								Meal meal = note as Meal;
								if (meal == null)
								{
									Log.Error("Try to insert incorrect note");
									continue;
								}

								_mealInsertCommand.Parameters[0].Value = meal.Name;
								_mealInsertCommand.Parameters[1].Value = (int)meal.CurrentState;
								_mealInsertCommand.Parameters[2].Value = meal.Comment;
								_mealInsertCommand.Parameters[3].Value = meal.Ingredients;
								_mealInsertCommand.Parameters[4].Value = meal.Recipe;

								_mealInsertCommand.Prepare();
								_mealInsertCommand.ExecuteNonQuery();

								meal.Id = (int)connection.LastInsertRowId;

								inserted = true;
							}

							transaction.Commit();
						}
					}

					connection.Close();
					_mealInsertCommand.Connection = null;
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", 
					Environment.NewLine, (_mealInsertCommand != null) ? _mealInsertCommand.CommandText : "", 
					Environment.NewLine, ex.ToString()));

				return false;
			}

			return inserted;
		}


		private static bool InsertPeople(List<Note> notes)
		{
			bool inserted = false;

			try
			{
				using (SQLiteConnection connection = Database.CreateConnection())
				{	
					connection.Open();
					_peopleInsertCommand.Connection = connection;

					if (connection.State == System.Data.ConnectionState.Open)
					{
						using (SQLiteTransaction transaction = connection.BeginTransaction())
						{
							foreach (Note note in notes)
							{
								Person person = note as Person;
								if (person == null)
								{
									Log.Error("Try to insert incorrect note");
									continue;
								}

								_peopleInsertCommand.Parameters[0].Value = person.Name;
								_peopleInsertCommand.Parameters[1].Value = (int)person.CurrentState;
								_peopleInsertCommand.Parameters[2].Value = person.Comment;
								_peopleInsertCommand.Parameters[3].Value = person.Address;
								_peopleInsertCommand.Parameters[4].Value = person.Birthdate;
								_peopleInsertCommand.Parameters[5].Value = person.Nickname;
								_peopleInsertCommand.Parameters[6].Value = person.Contacts;
								_peopleInsertCommand.Parameters[7].Value = person.Sex;

								_peopleInsertCommand.Prepare();
								_peopleInsertCommand.ExecuteNonQuery();

								person.Id = (int)connection.LastInsertRowId;

								inserted = true;
							}

							transaction.Commit();
						}
					}

					connection.Close();
					_peopleInsertCommand.Connection = null;
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", 
					Environment.NewLine, (_peopleInsertCommand != null) ? _peopleInsertCommand.CommandText : "", 
					Environment.NewLine, ex.ToString()));

				return false;
			}

			return inserted;
		}


		private static bool InsertPrograms(List<Note> notes)
		{
			bool inserted = false;

			try
			{
				using (SQLiteConnection connection = Database.CreateConnection())
				{	
					connection.Open();
					_programsInsertCommand.Connection = connection;

					if (connection.State == System.Data.ConnectionState.Open)
					{
						using (SQLiteTransaction transaction = connection.BeginTransaction())
						{
							foreach (Note note in notes)
							{
								Program program = note as Program;
								if (program == null)
								{
									Log.Error("Try to insert incorrect note");
									continue;
								}

								_programsInsertCommand.Parameters[0].Value = program.Name;
								_programsInsertCommand.Parameters[1].Value = (int)program.CurrentState;
								_programsInsertCommand.Parameters[2].Value = program.Comment;
								_programsInsertCommand.Parameters[3].Value = program.DownloadLink;
								_programsInsertCommand.Parameters[4].Value = program.Version;
								_programsInsertCommand.Parameters[5].Value = program.Login;
								_programsInsertCommand.Parameters[6].Value = program.Password;
								_programsInsertCommand.Parameters[7].Value = program.Email;

								_programsInsertCommand.Prepare();
								_programsInsertCommand.ExecuteNonQuery();

								program.Id = (int)connection.LastInsertRowId;

								inserted = true;
							}

							transaction.Commit();
						}
					}

					connection.Close();
					_programsInsertCommand.Connection = null;
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute command: {0}{1}{2}{3}", 
					Environment.NewLine, (_programsInsertCommand != null) ? _programsInsertCommand.CommandText : "", 
					Environment.NewLine, ex.ToString()));

				return false;
			}

			return inserted;
		}
	}
}
