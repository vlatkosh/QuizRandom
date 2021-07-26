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
            QuestionsSerialized = data;
            QuestionCount = questionCount;
        }

        // Private members
        private string name;

        // Public properties
        public string QuestionsSerialized { get; set; } = string.Empty;
        public int QuestionCount { get; set; } = 0;
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string Name
        {
            get => (name is null || name == string.Empty) ? (ID + 1).ToString() : name;
            set => name = value;
        }
    }
}
