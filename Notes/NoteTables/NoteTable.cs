using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Notes.NoteTables
{
	public abstract class NoteTable : DataGridView
	{
		public abstract void Initialize();

		public abstract void ChangeSize(Size tableSize);
	}
}
