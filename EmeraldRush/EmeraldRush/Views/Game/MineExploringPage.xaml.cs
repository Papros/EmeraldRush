using EmeraldRush.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmeraldRush.Views.Game
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MineExploringPage : ContentPage
    {
        readonly MineExploringViewModel viewModel;
        public MineExploringPage()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new MineExploringViewModel(ScrollToLowerCard, AskForDecision);

        }

        public void ScrollToLowerCard(int position)
        {
            this.CardCarousel.ScrollTo(position);
        }

        public async void AskForDecision(int decisionTime)
        {
            Device.BeginInvokeOnMainThread(() => DecisionBox.IsVisible = true);
            this.timeBar.Progress = 1;
            await this.timeBar.ProgressTo(0, (uint)decisionTime * 1000, Easing.Linear);
            Device.BeginInvokeOnMainThread(() =>
            {
                if (viewModel.waitingForDecision) { DecisionBox.IsVisible = false; }
            });
        }

        private void Decision_No_Clicked(object sender, EventArgs e)
        {
            this.viewModel.MakeDecision(false);
            Device.BeginInvokeOnMainThread(() => DecisionBox.IsVisible = false);
        }

        private void Decision_Yes_Clicked(object sender, EventArgs e)
        {
            this.viewModel.MakeDecision(true);
            Device.BeginInvokeOnMainThread(() => DecisionBox.IsVisible = false);
        }
    }
}