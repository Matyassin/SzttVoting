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

    public bool IsRegisterButtonEnabled =>
        IsEmailValid(EmailEntry) && IsPasswordValid(PasswordEntry);

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
            PasswordWarningText = "Password must be at least 5 characters long," +
                                  "must not contain any symbols," +
                                  "must have at least 1 number" +
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
        if (UsersRepo.ContainsEmail(EmailEntry))
            return;

        UsersRepo.Save(new UserData(EmailEntry, PasswordEntry));
    }

    public bool IsEmailValid(string email)
    {
        if (UsersRepo.ContainsEmail(EmailEntry) || string.IsNullOrWhiteSpace(email))
            return false;

        return Regex.IsMatch(email, ValidationPatterns.EmailPattern);
    }

    public bool IsPasswordValid(string password)
    {
        if (PasswordEntry.Length < 5 || string.IsNullOrWhiteSpace(password))
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