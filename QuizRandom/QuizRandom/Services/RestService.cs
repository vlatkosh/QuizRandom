using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuizRandom.Services
{
    public class RestService
    {
        readonly HttpClient client;

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<string> GetQuizDataAsync(string uri)
        {
            string data = string.Empty;
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsStringAsync();
                    //data = WebUtility.HtmlDecode(data);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }
            return data;
        }
    }
}
