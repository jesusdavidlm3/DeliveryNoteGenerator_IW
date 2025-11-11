using System.Configuration;
using System.Data;
using System.Windows;
using DeliveryNoteGenerator.modals;
using QuestPDF.Infrastructure;

namespace DeliveryNoteGenerator;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        QuestPDF.Settings.License = LicenseType.Community;
        Window windowToShow = new login();
        windowToShow.ShowDialog();
    }
}