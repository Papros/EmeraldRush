
using EmeraldRush.ViewModels.Menu;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmeraldRush.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel viewModel;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SettingsViewModel();

            this.MusicLevelSlider.Minimum = viewModel.MinMusicValue;
            this.MusicLevelSlider.Maximum = viewModel.MaxMusicValue;

            viewModel.LoadData();
         }

        private void Button_Menu(object sender, System.EventArgs e)
        {
            viewModel.SaveData();
            Device.BeginInvokeOnMainThread(() => {
                (Application.Current.MainPage).Navigation.PopModalAsync(true);
            });
        }
    }
}