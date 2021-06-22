using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace QuizRandom.Models
{
    public class QuizQuestion
    {
        [JsonProperty("category")]
        public string Category { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }
        
        [JsonProperty("question")]
        public string Question { get; set; }
        
        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; }

        // not List<> because it doesn't seem to work
        [JsonProperty("incorrect_answers")]
        public List<string> IncorrectAnswers { get; set; }
    }
}
