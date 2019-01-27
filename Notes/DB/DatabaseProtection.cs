using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace Notes.DB
{
	public partial class Database
	{
		/// <summary>
		/// Проверяет, установлен ли пароль для базы данных. По результату устанавливает значение свойства <c>IsPasswordProtected</c>.
		/// </summary>
		public static void CheckPassword()
		{
			try
			{
				using (SQLiteConnection con = CreateConnection())
				{
					con.Open();

					// Для проверки, что БД содержит пароль, нужно выполнить какую-то команду, 
					// т.к. иначе после вызова Open con.State будет Open, а con.ResultCode() == Ok даже при установленном пароле.
					using (SQLiteCommand command = new SQLiteCommand("PRAGMA schema_version;", con))
					{
						command.ExecuteScalar();
					}

					IsPasswordProtected = false;
				}
			}
			catch (Exception)
			{
				IsPasswordProtected = true;
			}
		}


		/// <summary>
		/// Проверяет, что выставлен правильный пароль (или его нет), и можно подключиться к базе данных. 
		/// </summary>
		public static bool PasswordIsOk()
		{
			try
			{
				using (SQLiteConnection con = CreateConnection())
				{
					con.Open();

					using (SQLiteCommand command = new SQLiteCommand("PRAGMA schema_version;", con))
					{
						command.ExecuteScalar();
					}

					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}


		/// <summary>
		/// Меняет пароль для базы данных. Также используется для его первой установки.
		/// </summary>
		/// <param name="password">Новый пароль. Если пароль пустой, то защита убирается.</param>
		public static void ChangePassword(string password)
		{
			try
			{
				password = password.Trim();

				SQLiteConnection con = CreateConnection();
				con.Open();
				con.ChangePassword(password);
				con.Close();
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Cannot change password:{0}{1}", Environment.NewLine, ex.ToString()));
			}
		}
	}
}
