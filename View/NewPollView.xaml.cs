using Services;
using ViewModel.UserViewModels;

namespace View.UserViews;

public partial class NewPollView : ContentPage
{
    private UserServices _userServices;
    private CreatePollViewModel _vm;

    public NewPollView(UserServices _userServices)
    {
        InitializeComponent();
    }

    private void CancelButton_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}