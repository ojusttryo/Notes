using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notes.CommonUIElements
{
	public static class InputEventHandler
	{
		public static void CheckNumeric(object sender, KeyPressEventArgs e)
		{
			// From https://ourcodeworld.com/articles/read/507/how-to-allow-only-numbers-inside-a-textbox-in-winforms-c-sharp
			// Verify that the pressed key isn't CTRL or any non-numeric digit
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
				e.Handled = true;
		}


		public static void CheckPasword(object sender, KeyPressEventArgs e)
		{
			// Пока запрещу только табы и пробелы.
			if (char.IsWhiteSpace(e.KeyChar))
				e.Handled = true;
		}


		public static void CheckDate(object sender, KeyPressEventArgs e)
		{
			TextBox birthdateTextBox = (TextBox)sender;
			if (birthdateTextBox == null)
				return;

			// TODO Можно в будущем сделать проверку на ввод. Но пока не решил, как же лучше. 
			// Может, \d{2}\.\d{2}\.\d{4}?
			// Но не хочется жестко фиксировать.
			// Ведь кто-то может не знать год рождения, а кто-то наоборот - только год знает. 
		}
	}
}
