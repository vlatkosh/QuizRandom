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

/*
 * Todo: figure out how to handle adding and editing questions neatly,
 * and passing back data from editquestionpage to delete or not
 * (maybe remove this feature and add a swiping gesture to each
 * collectionview item instead)?
 */

namespace QuizRandom.ViewModels
{
    [QueryProperty(nameof(QuizID), nameof(QuizID))]
    [QueryProperty(nameof(IncomingQuestion), nameof(IncomingQuestion))]
    public class EditQuizViewModel : BaseViewModel
    {
        // Constructor
        public EditQuizViewModel()
        {
            NewQuestionCommand = new Command(NewQuestion);
            EditQuestionCommand = new Command(EditQuestion);
            SaveQuizCommand = new Command(SaveQuiz);

            quiz = new Quiz();
            Questions = new List<QuizQuestion>();
            currentlyEditingIndex = -1;
        }

        // ICommand implementations
        public ICommand NewQuestionCommand { get; set; }
        public ICommand EditQuestionCommand { get; set; }
        public ICommand SaveQuizCommand { get; set; }

        // Private members
        private int currentlyEditingIndex;
        private Quiz quiz;

        // Public properties
        public string QuizID
        {
            set => LoadQuiz(Convert.ToInt32(value));
        }

        public string IncomingQuestion
        {
            set => UpdateQuestion(currentlyEditingIndex, ref value);
        }

        public List<QuizQuestion> Questions { get; set; }
        public QuizQuestion SelectedQuestion { get; set; }

        public string QuizName
        {
            get => quiz.Name;
            set => quiz.Name = value;
        }

        // Methods
        private async void LoadQuiz(int id)
        {
            quiz = await App.Database.GetItemAsync<Quiz>(id);
            Questions = JsonConvert.DeserializeObject<List<QuizQuestion>>(quiz.QuestionsSerialized);
            OnPropertyChanged(nameof(QuizName));
            OnPropertyChanged(nameof(Questions));
        }

        private async void SaveQuiz()
        {
            quiz.QuestionCount = Questions.Count;
            quiz.QuestionsSerialized = JsonConvert.SerializeObject(Questions);
            await App.Database.SaveItemAsync(ref quiz);
            await Shell.Current.GoToAsync("..");
        }

        private void NewQuestion()
        {
            Questions.Add(new QuizQuestion());
            SelectedQuestion = Questions[^1];
            EditQuestion();
        }

        private async void EditQuestion()
        {
            // Go to question editing page with the selected question
            if (SelectedQuestion is null)
            {
                return;
            }
            currentlyEditingIndex = Questions.IndexOf(SelectedQuestion);
            await Shell.Current.GoToAsync(
                $"{nameof(EditQuestionPage)}" +
                $"?{nameof(EditQuestionViewModel.IncomingQuestion)}" +
                $"={JsonConvert.SerializeObject(SelectedQuestion)}"
            );
        }

        private void UpdateQuestion(int index, ref string data)
        {
            // Update the relevant question
            if (currentlyEditingIndex < 0 || currentlyEditingIndex >= Questions.Count)
            {
                return;
            }
            if (data == string.Empty)
            {
                // delete it
            }
            else
            {
                // update it
            }
            Questions[currentlyEditingIndex] = JsonConvert.DeserializeObject<QuizQuestion>(value);
            OnPropertyChanged(nameof(Questions));
            currentlyEditingIndex = -1;
        }

    }
}
