using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizRandom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayingPage : ContentPage
    {
        public PlayingPage()
        {
            InitializeComponent();
            // BindingContext = new PlayingViewModel()
        }
    }
}