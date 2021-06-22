using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuizRandom.ViewModels;

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