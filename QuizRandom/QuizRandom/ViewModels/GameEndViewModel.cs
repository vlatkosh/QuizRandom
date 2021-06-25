using QuizRandom.Models;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizRandom.ViewModels
{
    public class GameEndViewModel : MyBindableObject
    {
        // Constructor
        public GameEndViewModel()
        {
            Debug.WriteLine($"{this.GetType()} constructor");

            SaveResultCommand = new Command(async () =>
            {
                // save to database if better
                currentQuiz.BestResultCount = CorrectCount;
                currentQuiz.BestResultDate = DateTime.Now;
                await App.Database.SaveQuizAsync(currentQuiz);
            });

            GoBackCommand = new Command(async () =>
            {
                // go to quiz info page
                await Shell.Current.GoToAsync("../..");
            });

            currentQuiz = null;
            CorrectCount = -1;
            SaveResultValid = false;
        }

        // Private members
        private Quiz currentQuiz;

        // Public properties
        public int CorrectCount
        {
            get => CorrectCount;
            set
            {
                CorrectCount = value;
                OnPropertyChanged(nameof(SaveResultValid));
                OnPropertyChanged(nameof(ResultInfo));
            }
        }

        public bool SaveResultValid
        {
            get
            {
                if (CorrectCount == -1 || currentQuiz is null)
                {
                    return false;
                }
                return CorrectCount > currentQuiz.BestResultCount;
            }
            private set => SaveResultValid = value;
        }

        public string ResultInfo
        {
            get
            {
                if (CorrectCount == -1 || currentQuiz is null)
                {
                    return string.Empty;
                }
                string s = string.Format(
                    "You answered {0} questions correctly out of {1} total.\n",
                    CorrectCount,
                    currentQuiz.QuestionCount
                );
                if (SaveResultValid)
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
        public ICommand SaveResultCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }

        // Methods
        public async void LoadQuiz(string QuizId)
        {
            int id = Convert.ToInt32(QuizId);
            currentQuiz = await App.Database.GetQuizAsync(id);
            OnPropertyChanged(nameof(SaveResultValid));
            OnPropertyChanged(nameof(ResultInfo));
        }
    }
}
