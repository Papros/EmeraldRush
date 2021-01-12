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
            //CardCarousel.ScrollTo(position);
            if (DecisionBox.IsVisible) DecisionBox.RaiseChild(Content);
        }

        public void AskForDecision(int decisionTime)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (viewModel.DecisionBoxDisapearing) DecisionBox.IsVisible = true;
                CardCarousel.Focus();
                timeBar.Focus();
                timeBar.Progress = 1;
                await timeBar.ProgressTo(0, (uint)decisionTime * 1000, Easing.Linear);
                if (viewModel.DecisionBoxDisapearing) DecisionBox.IsVisible = false;
            });

        }

        private void Decision_No_Clicked(object sender, EventArgs e)
        {
            viewModel.MakeDecision(false);
            if(viewModel.DecisionBoxDisapearing)  Device.BeginInvokeOnMainThread(() => DecisionBox.IsVisible = false);
        }

        private void Decision_Yes_Clicked(object sender, EventArgs e)
        {
            viewModel.MakeDecision(true);
            if (viewModel.DecisionBoxDisapearing) Device.BeginInvokeOnMainThread(() => DecisionBox.IsVisible = false);
        }

        private void Return_to_menu(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread( () => {
                (Application.Current.MainPage).Navigation.PopModalAsync(true);
            });

        }
    }
}