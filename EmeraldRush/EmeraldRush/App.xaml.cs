using EmeraldRush.Views.Menu;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmeraldRush
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainMenuPage());
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
