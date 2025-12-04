using Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;

public partial class LoginViewModel : BaseViewModel, ICredentialsValidator
{
    [ObservableProperty] private bool _isEmailWarning;
    [ObservableProperty] private string _emailWarningText;
    [ObservableProperty] private string _emailWarningColor;     // string is only TEMP

    [ObservableProperty] private bool _isPasswordWarning;
    [ObservableProperty] private string _passwordWarningText;
    [ObservableProperty] private string _passwordWarningColor; // string is only TEMP

    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsLoginButtonEnabled))]
    private string _emailEntry = "";

    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsLoginButtonEnabled))]
    private string _passwordEntry = "";
    
    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsLoginButtonEnabled)), NotifyPropertyChangedFor(nameof(LoginButtonText))]
    private bool _isBusy = false;

    public UserServices UserServices { get; private set; }

    public string LoginButtonText => IsBusy ? "Authenticating..." : "Log in!";

    public bool IsLoginButtonEnabled =>
        (IsEmailValid(EmailEntry) || IsUserAdmin(EmailEntry, PasswordEntry)) && !IsBusy;

    public LoginViewModel(UserServices userService)
    {
        UserServices = userService;
    }

    public void SetLoggedinUser()
    {
        UserServices.SetLoggedInUser(EmailEntry);
    }
    
    [RelayCommand]
    private void CheckEmailEntry()
    {
        IsEmailWarning = true;

        if (!IsEmailValid(EmailEntry))
        {
            EmailWarningColor = "Red";
            EmailWarningText = "Invalid email address!";
        }
        else
        {
            EmailWarningColor = "Green";
            EmailWarningText = "";
        }
    }

    [RelayCommand]
    private void CheckPasswordEntry(string password)
    {
        IsPasswordWarning = true;

        if (!IsPasswordValid(PasswordEntry))
        {
            PasswordWarningColor = "Red";
            
            PasswordWarningText = "Password must be at least 5 characters long, " +
                                  "must not contain any symbols, " +
                                  "must have at least 1 number " +
                                  "and at least 1 upper case character!";
            
        }
        else
        {
            PasswordWarningColor = "Green";
            PasswordWarningText = "";
        }
    }

    public bool IsEmailValid(string email)
    {
        return UserServices.ContainsEmail(email);
    }

    public bool TryAuthUser()
    {
        if (UserServices.TryValidateUser(EmailEntry, PasswordEntry))
        {
            SetLoggedinUser();
            return true;
        }

        return false;
    }

    public bool IsPasswordValid(string password)
    {
        return !(password.Length < 5 || string.IsNullOrWhiteSpace(password));
    }

    public bool IsUserAdmin(string email, string password)
    {
        return email == "admin" && password == "admin";
    }

    partial void OnEmailEntryChanged(string value)
    {
        OnPropertyChanged(nameof(IsLoginButtonEnabled));
    }

    partial void OnPasswordEntryChanged(string value)
    {
        OnPropertyChanged(nameof(IsLoginButtonEnabled));
    }
}
