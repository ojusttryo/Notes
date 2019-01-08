using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Notes
{
	public class Person : Note
	{
		// TODO придумать нормальное название для перечисления. Sexual identity, Gender - это не то.
		public enum PSex
		{
			NotSelected = 0,
			Male,
			Female
		}


		public string Address { get; set; }

		// Дату рождения решил хранить в строке, т.к. не всегда известен год или точная дата.
		public string Birthdate { get; set; }

		public string Nickname { get; set; }

		public string URL { get; set; }

		public string Contacts { get; set; }

		public PSex Sex { get; set; }


		public Person()
		{
			Address = string.Empty;
			Birthdate = string.Empty;
			Nickname = string.Empty;
			URL = string.Empty;
			Contacts = string.Empty;
			Sex = PSex.NotSelected;
		}
	}
}
