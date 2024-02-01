using IndDictionary.addition;
using IndDictionary.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndDictionary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ToolsPage : ContentPage
	{
		bool showAll = true;
		WhatToShow wts = WhatToShow.alltogether;
		ListView _ListTable;
		//private string LastDate = () => 
		WordPage detail;

		public ToolsPage(WordPage Detail)
		{
			InitializeComponent();
			if (Detail.transl) Title = "Translation"; else Title = "Word";
			detail = Detail;
			DateLabel.Text = "Last record " + App.Database.getInfo(2);
			CountLabel.Text = "Records count " + App.Database.getInfo(1);
			NavigationButtons navButtons = new NavigationButtons(Detail);
			forNavButtons.Children.Add(navButtons);
		}
		protected override void OnDisappearing()
		{
			detail.Refresh(showAll, wts);
			base.OnDisappearing();
		}
		protected void OnAll(object sender, EventArgs e)
		{
			wts = WhatToShow.alltogether;
			//detail.Refresh(showAll, wts);
		}
		protected void OnPhrases(object sender, EventArgs e)
		{
			wts = WhatToShow.phrases;
			//detail.Refresh(showAll, wts);
		}
		protected void OnWords(object sender, EventArgs e)
		{
			wts = WhatToShow.words;
			//detail.Refresh(showAll, wts);
		}
		protected void onReset(object sender, EventArgs e)
		{
			App.Database.ResetSelection();
			//detail.Refresh(showAll, wts);
		}
		protected void OnChecking(object sender, EventArgs e)
		{
			showAll = !(sender as CheckBox).IsChecked;
			//detail.Refresh(showAll, wts);
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
