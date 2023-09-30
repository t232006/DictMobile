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
		bool showAll = true;
		WhatToShow wts = WhatToShow.alltogether;
		bool transl;
		ListView ListTable;
		//IEnumerable<string> passedList;
		//IEnumerable<dict> passedList2;
		
		public WordPage(bool _transl)
		{
			InitializeComponent();
			//ContentPage contPage = new ContentPage();
			StackLayout resCont = new StackLayout();
			transl = _transl;
			if (transl)
			{
				contPage.Title = "Translation";
				this.Title = "Translation";
			}
			else
			{
				contPage.Title = "Word";
				this.Title = "Word";
			}
				
			//RelativeLayout mainCont = new RelativeLayout();
			ListTable = new ListView
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
			Button addBut = new Button { Text = "Add" };
			ListTable.ItemTapped += OnPress;
			addBut.Pressed += OnAddPressed;

			/*mainCont.Children.Add(ListTable,
				Constraint.RelativeToParent((parent) => { return parent.Width; }),
				Constraint.RelativeToParent((parent) => { return parent.Height; }));
			mainCont.Children.Add(addBut,
				Constraint.RelativeToView(ListTable, (parent, view) => { return ListTable.X + 5; }),
				Constraint.RelativeToView(ListTable, (paren, view) => { return ListTable.Y + 10; }),
				Constraint.Constant(40), Constraint.Constant(20));*/
			
			resCont.Children.Add(ListTable); resCont.Children.Add(addBut);
			
			this.contPage.Content = resCont;
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
		
	}
}