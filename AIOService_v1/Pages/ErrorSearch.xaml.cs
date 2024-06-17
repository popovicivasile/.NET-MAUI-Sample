namespace AIOService_v1.Pages;
using AIOService_v1.Models;
using Newtonsoft.Json;
using System.Reflection;
public partial class ErrorSearch : ContentPage
{
    public ErrorSearch()
    {
        InitializeComponent();
    }

    private async void OnSearchButtonClicked(object sender, EventArgs e)
    {
        string errorToSearch = ErrorSearchBar.Text.PadLeft(9, '0'); // Pad the error code with leading zeros

        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ErrorSearch)).Assembly;
        Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Json.Errors.json");
        if (stream == null)
        {
            ResultLabel.Text = "Error: Could not find Errors.json";
            return;
        }

        string jsonString;
        using (var reader = new StreamReader(stream))
        {
            jsonString = await reader.ReadToEndAsync();
        }

        var error = JsonConvert.DeserializeObject<List<JohnDeere>>(jsonString);
        var errors = error.ToDictionary(x => x.Error, x => x.Description);

        // Search for the error
        string result;
        if (errors.TryGetValue(errorToSearch, out result))
        {
            ResultLabel.Text = result;
        }
        else
        {
            ResultLabel.Text = "Error not found";
        }
    }

}
