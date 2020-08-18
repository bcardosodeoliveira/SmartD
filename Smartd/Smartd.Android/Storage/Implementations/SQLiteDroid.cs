using Smartd.Bibliotecas;
using Smartd.Droid.Storage.Implementations;
using Smartd.Storage.Interface;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDroid))]
namespace Smartd.Droid.Storage.Implementations
{
    public class SQLiteDroid : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            SQLitePCL.Batteries.Init();

            string strName = Configuracao.NomeDatabase,
                   strDocumentsPath = Path.Combine(Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath, "db"),
                   strPath = Path.Combine(strDocumentsPath, strName);

            if (!Directory.Exists(strDocumentsPath))
                Directory.CreateDirectory(strDocumentsPath);

            return new SQLiteConnection(strPath);
        }
    }
}