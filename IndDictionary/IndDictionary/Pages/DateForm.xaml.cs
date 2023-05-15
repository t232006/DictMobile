﻿using IndDictionary.addition;
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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DateForm : ContentPage
	{
		IEnumerable<dateClassAux> PassedList;
		
		public DateForm(IEnumerable<dateClassAux> _passedList)
		{
			PassedList = _passedList;
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
	}
}