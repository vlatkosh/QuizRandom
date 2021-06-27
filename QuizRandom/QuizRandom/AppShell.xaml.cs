using QuizRandom.Views;
using Xamarin.Forms;

namespace QuizRandom
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(QuizInfoPage), typeof(QuizInfoPage));
            Routing.RegisterRoute(nameof(QuizGenPage), typeof(QuizGenPage));
            Routing.RegisterRoute(nameof(GamePlayPage), typeof(GamePlayPage));
            Routing.RegisterRoute(nameof(GameEndPage), typeof(GameEndPage));
        }
    }
}
