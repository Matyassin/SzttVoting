using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _emailEntry = "example@example.com";
    
    [ObservableProperty]
    private string _passwordEntry = "";
    
    [ObservableProperty]
    private Boolean _isEmailError = false;

    [ObservableProperty]
    private string _emailErrorText = "Email is invalid";
    
    [ObservableProperty]
    private Boolean _isPasswordError = false;

    [ObservableProperty]
    private string _passwordErrorText = "Your password is too short";
}