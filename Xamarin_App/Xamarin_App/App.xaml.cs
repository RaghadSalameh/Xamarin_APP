using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_App.Data;

namespace Xamarin_App
{
    public partial class App : Application
    {
        static FieldDatabaseController database;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationDrawer();
        }

        public static FieldDatabaseController Database
        {
            get
            {
                if (database == null)
                {
                    database = new FieldDatabaseController();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
           
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
