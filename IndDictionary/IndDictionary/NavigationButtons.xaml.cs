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
		public NavigationButtons()
		{
			InitializeComponent();
		}
		protected void DictOpen(object sender, EventArgs e)
		{
			Navigation.PushAsync(new WordPage(translationShow));
		}
		protected void ToolsOpen(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ToolsPage(translationShow));
		}
	}
}