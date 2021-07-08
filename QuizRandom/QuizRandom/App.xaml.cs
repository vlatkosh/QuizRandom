using QuizRandom.Services.Database;
using System;
using System.IO;
using Xamarin.Forms;

namespace QuizRandom
{
    public partial class App : Application
    {
        public static LocalDatabase database;
        
        // create the database connection as a singleton
        public static LocalDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new LocalDatabase(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Database.db3")
                    );
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
