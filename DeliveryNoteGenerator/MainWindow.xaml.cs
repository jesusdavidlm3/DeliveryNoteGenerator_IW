using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DeliveryNoteGenerator.Models;
using System.Net.Http;
using System.Text.Json;
using DeliveryNoteGenerator.modals;
using DeliveryNoteGenerator.ViewModels;

namespace DeliveryNoteGenerator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenAddItemModal(object sender, RoutedEventArgs e)
    {
        var AddItemModal = new ItemSelector();
        AddItemModal.ShowDialog();
    }
}