﻿using IndDictionary.addition;
using IndDictionary.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndDictionary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WordPage : FlyoutPage
	{
		dict focusedItem;
		bool showAll = true;
		WhatToShow wts = WhatToShow.alltogether;
		bool transl;
		ListView ListTable;
		//IEnumerable<string> passedList;
		//IEnumerable<dict> passedList2;
		public WordPage(bool _transl)
		{
			InitializeComponent();
			transl = _transl;
			/*ListTable = new ListView
			{
				HeightRequest = 40,
				ItemsSource = App.Database.showTableDict(showAll, wts),
				ItemTemplate = new DataTemplate(() =>
				{
					Label MainField = new Label
					{
						LineBreakMode = LineBreakMode.TailTruncation,
						FontSize = 14
					};
					if (transl)
						MainField.SetBinding(Label.TextProperty, "Translation");
					else
						MainField.SetBinding(Label.TextProperty, "Word");
					AbsoluteLayout.SetLayoutBounds(MainField, new Rectangle(10, 0, .68, AbsoluteLayout.AutoSize));
					AbsoluteLayout.SetLayoutFlags(MainField, AbsoluteLayoutFlags.WidthProportional);
					ExtSwitch extswitch = new ExtSwitch();
					extswitch.SetBinding(ExtSwitch.IDProperty, "Number");
					extswitch.SetBinding(ExtSwitch.IsToggledProperty, "Usersel");
					AbsoluteLayout.SetLayoutBounds(extswitch, new Rectangle(.9, 0, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
					AbsoluteLayout.SetLayoutFlags(extswitch, AbsoluteLayoutFlags.PositionProportional);
					return new ViewCell
					{
						View = new AbsoluteLayout
						{
							Children = {extswitch, MainField}
						}
					};
				}
				)
			};
			ListTable.ItemTapped += OnPress;*/
		}

		protected void Searching(Object sender, TextChangedEventArgs e)
		{
			IEnumerable<dict> founded = App.Database.findRecords(SearchEntry.Text, f=>f.Word);
			if (founded != null)
				ListTable.ItemsSource = founded;
			if (e.NewTextValue == "")
				ListTable.ItemsSource = App.Database.showTableDict(showAll,wts);
		}

		protected override void OnAppearing()
		{	
			base.OnAppearing();
		}
		protected void OnPress(object sender, ItemTappedEventArgs e)
		{
			focusedItem = (dict)e.Item;
			FullInform fullinform = new FullInform(false);
			fullinform.BindingContext = focusedItem;
			Navigation.PushAsync(fullinform);
		}
		protected void OnToggled(object sender, ToggledEventArgs e)
		{
			focusedItem = App.Database.findOneRecord((sender as ExtSwitch).ID);
			if (focusedItem !=null) focusedItem.Usersel = e.Value;
			App.Database.saveRecD(focusedItem);
		}
		protected void OnAddPressed(object sender, EventArgs e)
		{
			FullInform fullinform = new FullInform(true);
			Navigation.PushAsync(fullinform);
		}
		protected void OnChecking (object sender, EventArgs e)
		{
			showAll = !(sender as CheckBox).IsChecked;
			OnAppearing();
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
			OnAppearing();
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
		protected void onReset(object sender, EventArgs e)
		{
			App.Database.ResetSelection();
			OnAppearing();
		}
	}
}