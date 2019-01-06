using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Notes.Notes
{
	/// <summary>
	/// Литература (книга, рассказ и т.п.)
	/// </summary>
    class Literature : DatedNote, ICloneable
    {
		private int MAX_AUTHOR_LEN = 255;
		private int MAX_UNIVERSE_LEN = 255;
		private int MAX_SERIES_LEN = 255;
		private int MAX_GENRE_LEN = 255;


		private string _author;
		private string _universe;
		private string _series;
		private string _genre;
		private int _pages;
		private int _currentPage;
		private int _volume;
		private int _chapter;


		public string Author
		{
            get { return _author; }
			set
			{
				if (String.IsNullOrEmpty(value))
					return;

				_author = value.Trim().Truncate(MAX_AUTHOR_LEN);
			}
		}


		/// <summary>
		/// Вселенная, в которой происходят события. 
		/// Например, могут быть разные серии книг про сталкеров, но это все происходит в одной вселенной - S.T.A.L.K.E.R.
		/// </summary>
		public string Universe
		{
			get { return _universe; }
			set
			{
				if (value == null)
					return;

				_universe = value.Trim().Truncate(MAX_UNIVERSE_LEN);
			}
		}


		public string Series
		{
			get { return _series; }
			set
			{
				if (value == null)
					return;

				_series = value.Trim().Truncate(MAX_SERIES_LEN);
			}
		}


		public string Genre
		{
			get { return _genre; }
			set
			{
				if (value == null)
					return;

				_genre = value.Trim().Truncate(MAX_GENRE_LEN);
			}
		}


		public int Pages
		{
			get { return _pages; }
			set
            {
                if (value >= 0)
                    _pages = value;
            }
		}


		public int Page
		{
			get { return _currentPage; }
			set
			{
				if (value >= 0 && value <= _pages)
					_currentPage = value;
                else if (value > _pages)
					_currentPage = _pages;
			}
		}


		public int Volume
        {
            get { return _volume; }
            set
            {
                if (value >= 0)
                    _volume = value;
            }
        }


		public int Chapter
        {
            get { return _chapter; }
            set
            {
                if (value >= 0)
                    _chapter = value;
            }
        }


		public Literature()
		{
			_author = string.Empty;
			_universe = string.Empty;
			_series = string.Empty;
			_genre = string.Empty;
			_pages = 0;
			_currentPage = 0;
			_volume = 0;
			_chapter = 0;
		}


		public object Clone()
		{
			return this.MemberwiseClone();
		}
    }
}
