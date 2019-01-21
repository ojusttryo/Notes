using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Notes
{
	/// <summary>
	/// Класс лога. Служит для вывода сообщений о ходе выполнения программы.
	/// </summary>
	public class Log
	{
		/// <summary>
		/// Объект для синхронизации. Предотвращает одновременное обращение к файлу из разных потоков.
		/// </summary>
		private static object _logObject;


		/// <summary>
		/// Имя лог файла.
		/// </summary>
		private const string FileName = "NotesLog.log";


		/// <summary>
		/// Статический конструктор. Создает лог файл.
		/// </summary>
		static Log()
		{
			try
			{
				_logObject = new object();

				if (!File.Exists(FileName))
				{
					using (FileStream fs = File.Create(FileName))
					{
						fs.Close();
					}
				}
			}
			catch (Exception)
			{
				// Что-то не так с созданием файла. Исключение игнорируется, т.к. его некуда выводить.
			}
		}


		/// <summary>
		/// Сообщение об ошибке в программе.
		/// </summary>
		/// <param name="message">Текст сообщения.</param>
		public static void Error(string message)
		{
			Write(string.Format("Error: {0}", message));
		}


		/// <summary>
		/// Информационное сообщение (что-то где-то случилось, вызвался метод и т.п.).
		/// </summary>
		/// <param name="message">Текст сообщения.</param>
		public static void Info(string message)
		{
			Write(string.Format("Info: {0}", message));
		}


		/// <summary>
		/// Записывает сообщение в лог файл.
		/// </summary>
		/// <param name="message">Текст сообщения.</param>
		private static void Write(string message)
		{
			try
			{
				lock (_logObject)
				{
					string dateTime = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

					using (StreamWriter sw = new StreamWriter(FileName, true))
					{
						sw.WriteLine(string.Format("{0}: {1}", dateTime, message));
					}
				}
			}
			catch (Exception)
			{
				// Что-то не так с файлом или записью в него. Исключение игнорируется, т.к. его некуда выводить.
			}
		}
	}
}
