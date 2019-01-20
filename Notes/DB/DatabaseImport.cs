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
		/// Импортирует заметки из БД старой программы. 
		/// </summary>
		public static void Import(string filePath, string tableName)
		{
			Dictionary<int, Note.State> oldStates = new Dictionary<int, Note.State>();
			oldStates.Add(1, Note.State.Active);
			oldStates.Add(4, Note.State.Deleted);
			oldStates.Add(2, Note.State.Finished);
			oldStates.Add(3, Note.State.Postponed);
			oldStates.Add(0, Note.State.Waiting);

			switch (tableName)
			{
				case "anime": Insert("AnimeSerials", SelectFromOldTable(filePath, "anime", oldStates)); break;
				case "serials": Insert("Serials", SelectFromOldTable(filePath, "serials", oldStates)); break;
				case "books": Insert("Literature", SelectFromOldTable(filePath, "books", oldStates)); break;
				case "films": Insert("Films", SelectFromOldTable(filePath, "films", oldStates)); break;
				default: break;
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
