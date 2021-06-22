using Newtonsoft.Json;
using QuizRandom.Models;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

/*
 *  TODO:
 *  There's a sys seg fault here?
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

            CorrectCount = -1;
        }

        // Public properties
        public Quiz CurrentQuiz { get; private set; }

        public int CorrectCount
        {
            get => CorrectCount;
            set
            {
                CorrectCount = value;
                if (!(CurrentQuiz is null))
                {
                    OnEverythingLoaded();
                }
            }
        }

        public string ResultInfo
        {
            get
            {
                string s = string.Format(
                    "You answered {0} questions correctly out of {1} total.\n",
                    CorrectCount,
                    CurrentQuiz.QuestionCount
                );
                if (CorrectCount > CurrentQuiz.BestResultCount)
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

            if (CorrectCount != -1)
            {
                OnEverythingLoaded();
            }
        }

        private async void OnEverythingLoaded()
        {
            // notify property changes
            OnPropertyChanged(ResultInfo);

            // save to database if better
            if (CorrectCount > CurrentQuiz.BestResultCount)
            {
                CurrentQuiz.BestResultCount = CorrectCount;
                CurrentQuiz.BestResultDate = DateTime.Now;
                await App.Database.SaveQuizAsync(CurrentQuiz);
            }
        }
    }
}
