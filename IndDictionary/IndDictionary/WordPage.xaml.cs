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
				HasUnevenRows = true,
				ItemsSource = App.Database.showTableDict(),
				ItemTemplate = new DataTemplate(() =>
				{
					Label Field1 = new Label();
					Field1.SetBinding(Label.TextProperty, "Word");

					ImageButton ShowItem = new ImageButton
					{
						Source = ImageSource.FromResource("IndDictionary.Resources.eye.png"),
						Style = (Style)Resources["ButtonStyle"]
					};
					ExtImageButton SelectItem = new ExtImageButton();
					SelectItem.SetBinding(ExtImageButton.CheckedProperty, "Usersel");
					SelectItem.Style = (Style)Resources["ButtonStyle"];
					StackLayout ButtonBlock = new StackLayout()
					{
						Orientation = StackOrientation.Horizontal,
						Children = { ShowItem, SelectItem }
					};
					AbsoluteLayout view = new AbsoluteLayout();
					view.Children.Add(Field1);
					AbsoluteLayout.SetLayoutFlags(ButtonBlock, AbsoluteLayoutFlags.PositionProportional);
					AbsoluteLayout.SetLayoutBounds(ButtonBlock, new Rectangle(.9, 0, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
					view.Children.Add(ButtonBlock);
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