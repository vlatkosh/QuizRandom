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
    [QueryProperty(nameof(ID), nameof(ID))]
    public class PlayingViewModel : BaseViewModel
    {
        // Constructor
        public PlayingViewModel()
        {
            Debug.WriteLine($"{this.GetType()} constructor");

            quizLoaded = false;

            InterpretAnswerCommand = new Command(async() => await InterpretAnswer());
        }

        // Private members
        private bool quizLoaded;
        private Quiz currentQuiz;

        private List<QuizQuestion> questions;
        private List<int> questionOrder;
        private int lastQuestionLoaded;
        private int questionNumber;

        // Public properties
        public int ID { set => LoadQuiz(Convert.ToInt32(value)); }
        public int Score { get; set; }
        public List<string> Answers { get; set; }
        public string SelectedAnswer { get; set; }
        public Color AnswerColor { get; set; }

        public string QuestionInfo
        {
            get
            {
                if (!ShouldContinuePlaying())
                {
                    return string.Empty;
                }

                string s = string.Empty;
                s += $"Question number {questionNumber + 1}\n";
                s += $"Category: {questions[questionOrder[questionNumber]].Category}\n";
                //s += $"Type: {questions[questionOrder[questionNumber]].Type}\n";
                s += $"Difficulty: {questions[questionOrder[questionNumber]].Difficulty}\n";
                return s;
            }
        }

        public string QuestionText
        {
            get
            {
                if (!ShouldContinuePlaying())
                {
                    return string.Empty;
                }
                return questions[questionOrder[questionNumber]].Question;
            }
        }

        // ICommand implementations
        public ICommand InterpretAnswerCommand { get; protected set; }

        // Methods
        public async void LoadQuiz(int id)
        {
            if (quizLoaded)
            {
                return;
            }
            currentQuiz = await App.Database.GetItemAsync<Quiz>(id);

            questions = JsonConvert.DeserializeObject<List<QuizQuestion>>(currentQuiz.QuestionDataRaw);

            questionOrder = new List<int>(questions.Count);
            for (int i = 0; i < questions.Count; i++)
            {
                questionOrder.Add(i);
            }
            Random rnd = new Random();
            questionOrder = questionOrder.OrderBy(i => rnd.Next()).ToList();

            lastQuestionLoaded = -1;
            questionNumber = 0;

            Score = 0;
            OnPropertyChanged(nameof(Score));

            AnswerColor = (Color)Application.Current.Resources["AppPrimaryColor"];
            OnPropertyChanged(nameof(AnswerColor));

            quizLoaded = true;

            Debug.WriteLine("Loaded the quiz");

            await LoadQuestion();
        }

        public async Task LoadQuestion()
        {
            if (!quizLoaded)
            {
                return;
            }
            if (questionNumber == questions.Count)
            {
                await GoToEnd();
                return;
            }
            if (lastQuestionLoaded == questionNumber)
            {
                // no need to update things
                return;
            }

            // load the question
            OnPropertyChanged(nameof(QuestionInfo));
            OnPropertyChanged(nameof(QuestionText));

            // list the answers in a random order
            Random rnd = new Random();
            Answers = new List<string>() { questions[questionOrder[questionNumber]].CorrectAnswer };
            Answers.AddRange(questions[questionOrder[questionNumber]].IncorrectAnswers);
            Answers = Answers.OrderBy(_ => rnd.Next()).ToList();
            OnPropertyChanged(nameof(Answers));

            SelectedAnswer = null;
            OnPropertyChanged(nameof(SelectedAnswer));

            AnswerColor = (Color)Application.Current.Resources["AppPrimaryColor"];
            OnPropertyChanged(nameof(AnswerColor));

            lastQuestionLoaded = questionNumber;
        }

        public async Task InterpretAnswer()
        {
            if (SelectedAnswer == null)
            {
                // For some reason the command is fired more than once per tap, so this handles that
                Debug.WriteLine("Already interpreting answer");
                return;
            }

            if (SelectedAnswer == questions[questionOrder[questionNumber]].CorrectAnswer)
            {
                // correct
                Score += 1;
                OnPropertyChanged(nameof(Score));
                
                AnswerColor = (Color)Shell.Current.CurrentPage.Resources["CorrectColor"];
                OnPropertyChanged(nameof(AnswerColor));
            }
            else
            {
                // incorrect
                AnswerColor = (Color)Shell.Current.CurrentPage.Resources["IncorrectColor"];
                OnPropertyChanged(nameof(AnswerColor));
            }

            // Let the visual effect be visible
            await Task.Delay(1000);

            // Load next question
            questionNumber += 1;
            await LoadQuestion();
        }

        private async Task GoToEnd()
        {
            // finished, go to end page
            await Shell.Current.GoToAsync(
                $"{nameof(EndPage)}" +
                $"?{nameof(EndViewModel.ID)}={currentQuiz.ID}" +
                $"&{nameof(EndViewModel.Score)}={Score}" +
                $"&{nameof(EndViewModel.QuestionCount)}={currentQuiz.QuestionCount}"
            );
        }

        private bool ShouldContinuePlaying()
        {
            return quizLoaded && questionNumber >= 0 && questionNumber < questions.Count;
        }
    }
}
