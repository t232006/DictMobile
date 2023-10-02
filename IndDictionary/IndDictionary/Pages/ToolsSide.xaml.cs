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
	public partial class ToolsSide : FlyoutPage
	{
		bool showAll = true;
		WhatToShow wts = WhatToShow.alltogether;
		ListView _ListTable;
		public ToolsSide (bool _transl)
		{
			InitializeComponent ();
			if (_transl) Title = "Translation"; else Title = "Word"; 
			Detail = new NavigationPage(new WordPage(_transl));
			if (Device.RuntimePlatform == Device.UWP)
			{
				FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;
			}
		}
		protected void Action()
		{
			
			((Detail as NavigationPage).RootPage as WordPage).Refresh(showAll, wts);
		}

		protected void OnAll(object sender, EventArgs e)
		{
			wts = WhatToShow.alltogether;
			Action();
		}
		protected void OnPhrases(object sender, EventArgs e)
		{
			wts = WhatToShow.phrases;
			Action();
		}
		protected void OnWords(object sender, EventArgs e)
		{
			wts = WhatToShow.words;
			Action();
		}
		protected void onReset(object sender, EventArgs e)
		{
			App.Database.ResetSelection();
			//OnAppearing();
		}
		protected void OnChecking(object sender, EventArgs e)
		{
			showAll = !(sender as CheckBox).IsChecked;
			Action();
		}
		
		protected void onDates(object sender, EventArgs e)
		{
			List<DateOrTopicClassAux> conteiner = new List<DateOrTopicClassAux>();
			IEnumerable<dict> tempcont = App.Database.showTopicsDates<dict>(!ShowSelected.IsChecked);
			foreach (dict t in tempcont)
			{
				conteiner.Add(new DateOrTopicClassAux { DaOrTo = t.DateRec, Spoted = false });
			}
			DateTopicForm dateForm = new DateTopicForm(conteiner, WhatToSelect.dates);
			Navigation.PushAsync(dateForm);
		}

		protected void onTopics(object sender, EventArgs e)
		{
			List<DateOrTopicClassAux> conteiner = new List<DateOrTopicClassAux>();
			IEnumerable<topic> tempcont = App.Database.showTopicsDates<topic>(!ShowSelected.IsChecked);
			foreach (topic t in tempcont)
			{
				conteiner.Add(new DateOrTopicClassAux { DaOrTo = t.Name, Spoted = false });
			}
			DateTopicForm topicForm = new DateTopicForm(conteiner, WhatToSelect.topics);
			Navigation.PushAsync(topicForm);
		}
		protected void Searching(Object sender, TextChangedEventArgs e)
		{
			IEnumerable<dict> founded = App.Database.findRecords(SearchEntry.Text, f => f.Word);
			if (founded != null)
				_ListTable.ItemsSource = founded;
			if (e.NewTextValue == "")
				_ListTable.ItemsSource = App.Database.showTableDict(showAll, wts);
		}
	}
}