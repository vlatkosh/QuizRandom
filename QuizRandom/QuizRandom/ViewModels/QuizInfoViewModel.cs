using QuizRandom.Models;
using QuizRandom.Views;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

/*
 *  
 */

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
        public int QuizID { get; private set; }

        public string BestResultInfo
        {
            get
            {
                if (currentQuiz is null)
                {
                    return string.Empty;
                }
                else if (currentQuiz.BestResultCount == -1)
                {
                    return "Play the quiz and see how well you do!";
                }
                else
                {
                    return string.Format(
                        "Best attempt was {0} questions out of {1}, done at {2}",
                        currentQuiz.BestResultCount,
                        currentQuiz.QuestionCount,
                        currentQuiz.BestResultDate
                    );
                }
            }
        }

        // ICommand implementations
        public ICommand PlayQuizCommand { get; protected set; }
        public ICommand DeleteQuizCommand { get; protected set; }

        // Methods
        public async void LoadQuiz(string itemId)
        {
            int id = Convert.ToInt32(itemId);
            QuizID = id;
            currentQuiz = await App.Database.GetQuizAsync(id);

            OnPropertyChanged(nameof(QuizID));
            OnPropertyChanged(nameof(BestResultInfo));
        }
    }
}
