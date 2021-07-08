using QuizRandom.Models;
using QuizRandom.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizRandom.ViewModels
{
    [QueryProperty(nameof(ID), nameof(ID))]
    public class InfoViewModel : BaseViewModel
    {
        // Constructor
        public InfoViewModel()
        {
            Debug.WriteLine($"{this.GetType()} constructor");

            PlayQuizCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(PlayingPage)}?{nameof(PlayingViewModel.ID)}={quiz.ID}");
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
                await App.Database.DeleteItemAsync(ref quiz);
                // go to MainPage
                await Shell.Current.GoToAsync("..");
            });

            Results = new List<QuizResult>();
        }

        // Private members
        private Quiz quiz;

        // ICommand implementations
        public ICommand PlayQuizCommand { get; set; }
        public ICommand DeleteQuizCommand { get; set; }

        // Public properties
        public int ID { set => LoadQuiz(Convert.ToInt32(value)); }
        public List<QuizResult> Results { get; set; }
        public string QuizName => quiz is null ? string.Empty : quiz.Name;

        public string QuizInfo
        {
            get
            {
                if (quiz is null)
                {
                    return string.Empty;
                }
                string s = string.Empty;
                s += $"This quiz was created on:\n";
                s += $"{quiz.CreationDate:h\\:mm tt, dddd, MMM d, yyyy}.\n\n";
                s += $"There are {quiz.QuestionCount} questions.\n\n";
                s += "Play and see how well you do!";
                return s;
            }
        }

        // Methods
        public async void LoadQuiz(int id)
        {
            // get quiz
            quiz = await App.Database.GetItemAsync<Quiz>(id);

            // get result list, sorted by score (biggest first)
            Results = await App.Database.GetItemsAsync<QuizResult>();
            Results = Results.Where(item => item.QuizID == id).ToList();
            Results.Sort((x, y) =>
            {
                if (x.Score != y.Score)
                {
                    return x.Score > y.Score ? -1 : 1;
                }
                return x.Date < y.Date ? -1 : 1;
            });

            foreach (QuizResult result in Results)
            {
                Debug.WriteLine($"Result Score={result.Score} Date={result.Date}");
            }

            OnPropertyChanged(nameof(QuizName));
            OnPropertyChanged(nameof(QuizInfo));
            OnPropertyChanged(nameof(Results));
        }
    }
}
