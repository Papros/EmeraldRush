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
        internal MineExploringPage(IGameManager manager)
        {
            InitializeComponent();

            BindingContext = viewModel = new MineExploringViewModel(ScrollToLowerCard, AskForDecision, manager);

        }

        public void ScrollToLowerCard(int position)
        {
            CardCarousel.ScrollTo(position);
            if (DecisionBox.IsVisible) DecisionBox.RaiseChild(Content);
        }

        public async void AskForDecision(int decisionTime)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                //DecisionBox.IsVisible = true;

                CardCarousel.Focus();
                timeBar.Focus();
                timeBar.Progress = 1;
                await timeBar.ProgressTo(0, (uint)decisionTime * 1000, Easing.Linear);

                //DecisionBox.IsVisible = false;
            });

        }

        private void Decision_No_Clicked(object sender, EventArgs e)
        {
            viewModel.MakeDecision(false);
            // Device.BeginInvokeOnMainThread(() => DecisionBox.IsVisible = false);
        }

        private void Decision_Yes_Clicked(object sender, EventArgs e)
        {
            viewModel.MakeDecision(true);
            //Device.BeginInvokeOnMainThread(() => DecisionBox.IsVisible = false);
        }
    }
}