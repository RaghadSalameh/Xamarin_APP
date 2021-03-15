using SQLite;

namespace Xamarin_App.Data
{
    public interface ISQLiteDb
    {
        SQLiteConnection GetConnection();
    }
}
