using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Notes.Notes;

namespace Notes
{
	public class BookmarksImport
	{
		/// <summary>
		/// Импортирует закладки из браузеров. Работает с экспортом Firefox 64.0.2 в формате json.
		/// </summary>
		public List<Bookmark> ImportBookmarks(string fileName)
		{
			List<Bookmark> bookmarks = new List<Bookmark>();

			try
			{
				JObject json = JObject.Parse(File.ReadAllText(fileName));

				if (!IsMozillaBookmarks(json))
					return bookmarks;

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


		private bool IsMozillaBookmarks(JObject jsonRootObject)
		{
			return (jsonRootObject.Properties().Any(x => x.Name == "type" && x.Value.ToString() == "text/x-moz-place-container"));
		}
	}
}
