using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuizRandom.ViewModels;
using QuizRandom.Models;

namespace QuizRandom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(QuizId), nameof(QuizId))]
    [QueryProperty(nameof(Result), nameof(Result))]
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

        public string Result
        {
            set => ((GameEndViewModel)BindingContext).LoadResult(value);
        }

    }
}