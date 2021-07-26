using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using QuizRandom.Models;
using QuizRandom.Services;
using QuizRandom.Views;
using System.Diagnostics;
using System.Net;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace QuizRandom.ViewModels
{
    [QueryProperty(nameof(IncomingQuestion), nameof(IncomingQuestion))]
    public class EditQuestionViewModel : BaseViewModel
    {
        // Constructor
        public EditQuestionViewModel()
        {

        }

        // ICommand implementations

        // Private members
        private QuizQuestion question;

        // Public properties
        public string IncomingQuestion
        {
            set => question = JsonConvert.DeserializeObject<QuizQuestion>(value);
        }

        // Methods
    }
}
