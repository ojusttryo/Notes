using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Notes
{
	public class Bookmark : AuthorizationNote
	{
		public string URL { get; set; }


		public Bookmark()
		{
			URL = string.Empty;
		}
	}
}
