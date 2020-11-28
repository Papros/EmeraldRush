using EmeraldRush.Services;
using EmeraldRush.ViewModels.Lobby;
using EmeraldRush.Views.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            this.BindingContext = viewModel = new OnlineGameLobbyViewModel();
        }

        private void PlayerMode_2_Clicked(object sender, EventArgs e)
        {
            Task.Run( () => this.viewModel.SignInToPlayersQueue(2, OpenViewPage) );
        }

        private void PlayerMode_4_Clicked(object sender, EventArgs e)
        {
            Task.Run( () => this.viewModel.SignInToPlayersQueue(4, OpenViewPage) );
        }

        private void PlayerMode_8_Clicked(object sender, EventArgs e)
        {
            Task.Run( () => this.viewModel.SignInToPlayersQueue(8, OpenViewPage) );
        }

        private void OpenViewPage()
        {
            LogManager.Print("Opening MainExploringPage from GameLobby");
            Device.BeginInvokeOnMainThread( () => (Application.Current.MainPage).Navigation.PushModalAsync(new MineExploringPage()) );
            
        }

    }
}