using QuizRandom.Services.Database;
using SQLite;
using System;

namespace QuizRandom.Models
{
    public class QuizResult : DatabaseItem
    {
        public QuizResult()
        {

        }

        public QuizResult(int id, int score)
        {
            QuizID = id;
            Score = score;
        }

        public int QuizID { get; set; }
        public int Score { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
