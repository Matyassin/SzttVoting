using Services;
using ViewModel.UserViewModels;

namespace View;

public partial class NewPollView : ContentPage
{
    private NewPollViewModel _vm;

    public NewPollView(UserServices _userServices, PollServices _pollServices)
    {
        InitializeComponent();
        _vm =  new NewPollViewModel(_userServices, _pollServices);
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
        if (await _vm.Publish()){
            await DisplayAlert("Poll saved", "To change poll details, please go to \"View votes\" tab!", "OK");
            await Navigation.PopAsync();
        }
    }
}
