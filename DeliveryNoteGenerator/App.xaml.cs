using System.Configuration;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using DeliveryNoteGenerator.modals;
using QuestPDF.Infrastructure;

namespace DeliveryNoteGenerator;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        QuestPDF.Settings.License = LicenseType.Community;
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string apiKeyFile = Path.Combine(appDataPath, "DeliveryNoteGenerator", "key.json");
        if (File.Exists(apiKeyFile))
        {
            string rawFile = File.ReadAllText(apiKeyFile);
            var jsonFile = JsonSerializer.Deserialize<ApiKeyDto>(rawFile);
            Client.setApiKey(jsonFile.ApiKey, null);
        }
        else
        {
            Window windowToShow = new login();
            windowToShow.ShowDialog();            
        }
    }
}

class ApiKeyDto(string apiKey)
{
    public string ApiKey { get; } = apiKey;
}