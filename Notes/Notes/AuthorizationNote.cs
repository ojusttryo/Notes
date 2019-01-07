using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Notes.Notes
{
	public class AuthorizationNote : Note
	{
		public string Login { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }


		public AuthorizationNote()
		{
			Login = string.Empty;
			Password = string.Empty;
			Email = string.Empty;
		}
	}
}
