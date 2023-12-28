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
		public const string DATABASENAME = "dictionaryCut.db";
		public static string APPFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		static baseManipulation database;

		
		public static void copyFiles(string fromPath, string toPath)
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
			
		}

		public static baseManipulation Database
		{
			get
			{
				string dbPath = System.IO.Path.Combine(APPFOLDER, databasename);
				if ((database == null) || (database.toReboot))
				{
					if (!File.Exists(dbPath))
					{
						var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
						//App.Current.Properties.Remove("current");
						dbPath = System.IO.Path.Combine(APPFOLDER, DATABASENAME);
						using (Stream s = assembly.GetManifestResourceStream($"IndDictionary.Resources.{DATABASENAME}"))
						{
							using (FileStream dest = new FileStream(dbPath, FileMode.OpenOrCreate))
							{
								s.CopyTo(dest);
								dest.Flush();
							}
						}
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
					database = new baseManipulation(dbPath);
				}
				return database;
			}
		}
		public App()
		{
			//InitializeComponent();
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
			MainPage = new NavigationPage(new MainPage());
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
