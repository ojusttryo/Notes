using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CsQuery.HtmlParser;

using Notes.Notes;
using Notes;

namespace Notes.Import
{
	public class BookmarksImport
	{
		private string FileName { get; set; }


		public BookmarksImport(string fileName)
		{
			FileName = fileName;
		}


		/// <summary>
		/// Импортирует закладки из браузеров. 
		/// Работа проверена на Firefox 64.0.2 (json), IE 11.0.9600, Opera 57.0.3098.
		/// </summary>
		public List<Bookmark> ImportBookmarks()
		{
			if (IsMozillaBookmarks())
				return ImportFromMozillaJson();
			else if (IsInternetExplorerBookmarks() || IsOperaBookmarks())
				return ImportFromHtml();
			else
				return new List<Bookmark>();
		}


		private bool IsMozillaBookmarks()
		{
			if (!FileName.EndsWith(".json", StringComparison.InvariantCultureIgnoreCase))
				return false;

			JObject json = JObject.Parse(File.ReadAllText(FileName));

			return (json.Properties().Any(x => x.Name == "type" && x.Value.ToString() == "text/x-moz-place-container"));
		}


		private bool IsInternetExplorerBookmarks()
		{
			// Пока других особых признаков не нашел.
			return (FileName.EndsWith(".htm", StringComparison.InvariantCultureIgnoreCase));
		}


		private bool IsOperaBookmarks()
		{
			// Пока других особых признаков не нашел.
			return (FileName.EndsWith(".html", StringComparison.InvariantCultureIgnoreCase));
		}


		private List<Bookmark> ImportFromMozillaJson()
		{
			List<Bookmark> bookmarks = new List<Bookmark>();

			try
			{				
				JObject json = JObject.Parse(File.ReadAllText(FileName));

				// Нужно найти все объекты, у которых нет потомков (скорей всего закладка) и есть свойства title и uri

				List<JObject> toParse = new List<JObject>();
				Stack<JObject> toSearch = new Stack<JObject>();
				foreach (JProperty property in json.Properties())
				{
					if (property.Name == "children" && property.Value.Type == JTokenType.Array)
					{
						JArray children = property.Value.ToObject<JArray>();
						foreach (JObject obj in children.Children<JObject>())
							toSearch.Push(obj);
					}
				}

				while (toSearch.Count > 0)
				{
					JObject current = toSearch.Pop();

					foreach (JProperty property in current.Properties())
					{
						if (property.Value.Type == JTokenType.Array)
						{
							// По идее, в children всегда только объекты. Они и нужны.
							if (property.Name == "children")
							{
								JArray children = property.Value.ToObject<JArray>();
								foreach (JObject obj in children.Children<JObject>())
								{
									// Только у закладок нет массивов children и вложенных объектов
									if (obj.Properties().All(x => x.Name != "children" && x.Type != JTokenType.Object))
										toParse.Add(obj);
									// Все остальные объекты - потенциальные родительские узлы закладок.
									else
										toSearch.Push(obj);
								}
							}
						}
						// Отдельных объектов в объектах пока не встречалось, только массивы. Но вдруг? 
						// На всякий случай оставлю пока этот кусок кода.
						else if (property.Value.Type == JTokenType.Object)
						{
							JObject obj = property.Value.ToObject<JObject>();

							// Только у закладок нет массивов children и вложенных объектов
							if (obj.Properties().All(x => x.Name != "children" && x.Type != JTokenType.Object))
								toParse.Add(obj);
							// Все остальные объекты - потенциальные родительские узлы закладок.
							else
								toSearch.Push(obj);
						}
					}
				}

				// Для закладок должны быть свойства title и uri
				toParse = toParse.Where(x => x.Properties().Any(p => p.Name == "title") && x.Properties().Any(p => p.Name == "uri")).ToList();

				foreach (JObject obj in toParse)
				{
					Bookmark b = new Bookmark();
					b.Name = obj["title"].ToString();
					b.URL = obj["uri"].ToString();
					bookmarks.Add(b);
				}
			}
			catch (Exception ex)
			{
				Log.Error(string.Format("Can not import mozilla bookmarks: {0}{1}", Environment.NewLine, ex.ToString()));
			}

			return bookmarks;
		}

		
		private List<Bookmark> ImportFromHtml()
		{
			// Для парсинга html выбрана библиотека CsQuery, т.к. другие варианты немного не подходят. 
			// AngleSharp требует более высокую версию .NET Framework, а HtmlAgilityPack содержит баги и больше не поддерживается.
			// https://habr.com/en/post/273807/#AngleSharp
			// https://ru.stackoverflow.com/questions/420354/%D0%9A%D0%B0%D0%BA-%D1%80%D0%B0%D1%81%D0%BF%D0%B0%D1%80%D1%81%D0%B8%D1%82%D1%8C-html-%D0%B2-net

			List<Bookmark> bookmarks = new List<Bookmark>();

			CsQuery.CQ cq = CsQuery.CQ.Create(File.ReadAllText(FileName));
			foreach (CsQuery.IDomObject obj in cq.Find("a"))
			{
				if (obj.HasAttribute("href"))
				{
					Bookmark b = new Bookmark();

					b.URL = obj.GetAttribute("href");
					// Не английский текст выводится в виде кодов символов. Нужно декодировать.
					b.Name = System.Net.WebUtility.HtmlDecode(obj.InnerText);

					bookmarks.Add(b);
				}				
			}

			return bookmarks;
		}
	}
}
