using QuizRandom.Services;
using System;
using System.IO;
using Xamarin.Forms;

namespace QuizRandom
{
    public partial class App : Application
    {
        public static QuizDatabase database;
        
        // create the database connection as a singleton
        public static QuizDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new QuizDatabase(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Quizzes.db3"));
                }
                return database;
            }
        }
        
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
