using QuizRandom.Views;
using QuizRandom.Models;
using System;
using System.ComponentModel;
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
                await Shell.Current.GoToAsync($"{nameof(GamePlayPage)}?{nameof(GamePlayPage.ItemId)}={CurrentQuiz.ID}");
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
                await App.Database.DeleteQuizAsync(CurrentQuiz);
                await Shell.Current.GoToAsync("..");
            });
        }

        // Public properties
        public int QuizID { get; private set; }
        public Quiz CurrentQuiz { get; private set; }

        public string BestResultInfo
        {
            get
            {
                if (CurrentQuiz is null)
                {
                    return string.Empty;
                }
                else if (CurrentQuiz.BestResultCount == -1)
                {
                    return "Play the quiz and see how well you do!";
                }
                else
                {
                    return string.Format(
                        "Best attempt was {0} questions out of {1}, done at {2}",
                        CurrentQuiz.BestResultCount,
                        CurrentQuiz.QuestionCount,
                        CurrentQuiz.BestResultDate
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
            CurrentQuiz = await App.Database.GetQuizAsync(id);
            
            OnPropertyChanged(nameof(QuizID));
            OnPropertyChanged(nameof(BestResultInfo));
        }
    }
}
