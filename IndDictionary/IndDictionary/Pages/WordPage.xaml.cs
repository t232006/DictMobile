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
		}
		protected override void OnAppearing()
		{
			ListTable.ItemsSource = App.Database.showTableDict();
			base.OnAppearing();
		}
		protected void OnToggled(object sender, ToggledEventArgs e)
		{
			dict focusedItem = App.Database.findOneRecord((sender as ExtSwitch).ID);
			if (focusedItem !=null) focusedItem.Usersel = e.Value;
			App.Database.saveRecD(focusedItem);
		}
	}
}