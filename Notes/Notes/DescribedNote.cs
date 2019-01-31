using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Notes
{
	public class DescribedNote : Note
	{
		public string Description { get; set; }


		public DescribedNote()
		{
			Description = string.Empty;
		}
	}
}
