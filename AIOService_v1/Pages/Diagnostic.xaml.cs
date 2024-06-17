namespace AIOService_v1.Pages;

public partial class Diagnostic : ContentPage
{
	public Diagnostic()
	{
		InitializeComponent();
	}

    private async void OnErrorButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ErrorSearch());
    }

    private async void OnECUSearchButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ECUSearch());
    }

    private void OnInstructionsButtonClicked(object sender, EventArgs e)
    {
        // Handle Instructions button click
    }

    private void OnContactButtonClicked(object sender, EventArgs e)
    {
        // Handle Contact button click
    }
}