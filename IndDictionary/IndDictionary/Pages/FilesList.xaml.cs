using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndDictionary.Pages
{
	public class str 
	{
		public string filename { get; set; }
		public string wordsCount { get; set; }
		public string lastDate { get; set; } 
	}
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilesList : ContentPage
	{
		public List<str> Items { get; set; }
		public FilesList()
		{
			InitializeComponent();
			//DirectoryInfo AppDir = new DirectoryInfo(App.APPFOLDER);
			//FList.ItemsSource = Directory.GetFiles(App.APPFOLDER).Select(fi=>Path.GetFileName(fi));
			//App.Current.Properties.Remove("dbPath");
			this.BindingContext = this;
			PageRefresh();
			
		}
		protected void PageRefresh()
		{
			
			Items = new List<str>();
			object fileInfoString;
			IEnumerable<string> fileList = Directory.GetFiles(App.APPFOLDER).Select(fi => Path.GetFileName(fi));
			foreach (string s in fileList)
			{
				str Item = new str();
				Item.filename = s;
				if (App.Current.Properties.TryGetValue(s, out fileInfoString))
				{
					string[] ss = fileInfoString.ToString().Split('.');
					Item.filename = ss[0]; Item.lastDate = ss[1];
				}
				Items.Add(Item);
			}
			FList.ItemsSource = Items;
			FList.SelectedItem = null;
		}
		protected void OnDelete(Object Sender, EventArgs e)
		{
			object o;
			Button button = (Button) Sender;
			string filename = (button.CommandParameter as str).filename;
			App.Current.Properties.TryGetValue("current", out o);
			if (Path.GetFileName((string)o) == filename)
				DisplayAlert("Unable to delete current database. Select another one.", "Error", "Ok");
			else
				File.Delete(Path.Combine(App.APPFOLDER,filename));
			PageRefresh();
		}

		protected void OnOpen(Object Sender, EventArgs e)
		{
			Button button = (Button)Sender;
			string filename = (button.CommandParameter as str).filename;
			App.Current.Properties.Remove("current");
			App.Current.Properties.Add("current", Path.Combine(App.APPFOLDER, filename));
			App.Database.toReboot = true;
			App.Database.ResetSelection();
			
			//App.MainPage = new NavigationPage(new MainPage());
		}


		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}
