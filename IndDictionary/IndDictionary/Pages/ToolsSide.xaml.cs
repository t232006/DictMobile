using IndDictionary.addition;
using IndDictionary.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndDictionary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ToolsSide : ContentPage
	{
		bool showAll = true;
		WhatToShow wts = WhatToShow.alltogether;
		ListView _ListTable;
		public ToolsSide ()
		{
			InitializeComponent ();
		}
		
		protected void OnAll(object sender, EventArgs e)
		{
			wts = WhatToShow.alltogether;
			//OnAppearing();
		}
		protected void OnPhrases(object sender, EventArgs e)
		{
			wts = WhatToShow.phrases;
			//OnAppearing();
		}
		protected void OnWords(object sender, EventArgs e)
		{
			wts = WhatToShow.words;
			//OnAppearing();
		}
		protected void onReset(object sender, EventArgs e)
		{
			App.Database.ResetSelection();
			//OnAppearing();
		}
		protected void OnChecking(object sender, EventArgs e)
		{
			showAll = !(sender as CheckBox).IsChecked;
			//OnAppearing();
		}
	}
}