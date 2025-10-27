using System.Collections.ObjectModel;
using System.Text.Json;
namespace DeliveryNoteGenerator.Models;

public class User
{
    public int id { get; set; }
    public string name { get; set; }

    public User(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public static async Task<ObservableCollection<User>> GetAllUsersList()
    {
        ObservableCollection<User> resultList = new ObservableCollection<User>();
        string res = await Client.client.GetStringAsync("users");
        RootDto rawUserList = JsonSerializer.Deserialize<RootDto>(res);
        foreach (var user in rawUserList.rows)
        {
            resultList.Add(user);
        }

        return resultList;
    }
}

class RootDto
{
    public List <User> rows { get; set; }
}