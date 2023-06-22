using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IndDictionary
{
	public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			InitializeComponent();
			FlyoutPage WordPage = new WordPage(false);
			FlyoutPage TranslationPage = new WordPage(true);
			FlyoutPage TransPage = new TransPage(true);
			Children.Add(TransPage);
			Children.Add(WordPage);
			Children.Add(TranslationPage);
			
		}
		protected override void OnAppearing()
		{
			
		}
	}
}
