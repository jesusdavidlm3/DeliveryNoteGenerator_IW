using System.Windows;
using DeliveryNoteGenerator.ViewModels;

namespace DeliveryNoteGenerator.Modals;

public partial class NotListedItem : Window
{
    public NotListedItem()
    {
        InitializeComponent();
        var vm = (NotListedItemViewModel)this.Resources["viewmodel"];
        vm.CloseWindow = () => this.Close();
    }
}