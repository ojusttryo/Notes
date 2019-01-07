using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Notes
{
	public class Bookmark : Note
	{
		public string URL { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }


		public Bookmark()
		{
			URL = string.Empty;
			Login = string.Empty;
			Password = string.Empty;
			Email = string.Empty;
		}
	}
}
