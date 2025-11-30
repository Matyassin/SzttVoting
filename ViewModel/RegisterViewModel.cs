using Services;
using Model;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;

public partial class RegisterViewModel : BaseViewModel, ICredentialsValidator
{
    [ObservableProperty] private bool _isEmailWarning;
    [ObservableProperty] private string _emailWarningText;
    [ObservableProperty] private string _emailWarningColor;   // string is only TEMP
    
    [ObservableProperty] private bool _isPasswordWarning;
    [ObservableProperty] private string _passwordWarningText;
    [ObservableProperty] private string _passwordWarningColor; // string is only TEMP

    [ObservableProperty] private string _emailEntry = "";
    [ObservableProperty] private string _passwordEntry = "";
    private UserServices _userServices;

    public bool IsRegisterButtonEnabled =>
        IsEmailValid(EmailEntry) && IsPasswordValid(PasswordEntry);

    public RegisterViewModel(UserServices userServices)
    {
        _userServices = userServices;
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
        if (_userServices.ContainsEmail(EmailEntry))
            return;

        _userServices.Save(new UserData(EmailEntry, PasswordEntry));
    }

    public bool IsEmailValid(string email)
    {
        if (_userServices.ContainsEmail(email) || string.IsNullOrWhiteSpace(email))
            return false;

        return Regex.IsMatch(email, ValidationPatterns.EmailPattern);
    }

    public bool IsPasswordValid(string password)
    {
        if (password.Length < 5 || string.IsNullOrWhiteSpace(password))
            return false;

        return Regex.IsMatch(password, ValidationPatterns.PasswordPattern);
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