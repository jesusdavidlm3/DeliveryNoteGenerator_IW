using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using DeliveryNoteGenerator.Models;

namespace DeliveryNoteGenerator.ViewModels;

public class UsersListViewModel : INotifyPropertyChanged
{

    private string _SearchText;
    private ObservableCollection<User> AllUsersList { get; set; }
    private ObservableCollection<User> _FilteredUsersList { get; set; }

    public string SearchText
    {
        get => _SearchText;
        set
        {
            _SearchText = value;
            FilterUsers();
            OnPropertyChanged(nameof(SearchText));
        }
    }

    public ObservableCollection<User> FilteredUsersList
    {
        get => _FilteredUsersList;
        set
        {
            _FilteredUsersList = value;
            OnPropertyChanged(nameof(FilteredUsersList));
        }
    }

    public UsersListViewModel(){ }

    public async Task getValues()
    {
        var response = await User.GetAllUsersList();
        this.AllUsersList = new ObservableCollection<User>(response);
        this.FilteredUsersList = new ObservableCollection<User>(response);
    }

    private void FilterUsers()
    {
        List<User> FilteredList = AllUsersList.Where(u => $"{u.name}".ToLower().Contains(SearchText)).ToList();
        _FilteredUsersList.Clear();
        foreach (var user in FilteredList)
        {
            _FilteredUsersList.Add(user);
        }
        OnPropertyChanged(nameof(FilteredUsersList));
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