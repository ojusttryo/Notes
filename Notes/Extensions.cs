using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Notes.Notes;
using Notes.NoteTables;
using static Notes.Info;

namespace Notes
{
	static class Extensions
	{
		public static bool EqualsInvariant(this string a, string b)
		{
			return string.Equals(a, b, StringComparison.InvariantCulture);
		}


		public static bool EqualsInvariantCI(this string a, string b)
		{
			return string.Equals(a, b, StringComparison.InvariantCultureIgnoreCase);
		}


		public static string Truncate(this string source, int maxLength)
		{
			return (source.Length <= maxLength) ? source : source.Substring(0, maxLength);
		}


		public static int ToIntOrDefault(this string source, int defaultValue = 0)
		{
			string temp = source.Trim();
			if (temp.Length == 0)
				return defaultValue;

			int n = 0;
			bool isNumeric = Int32.TryParse(temp, out n);

			return (isNumeric) ? n : defaultValue;
		}
		
		
		public static int ToIntOrException(this string source)
		{
			// В принципе этот метод без проблем заменяется Int32.Parse, но хочется явно подчеркнуть, что значение должно быть всегда правильное.
			return Int32.Parse(source);
		}


		public static Note.State ToNoteState(this string source, Note.State defaultValue = Note.State.All)
		{
			string temp = source.Trim();
			if (temp.Length == 0)
				return defaultValue;

			int index = Array.IndexOf(States, temp);
			return (Enum.IsDefined(typeof(Note.State), index)) ? (Note.State)index : defaultValue;
		}


		public static Game.PlayersCount ToPlayersCount(this string source, Game.PlayersCount defaultValue = Game.PlayersCount.NotDefined)
		{
			string temp = source.Trim();
			if (temp.Length == 0)
				return defaultValue;

			int index = Array.IndexOf(GameTable.PlayersCount, temp);
			return (Enum.IsDefined(typeof(Game.PlayersCount), index)) ? (Game.PlayersCount)index : defaultValue;
		}


		public static Person.PSex ToSex(this string source, Person.PSex defaultValue = Person.PSex.NotSelected)
		{
			string temp = source.Trim();
			if (temp.Length == 0)
				return defaultValue;

			int index = Array.IndexOf(PeopleTable.Sex, temp);
			return (Enum.IsDefined(typeof(Person.PSex), index)) ? (Person.PSex)index : defaultValue;
		}
	}
}