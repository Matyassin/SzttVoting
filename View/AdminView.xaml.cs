using ViewModel;

namespace View;

public partial class AdminView : ContentPage
{
    public AdminView()
    {
        InitializeComponent();
    }

    private async void Signout_OnClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
