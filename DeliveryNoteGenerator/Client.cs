using System.Windows;
using DeliveryNoteGenerator.Models;
using System.Text.Json;
using DeliveryNoteGenerator.ViewModels;

namespace DeliveryNoteGenerator;
using System.Net.Http;
public static class Client
{
    private static string _apiKey = "";
    public static HttpClient client;

    public static async void setApiKey(string key, Window loginWindow)
    {   
        client = new HttpClient()
        {
            BaseAddress = new Uri("http://10.200.12.13/api/v1/b n"),
            DefaultRequestHeaders =
            {
                { "Authorization", $"Bearer {key}" },
                { "Accept", "application/json" }
            }
        };

        HttpResponseMessage res = await client.GetAsync("users/me");
        if (res.IsSuccessStatusCode)
        {
            Window mainWindow = new MainWindow();
            loginWindow.Close();
            mainWindow.Show();
            string resContent = await res.Content.ReadAsStringAsync();
            User user = JsonSerializer.Deserialize<User>(resContent)!;
            MainWindowViewModel.SetLoggedUser(user!);
        }
        Console.WriteLine(res.StatusCode);
    }
}