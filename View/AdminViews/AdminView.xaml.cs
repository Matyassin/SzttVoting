using ViewModel;

namespace View;

public partial class AdminView : ContentPage
{
    public AdminView()
    {
        InitializeComponent();
    }

    private async void Signout_OnClickedAsync(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Sign Out?", "Are you sure you want to log out?", "Yes", "No");
        if (answer)
        {
            await Navigation.PopAsync();
        }
    }

    private void CreateNewVote_OnClicked(object sender, EventArgs e)
    {
        
    }

    private void SeeOngoingVotes_OnClicked(object sender, EventArgs e)
    {
        
    }

    private void SeeUsers_OnClicked(object sender, EventArgs e)
    {

    }
}
