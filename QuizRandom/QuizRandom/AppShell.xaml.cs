using QuizRandom.ViewModels;
using QuizRandom.Views;
using System;
using System.Collections.Generic;
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
