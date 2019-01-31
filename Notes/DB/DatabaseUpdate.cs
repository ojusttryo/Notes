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
		public static bool Update(string tableName, Note note)
		{
			switch (tableName)
			{
				case "Affairs":       return UpdateAffair(note);
				case "AnimeFilms":    return UpdateDatedNote(note, _animeFilmsUpdateCommand);
				case "AnimeSerials":  return UpdateSerial(note, _animeSerialsUpdateCommand);
				case "Bookmarks":     return UpdateBookmark(note);
				case "Desires":       return UpdateDescribedNote(note, _desiresUpdateCommand);
				case "Films":         return UpdateDatedNote(note, _filmsUpdateCommand);
				case "Games":         return UpdateGame(note);
				case "Literature":    return UpdateLiterature(note);
				case "Meal":          return UpdateMeal(note);				
				case "Performances":  return UpdateDatedNote(note, _performancesUpdateCommand);
				case "People":        return UpdatePerson(note);
				case "Programs":      return UpdateProgram(note);
				case "RegularDoings": return UpdateDescribedNote(note, _regularDoingsUpdateCommand);
				case "Serials":       return UpdateSerial(note, _serialsUpdateCommand);
				case "TVShows":       return UpdateSerial(note, _TVShowsUpdateCommand);
				default: return false;
			}
		}

	
		private static bool UpdateAffair(Note note)
		{
			Affair affair = note as Affair;
			if (affair == null || affair.Id < 0)
			{
				Log.Error("Try to save incorrect affair note");
				return false;
			}

			_affairsUpdateCommand.Parameters[0].Value = affair.Id;
			_affairsUpdateCommand.Parameters[1].Value = affair.Name;
			_affairsUpdateCommand.Parameters[2].Value = (int)affair.CurrentState;
			_affairsUpdateCommand.Parameters[3].Value = affair.Comment;
			_affairsUpdateCommand.Parameters[4].Value = affair.Description;
			_affairsUpdateCommand.Parameters[5].Value = affair.IsDateSet;
			_affairsUpdateCommand.Parameters[6].Value = affair.GetDate();

			_affairsUpdateCommand.Prepare();

			return ExecuteNonQuery(_affairsUpdateCommand) == 1;
		}


		private static bool UpdateDatedNote(Note note, SQLiteCommand datedNoteUpdateCommand)
		{
			DatedNote datedNote = note as DatedNote;
			if (datedNote == null || datedNote.Id < 0)
			{
				Log.Error("Try to save incorrect dated note");
				return false;
			}

			datedNoteUpdateCommand.Parameters[0].Value = datedNote.Id;
			datedNoteUpdateCommand.Parameters[1].Value = datedNote.Name;
			datedNoteUpdateCommand.Parameters[2].Value = (int)datedNote.CurrentState;
			datedNoteUpdateCommand.Parameters[3].Value = datedNote.Comment;
			datedNoteUpdateCommand.Parameters[4].Value = datedNote.Year;

			datedNoteUpdateCommand.Prepare();

			return ExecuteNonQuery(datedNoteUpdateCommand) == 1;
		}


		private static bool UpdateSerial(Note note, SQLiteCommand serialsUpdateCommand)
		{
			Serial serial = note as Serial;
			if (serial == null || serial.Id < 0)
			{
				Log.Error("Try to save incorrect serial note");
				return false;
			}

			serialsUpdateCommand.Parameters[0].Value = serial.Id;
			serialsUpdateCommand.Parameters[1].Value = serial.Name;
			serialsUpdateCommand.Parameters[2].Value = (int)serial.CurrentState;
			serialsUpdateCommand.Parameters[3].Value = serial.Comment;
			serialsUpdateCommand.Parameters[4].Value = serial.Season;
			serialsUpdateCommand.Parameters[5].Value = serial.Episode;

			serialsUpdateCommand.Prepare();

			return ExecuteNonQuery(serialsUpdateCommand) == 1;
		}


		private static bool UpdateBookmark(Note note)
		{
			Bookmark b = note as Bookmark;
			if (b == null || b.Id < 0)
			{
				Log.Error("Try to save incorrect bookmark note");
				return false;
			}

			_bookmarksUpdateCommand.Parameters[0].Value = b.Id;
			_bookmarksUpdateCommand.Parameters[1].Value = b.Name;
			_bookmarksUpdateCommand.Parameters[2].Value = (int)b.CurrentState;
			_bookmarksUpdateCommand.Parameters[3].Value = b.Comment;
			_bookmarksUpdateCommand.Parameters[4].Value = b.URL;
			_bookmarksUpdateCommand.Parameters[5].Value = b.Login;
			_bookmarksUpdateCommand.Parameters[6].Value = b.Password;
			_bookmarksUpdateCommand.Parameters[7].Value = b.Email;

			_bookmarksUpdateCommand.Prepare();

			return ExecuteNonQuery(_bookmarksUpdateCommand) == 1;
		}


		private static bool UpdateDescribedNote(Note note, SQLiteCommand describedNoteUpdateCommand)
		{
			DescribedNote describedNote = note as DescribedNote;
			if (describedNote == null || describedNote.Id < 0)
			{
				Log.Error("Try to save incorrect described note note");
				return false;
			}

			describedNoteUpdateCommand.Parameters[0].Value = describedNote.Id;
			describedNoteUpdateCommand.Parameters[1].Value = describedNote.Name;
			describedNoteUpdateCommand.Parameters[2].Value = (int)describedNote.CurrentState;
			describedNoteUpdateCommand.Parameters[3].Value = describedNote.Comment;
			describedNoteUpdateCommand.Parameters[4].Value = describedNote.Description;

			describedNoteUpdateCommand.Prepare();

			return ExecuteNonQuery(describedNoteUpdateCommand) == 1;
		}


		private static bool UpdateGame(Note note)
		{
			Game game = note as Game;
			if (game == null || game.Id < 0)
			{
				Log.Error("Try to save incorrect game note");
				return false;
			}

			_gamesUpdateCommand.Parameters[0].Value = game.Id;
			_gamesUpdateCommand.Parameters[1].Value = game.Name;
			_gamesUpdateCommand.Parameters[2].Value = (int)game.CurrentState;
			_gamesUpdateCommand.Parameters[3].Value = game.Comment;
			_gamesUpdateCommand.Parameters[4].Value = game.DownloadLink;
			_gamesUpdateCommand.Parameters[5].Value = game.Version;
			_gamesUpdateCommand.Parameters[6].Value = game.Login;
			_gamesUpdateCommand.Parameters[7].Value = game.Password;
			_gamesUpdateCommand.Parameters[8].Value = game.Email;
			_gamesUpdateCommand.Parameters[9].Value = game.Genre;
			_gamesUpdateCommand.Parameters[10].Value = (int)game.Players;

			_gamesUpdateCommand.Prepare();

			return ExecuteNonQuery(_gamesUpdateCommand) == 1;
		}
		

		private static bool UpdateLiterature(Note note)
		{
			Literature lit = note as Literature;
			if (lit == null || lit.Id < 0)
			{
				Log.Error("Try to save incorrect literature note");
				return false;
			}

			_literatureUpdateCommand.Parameters[0].Value = lit.Id;
			_literatureUpdateCommand.Parameters[1].Value = lit.Name;
			_literatureUpdateCommand.Parameters[2].Value = (int)lit.CurrentState;
			_literatureUpdateCommand.Parameters[3].Value = lit.Comment;
			_literatureUpdateCommand.Parameters[4].Value = lit.Year;
			_literatureUpdateCommand.Parameters[5].Value = lit.Author;
			_literatureUpdateCommand.Parameters[6].Value = lit.Genre;
			_literatureUpdateCommand.Parameters[7].Value = lit.Universe;
			_literatureUpdateCommand.Parameters[8].Value = lit.Series;
			_literatureUpdateCommand.Parameters[9].Value = lit.Volume;
			_literatureUpdateCommand.Parameters[10].Value = lit.Chapter;
			_literatureUpdateCommand.Parameters[11].Value = lit.Page;
			_literatureUpdateCommand.Parameters[12].Value = lit.Pages;

			_literatureUpdateCommand.Prepare();

			return ExecuteNonQuery(_literatureUpdateCommand) == 1;
		}


		private static bool UpdateMeal(Note note)
		{
			Meal meal = note as Meal;
			if (meal == null || meal.Id < 0)
			{
				Log.Error("Try to save incorrect meal note");
				return false;
			}

			_mealUpdateCommand.Parameters[0].Value = meal.Id;
			_mealUpdateCommand.Parameters[1].Value = meal.Name;
			_mealUpdateCommand.Parameters[2].Value = (int)meal.CurrentState;
			_mealUpdateCommand.Parameters[3].Value = meal.Comment;
			_mealUpdateCommand.Parameters[4].Value = meal.Ingredients;
			_mealUpdateCommand.Parameters[5].Value = meal.Recipe;

			_mealUpdateCommand.Prepare();

			return ExecuteNonQuery(_mealUpdateCommand) == 1;
		}


		private static bool UpdatePerson(Note note)
		{
			Person person = note as Person;
			if (person == null || person.Id < 0)
			{
				Log.Error("Try to save incorrect person note");
				return false;
			}			

			_peopleUpdateCommand.Parameters[0].Value = person.Id;
			_peopleUpdateCommand.Parameters[1].Value = person.Name;
			_peopleUpdateCommand.Parameters[2].Value = (int)person.CurrentState;
			_peopleUpdateCommand.Parameters[3].Value = person.Comment;
			_peopleUpdateCommand.Parameters[4].Value = person.Address;
			_peopleUpdateCommand.Parameters[5].Value = person.Birthdate;
			_peopleUpdateCommand.Parameters[6].Value = person.Nickname;
			_peopleUpdateCommand.Parameters[7].Value = person.Contacts;
			_peopleUpdateCommand.Parameters[8].Value = person.Sex;

			_peopleUpdateCommand.Prepare();

			return ExecuteNonQuery(_peopleUpdateCommand) == 1;
		}


		private static bool UpdateProgram(Note note)
		{
			Program program = note as Program;
			if (program == null || program.Id < 0)
			{
				Log.Error("Try to save incorrect program note");
				return false;
			}

			_programsUpdateCommand.Parameters[0].Value = program.Id;
			_programsUpdateCommand.Parameters[1].Value = program.Name;
			_programsUpdateCommand.Parameters[2].Value = (int)program.CurrentState;
			_programsUpdateCommand.Parameters[3].Value = program.Comment;
			_programsUpdateCommand.Parameters[4].Value = program.DownloadLink;
			_programsUpdateCommand.Parameters[5].Value = program.Version;
			_programsUpdateCommand.Parameters[6].Value = program.Login;
			_programsUpdateCommand.Parameters[7].Value = program.Password;
			_programsUpdateCommand.Parameters[8].Value = program.Email;

			_programsUpdateCommand.Prepare();

			return ExecuteNonQuery(_programsUpdateCommand) == 1;
		}
	}
}
