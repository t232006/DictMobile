﻿using System;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;
using System.Reflection;
using IndDictionary.addition;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace IndDictionary
{
	public partial class App : Application
	{
		public const string DATABASENAME = "dictionaryCut.db";//!!reset after development
		public static baseManipulation database;
		public static baseManipulation Database
		{
			get
			{
				if (database == null)
				{
					string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASENAME);
					if (!File.Exists(dbPath))
					{
						var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
						using (Stream s = assembly.GetManifestResourceStream($"IndDictionary.{DATABASENAME}"))
						{
							using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
							{
								s.CopyTo(fs);
								fs.Flush();
							}
						}
						database = new baseManipulation(dbPath);
						foreach (dict d in database.showTableDict(true, WhatToShow.alltogether))
						{
							d.DateRec = datesCorrection.toCorrectDate(d.DateRec);
							App.Database.saveRecD(d);
						}
					}
					else database = new baseManipulation(dbPath);
				}
				return database;
			}
		}
		public App()
		{
			//InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
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
