using ViewModel;
using Services;
using View.UserViews;

namespace View;

public partial class UserView : ContentPage
{
    private readonly UserViewModel _vm;

    public UserView(UserServices userServices)
    {
        InitializeComponent();
        _vm = new UserViewModel(userServices);
        BindingContext = _vm;
    }

    private async void Signout_OnClickedAsync(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Sign Out?", "Are you sure you want to log out?", "Yes", "No");
        if (answer)
        {
            _vm.UserServices.ClearLoggedInUser();
            await Navigation.PopAsync();
        }
    }

    private void CreateNewVote_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new NewPollView(_vm.UserServices, _vm.PollServices));
    }

    private void MyProfile_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MyProfile(_vm.UserServices));
    }

    private void SeeOngoingVotes_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new ListOngoingVotesView(_vm.UserServices));
    }
}
