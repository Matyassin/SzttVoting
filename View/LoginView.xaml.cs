using ViewModel;

namespace View;

public partial class LoginView : ContentPage
{
    private readonly LoginViewModel _vm;

    public LoginView()
    {
        InitializeComponent();
        _vm = new LoginViewModel();
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
        if (!LoginButton.IsEnabled)
            return;

        if (_vm.IsUserAdmin(_vm.EmailEntry, _vm.PasswordEntry))
        {
            await Navigation.PushAsync(new AdminView());
        }
        else if (_vm.IsEmailValid(_vm.EmailEntry) && _vm.IsPasswordValid(_vm.PasswordEntry))
        {
            await Navigation.PushAsync(new UserView(_vm.EmailEntry));
        }
    }

    private async void ToRegisterButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterView());
    }
}
