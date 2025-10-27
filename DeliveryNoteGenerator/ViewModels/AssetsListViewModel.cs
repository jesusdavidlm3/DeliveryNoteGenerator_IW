using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DeliveryNoteGenerator.Models;

namespace DeliveryNoteGenerator.ViewModels;

public class AssetsListViewModel : INotifyPropertyChanged
{
    private string _SearchText { get; set; }
    private ObservableCollection<Asset> AllAssetsList { get; set; }
    public ObservableCollection<Asset> _FilteredAssetsList { get; set; }

    public string SearchText
    {
        get => _SearchText;
        set
        {
            _SearchText = value;
            FilterAssets();
            OnPropertyChanged(nameof(FilteredAssetsList));
        }
    }

    public ObservableCollection<Asset> FilteredAssetsList
    {
        get => _FilteredAssetsList;
        set
        {
            _FilteredAssetsList = value;
            OnPropertyChanged(nameof(FilteredAssetsList));
        }
    }

    public AssetsListViewModel()
    {
        GetValues();
    }

    private async Task GetValues()
    {
        var response = await Asset.GetAllAssets();
        AllAssetsList = new ObservableCollection<Asset>(response);
        FilteredAssetsList = new ObservableCollection<Asset>(response);
    }

    private void FilterAssets()
    {
        var filteredList = AllAssetsList.Where(a => $"{a.name}".ToLower().Contains(SearchText) || $"{a.asset_tag}".ToLower().Contains(SearchText)).ToList();
        _FilteredAssetsList.Clear();
        foreach (var asset in filteredList)
        {
            _FilteredAssetsList.Add(asset);            
        }
        OnPropertyChanged(nameof(FilteredAssetsList));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}