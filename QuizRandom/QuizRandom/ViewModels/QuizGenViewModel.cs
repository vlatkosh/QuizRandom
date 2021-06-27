using Newtonsoft.Json;
using QuizRandom.Models;
using QuizRandom.Services;
using QuizRandom.Views;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizRandom.ViewModels
{
    public class QuizGenViewModel
    {
        // Constructor
        public QuizGenViewModel()
        {
            Debug.WriteLine($"{this.GetType()} constructor");

            restService = new RestService();

            categories = new Dictionary<string, string>()
            {
                {"Any category", "any"},
                {"General knowledge", "9"},
                {"Entertainment: Books", "10"},
                {"Entertainment: Film", "11"},
                {"Entertainment: Music", "12"},
                {"Entertainment: Musicals and theatres", "13"},
                {"Entertainment: Television", "14"},
                {"Entertainment: Video games", "15"},
                {"Entertainment: Board games", "16"},
                {"Science and nature", "17"},
                {"Science: Computers", "18"},
                {"Science: Mathematics", "19"},
                {"Mythology", "20"},
                {"Sports", "21"},
                {"Geography", "22"},
                {"History", "23"},
                {"Politics", "24"},
                {"Art", "25"},
                {"Celebrities", "26"},
                {"Animals", "27"},
                {"Vehicles", "28"},
                {"Entertainment: Comics", "29"},
                {"Science: Gadgets", "30"},
                {"Entertainment: Japanese Anime and Manga", "31"},
                {"Entertainment: Cartoon and Animations", "32"}
            };
            CategoriesKeys = new List<string>(categories.Keys);

            difficulties = new Dictionary<string, string>()
            {
                {"Any difficulty", "any"},
                {"Easy", "easy"},
                {"Medium", "medium"},
                {"Hard", "hard"}
            };
            DifficultiesKeys = new List<string>(difficulties.Keys);

            quizTypes = new Dictionary<string, string>()
            {
                {"Any type", "any"},
                {"Multiple choice", "multiple"},
                {"True / False", "boolean"}
            };
            QuizTypesKeys = new List<string>(quizTypes.Keys);

            CreateQuizCommand = new Command(() => CreateQuiz());
        }

        // Private members
        private readonly RestService restService;

        private readonly Dictionary<string, string> categories;
        private readonly Dictionary<string, string> difficulties;
        private readonly Dictionary<string, string> quizTypes;

        // Public properties
        public List<string> CategoriesKeys { get; private set; }
        public List<string> DifficultiesKeys { get; private set; }
        public List<string> QuizTypesKeys { get; private set; }

        public int QuestionCount { get; set; } = 5;
        public int CategoryIndex { get; set; } = 0;
        public int DifficultyIndex { get; set; } = 0;
        public int QuizTypeIndex { get; set; } = 0;

        // ICommand implementations
        public ICommand CreateQuizCommand { protected set; get; }

        // Methods
        private async void CreateQuiz()
        {
            // get quiz data from the internet
            string uri = CreateURI();
            Debug.WriteLine($"Outgoing URI: {uri}");

            string data = await restService.GetQuizDataAsync(uri);
            if (string.IsNullOrWhiteSpace(data))
            {
                await Shell.Current.DisplayAlert("Failed", "There is no internet connection, or the quiz API is down.", "OK");
                return;
            }

            JSONRootObject rootObject = JsonConvert.DeserializeObject<JSONRootObject>(data);
            if (rootObject.ResponseCode != 0)
            {
                await Shell.Current.DisplayAlert("Failed", $"The API returned response code {rootObject.ResponseCode} instead of 0.", "OK");
            }

            // create quiz, add it to database
            Quiz quiz = new Quiz(ref data, rootObject.Results.Count);
            await App.Database.SaveQuizAsync(quiz);

            // go to its quiz page
            await Shell.Current.GoToAsync($"{nameof(QuizInfoPage)}?{nameof(QuizInfoPage.ItemId)}={quiz.ID}");
        }

        private string CreateURI()
        {
            string uri = "https://opentdb.com/api.php";
            uri = uri + "?amount=" + QuestionCount.ToString();
            if (CategoryIndex > 0)
            {
                // Not (any)
                uri = uri + "&category=" + categories[CategoriesKeys[CategoryIndex]];
            }
            if (DifficultyIndex > 0)
            {
                uri = uri + "&difficulty=" + difficulties[DifficultiesKeys[DifficultyIndex]];
            }
            if (QuizTypeIndex > 0)
            {
                uri = uri + "&type" + quizTypes[QuizTypesKeys[QuizTypeIndex]];
            }
            //uri += "&type=multiple";
            return uri;
        }
    }
}
