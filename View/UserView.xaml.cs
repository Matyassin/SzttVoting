using ViewModel;

namespace View;
using Services;

public partial class UserView : ContentPage
{
    private readonly UserViewModel _vm;
    private readonly UserServices _userServices;

    public UserView(UserServices userServices)
    {
        InitializeComponent();
        _userServices = userServices;
        _vm = new UserViewModel(_userServices);
        BindingContext = _vm;
    }

    private async void Signout_OnClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Sign Out?", "Are you sure you want to log out?", "Yes", "No");
        if (answer)
        {
            _userServices.ClearLoggedInUser();
            await Navigation.PopAsync();
        }
    }

    private void CreateNewVote_OnClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void SeeOngoingVotes_OnClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
