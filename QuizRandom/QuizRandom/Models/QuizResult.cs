using System;
using QuizRandom.Services.Database;

namespace QuizRandom.Models
{
    public class QuizResult : DatabaseItem
    {
        public int CorrectCount { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
