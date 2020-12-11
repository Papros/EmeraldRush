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
    public partial class SinglePlayerLobbyPage : ContentPage
    {
        SinglePlayerViewModel viewModel;
        public SinglePlayerLobbyPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new SinglePlayerViewModel();

            this.DecisionTimeSlider.Maximum = (double)viewModel.maxDecisiontime;
            this.DecisionTimeSlider.Minimum = (double)viewModel.minDecisionTime;
            this.DragonsDeepSlider.Maximum = (double)viewModel.maxDragonsDeep;
            this.DragonsDeepSlider.Minimum = (double)viewModel.minDragonsDeep;
            this.RoundNumberSlider.Maximum = (double)viewModel.maxRoundNumber;
            this.RoundNumberSlider.Minimum = (double)viewModel.minRoundNumber;
        }

        private void AddPlayer(object sender, EventArgs e)
        {
            this.viewModel.AddAIPlayer();
        }

        private void StartSinglePlayerGame(object sender, EventArgs e)
        {
            
            this.Navigation.PushModalAsync();
        }
    }
}