using EmeraldRush.ViewModels.Menu;
using EmeraldRush.Views.Lobby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmeraldRush.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : ContentPage
    {
        MainMenuViewModel viewModel;

        public MainMenuPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MainMenuViewModel();
        }

        private void Multiplayer_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new OnlineGameLobbyPage());
        }

        private void Settings_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new SettingsPage());
        }
    }
}