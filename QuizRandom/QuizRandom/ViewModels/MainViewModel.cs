using QuizRandom.ViewModels;
using QuizRandom.Models;
using QuizRandom.Views;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizRandom.ViewModels
{
    public class MainViewModel : BaseViewModel
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
        public List<Quiz> Quizzes { get; set; }
        public Quiz SelectedQuiz { get; set; }

        // ICommand implementations
        public ICommand GoToQuizGenPageCommand { get; set; }
        public ICommand GoToQuizInfoPageCommand { get; set; }

        // Methods
        public async Task ReloadQuizzesAsync()
        {
            // Called by the page's overloaded OnAppearing method
            Quizzes = await App.Database.GetItemsAsync<Quiz>();
            OnPropertyChanged(nameof(Quizzes));
        }
    }
}
