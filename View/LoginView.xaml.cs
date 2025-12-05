using Services;
using ViewModel;

namespace View;

public partial class LoginView : ContentPage
{
    private readonly LoginViewModel _vm;

    public LoginView(UserServices userService)
    {
        InitializeComponent();
        _vm = new LoginViewModel(userService);
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

    private async void LoginButton_OnClickedAsync(object? sender, EventArgs e)
    {
        if (_vm.IsBusy)
            return;

        _vm.IsBusy = true;
        try
        {
            if (await _vm.TryAuthUser())
            {
                if (_vm.IsLoggedInUserAdmin())
                {
                    await Navigation.PushAsync(new AdminView(_vm.UserService));
                }else {
                    await Navigation.PushAsync(new UserView(_vm.UserService));
                }
            }
            else
            {
                await DisplayAlert("Incorrect credentials", "Try again later!", "OK");
            }
        }
        finally
        {
            _vm.IsBusy = false;
        }

        
    }

    private async void ToRegisterButton_OnClickedAsync(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterView(_vm.UserService));
    }
}
