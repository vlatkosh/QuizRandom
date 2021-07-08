using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizRandom.Services.Database
{
    public class LocalDatabase
    {
        // Private members
        private readonly SQLiteAsyncConnection database;
        private readonly HashSet<Type> createdTables;

        // Constructor
        public LocalDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
        }

        // Methods
        private void CreateTableIfNeeded<T>()
            where T : DatabaseItem, new()
        {
            if (!createdTables.Contains(typeof(T)))
            {
                createdTables.Add(typeof(T));
                database.CreateTableAsync<T>().Wait();
            }
        }

        public Task<List<T>> GetItemsAsync<T>()
            where T : DatabaseItem, new()
        {
            CreateTableIfNeeded<T>();
            return database.Table<T>().ToListAsync();
        }

        public Task<List<T>> GetItemsAsync<T>(int id)
            where T : DatabaseItem, new()
        {
            CreateTableIfNeeded<T>();
            // get all items with that id
            return database.Table<T>()
                .Where(item => item.ID == id)
                .ToListAsync();
        }

        public Task<T> GetItemAsync<T>(int id)
            where T : DatabaseItem, new()
        {
            CreateTableIfNeeded<T>();
            // get one item
            return database.Table<T>()
                .Where(item => item.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync<T>(ref T item)
            where T : DatabaseItem, new()
        {
            CreateTableIfNeeded<T>();
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync<T>(ref T item)
            where T : DatabaseItem, new()
        {
            CreateTableIfNeeded<T>();
            return database.DeleteAsync(item);
        }

        public Task<int> DeleteEverythingAsync<T>()
            where T : DatabaseItem, new()
        {
            CreateTableIfNeeded<T>();
            return database.DeleteAllAsync<T>();
        }
    }
}
