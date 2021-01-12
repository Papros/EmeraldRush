using EmeraldRush.ViewModels.Instruction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmeraldRush.Views.Instruction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionsPage : ContentPage
    {
        InstructionViewModel viewModel;
        public InstructionsPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new InstructionViewModel();
        }

        private void Menu_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => {
                (Application.Current.MainPage).Navigation.PopModalAsync(true);
            });
        }
    }
}