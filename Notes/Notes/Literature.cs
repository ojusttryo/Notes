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
		private int _pages;
		private int _currentPage;
		private int _volume;
		private int _chapter;


		public string Author { get; set; }


		/// <summary>
		/// Вселенная, в которой происходят события. 
		/// Например, могут быть разные серии книг про сталкеров, но это все происходит в одной вселенной - S.T.A.L.K.E.R.
		/// </summary>
		public string Universe { get; set; }


		public string Series { get; set; }


		public string Genre { get; set; }


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
				if (value >= 0)
					_currentPage = value;
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
			Author = string.Empty;
			Universe = string.Empty;
			Series = string.Empty;
			Genre = string.Empty;
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
