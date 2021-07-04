using Newtonsoft.Json;
using System.Collections.Generic;

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
