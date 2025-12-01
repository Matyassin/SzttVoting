using System.Data;
using System.Security.AccessControl;
using ViewModel;
using Services;
using View.UserViews;

namespace View;


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
        Navigation.PushAsync(new NewPollView(_userServices));
    }

    private void MyProfile_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MyProfile(_userServices));
    }
    private void SeeOngoingVotes_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new SeeOngoingVotesView(_userServices));
    }
}
