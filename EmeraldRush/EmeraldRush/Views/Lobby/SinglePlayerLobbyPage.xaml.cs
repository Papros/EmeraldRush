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
    public partial class SinglePlayerLobbyPage : ContentPage
    {
        SinglePlayerLobbyViewModel viewModel;
        public SinglePlayerLobbyPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new SinglePlayerLobbyViewModel();

            this.DecisionTimeSlider.Maximum = viewModel.maxDecisiontime;
            this.DecisionTimeSlider.Minimum = viewModel.minDecisionTime;
            this.DragonsDeepSlider.Maximum = viewModel.maxDragonsDeep;
            this.DragonsDeepSlider.Minimum = viewModel.minDragonsDeep;
            this.RoundNumberSlider.Maximum = viewModel.maxRoundNumber;
            this.RoundNumberSlider.Minimum = viewModel.minRoundNumber;
            this.diffSlider.Maximum = viewModel.maxDifficulty;
            this.diffSlider.Minimum = viewModel.minDifficulty;
        }

        private void AddPlayer(object sender, EventArgs e)
        {
            this.viewModel.AddAIPlayer();
        }

        public void Done(object sender, EventArgs e)
        {
            this.viewModel.selectionDone();
        }

        public void BotRemoved(object sender, EventArgs e)
        {
            this.viewModel.RemoveBot((sender as Button).BindingContext);
        }

        private void StartSinglePlayerGame(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread( () => (Application.Current.MainPage).Navigation.PushModalAsync(new MineExploringPage(viewModel.GetGameManager())) );
        }

    }
}