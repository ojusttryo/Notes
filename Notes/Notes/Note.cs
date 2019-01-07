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
		

		public int Id { get; set; }


		public string Name { get; set; }


		public State CurrentState { get; set; }


		public string Comment { get; set; }


		public Note()
		{
			// В БД идентификаторы начинается с 0. Здесь нужно значение, не входящее в тот диапазон.
			Id = -1;	
			Name = String.Empty;
			CurrentState = State.All;
			Comment = String.Empty;
		}
	}
}
