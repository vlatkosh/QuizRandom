using SQLite;

namespace QuizRandom.Services.Database
{
    public class DatabaseItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public DatabaseItem()
        {

        }
    }
}
