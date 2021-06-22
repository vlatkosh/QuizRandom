using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace QuizRandom.Models
{
    public class JSONRootObject
    {
        [JsonProperty("response_code")]
        public int ResponseCode { get; set; }

        // not List<> because it doesn't seem to work
        [JsonProperty("results")]
        public List<QuizQuestion> Results { get; set; }
    }
}
