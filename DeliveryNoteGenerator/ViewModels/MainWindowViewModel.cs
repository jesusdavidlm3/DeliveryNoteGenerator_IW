using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Windows.Documents.DocumentStructures;
using DeliveryNoteGenerator.Classes;
using DeliveryNoteGenerator.Models;
using DeliveryNoteGenerator.PDF;
using QuestPDF.Fluent;


namespace DeliveryNoteGenerator.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _SearchText;
    private ObservableCollection<User> AllUsersList { get; set; }
    private ObservableCollection<User> _FilteredUsersList { get; set; }
    private User _SelectedUser { get; set; }
    private static ObservableCollection<Asset> _SelectedAssets { get; set; } = new ObservableCollection<Asset>();
    private static User LoggedUser { get; set; }
    private DateTime _IssueDate { get; set; }

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

    public DateTime IssueDate
    {
        get => _IssueDate;
        set
        {
            if (_IssueDate != value)
            {
                _IssueDate = value;
                OnPropertyChanged();
            }
        }
    }
    
    public RelayCommand DeleteItem { get; }
    public RelayCommand IssueNote { get; }

    public MainWindowViewModel()
    {
        getValues();
        IssueDate = DateTime.Now;
        DeleteItem = new RelayCommand(
            execute: id => _DeleteItem((int)id));
        IssueNote = new RelayCommand(
            execute: _ => _IssueNote(),
            canExecute: _ => ((_SelectedAssets.Count >= 1) && (_SelectedUser != null)));
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

    public static void AddItem(Asset asset)
    {
        _SelectedAssets.Add(asset);
    }

    public static void SetLoggedUser(User user)
    {
        LoggedUser = user;
    }

    private void _DeleteItem(int id)
    {
        var itemToDelete = _SelectedAssets.FirstOrDefault(a => a.id == id);
        SelectedAssets.Remove(itemToDelete);
    }

    private void _IssueNote()
    {
        var document = new IssueNote(SelectedUser, SelectedAssets, LoggedUser, IssueDate);
        document.GeneratePdfAndShow();
    }
}