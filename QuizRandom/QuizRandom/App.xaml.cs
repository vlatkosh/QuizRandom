using QuizRandom.Services;
using QuizRandom.Views;
using QuizRandom.ViewModels;
using QuizRandom.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizRandom
{
    public partial class App : Application
    {
        static QuizDatabase database;
        //static GameManager manager;

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

        //public static GameManager Manager
        //{
        //    get
        //    {
        //        if (manager == null)
        //        {
        //            manager = new GameManager();
        //        }
        //        return manager;
        //    }
        //}

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
