using QuizRandom.Models;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QuizRandom.Services
{
    public class QuizDatabase
    {
        // Private members
        readonly SQLiteAsyncConnection database;

        // Constructor
        public QuizDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Quiz>().Wait();

            //DeleteEverythingAsync();
        }

        // Methods 
        public Task<List<Quiz>> GetQuizzesAsync()
        {
            return database.Table<Quiz>().ToListAsync();
        }

        public Task<Quiz> GetQuizAsync(int id)
        {
            // get a specific quiz
            return database.Table<Quiz>()
                    .Where(quiz => quiz.ID == id)
                    .FirstOrDefaultAsync();
        }

        public Task<int> SaveQuizAsync(Quiz quiz)
        {
            if (quiz.ID != 0)
            {
                return database.UpdateAsync(quiz);
            }
            else
            {
                return database.InsertAsync(quiz);
            }
        }

        public Task<int> DeleteQuizAsync(Quiz quiz)
        {
            // delete a quiz
            return database.DeleteAsync(quiz);
        }

        public async Task<int> DeleteEverythingAsync()
        {
            int cnt = await database.DeleteAllAsync<Quiz>();
            Debug.WriteLine($"Database: Deleted {cnt} objects");
            return cnt;
        }

    }
}
