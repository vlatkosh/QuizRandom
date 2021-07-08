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

            GoToAboutPageCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(AboutPage));
            });

            GoToNewAutoPageCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(NewAutoPage));
            });

            GoToInfoPageCommand = new Command(async () =>
            {
                if (SelectedQuiz is null || SelectedQuiz.ID == 0)
                {
                    return;
                }
                await Shell.Current.GoToAsync($"{nameof(InfoPage)}?{nameof(InfoViewModel.ID)}={SelectedQuiz.ID}");
            });
        }

        // Public properties
        public List<Quiz> Quizzes { get; set; }
        public Quiz SelectedQuiz { get; set; }

        // ICommand implementations
        public ICommand GoToAboutPageCommand { get; set; }
        public ICommand GoToNewAutoPageCommand { get; set; }
        public ICommand GoToInfoPageCommand { get; set; }

        // Methods
        public async Task ReloadQuizzesAsync()
        {
            // Called by the page's overloaded OnAppearing method
            Quizzes = await App.Database.GetItemsAsync<Quiz>();
            OnPropertyChanged(nameof(Quizzes));
        }
    }
}
