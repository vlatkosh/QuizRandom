using Newtonsoft.Json;
using QuizRandom.Models;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

/*
 * display the current QuizResult
 * 
 */

namespace QuizRandom.ViewModels
{
    public class GameEndViewModel : MyBindableObject
    {
        // Constructor
        public GameEndViewModel()
        {
            Debug.WriteLine($"{this.GetType()} constructor");

            GoBackCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("../..");
            });
        }

        // Public properties
        public Quiz CurrentQuiz { get; private set; }
        public QuizResult Result { get; private set; }

        public string ResultInfo
        {
            get
            {
                string s = string.Format(
                    "You answered {0} questions correctly out of {1} total.\n",
                    Result.CorrectCount,
                    CurrentQuiz.QuestionCount
                );
                if (Result.CorrectCount > CurrentQuiz.BestResult.CorrectCount)
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

        // ICommand implementations
        public ICommand GoBackCommand { get; protected set; }

        // Methods
        public async void LoadQuiz(string QuizId)
        {
            int id = Convert.ToInt32(QuizId);
            CurrentQuiz = await App.Database.GetQuizAsync(id);

            if (!(Result is null))
            {
                OnEverythingLoaded();
            }
        }

        public void LoadResult(string data)
        {
            Result = JsonConvert.DeserializeObject<QuizResult>(data);
        
            if (!(CurrentQuiz is null))
            {
                OnEverythingLoaded();
            }
        }

        private async void OnEverythingLoaded()
        {
            // notify property changes
            OnPropertyChanged(ResultInfo);

            // save to database if better
            if (Result.CorrectCount > CurrentQuiz.BestResult.CorrectCount)
            {
                CurrentQuiz.BestResult = Result;
                await App.Database.SaveQuizAsync(CurrentQuiz);
            }
        }
    }
}
