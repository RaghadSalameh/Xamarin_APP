using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;
using Xamarin_App.Data;
using Xamarin.Forms;
using Xamarin_App.iOS.Data;

[assembly: Dependency(typeof(SQLiteDb))]
namespace Xamarin_App.iOS.Data
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteConnection(path);
        }
    }
}