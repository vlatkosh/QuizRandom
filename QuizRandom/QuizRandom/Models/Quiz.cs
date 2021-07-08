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

        // Private members
        private string name = string.Empty;

        // Public properties
        public string QuestionDataRaw { get; set; } = string.Empty;
        public int QuestionCount { get; set; } = 0;
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string Name
        {
            get => name == string.Empty ? $"Quiz {ID + 1}" : name;
            set => name = value;
        }
    }
}
