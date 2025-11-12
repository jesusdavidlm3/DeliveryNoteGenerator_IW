using DeliveryNoteGenerator.Classes;
using DeliveryNoteGenerator.Models;

namespace DeliveryNoteGenerator.ViewModels;

public class NotListedItemViewModel : ViewModelBase
{
    private string _ItemName { get; set; }
    private int _Quantity { get; set; }

    public string ItemName
    {
        get => _ItemName;
        set
        {
            if (_ItemName != value)
            {
                _ItemName = value;
                OnPropertyChanged();
            }
        }
    }

    public int Quantity
    {
        get => _Quantity;
        set
        {
            if (_Quantity != value)
            {
                _Quantity = value;
                OnPropertyChanged();
            }
        }
    }
    
    public RelayCommand AddItem { get; }
    public Action? CloseWindow { get; set; }
    
    public NotListedItemViewModel()
    {
        Quantity = 1;
        AddItem = new RelayCommand(
            execute: _ => _AddItem(),
            canExecute: _ => ((_ItemName != null) && (Quantity >= 1))
        );
    }

    public void _AddItem()
    {
        var item = new Asset(null, _ItemName, "-", _Quantity);
        MainWindowViewModel.AddItem(item);
        CloseWindow.Invoke();
    }
}