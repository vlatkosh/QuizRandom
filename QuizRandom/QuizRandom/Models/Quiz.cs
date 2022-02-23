using QuizRandom.Services.Database;
using SQLite;
using System;

namespace QuizRandom.Models
{
    public class Quiz : DatabaseItem
    {
        // Constructors
        public Quiz()
        {
        }

        public Quiz(ref string data, int questionCount)
        {
            QuestionDataRaw = data;
            QuestionCount = questionCount;
        }

        // Public properties
        public string QuestionDataRaw { get; set; } = string.Empty;
        public int QuestionCount { get; set; } = 0;
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string Name { get; set; } = string.Empty;
    }
}
