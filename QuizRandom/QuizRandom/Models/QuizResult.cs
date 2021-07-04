using System;

namespace QuizRandom.Models
{
    public class QuizResult
    {
        public int CorrectCount { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
