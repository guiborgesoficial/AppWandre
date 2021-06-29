using AppWandre.Views;
using Xamarin.Forms;

namespace AppWandre
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var página = new NavigationPage(new MainPage());
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
