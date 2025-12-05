using Services;
using ViewModel;
using View.AdminViews;

namespace View;

public partial class AdminView : ContentPage
{
    private readonly AdminViewModel _vm;

    public AdminView(UserServices userServices)
    {
        InitializeComponent();
        _vm = new AdminViewModel(userServices);
        BindingContext = _vm;
    }

    private async void Signout_OnClickedAsync(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Sign Out?", "Are you sure you want to log out?", "Yes", "No");
        if (answer)
        {
            await Navigation.PopToRootAsync();
        }
    }

    private void CreateNewVote_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NewPollView(_vm.UserServices, _vm.PollServices));
    }

    private void ListOngoingVotes_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ListPollsView(_vm.PollServices));
    }

    private void ListUsers_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ListUsersView(_vm.UserServices));
    }
}
