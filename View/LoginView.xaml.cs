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

    private async void LoginButton_OnClicked(object? sender, EventArgs e)
    {
        if (_vm.IsBusy)
            return;

        _vm.IsBusy = true;

        if (_vm.IsUserAdmin(_vm.EmailEntry, _vm.PasswordEntry))
        {
            await Navigation.PushAsync(new AdminView());
        }
        else if (_vm.TryAuthUser())
        {
            _vm.SetLoggedinUser();
            await Navigation.PushAsync(new UserView(_vm.UserServices));
        }
        else
        {
            await DisplayAlert("Incorrect credentials", "Try again later!", "OK");
        }

        _vm.IsBusy = false;
    }

    private async void ToRegisterButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterView(_vm.UserServices));
    }
}
