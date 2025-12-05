using System.Text.RegularExpressions;
using Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;

public partial class RegisterViewModel : BaseViewModel, ICredentialsValidator
{
    [ObservableProperty] private bool _isUserWarning;
    [ObservableProperty] private string _userWarningText;
    [ObservableProperty] private string _userWarningColor;   // string is only TEMP
    
    [ObservableProperty] private bool _isEmailWarning;
    [ObservableProperty] private string _emailWarningText;
    [ObservableProperty] private string _emailWarningColor;   // string is only TEMP
    
    [ObservableProperty] private bool _isPasswordWarning;
    [ObservableProperty] private string _passwordWarningText;
    [ObservableProperty] private string _passwordWarningColor; // string is only TEMP

    [ObservableProperty] private string _usernameEntry = "";
    [ObservableProperty] private string _emailEntry = "";
    [ObservableProperty] private string _passwordEntry = "";
    
    public UserServices UserServices { get; private set; }

    public bool IsRegisterButtonEnabled =>
        IsEmailValid(EmailEntry) && IsPasswordValid(PasswordEntry);

    public RegisterViewModel(UserServices userServices)
    {
        UserServices = userServices;
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
            EmailWarningText = "Valid email address!";
        }
    }
    
    [RelayCommand]
    private void CheckPasswordEntry()
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
            PasswordWarningText = "Password is valid!";
        }
    }

    [RelayCommand]
    private void SaveUser()
    {
        if (UserServices.ContainsEmail(EmailEntry))
            return;

        UserServices.AddUser(UsernameEntry,EmailEntry,PasswordEntry);
    }

    public bool IsEmailValid(string email)
    {
        if (UserServices.ContainsEmail(email) || string.IsNullOrWhiteSpace(email))
            return false;

        return Regex.IsMatch(email, UserServices.EmailPattern);
    }

    public bool IsPasswordValid(string password)
    {
        if (password.Length < 5 || string.IsNullOrWhiteSpace(password))
            return false;

        return Regex.IsMatch(password, UserServices.PasswordPattern);
    }

    public void SetLoggedInUser()
    {
        UserServices.SetLoggedInUser(EmailEntry);
    }

    partial void OnEmailEntryChanged(string value)
    {
        OnPropertyChanged(nameof(IsRegisterButtonEnabled));
    }

    partial void OnPasswordEntryChanged(string value)
    {
        OnPropertyChanged(nameof(IsRegisterButtonEnabled));
    }
}
