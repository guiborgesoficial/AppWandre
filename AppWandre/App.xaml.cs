using AppWandre.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWandre
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var página = new NavigationPage(new PageDadosCarro());
            MainPage = página;
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
