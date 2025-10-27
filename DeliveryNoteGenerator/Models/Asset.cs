using System.Collections.ObjectModel;
using System.Text.Json;

namespace DeliveryNoteGenerator.Models;

public class Asset
{
    public int id { get; set; }
    public string name { get; set; }
    public string asset_tag { get; set; }

    public Asset(int id, string name, string asset_tag)
    {
        this.id = id;
        this.name = name;
        this.asset_tag = asset_tag;
    }

    public static async Task<ObservableCollection<Asset>> GetAllAssets()
    {
        ObservableCollection<Asset> resultList = new ObservableCollection<Asset>();
        string res = await Client.client.GetStringAsync("hardware");
        RootDto rawAssetsList = JsonSerializer.Deserialize<RootDto>(res);
        foreach (var asset in rawAssetsList.rows)
        {
            resultList.Add(asset);
        }

        return resultList;
    }

    private class RootDto
    {
        public List<Asset> rows { get; set; }
    }
    
}