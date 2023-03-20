using SQLite;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IndDictionary
{
    public class baseManipulation
    {
		SQLiteConnection database;
		public baseManipulation (string databasePath)
		{
			database = new SQLiteConnection(databasePath);
		}
		public IEnumerable<dict> showTable()
		{
			return database.Table<dict>().ToList();
		}
		public int saveRec(dict item)
		{
			if (item.number != 0)
			{
				database.Update(item);
				return item.number;
			}
			else
				return database.Insert(item);
		}

		public int deleteRec(int id)
		{
			return database.Delete<dict>(id);
		}

		public IEnumerable<dict> findWord(string W)
		{
			var items = from s in database.Table<dict>().ToList()
						where s.Word.Contains(W)
						select s;
			return items;
		}

		public IEnumerable<dict> findTranslation(string T)
		{
			var items = from s in database.Table<dict>().ToList()
						where s.Translation.Contains(T)
						select s;
			return items;
		}

    }
}
