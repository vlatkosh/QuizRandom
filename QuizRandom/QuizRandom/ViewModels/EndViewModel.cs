using QuizRandom.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizRandom.ViewModels
{
    public class EndViewModel : BaseViewModel, IQueryAttributable
    {
        // Constructor
        public EndViewModel()
        {
            Debug.WriteLine($"{this.GetType()} constructor");

            GoBackCommand = new Command(async () =>
            {
                // go to quiz info page
                await Shell.Current.GoToAsync("../..");
            });
        }

        // Private members
        private QuizResult newResult;
        private bool isBest;

        // ICommand implementations
        public ICommand SaveResultCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        // Public properties
        public int ID { get; set; } = -1;
        public int Score { get; set; } = -1;
        public int QuestionCount { get; set; } = -1;

        public string ResultInfo
        {
            get
            {
                if (Score == -1)
                {
                    return string.Empty;
                }
                string s = string.Format(
                    "You answered {0} questions correctly out of {1} total.\n\n",
                    Score,
                    QuestionCount
                );
                if (isBest)
                {
                    s += "Congratulations! You have a new best result.";
                }
                else
                {
                    s += "You've done better before. Better luck next time!";
                }
                return s;
            }
        }

        // Methods
        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            Score = Convert.ToInt32(query[nameof(Score)]);
            ID = Convert.ToInt32(query[nameof(ID)]);
            QuestionCount = Convert.ToInt32(query[nameof(QuestionCount)]);

            newResult = new QuizResult(ID, Score);
            await App.Database.SaveItemAsync(ref newResult);
            Debug.WriteLine($"Saved new result: ItemID={newResult.ID}, QuizID={ID}, {nameof(Score)}={Score}");

            // get results for this quiz and sort them
            List<QuizResult> results = await App.Database.GetItemsAsync<QuizResult>();

            isBest = true;
            foreach (QuizResult result in results)
            {
                if (result.ID != newResult.ID && result.QuizID == ID && result.Score >= newResult.Score)
                {
                    isBest = false;
                    break;
                }
            }

            OnPropertyChanged(nameof(ResultInfo));
        }
    }
}
