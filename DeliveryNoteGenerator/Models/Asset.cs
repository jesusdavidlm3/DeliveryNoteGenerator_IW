namespace DeliveryNoteGenerator.Models;

public class Asset
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AssetTag { get; set; }

    public Asset(int id, string name, string assetTag)
    {
        this.Id = id;
        this.Name = name;
        this.AssetTag = assetTag;
    }
}