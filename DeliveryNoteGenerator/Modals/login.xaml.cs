using System.IO;
using System.Text.Json;
using System.Windows;

namespace DeliveryNoteGenerator.modals;

public partial class login : Window
{
    public login()
    {
        InitializeComponent();
    }
    
    private async void SetToken(object sender, EventArgs e)
    {
        var result = await Client.setApiKey(TokenField.Text, this);
        if (result)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folder = Path.Combine(appDataPath, "DeliveryNoteGenerator");
            Directory.CreateDirectory(folder);
            var keyFile = new { ApiKey = TokenField.Text };
            string json = JsonSerializer.Serialize(keyFile, new JsonSerializerOptions {WriteIndented = true});
            string keyFilePath = Path.Combine(folder, "key.json");
            File.WriteAllText(keyFilePath, json);
        }
    }
}