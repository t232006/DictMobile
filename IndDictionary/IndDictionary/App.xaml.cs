using System;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;
using System.Reflection;
using IndDictionary.addition;
using Xamarin.Forms.Shapes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace IndDictionary
{
	public partial class App : Application
	{
		public static string databasename;//!!reset after development
		public const string DEFAULTDATABASENAME = "dictionaryCut.db";	//only for default!
		public static string APPFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		static baseManipulation database;
		private static void SetDatabasename()
		{
				object dbPathOuther;
				if (App.Current.Properties.TryGetValue("current", out dbPathOuther))
				{
					databasename = dbPathOuther.ToString();
				}
				else
				{
					databasename = "dictionaryCut.db";
					App.Current.Properties.Add("current", databasename);
				}
		}
		
		private static void LoadDBFirstTime(string dbPath)
		{
			database = new baseManipulation(dbPath);
			//for first open to write information about database
			object dbPathOuther;
			if (!App.Current.Properties.TryGetValue(databasename, out dbPathOuther))
			{
				string baseInfo = $"{database.getInfo(1)}.{database.getInfo(2)}";
				App.Current.Properties.Add(databasename, baseInfo);
			}


			foreach (dict d in database.showTableDict(true, WhatToShow.alltogether))
			{
				d.DateRec = datesCorrection.toCorrectDate(d.DateRec);
				App.Database.saveRecD(d);
			}
		}

		
		/*public static void copyFiles(string fromPath, string toPath)
		{
			//
			//if (!File.Exists(toPath))
			{
				using (FileStream source = new FileStream(fromPath, FileMode.Open)) //
				{
					using (FileStream dest = new FileStream(toPath, FileMode.OpenOrCreate))
					{
						source.CopyTo(dest);
						dest.Flush();
					}
				}
			}
			
		}*/

		public static baseManipulation Database
		{
			get
			{
				if ((database == null) || (database.toReboot))
				{
					if (database!=null) SetDatabasename();
					string dbPath = System.IO.Path.Combine(APPFOLDER, databasename);
					if (!File.Exists(dbPath))
					{
						var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
						//App.Current.Properties.Remove("current");
						dbPath = System.IO.Path.Combine(APPFOLDER, DEFAULTDATABASENAME);
						using (Stream s = assembly.GetManifestResourceStream($"IndDictionary.Resources.{DEFAULTDATABASENAME}"))
						{
							using (FileStream dest = new FileStream(dbPath, FileMode.OpenOrCreate))
							{
								s.CopyTo(dest);
								dest.Flush();
							}
						}
					}
					LoadDBFirstTime(dbPath);
					database = new baseManipulation(dbPath);
					//RefreshAllForms();
				}
				return database;
			}
		}

		public App()
		{
			//InitializeComponent();
			SetDatabasename();
			//MainPage = new NavigationPage(new WordPage(false));
			MainPage = new Flyout_Page();

		}

		protected override void OnStart()
		{
			
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
