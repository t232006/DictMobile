using SQLite;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

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
			IEnumerable<dict> result;
			result = itemsD;
			if (!allrec)
			{
				result = from s in itemsD
						 where s.Usersel == true
						 select s;
			}
			switch (_wts)
			{
				case WhatToShow.words:
					result = from s in result
							 where s.Phrase == false
							 select s;
					break;
				case WhatToShow.phrases:
					result = from s in result
							 where s.Phrase == true
							 select s;
					break;
			}
			return result;
		}

    }
}
