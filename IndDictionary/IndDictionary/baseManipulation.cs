using SQLite;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using IndDictionary.addition;

namespace IndDictionary
{
	public enum WhatToShow { words, phrases, alltogether}
	public class baseManipulation
	{
		SQLiteConnection database;
		IEnumerable<dict> itemsD;
		IEnumerable<topic> itemsT;
		public baseManipulation(string databasePath)
		{
			database = new SQLiteConnection(databasePath);
			itemsD = database.Table<dict>().ToList();
			itemsT = database.Table<topic>().ToList();
		}

		public IEnumerable<dict> showTableDict(bool allrec, WhatToShow wts)
		{
			return filtr(allrec, wts);
		}

		public IEnumerable<topic> showTableTopic()
		{
			return itemsT;
		}
		public int saveRecD(dict item)
		{
			if (item != null)
			{
				if (item.Number != 0)
				{
					item.DateRec = datesCorrection.toCorrectDate(item.DateRec);
					database.Update(item);
					return item.Number;
				}
				else
					return database.Insert(item);
			}
			return -1;
		}
		public int saveRecT(topic item)
		{
			if (item.id != 0)
			{
				database.Update(item);
				return item.id;
			}
			else
				return database.Insert(item);
		}
		public int deleteRecD(int id)
		{
			return database.Delete<dict>(id);
		}
		public int deleteRecT(int id)
		{
			return database.Delete<topic>(id);
		}
		public IEnumerable<dict> findRecords(string needle, Func<dict, string> _field)
		{
			return from s in itemsD
				   where _field(s).Contains(needle)
				   select s;
		}
		public dict findOneRecord(int id)
		{
			if (id != 0)
				return database.Get<dict>(id);
			else return null;
		}
		IEnumerable<dict> filtr(bool allrec, WhatToShow _wts)
		{
			string request = "SELECT * FROM Dict ";
			if (!allrec) request += "WHERE usersel=true";
			switch (_wts)
			{
				case WhatToShow.words:
					request += "AND phrase=false";
					break;
				case WhatToShow.phrases:
					request += "AND phrase=true";
					break;
			}
			return database.Query<dict>(request);	
		}

		public IEnumerable<dict> showDates(bool showAll)
		{
			string request = "select distinct DateRec from Dict ";
			if (showAll==false) request += "where Usersel=true";
			return database.Query<dict>(request);
		}
		public void ResetSelection()
		{
			database.Execute("Update Dict set Usersel=false");
			database.Commit();
		}

		public void selectDates(IEnumerable<dateClassAux> datesList)
		{
			IEnumerable<string> l = from sl in datesList
									where sl.Spoted == true
									select sl.Date;
			string collect = string.Join("','", l);
			collect = "'" + collect +"'";
			string requestString = "Update Dict set Usersel=true where daterec in (" + collect + ")";
			ResetSelection();
			database.Execute(requestString);
			database.Commit();
		}

    }
}
