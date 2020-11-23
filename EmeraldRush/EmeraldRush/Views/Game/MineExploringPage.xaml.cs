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
        MineExploringViewModel viewModel;
        public MineExploringPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new MineExploringViewModel();
        }
    }
}