using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Notes
{
	public class Game: Program
	{
		public enum PlayersCount
		{
			NotDefined,
			Singleplayer,
			Mutiplayer,
			Mixed
		}


		public string Genre { get; set; }


		public PlayersCount Players { get; set; }


		public Game()
		{
			Genre = string.Empty;
			Players = PlayersCount.NotDefined;
		}
	}
}
