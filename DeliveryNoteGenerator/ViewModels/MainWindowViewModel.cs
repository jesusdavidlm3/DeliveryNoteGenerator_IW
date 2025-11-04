using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Windows.Documents.DocumentStructures;
using DeliveryNoteGenerator.Models;

namespace DeliveryNoteGenerator.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _SearchText;
    private ObservableCollection<User> AllUsersList { get; set; }
    private ObservableCollection<User> _FilteredUsersList { get; set; }
    private User _SelectedUser { get; set; }
    private ObservableCollection<Asset> _SelectedAssets { get; set; }

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

    public MainWindowViewModel()
    {
        getValues();
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
}