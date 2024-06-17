using AIOService_v1.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Reflection;

namespace AIOService_v1.Pages;

public partial class ECUSearch : ContentPage
{
    public List<string> Ecus { get; set; }
    public ObservableCollection<string> Errors { get; set; } = new ObservableCollection<string>();

    public bool IsErrorPickerVisible { get; set; } = false;

    public string SelectedEcu { get; set; }
    public string SelectedError { get; set; }

    public ECUSearch()
    {
        LoadEcus();
        InitializeComponent();
        this.BindingContext = this;


    }
    

        private async void SearchForError(object sender, EventArgs e)
        {
 

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
            if (errors.TryGetValue(SelectedError, out result))
            {
                ResultLabel.Text = result;
            }
            else
            {
                ResultLabel.Text = "Error not found";
            }
        }
    


    public async void OnEcuSelected(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        SelectedEcu = (string)picker.SelectedItem;

        // Load the data for the second Picker based on the selected ECU
        GetErrors(sender, e);
    }


    public async void GetErrors(object sender, EventArgs e)
    {
        var fff = await SearchErrorsAsync(SelectedEcu);

        Errors.Clear();
        foreach (var error in fff)
        {
            Errors.Add(error);
        }
    }




    private async void LoadEcus()
    {
        var data = await GetEcusAsync();
        Ecus = new List<string>(data);


    }
    private async Task<List<string>> SearchErrorsAsync(string selectedecu)
    {
        // Your logic to search errors related to SelectedEcu
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ErrorSearch)).Assembly;
        Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Json.Errors.json");

        string jsonString;
        using (var reader = new StreamReader(stream))
        {
            jsonString = await reader.ReadToEndAsync();
        }
        var error = JsonConvert.DeserializeObject<List<JohnDeere>>(jsonString);

        List<string> Errors = error.Where(x => x.Ecu == selectedecu).Select(s => s.Error).ToList();

        return Errors;
    }


    public async Task<List<string>> GetEcusAsync()
    {
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ECUSearch)).Assembly;
        Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Json.JDECUS.json");
        List<string> ecus = new List<string>();



        using (var reader = new StreamReader(stream))
        {
            var jsonString = await reader.ReadToEndAsync();
            var ggg = JsonConvert.DeserializeObject<List<ECUSDto>>(jsonString);

            ecus = ggg.Select(x => x.ECUS).ToList();

            return ecus;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}