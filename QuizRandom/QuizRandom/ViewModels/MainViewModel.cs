using QuizRandom.Services;
using QuizRandom.Models;
using QuizRandom.ViewModels;
using QuizRandom.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace QuizRandom.ViewModels
{
    public class MainViewModel : MyBindableObject
    {
        // Constructor
        public MainViewModel()
        {
            Debug.WriteLine($"{this.GetType()} constructor");

            GoToQuizGenPageCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(QuizGenPage));
            });

            GoToQuizInfoPageCommand = new Command(async () =>
            {
                if (SelectedQuiz is null || SelectedQuiz.ID == 0)
                {
                    return;
                }
                await Shell.Current.GoToAsync($"{nameof(QuizInfoPage)}?{nameof(QuizInfoPage.ItemId)}={SelectedQuiz.ID}");
            });
        }

        // Public properties
        public List<Quiz> Quizzes { get; private set; }
        public Quiz SelectedQuiz { get; set; }

        // ICommand implementations
        public ICommand GoToQuizGenPageCommand { get; protected set; }
        public ICommand GoToQuizInfoPageCommand { get; protected set; }

        // Methods
        public async Task ReloadQuizzesAsync()
        {
            // Called by the page's overloaded OnAppearing method
            Quizzes = await App.Database.GetQuizzesAsync();
            OnPropertyChanged(nameof(Quizzes));
        }
    }
}
