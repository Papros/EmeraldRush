using EmeraldRush.Model.ConfigEnum;
using EmeraldRush.Model.GameEnum;
using EmeraldRush.Model.GameManager;
using EmeraldRush.ViewModels.Game;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmeraldRush.Views.Game
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MineExploringPage : ContentPage
    {
        readonly MineExploringViewModel viewModel;
        public MineExploringPage(IGameManager manager)
        {
            InitializeComponent();

            this.BindingContext = viewModel = new MineExploringViewModel(ScrollToLowerCard, AskForDecision, manager);

        }

        public void ScrollToLowerCard(int position)
        {
            this.CardCarousel.ScrollTo(position);
            if(DecisionBox.IsVisible) DecisionBox.RaiseChild(this.Content);
        }

        public async void AskForDecision(int decisionTime)
        {
            
            Device.BeginInvokeOnMainThread(async () => 
            {
                //DecisionBox.IsVisible = true;

                this.CardCarousel.Focus();
                this.timeBar.Focus();
                this.timeBar.Progress = 1;
                await this.timeBar.ProgressTo(0, (uint)decisionTime * 1000, Easing.Linear);

                //DecisionBox.IsVisible = false;
            });            

        }

        private void Decision_No_Clicked(object sender, EventArgs e)
        {
            this.viewModel.MakeDecision(false);
           // Device.BeginInvokeOnMainThread(() => DecisionBox.IsVisible = false);
        }

        private void Decision_Yes_Clicked(object sender, EventArgs e)
        {
            this.viewModel.MakeDecision(true);
            //Device.BeginInvokeOnMainThread(() => DecisionBox.IsVisible = false);
        }
    }
}