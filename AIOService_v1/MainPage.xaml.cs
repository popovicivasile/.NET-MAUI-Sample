using Microsoft.Maui.Controls;
using AIOService_v1.Pages;

namespace AIOService_v1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAgricultureButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Diagnostic());
        }

        private async void OnConstructionButtonClicked(object sender, EventArgs e)
        {
            
        }

    }
}
