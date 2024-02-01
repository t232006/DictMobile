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
	public partial class NavigationButtons : ContentView
	{
		bool translationShow = false;
		public bool TranslationShow { set { translationShow = value; } }
		WordPage WordPage { get; set; }
		public NavigationButtons(WordPage _WordPage)
		{
			WordPage = _WordPage;
			InitializeComponent();
		}
		protected void DictOpen(object sender, EventArgs e)
		{
			//WordPage = new WordPage(translationShow);
			
			Navigation.PopAsync();
		}
		protected void ToolsOpen(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ToolsPage(WordPage));
		}
	}
}