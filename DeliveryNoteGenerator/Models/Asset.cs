using System.Collections.ObjectModel;
using System.Text.Json;

namespace DeliveryNoteGenerator.Models;

public class Asset
{
    public int? id { get; set; }
    public string name { get; set; }
    public string asset_tag { get; set; }
    public int? Quantity { get; set; }

    public Asset(int? id, string name, string asset_tag, int? quantity = null)
    {
        this.id = id;
        this.name = name;
        this.asset_tag = asset_tag;
        this.Quantity = quantity;
    }

    public static async Task<ObservableCollection<Asset>> GetAllAssets()
    {
        ObservableCollection<Asset> resultList = new ObservableCollection<Asset>();
        string res = await Client.client.GetStringAsync("hardware");
        RootDto rawAssetsList = JsonSerializer.Deserialize<RootDto>(res);
        resultList = new ObservableCollection<Asset>(rawAssetsList?.rows);

        return resultList;
    }

    private class RootDto
    {
        public List<Asset> rows { get; set; }
    }
    
}