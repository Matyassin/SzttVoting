using Model;
using Services;
using ViewModel.UserViewModels;

namespace View;

public partial class ModifyPollView : ContentPage
{
    private readonly ModifyPollViewModel _vm;

    public ModifyPollView(UserServices _userServices, PollServices _pollServices, PollData _pollData)
    {
        InitializeComponent();
        _vm =  new ModifyPollViewModel(_userServices, _pollServices, _pollData);
        BindingContext = _vm;
    }

    private async void CancelButton_OnClicked(object? sender, EventArgs e)
    {
        if (await DisplayAlert("Discard", "Are you sure you want to discard your pool?", "Yes", "No"))
        {
            await Navigation.PopAsync();
        }
    }

    private async void PublishButton_OnClicked(object? sender, EventArgs e)
    {
        if (await _vm.Publish())
        {
            await DisplayAlert("Poll saved", "To change poll details, please go to \"View votes\" tab, and there \"Modify Poll\"", "OK");
            await Navigation.PopAsync();
        }
    }
}
