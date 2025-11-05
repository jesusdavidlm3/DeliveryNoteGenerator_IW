using System.Windows;
using DeliveryNoteGenerator.ViewModels;

namespace DeliveryNoteGenerator.modals;

public partial class ItemSelector : Window
{
    public ItemSelector()
    {
        InitializeComponent();
        var viewModel = new ItemSelectorViewModel();
        DataContext = viewModel;
    }
}