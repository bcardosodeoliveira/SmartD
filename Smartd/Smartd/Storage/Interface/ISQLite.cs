using SQLite;

namespace Smartd.Storage.Interface
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
