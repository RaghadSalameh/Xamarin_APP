using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Xamarin_App.Data;
using Xamarin_App.Droid.Data;
using static Android.Provider.Settings;

[assembly: Dependency(typeof(SQLiteDb))]
namespace Xamarin_App.Droid.Data
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");
            SQLiteConnection s = new SQLiteConnection(path);
            return s;
        }
    }
}