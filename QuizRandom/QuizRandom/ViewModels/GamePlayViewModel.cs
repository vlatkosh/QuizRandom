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

            quizLoaded = false;

            InterpretAnswerCommand = new Command(() => InterpretAnswer());
        }

        // Private members
        private bool quizLoaded;
        private Quiz currentQuiz;

        private List<QuizQuestion> questions;
        private List<int> questionOrder;
        private int lastQuestionLoaded;

        // Public properties
        public int CorrectCount { get; private set; }
        public int QuestionNumber { get; private set; }
        public List<string> Answers { get; private set; }
        public string SelectedAnswer { get; set; }

        public string QuestionText
        {
            get
            {
                if (!quizLoaded || QuestionNumber < 0 || QuestionNumber > questions.Count)
                {
                    return string.Empty;
                }
                return questions[questionOrder[QuestionNumber]].Question;
            }
        }

        // ICommand implementations
        public ICommand InterpretAnswerCommand { get; protected set; }

        // Methods
        public async void LoadQuiz(string itemId)
        {
            if (quizLoaded)
            {
                return;
            }

            int id = Convert.ToInt32(itemId);
            currentQuiz = await App.Database.GetQuizAsync(id);

            JSONRootObject rootObject = JsonConvert.DeserializeObject<JSONRootObject>(currentQuiz.JSONData);
            questions = rootObject.Results;

            questionOrder = new List<int>(questions.Count);
            for (int i = 0; i < questions.Count; i++)
            {
                questionOrder.Add(i);
            }
            Random rnd = new Random();
            questionOrder = questionOrder.OrderBy(i => rnd.Next()).ToList();

            lastQuestionLoaded = -1;

            CorrectCount = 0;
            OnPropertyChanged(nameof(CorrectCount));

            QuestionNumber = 0;
            OnPropertyChanged(nameof(QuestionNumber));

            quizLoaded = true;

            Debug.WriteLine("GamePlayVM: Loaded quiz");

            await LoadQuestion();
        }

        public async Task LoadQuestion()
        {
            if (!quizLoaded)
            {
                return;
            }
            if (QuestionNumber == questions.Count)
            {
                // finished, go to end page

                /*
                 *  The segfault seems to happen as soon as the object of the next page instantiated,
                 *  or right after the following function is called.
                 */

                await Shell.Current.GoToAsync(
                    $"{nameof(GameEndPage)}" +
                    $"?{nameof(GameEndPage.QuizId)}={currentQuiz.ID}" +
                    $"&{nameof(GameEndPage.CorrectCount)}={CorrectCount}"
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
            Answers = new List<string>() { questions[questionOrder[QuestionNumber]].CorrectAnswer };
            Answers.AddRange(questions[questionOrder[QuestionNumber]].IncorrectAnswers);
            Answers = Answers.OrderBy(_ => rnd.Next()).ToList();
            OnPropertyChanged(nameof(Answers));

            SelectedAnswer = null;
            OnPropertyChanged(nameof(SelectedAnswer));

            lastQuestionLoaded = QuestionNumber;
        }

        public async void InterpretAnswer()
        {
            if (SelectedAnswer == null)
            {
                // For some reason the command is fired more than once per tap, so this handles that
                return;
            }
            if (SelectedAnswer == questions[questionOrder[QuestionNumber]].CorrectAnswer)
            {
                // correct!
                //CurrentAttempt.RegisterCorrect();
                CorrectCount += 1;
                OnPropertyChanged(nameof(CorrectCount));
            }
            else
            {
                // incorrect!

            }

            QuestionNumber += 1;
            OnPropertyChanged(nameof(QuestionNumber));

            await LoadQuestion();
        }

    }
}
