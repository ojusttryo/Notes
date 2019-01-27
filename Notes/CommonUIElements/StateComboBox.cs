using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Notes.CommonUIElements
{
	[ToolboxItem(true)]
	[DesignTimeVisible(true)]
	public class StateComboBox : ComboBox
	{
		public StateComboBox()
		{
			BackColor = System.Drawing.Color.White;
			DropDownStyle = ComboBoxStyle.DropDownList;
			FlatStyle = FlatStyle.Popup;

			// I add items to combobox here, but VS also adds them in InitializeComponent().
			// So if the duplicates found, you need to remove them from InitializeComponent().
			Items.AddRange(Info.States);
			SelectedIndex = 0;
		}
	}
}
