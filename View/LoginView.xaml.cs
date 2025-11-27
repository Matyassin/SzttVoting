using ViewModel;

namespace SzttVoting.View;

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

    private void LoginButton_OnClicked(object? sender, EventArgs e)
    {
        if (_vm.IsUserAdmin(_vm.EmailEntry, _vm.PasswordEntry))
        {
            Application.Current.MainPage = new AdminView();
        }
        else if (_vm.IsEmailValid(_vm.EmailEntry) && _vm.IsPasswordValid(_vm.PasswordEntry))
        {
            Application.Current.MainPage = new UserView(_vm.EmailEntry);
        }
    }

    private void ToRegisterButton_OnClicked(object? sender, EventArgs e)
    {
        Application.Current.MainPage = new RegisterView();
    }
}
