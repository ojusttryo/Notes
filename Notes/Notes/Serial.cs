using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Notes
{
	public class Serial : Note
	{
		public int Season { get; set; }


		public int Episode { get; set; }


		public Serial()
		{
			Season = 0;
			Episode = 0;
		}
	}
}
