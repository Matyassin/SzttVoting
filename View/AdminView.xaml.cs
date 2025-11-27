using ViewModel;

namespace SzttVoting.View;

public partial class AdminView : ContentPage
{
    public AdminView()
    {
        InitializeComponent();
    }

    private void Signout_OnClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginView();
    }
}
