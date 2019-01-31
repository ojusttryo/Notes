using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Notes.Notes
{
	public class Affair : DescribedNote
	{
		public const string DateFormat = "dd MM yyyy";

		public DateTime Date { get; set; }

		public bool IsDateSet { get; set; }


		public Affair()
		{
			Date = DateTime.Now;
			IsDateSet = false;
		}


		public void SetDate(string date)
		{
			if (string.IsNullOrEmpty(date))
				return;

			if (Regex.IsMatch(date, @"\d{2} \d{2} \d{4}", RegexOptions.CultureInvariant))
				Date = DateTime.ParseExact(date, DateFormat, CultureInfo.InvariantCulture);
		}


		public string GetDate()
		{
			return Date.ToString(DateFormat, CultureInfo.InvariantCulture);
		}
	}
}
