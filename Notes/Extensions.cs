using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
	static class Extensions
	{
		public static bool EqualsInvariant(this string a, string b)
		{
			return string.Equals(a, b, StringComparison.InvariantCulture);
		}


		public static bool EqualsInvariantCI(this string a, string b)
		{
			return string.Equals(a, b, StringComparison.InvariantCultureIgnoreCase);
		}


		public static string Truncate(this string source, int maxLength)
		{
			return (source.Length <= maxLength) ? source : source.Substring(0, maxLength);
		}
	}
}
