using EmeraldRush.ViewModels.Lobby;
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
            this.viewModel.AwaitForGame(2);
        }

        private void PlayerMode_4_Clicked(object sender, EventArgs e)
        {
            this.viewModel.AwaitForGame(4);
        }

        private void PlayerMode_8_Clicked(object sender, EventArgs e)
        {
            this.viewModel.AwaitForGame(8);
        }
    }
}