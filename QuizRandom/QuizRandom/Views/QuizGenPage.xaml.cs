using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizRandom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizGenPage : ContentPage
    {
        public QuizGenPage()
        {
            InitializeComponent();
            //BindingContext = new QuizGenViewModel();
        }
    }
}