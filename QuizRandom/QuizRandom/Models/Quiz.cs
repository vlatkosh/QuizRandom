using SQLite;
using System.ComponentModel;
using System;

/*
 *  Unfortunately, there's no clean way storing quiz questions in a SQLIte database,
 *  because it can't store List<>, so we keep the data as the original raw string
 */

namespace QuizRandom.Models
{
    public class Quiz
    {
        // Constructors
        public Quiz()
        {
            BestResultCount = -1;
            BestResultDate = DateTime.Now;
            JSONData = string.Empty;
            QuestionCount = 0;
        }

        public Quiz(ref string data, int questionCount)
        {
            BestResultCount = -1;
            BestResultDate = DateTime.Now;
            JSONData = data;
            QuestionCount = questionCount;
        }

        // Public properties
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string JSONData { get; set; }
        public int QuestionCount { get; set; }
        public int BestResultCount { get; set; }
        public DateTime BestResultDate { get; set; }
    }
}
