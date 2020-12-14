using EmeraldRush.ViewModels.Lobby;
using EmeraldRush.Views.Game;
using System;

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
            BindingContext = viewModel = new SinglePlayerLobbyViewModel();

            DecisionTimeSlider.Maximum = viewModel.maxDecisiontime;
            DecisionTimeSlider.Minimum = viewModel.minDecisionTime;
            DragonsDeepSlider.Maximum = viewModel.maxDragonsDeep;
            DragonsDeepSlider.Minimum = viewModel.minDragonsDeep;
            RoundNumberSlider.Maximum = viewModel.maxRoundNumber;
            RoundNumberSlider.Minimum = viewModel.minRoundNumber;
            diffSlider.Maximum = viewModel.maxDifficulty;
            diffSlider.Minimum = viewModel.minDifficulty;
        }

        private void AddPlayer(object sender, EventArgs e)
        {
            viewModel.AddAIPlayer();
        }

        public void Done(object sender, EventArgs e)
        {
            viewModel.IsSelectionDone();
        }

        public void BotRemoved(object sender, EventArgs e)
        {
            viewModel.RemoveBot((sender as Button).BindingContext);
        }

        private void StartSinglePlayerGame(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => (Application.Current.MainPage).Navigation.PushModalAsync(new MineExploringPage(viewModel.GetGameManager())));
        }

    }
}