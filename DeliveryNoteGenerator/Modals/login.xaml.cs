using System.Windows;

namespace DeliveryNoteGenerator.modals;

public partial class login : Window
{
    public login()
    {
        InitializeComponent();
    }
    
    private void SetToken(object sender, EventArgs e)
    {
        Client.setApiKey(TokenField.Text, this);
    }
}