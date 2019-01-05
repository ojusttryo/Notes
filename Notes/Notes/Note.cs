using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Notes
{
    public abstract class Note
    {
		public enum State
		{
			All = 0,
			Active = 1,
			Deleted = 2,
			Finished = 3,
			Postponed = 4,
			Waiting = 5,
		}


		public const int MAX_NAME_LEN = 255;
		public const int MAX_COMMENT_LEN = 32767;

        private string _name;
        private string _comment;
		

		public int Id { get; set; }


		public string Name
		{
			get { return _name; }
			set
			{
				if (String.IsNullOrEmpty(value))
					return;

				_name = value.Trim().Truncate(MAX_NAME_LEN);
			}
		}


		public State CurrentState { get; set; }


		public string Comment
		{
			get { return _comment; }
			set
			{
				if (value == null)
					return;

				_comment = value.Trim().Truncate(MAX_COMMENT_LEN);
			}
		}


		public Note()
		{
			Id = -1;	// В БД начинается с 0
			_name = String.Empty;
			CurrentState = State.All;
			Comment = String.Empty;
		}


        public bool IsValid()
        {
            return (!string.IsNullOrEmpty(Name));
        }
	}
}
