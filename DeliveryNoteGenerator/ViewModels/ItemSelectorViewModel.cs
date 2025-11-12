using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using DeliveryNoteGenerator.Classes;
using DeliveryNoteGenerator.Models;

namespace DeliveryNoteGenerator.ViewModels;

public class ItemSelectorViewModel : ViewModelBase
{
    private string _SearchText { get; set; }
    private ObservableCollection<Asset> AllAssetsList { get; set; }
    private Asset _SelectedAsset { get; set; }
    public ObservableCollection<Asset> _FilteredAssetsList { get; set; }
    public int Quantity { get; set; }

    public string SearchText
    {
        get => _SearchText;
        set
        {
            if (_SearchText != value)
            {
                _SearchText = value;
                FilterAssets();
                OnPropertyChanged();                
            }
        }
    }

    public ObservableCollection<Asset> FilteredAssetsList
    {
        get => _FilteredAssetsList;
        set
        {
            if (_FilteredAssetsList != value)
            {
                _FilteredAssetsList = value;
                OnPropertyChanged();
            }
        }
    }

    public Asset SelectedAset
    {
        get => _SelectedAsset;
        set
        {
            if (_SelectedAsset != value)
            {
                _SelectedAsset = value;
                _SearchText = value.name;
                OnPropertyChanged();
            }
        }
    }
    
    public RelayCommand AddItemToList { get; }
    public Action? CloseWindow { get; set; }
    
    public ItemSelectorViewModel()
    {
        Quantity = 1;
        GetValues();
        AddItemToList = new RelayCommand(
            execute: _ => _AddItemToList(),
            canExecute: _ => ((_SelectedAsset != null) && (Quantity >= 1))
        );
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
        FilteredAssetsList = new ObservableCollection<Asset>(filteredList);
    }

    private void _AddItemToList()
    {
        SelectedAset.Quantity = Quantity;
        MainWindowViewModel.AddItem(SelectedAset);
        CloseWindow?.Invoke();
    }
    
}