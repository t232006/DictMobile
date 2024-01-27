using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IndDictionary
{
	public partial class MainPageOld : TabbedPage
	{
		public MainPageOld()
		{
			InitializeComponent();
			FlyoutPage ToolsSide1 = new ToolsSide(false);
			FlyoutPage ToolsSide2 = new ToolsSide(true);
			//FlyoutPage TranslationPage = new WordPage(true);
			//FlyoutPage TransPage = new TransPage(true);
			//Children.Add(TransPage);
			Children.Add(ToolsSide1);
			Children.Add(ToolsSide2);
			//Children.Add(TranslationPage);

		}
		protected override void OnAppearing()
		{
			
		}
	}
}
