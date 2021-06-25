using QuizRandom.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/*
 *  The segfault seems to happen as soon as the object of this instantiated,
 *  or right after Shell.Current.GoToAsync(this page) is called.
 */

namespace QuizRandom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(QuizId), nameof(QuizId))]
    [QueryProperty(nameof(CorrectCount), nameof(CorrectCount))]
    public partial class GameEndPage : ContentPage
    {
        public GameEndPage()
        {
            InitializeComponent();
            //BindingContext = new GameEndViewModel();
        }

        public string QuizId
        {
            set => ((GameEndViewModel)BindingContext).LoadQuiz(value);
        }

        public string CorrectCount
        {
            set => ((GameEndViewModel)BindingContext).CorrectCount = Convert.ToInt32(value);
        }

    }
}