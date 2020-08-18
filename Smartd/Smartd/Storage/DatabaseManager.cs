using Smartd.Bibliotecas;
using Smartd.Models;
using Smartd.Storage.Interface;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.Storage
{
    public class DatabaseManager
    {
        SQLiteConnection db;

        public DatabaseManager()
        {
            db = DependencyService.Get<ISQLite>().GetConnection();
        }

        public async Task<int> Save<T>(T value) where T : IKeyObject, new()
        {
            await CreateTable<T>();

            if (await Get<T>(value.Key) == null)
            {
                db.Insert(value);
                return await Task.FromResult(GetLastInsertId());
            }
            else
            {
                db.Update(value);
                return await Task.FromResult(value.Key.ToInt());
            }
        }

        public async Task<List<T>> GetAll<T>() where T : new()
        {
            await CreateTable<T>();

            return await Task.FromResult(db.Table<T>().ToList());
        }

        public async Task<T> Get<T>(string Id) where T : new()
        {
            await CreateTable<T>();

            return await Task.FromResult(db.Find<T>(Id));
        }

        public async Task<int> Delete<T>(int id)
        {
            await CreateTable<T>();

            return await Task.FromResult(db.Delete<T>(id));
        }

        public async Task<int> DeleteAll<T>()
        {
            await CreateTable<T>();

            return await Task.FromResult(db.DeleteAll<T>());
        }

        public async Task<bool> DropTable<T>()
        {
            try
            {
                if (await HasTable<T>())
                    db.DropTable<T>();

                return await Task.FromResult(true);
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine(ex);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> HasTable<T>()
        {
            const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
            var cmd = db.CreateCommand(cmdText, typeof(T).Name);
            return await Task.FromResult(cmd.ExecuteScalar<string>() != null);
        }

        private async Task<bool> CreateTable<T>()
        {
            try
            {
                if (!await HasTable<T>())
                    db.CreateTable<T>();

                return await Task.FromResult(true);
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine(ex);
                return await Task.FromResult(false);
            }
        }

        private int GetLastInsertId()
        {
            return (int)SQLite3.LastInsertRowid(db.Handle);
        }
    }
}
