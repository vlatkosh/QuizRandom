using QuizRandom.ViewModels;
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
    }
}