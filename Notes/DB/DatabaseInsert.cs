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
	}
}
