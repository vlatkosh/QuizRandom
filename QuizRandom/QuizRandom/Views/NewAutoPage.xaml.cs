using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizRandom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAutoPage : ContentPage
    {
        public NewAutoPage()
        {
            InitializeComponent();
            //BindingContext = new NewAutoViewModel();
        }
    }
}