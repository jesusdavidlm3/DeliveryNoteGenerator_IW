using System.Configuration;
using System.Data;
using System.Windows;
using WpfApp1.modals;

namespace WpfApp1;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Window windowToShow = new login();
        windowToShow.ShowDialog();
    }
}