using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Windows.Documents.DocumentStructures;
using DeliveryNoteGenerator.Classes;
using DeliveryNoteGenerator.Models;

namespace DeliveryNoteGenerator.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _SearchText;
    private ObservableCollection<User> AllUsersList { get; set; }
    private ObservableCollection<User> _FilteredUsersList { get; set; }
    private User _SelectedUser { get; set; }
    public static ObservableCollection<Asset> _SelectedAssets { get; set; } = new ObservableCollection<Asset>();

    public string SearchText
    {
        get => _SearchText;
        set
        {
            if (_SearchText != value)
            {
                _SearchText = value;
                FilterUsers();
                OnPropertyChanged();                
            }
        }
    }

    public ObservableCollection<User> FilteredUsersList
    {
        get => _FilteredUsersList;
        set
        {
            if (_FilteredUsersList != value)
            {
                _FilteredUsersList = value;
                OnPropertyChanged();                
            }
        }
    }

    public User SelectedUser
    {
        get => _SelectedUser;
        set
        {
            if (_SelectedUser != value)
            {
                _SearchText = value.name;
                _SelectedUser = value;
                OnPropertyChanged();   
            }
        }
    }

    public ObservableCollection<Asset> SelectedAssets
    {
        get => _SelectedAssets;
        set
        {
            if (_SelectedAssets != value)
            {
                _SelectedAssets = value;
                OnPropertyChanged();   
            }
        }
    }
    
    public RelayCommand DeleteItem { get; }

    public MainWindowViewModel()
    {
        getValues();
        DeleteItem = new RelayCommand(
            execute: id => _DeleteItem((int)id));
    }

    public async Task getValues()
    {
        var response = await User.GetAllUsersList();
        AllUsersList = new ObservableCollection<User>(response);
        FilteredUsersList = new ObservableCollection<User>(response);
    }

    private void FilterUsers()
    {
        List<User> FilteredList = AllUsersList.Where(u => $"{u.name}".ToLower().Contains(SearchText)).ToList();
        FilteredUsersList = new ObservableCollection<User>(FilteredList);
    }

    public void _DeleteItem(int id)
    {
        var itemToDelete = _SelectedAssets.FirstOrDefault(a => a.id == id);
        _SelectedAssets.Remove(itemToDelete);
    }
}