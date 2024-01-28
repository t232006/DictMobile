using IndDictionary.addition;
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
	public partial class WordPage : ContentPage
	{
		dict focusedItem;
		//bool showAll = true;
		//WhatToShow wts = WhatToShow.alltogether;
		bool transl;
		ListView ListTable;
		//IEnumerable<string> passedList;
		//IEnumerable<dict> passedList2;
		
		public WordPage(bool _transl)
		{
			InitializeComponent();
			//ContentPage contPage = new ContentPage();
			//StackLayout resCont = new StackLayout();
			transl = _transl;

			ListTable = new ListView
			{
				HeightRequest = 40,
				ItemsSource = App.Database.showTableDict(true, WhatToShow.alltogether),
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

			NavigationButtons nb = new NavigationButtons();
			Button addBut = new Button
			{
				Text = "+",
				CornerRadius = 30,
				FontSize =16,
				HeightRequest = 60,
				WidthRequest = 60,
			};
			RelativeLayout relativeLayout = new RelativeLayout();
		
			ListTable.ItemTapped += OnPress;
			addBut.Pressed += OnAddPressed;
			
			relativeLayout.Children.Add(ListTable, Constraint.Constant(0), Constraint.Constant(0),
				Constraint.RelativeToParent((parent) => { return parent.Width; }),
				Constraint.RelativeToParent((parent) => { return parent.Height; }));
			relativeLayout.Children.Add(addBut,
				Constraint.RelativeToParent((parent) => { return parent.Width * 0.7; }),
				Constraint.RelativeToParent((parent) => { return parent.Height * 0.8; }),
				Constraint.Constant(60), Constraint.Constant(60)
				);
			relativeLayout.Children.Add(nb,
				Constraint.Constant(0),
				Constraint.RelativeToParent((parent) => { return parent.Height * 0.9; }));
			

			this.contPage.Content =  relativeLayout;
		}
		
		public void Refresh(bool _showAll, WhatToShow _wts)
		{
			ListTable.ItemsSource = App.Database.showTableDict(_showAll, _wts);
		}
		protected void OnPress(object sender, ItemTappedEventArgs e)
		{
			focusedItem = (dict)e.Item;
			FullInform fullinform = new FullInform(false);
			fullinform.BindingContext = focusedItem;
			//MainPage.TitleProperty.PropertyName =
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
		
		
	}
}