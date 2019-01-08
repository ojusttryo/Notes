using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Notes
{
	public class Meal : Note
	{
		public string Ingredients { get; set; }
		
		public string Recipe { get; set; }


		public Meal()
		{
			Ingredients = string.Empty;
			Recipe = string.Empty;
		}
	}
}
