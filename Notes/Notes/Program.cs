using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Notes
{
	public class Program : AuthorizationNote
	{
		public string DownloadLink { get; set; }

		public string Version { get; set; }


		public Program()
		{
			DownloadLink = string.Empty;
			Version = string.Empty;
		}
	}
}
