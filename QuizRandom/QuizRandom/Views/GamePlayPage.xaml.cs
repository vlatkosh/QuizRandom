using QuizRandom.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizRandom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class GamePlayPage : ContentPage
    {
        public GamePlayPage()
        {
            InitializeComponent();
            //BindingContext = new GamePlayViewModel();
        }

        public string ItemId
        {
            set => ((GamePlayViewModel)BindingContext).LoadQuiz(value);
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    Debug.WriteLine("GamePlayPage OnAppearing");
        //    await ((GamePlayViewModel)BindingContext).LoadQuestion();
        //}
    }
}