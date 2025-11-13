using System.Windows;

namespace DeliveryNoteGenerator.Modals;

public partial class ErrorModal : Window
{
    public ErrorModal()
    {
        InitializeComponent();
    }

    private void CloseWindow(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}