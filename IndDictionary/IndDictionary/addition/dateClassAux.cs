using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IndDictionary.addition
{
    public class dateClassAux :INotifyPropertyChanged
    {
		string date;
		bool spoted;

		public string Date
		{
			get { return date; }
			set
			{
				date = value;
				OnPropertyChanged("Date");
			}
		}
		public bool Spoted
		{
			get { return spoted; }
			set
			{
				spoted = value;
				OnPropertyChanged("Spoted");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged (string prop)
		{ 
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}	
}
