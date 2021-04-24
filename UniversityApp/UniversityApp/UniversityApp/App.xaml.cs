using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniveristyApp.Views;

namespace UniversityApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StudentListView());
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
