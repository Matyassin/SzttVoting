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
        if (_vm.IsBusy)
            return;

        var isAdmin = false;
        var isAuthorized = false;
        
        try
        {
            _vm.IsBusy = true;

            await Task.Run(() =>
            {
                isAdmin = _vm.IsUserAdmin(_vm.EmailEntry, _vm.PasswordEntry);
                if (!isAdmin)
                {
                    isAuthorized = _vm.AuthUser();
                }
            });
            
            if (isAdmin)
            {
                await Navigation.PushAsync(new RegisterView(_userService));
                return;
            }
    
            if (isAuthorized)
            {
                _vm.SetLoggedinUser();
                await Navigation.PushAsync(new UserView(_userService));
                return;
            }
            
            await DisplayAlert("Incorrect credentials", "Try again later!", "OK");
            
        }
        finally
        {
            _vm.IsBusy = false;
        }
    }

    private async void ToRegisterButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterView(_userService));
    }
}
