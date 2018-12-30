using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Notes
{
	public static class Constant
	{
		/// <summary>
		/// Обычно используемый набор опций для регулярного выражения: CultureInvariant | IgnoreCase.
		/// </summary>
		public const RegexOptions CommonRegexOptions = (RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);


	}
}
