using QuizRandom.Models;
using QuizRandom.Views;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizRandom.ViewModels
{
    public class QuizInfoViewModel : MyBindableObject
    {
        // Constructor
        public QuizInfoViewModel()
        {
            Debug.WriteLine($"{this.GetType()} constructor");

            PlayQuizCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(GamePlayPage)}?{nameof(GamePlayPage.ItemId)}={currentQuiz.ID}");
            });

            DeleteQuizCommand = new Command(async () =>
            {
                bool answer = await Shell.Current.DisplayAlert(
                    "Confirmation",
                    "Are you sure you want to delete this quiz?",
                    "Yes",
                    "No"
                );
                if (answer == false)
                {
                    return;
                }
                // delete the quiz
                await App.Database.DeleteQuizAsync(currentQuiz);
                // go to MainPage
                await Shell.Current.GoToAsync("..");
            });
        }

        // Private members
        private Quiz currentQuiz;

        // Public properties
        public string QuizName => currentQuiz is null ? string.Empty : currentQuiz.Name;

        public string QuizInfo
        {
            get
            {
                if (currentQuiz is null)
                {
                    return string.Empty;
                }
                string s = string.Empty;
                s += $"This quiz was created on {currentQuiz.CreationDate.ToString("h\\:mm tt, dddd, MMM d, yyyy")}.\n";
                s += $"It has {currentQuiz.QuestionCount} questions.\n\n";
                if (currentQuiz.BestResultCount == -1)
                {
                    s += "Play the quiz and see how well you do!\n";
                }
                else
                {
                    s += $"Best attempt was {currentQuiz.BestResultCount} / {currentQuiz.QuestionCount} at {currentQuiz.BestResultDate}.\n";
                }
                return s;
            }
        }

        // ICommand implementations
        public ICommand PlayQuizCommand { get; set; }
        public ICommand DeleteQuizCommand { get; set; }

        // Methods
        public async void LoadQuiz(string itemId)
        {
            int id = Convert.ToInt32(itemId);
            currentQuiz = await App.Database.GetQuizAsync(id);

            OnPropertyChanged(nameof(QuizName));
            OnPropertyChanged(nameof(QuizInfo));
        }
    }
}
