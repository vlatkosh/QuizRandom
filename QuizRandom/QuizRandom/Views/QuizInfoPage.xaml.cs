using QuizRandom.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizRandom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class QuizInfoPage : ContentPage
    {
        public QuizInfoPage()
        {
            InitializeComponent();
            //BindingContext = new QuizInfoViewModel();
        }

        public string ItemId
        {
            set => ((QuizInfoViewModel)BindingContext).LoadQuiz(value);
        }
    }
}