using Foundation;
using Services;
using ViewModel;
namespace View;

public partial class LoginView : ContentPage
{
    private readonly LoginViewModel _vm;
    private readonly UserServices _userService;

    public LoginView(UserServices userService)
    {
        InitializeComponent();
        _vm = new LoginViewModel(userService);
        _userService = userService;
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
            await Navigation.PushAsync(new RegisterView(_userService));
        }
        else if (_vm.IsEmailValid(_vm.EmailEntry) && _vm.IsPasswordValid(_vm.PasswordEntry))
        {
            _vm.SetLoggedinUser();
            await Navigation.PushAsync(new UserView(_userService));
        }
    }

    private async void ToRegisterButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterView(_userService));
    }
}
