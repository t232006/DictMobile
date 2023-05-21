using IndDictionary.addition;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndDictionary.Pages
{
	public delegate void WhatToSelect(IEnumerable<dateClassAux> _passedList);
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DateForm : ContentPage
	{
		WhatToSelect wts;
		IEnumerable<dateClassAux> PassedList;
		public DateForm(IEnumerable<dateClassAux> _passedList, WhatToSelect _wts)
		{
			PassedList = _passedList;
			wts=_wts;
			InitializeComponent ();
		}
		protected override void OnAppearing()
		{
			DatesList.ItemsSource = PassedList;
			base.OnAppearing();
		}

		public void onSelect(object sender, ItemTappedEventArgs e)
		{
			var temp = e.Item as dateClassAux;
			temp.Spoted = !temp.Spoted;
		}

		public void onCancelPress (object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		public void onApplyPress(object sender, EventArgs e)
		{
			wts(PassedList);
			Navigation.PopAsync();
			//OnAppearing();
		}
	}
}