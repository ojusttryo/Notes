using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Notes
{
    public class DatedNote: Note
    {
		private int _year;


		public int Year
		{
			get { return _year; }
			set
			{
				if (value >= 0)
					_year = value;
			}
		}


		public DatedNote()
		{
			Year = 0;
		}
	}
}
