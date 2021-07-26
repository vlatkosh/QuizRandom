using QuizRandom.Views;
using Xamarin.Forms;

namespace QuizRandom
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(NewAutoPage), typeof(NewAutoPage));
            Routing.RegisterRoute(nameof(EditQuizPage), typeof(EditQuizPage));
            Routing.RegisterRoute(nameof(EditQuestionPage), typeof(EditQuestionPage));
            Routing.RegisterRoute(nameof(InfoPage), typeof(InfoPage));
            Routing.RegisterRoute(nameof(PlayingPage), typeof(PlayingPage));
            Routing.RegisterRoute(nameof(EndPage), typeof(EndPage));
        }
    }
}
