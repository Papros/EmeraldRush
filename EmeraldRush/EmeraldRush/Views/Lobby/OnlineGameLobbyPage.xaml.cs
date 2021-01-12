using EmeraldRush.Model.GameManager;
using EmeraldRush.Services;
using EmeraldRush.ViewModels.Lobby;
using EmeraldRush.Views.Game;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmeraldRush.Views.Lobby
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnlineGameLobbyPage : ContentPage
    {
        OnlineGameLobbyViewModel viewModel;

        public OnlineGameLobbyPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new OnlineGameLobbyViewModel();
        }

        private void PlayerMode_2_Clicked(object sender, EventArgs e)
        {
            Task.Run(() => viewModel.SignInToPlayersQueue(2, OpenViewPage));
        }

        private void PlayerMode_4_Clicked(object sender, EventArgs e)
        {
            Task.Run(() => viewModel.SignInToPlayersQueue(4, OpenViewPage));
        }

        private void PlayerMode_8_Clicked(object sender, EventArgs e)
        {
            Task.Run(() => viewModel.SignInToPlayersQueue(8, OpenViewPage));
        }

        private void OpenViewPage(IGameManager manager)
        {
            LogManager.Print("Opening MainExploringPage from GameLobby");
            Device.BeginInvokeOnMainThread(() => (Application.Current.MainPage).Navigation.PushModalAsync(new MineExploringPage(manager)));

        }

        private void Menu_Clicked(object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => {
                (Application.Current.MainPage).Navigation.PopModalAsync(true);
            });
        }

    }
}