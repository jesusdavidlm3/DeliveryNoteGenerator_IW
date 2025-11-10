using System.Windows;
using DeliveryNoteGenerator.ViewModels;

namespace DeliveryNoteGenerator.modals;

public partial class ItemSelector : Window
{
    public ItemSelector()
    {
        InitializeComponent();
        var vm = (ItemSelectorViewModel)this.Resources["ViewModel"];
        vm.CloseWindow = () => this.Close();
    }
}