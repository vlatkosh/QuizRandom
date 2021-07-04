using QuizRandom.Models;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuizRandom.ViewModels
{
    public class AboutViewModel : MyBindableObject
    {
        // Constructor
        public AboutViewModel()
        {
            OpenGithubCommand = new Command(async () =>
            {
                await Launcher.OpenAsync("https://github.com/vlatkosh/QuizRandom");
            });

            DeleteAllCommand = new Command(async () =>
            {
                bool answer = await Shell.Current.DisplayAlert(
                    "Confirmation",
                    "Are you sure you want to delete ALL data (quizzes and results)?",
                    "Yes",
                    "No"
                );
                if (answer == false)
                {
                    return;
                }
                await App.Database.DeleteEverythingAsync();
            });
        }

        // ICommand implementations
        public ICommand OpenGithubCommand { get; set; }
        public ICommand DeleteAllCommand { get; set; }

    }
}
