using Model;
using Services;
using ViewModel;

namespace View;

public partial class RegisterView : ContentPage
{
    private readonly RegisterViewModel _vm;
    private readonly UserServices _userServices;

    public RegisterView(UserServices userServices)
    {
        InitializeComponent();
        _userServices = userServices;
        _vm = new RegisterViewModel(_userServices);
        BindingContext = _vm;
    }
    
    private void Email_OnUnfocused(object? sender, FocusEventArgs e)
    {
        _vm.CheckEmailEntryCommand.Execute(null);
    }
        
    private void Password_OnUnfocused(object? sender, FocusEventArgs e)
    {
        _vm.CheckPasswordEntryCommand.Execute(null);
    }

    private void Username_OnUnfocused(object? sender, FocusEventArgs e)
    {
        //MAYBE TODO - Implement Username validation
    }

    private async void RegisterButton_OnClicked(object sender, EventArgs e)
    {
        if (RegisterButton.IsEnabled)
        {
            _vm.SaveUserCommand.Execute(null);
            _vm.SetLoggedInUser();
            await Navigation.PushAsync(new UserView(_userServices));
        }
    }

    private async void ToLoginButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
