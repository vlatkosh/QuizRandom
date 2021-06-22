using Newtonsoft.Json;
using QuizRandom.Models;
using QuizRandom.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizRandom.ViewModels
{
    public class GamePlayViewModel : MyBindableObject
    {
        // Constructor
        public GamePlayViewModel()
        {
            Debug.WriteLine($"{this.GetType()} constructor");

            InterpretAnswerCommand = new Command(() => InterpretAnswer());
        }

        // Private members
        private List<int> questionOrder;
        private int lastQuestionLoaded;

        // Public properties
        public Quiz CurrentQuiz { get; private set; }
        public List<QuizQuestion> Questions { get; private set; }
        public QuizResult CurrentAttempt { get; private set; }
        public int QuestionNumber { get; private set; }
        public List<string> Answers { get; private set; }
        public string SelectedAnswer { get; set; }

        public string QuestionText
        {
            get
            {
                if (QuestionNumber >= 0 && QuestionNumber < Questions.Count)
                {
                    return Questions[questionOrder[QuestionNumber]].Question;
                }
                return string.Empty;
            }
        }

        // ICommand implementations
        public ICommand InterpretAnswerCommand { get; protected set; }

        // Methods
        public async void LoadQuiz(string itemId)
        {
            int id = Convert.ToInt32(itemId);
            CurrentQuiz = await App.Database.GetQuizAsync(id);

            JSONRootObject rootObject = JsonConvert.DeserializeObject<JSONRootObject>(CurrentQuiz.JSONData);
            Questions = rootObject.Results;

            CurrentAttempt = new QuizResult();
            
            Random rnd = new Random();
            questionOrder = new List<int>(Questions.Count).OrderBy(_ => rnd.Next()).ToList();

            QuestionNumber = 0;
            OnPropertyChanged(nameof(QuestionNumber));

            lastQuestionLoaded = -1;
        }

        public async void LoadQuestion()
        {
            // Called by GamePlayPage.xaml.cs when OnAppearing
            if (QuestionNumber == Questions.Count)
            {
                // finished, go to end page
                string resultSerialized = JsonConvert.SerializeObject(CurrentAttempt);
                await Shell.Current.GoToAsync(
                    $"{nameof(GameEndPage)}" +
                    $"?{nameof(GameEndPage.QuizId)}={CurrentQuiz.ID}" +
                    $"&{nameof(GameEndPage.Result)}={resultSerialized}"
                );
                return;
            }
            if (lastQuestionLoaded == QuestionNumber)
            {
                // no need to update things
                return;
            }

            // load the question
            OnPropertyChanged(nameof(QuestionText));

            // list the answers in a random order
            Random rnd = new Random();
            Answers = new List<string>() { Questions[questionOrder[QuestionNumber]].CorrectAnswer };
            Answers.AddRange(Questions[questionOrder[QuestionNumber]].IncorrectAnswers);
            Answers = Answers.OrderBy(_ => rnd.Next()).ToList();
            OnPropertyChanged(nameof(Answers));

            SelectedAnswer = null;
            OnPropertyChanged(nameof(SelectedAnswer));

            lastQuestionLoaded = QuestionNumber;
        }

        public void InterpretAnswer()
        {
            if (SelectedAnswer == Questions[questionOrder[QuestionNumber]].CorrectAnswer)
            {
                // correct!
                //CurrentAttempt.RegisterCorrect();
                CurrentAttempt.CorrectCount += 1;
            }
            else
            {
                // incorrect!

            }

            QuestionNumber += 1;
            OnPropertyChanged(nameof(QuestionNumber));

            LoadQuestion();
        }

    }
}
