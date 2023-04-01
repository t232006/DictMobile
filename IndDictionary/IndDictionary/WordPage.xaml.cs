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
	public partial class WordPage : ContentPage
	{
		
		public WordPage ()
		{
			InitializeComponent ();

			ListView ListTable = new ListView
			{
				HasUnevenRows = false,
				RowHeight = 40,
				ItemsSource = App.Database.showTableDict(),
				ItemTemplate = new DataTemplate(() =>
				{
					Label Field1 = new Label();
					Field1.SetBinding(Label.TextProperty, "Word");

					Button SelectItem = new Button();
					ExtImageButton SelectItem1 = new ExtImageButton();
					SelectItem.SetBinding(Button.TextProperty, "Usersel");
					SelectItem1.SetBinding(ExtImageButton.CheckedProperty, "Usersel");
					SelectItem.Style = (Style)Resources["ButtonStyle"];
					
					SelectItem1.Style = (Style)Resources["ButtonStyle"];
					
					Label l1 = new Label();
					/*l1.BindingContext = SelectItem1;
					l1.SetBinding(Label.TextProperty, "Checked");*/
					l1.Text = SelectItem.Text;

					
					AbsoluteLayout view = new AbsoluteLayout();
					view.Children.Add(Field1);
					StackLayout sl1 = new StackLayout
					{
						Children = {SelectItem, SelectItem1, l1},
						Orientation = StackOrientation.Horizontal
					};
					AbsoluteLayout.SetLayoutFlags(sl1, AbsoluteLayoutFlags.PositionProportional);
					AbsoluteLayout.SetLayoutBounds(sl1, new Rectangle(.75, 10, 200, AbsoluteLayout.AutoSize));
					view.Children.Add(sl1); //view.Children.Add(l1);
					return new ViewCell
					{
						View = view
					};
				})
			};
			//this.Content = new StackLayout { Children = { ListTable } };
			sl.Children.Add(ListTable);
				
		}
		protected override void OnAppearing()
		{
			//ListTable.ItemsSource = App.database.showTableDict();
			
			base.OnAppearing();
		}
	}
}