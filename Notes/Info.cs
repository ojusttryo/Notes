using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes
{
	public class Info
	{
		public static string[] States = { "Not selected", "Active", "Deleted", "Finished", "Postponed", "Waiting" };


		protected static Dictionary<string, string> _tableNamesUI;


		protected static Dictionary<string, string> _tableNamesDB;


		public static string GetTableNameUI(string nameDB)
		{
			// No check. I want to see exception.
			return _tableNamesUI[nameDB];
		}


		public static string GetTableNameDB(string nameUI)
		{
			// No check. I want to see exception.
			return _tableNamesDB[nameUI];
		}


		public static string[] GetTableNamesDB()
		{
			return _tableNamesDB.Values.ToArray();
		}


		public static string[] GetTableNamesUI()
		{
			return _tableNamesUI.Values.ToArray();
		}


		static Info()
		{
			_tableNamesUI = new Dictionary<string, string>();
			_tableNamesUI.Add("Affairs",       "Affairs");
			_tableNamesUI.Add("AnimeFilms",    "Anime films");
			_tableNamesUI.Add("AnimeSerials",  "Anime serials");
			_tableNamesUI.Add("Bookmarks",     "Bookmarks");
			_tableNamesUI.Add("Desires",       "Desires");
			_tableNamesUI.Add("Films",         "Films");
			_tableNamesUI.Add("Games",         "Games");
			_tableNamesUI.Add("Literature",    "Literature");
			_tableNamesUI.Add("Meal",          "Meal");
			_tableNamesUI.Add("Performances",  "Performances");
			_tableNamesUI.Add("People",        "People");
			_tableNamesUI.Add("Programs",      "Programs");
			_tableNamesUI.Add("RegularDoings", "Regular doings");
			_tableNamesUI.Add("Serials",       "Serials");
			_tableNamesUI.Add("TVShows",       "TV shows");

			_tableNamesDB = new Dictionary<string, string>();
			foreach (KeyValuePair<string, string> pair in _tableNamesUI)
				_tableNamesDB.Add(pair.Value, pair.Key);
		}
	}
}
