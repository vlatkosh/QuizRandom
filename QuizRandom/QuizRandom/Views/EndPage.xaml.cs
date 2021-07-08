using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizRandom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EndPage : ContentPage
    {
        public EndPage()
        {
            InitializeComponent();
            //BindingContext = new EndViewModel();
        }
    }
}