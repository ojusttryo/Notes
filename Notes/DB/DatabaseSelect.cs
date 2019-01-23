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
						connection.Open();
						command.Connection = connection;
						if (connection.State == System.Data.ConnectionState.Open)
						{
							using (SQLiteDataReader reader = command.ExecuteReader())
							{
								if (reader.Read() && reader.FieldCount > 0)
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
				Log.Error(string.Format("Can not select max Id from {0}.{1}{2}", tableName, Environment.NewLine, ex.ToString()));
				return maxId;
			}
		}


		/// <summary>
		/// Извлекает уникальные (distinct) значения из поля таблицы.
		/// </summary>
		public static List<string> SelectUniqueValues(string tableName, string fieldName)
		{
			List<string> values = new List<string>();

			try
			{
				using (SQLiteCommand command = new SQLiteCommand(string.Format("SELECT DISTINCT {0} FROM {1}", fieldName, tableName)))
				{
					using (SQLiteConnection connection = CreateConnection())
					{
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


		/// <summary>
		/// Получить все заметки из таблицы. Заметки сортируются по имени.
		/// </summary>
		public static List<Note> SelectNotes(string tableName)
		{
			string commandText = string.Format("SELECT * FROM {0} ORDER BY Name", tableName);
            SQLiteCommand command = new SQLiteCommand(commandText);

			List<Note> notes = ReadNotes(command, tableName);

			return notes;
		}


		/// <summary>
		/// Подключается к БД и считывает заметки. Результат никогда не равен null.
		/// </summary>
		private static List<Note> ReadNotes(SQLiteCommand selectCommand, string tableName)
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
									case "AnimeFilms":   notes = ReadDatedNotes(reader); break;
									case "AnimeSerials": notes = ReadSerials(reader); break;
									case "Bookmarks":    notes = ReadBookmarks(reader); break;
									case "Desires":      notes = ReadDesires(reader); break;
									case "Films":        notes = ReadDatedNotes(reader); break;
									case "Games":        notes = ReadGames(reader); break;
									case "Literature":   notes = ReadLiterature(reader); break;
									case "Meal":         notes = ReadMeal(reader); break;
									case "Performances": notes = ReadDatedNotes(reader); break;
									case "People":       notes = ReadPeople(reader); break;
									case "Programs":     notes = ReadPrograms(reader); break;
									case "Serials":      notes = ReadSerials(reader); break;									
									case "TVShows":      notes = ReadSerials(reader); break;
									default: break;
								}
							}
						}

						connection.Close();

						return notes;
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not execute select command:{0}{1}{2}{3}",
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
				Log.Error(string.Format("Can not read dated notes:{0}{1}",
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
				Log.Error(string.Format("Can not read programs:{0}{1}",
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
					game.Players = (Game.PlayersCount)reader.GetInt32(10);

					notes.Add(game);
				}

				return notes;
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not read games:{0}{1}",
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
				Log.Error(string.Format("Can not read literature:{0}{1}",
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
				Log.Error(string.Format("Can not read bookmarks:{0}{1}",
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
				Log.Error(string.Format("Can not read meal:{0}{1}",
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
				Log.Error(string.Format("Can not read people:{0}{1}",
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
				Log.Error(string.Format("Can not read serials:{0}{1}",
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
				Log.Error(string.Format("Can not read desires:{0}{1}",
					Environment.NewLine, ex.ToString()));
				return new List<Note>();
			}
		}
	}
}
