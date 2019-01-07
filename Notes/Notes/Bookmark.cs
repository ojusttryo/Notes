using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Notes
{
	public class Bookmark : Note
	{
		protected int MAX_URL_LEN = 32768;

		private string _url;


		public string URL
		{
			get { return _url; }
			set
			{
				if (value == null)
					return;

				_url = value.Trim().Truncate(MAX_URL_LEN);
			}
		}


		public string Login { get; set; }


		public string Password { get; set; }


		public string Email { get; set; }


		public Bookmark()
		{
			_url = string.Empty;
			Login = string.Empty;
			Password = string.Empty;
			Email = string.Empty;
		}
	}
}
