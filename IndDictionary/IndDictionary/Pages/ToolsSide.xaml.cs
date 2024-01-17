using IndDictionary.addition;
using IndDictionary.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using static System.Net.Mime.MediaTypeNames;

namespace IndDictionary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ToolsSide : FlyoutPage
	{
		bool showAll = true;
		WhatToShow wts = WhatToShow.alltogether;
		ListView _ListTable;
		//private string LastDate = () => 
		
		public ToolsSide(bool _transl)
		{
			InitializeComponent();
			if (_transl) Title = "Translation"; else Title = "Word";
			Detail = new NavigationPage(new WordPage(_transl));
			if (Device.RuntimePlatform == Device.UWP)
			{
				FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;
			}
			DateLabel.Text = "Last record " + App.Database.getInfo(2);
			CountLabel.Text = "Records count " + App.Database.getInfo(1);
		}
		protected void Action()
		{
			((Detail as NavigationPage).RootPage as WordPage).Refresh(showAll, wts);
		}

		protected void OnAll(object sender, EventArgs e)
		{
			wts = WhatToShow.alltogether;
			Action();
		}
		protected void OnPhrases(object sender, EventArgs e)
		{
			wts = WhatToShow.phrases;
			Action();
		}
		protected void OnWords(object sender, EventArgs e)
		{
			wts = WhatToShow.words;
			Action();
		}
		protected void onReset(object sender, EventArgs e)
		{
			App.Database.ResetSelection();
			//OnAppearing();
		}
		protected void OnChecking(object sender, EventArgs e)
		{
			showAll = !(sender as CheckBox).IsChecked;
			Action();
		}
		
		async Task<FileResult> PickAndShow(PickOptions options)
		{
			
				var result = await FilePicker.PickAsync(options);
				if (result != null)
				{
					if (result.FileName.EndsWith("db", StringComparison.OrdinalIgnoreCase))
					{
						string st = Path.Combine(App.APPFOLDER, result.FileName);
						st = st.Insert(st.LastIndexOf('.'), DateTime.Now.ToString());
						App.Current.Properties.Remove("current");
						App.Current.Properties.Add("current", st);
						App.databasename = result.FileName;
						//App.copyFiles(result.FullPath, st);
						FileInfo f = new FileInfo(result.FullPath);
						//string st = Path.Combine(@"z:\",Path.GetFileName(result.FileName));

						try
						{ 
							f.CopyTo(st);
							App.Database.toReboot = true;
							App.Database.ResetSelection();
						}
						catch (Exception ex) { };
					}	
				}
				return result;
			
		}
		protected async void OnSynchr(object sender, EventArgs e)
		{
			var options = new PickOptions
			{
				FileTypes = new FilePickerFileType
				(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.Android, new[] { "*/*"} },
					{ DevicePlatform.UWP, new[] { ".db" } }
				}),
				PickerTitle = "Please, select database file"
			};
			await PickAndShow(options);
			
		}
		protected async void OpenLibrary(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new FilesList());
		}
		protected void onDates(object sender, EventArgs e)
		{
			List<DateOrTopicClassAux> conteiner = new List<DateOrTopicClassAux>();
			IEnumerable<dict> tempcont = App.Database.showTopicsDates<dict>(!ShowSelected.IsChecked);
			foreach (dict t in tempcont)
			{
				conteiner.Add(new DateOrTopicClassAux { DaOrTo = t.DateRec, Spoted = false });
			}
			DateTopicForm dateForm = new DateTopicForm(conteiner, WhatToSelect.dates);
			Navigation.PushAsync(dateForm);
		}

		protected void onTopics(object sender, EventArgs e)
		{
			List<DateOrTopicClassAux> conteiner = new List<DateOrTopicClassAux>();
			IEnumerable<topic> tempcont = App.Database.showTopicsDates<topic>(!ShowSelected.IsChecked);
			foreach (topic t in tempcont)
			{
				conteiner.Add(new DateOrTopicClassAux { DaOrTo = t.Name, Spoted = false });
			}
			DateTopicForm topicForm = new DateTopicForm(conteiner, WhatToSelect.topics);
			Navigation.PushAsync(topicForm);
		}
		protected void Searching(Object sender, TextChangedEventArgs e)
		{
			IEnumerable<dict> founded = App.Database.findRecords(SearchEntry.Text, f => f.Word);
			if (founded != null)
				_ListTable.ItemsSource = founded;
			if (e.NewTextValue == "")
				_ListTable.ItemsSource = App.Database.showTableDict(showAll, wts);
		}
	}
}