using Model;
using System.Net.Mail;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel;

public partial class RegisterViewModel: BaseViewModel, ICredentialsValidator
{
    [ObservableProperty] private bool _isEmailWarning;
    [ObservableProperty] private string _emailWarningText;
    [ObservableProperty] private Color _emailWarningColor;
    
    [ObservableProperty] private bool _isPasswordWarning;
    [ObservableProperty] private string _passwordWarningText;
    [ObservableProperty] private Color _passwordWarningColor;
    
    [ObservableProperty] private string _emailEntry = "";
    [ObservableProperty] private string _passwordEntry = "";

    public bool RegisterButtonEnabled => IsEmailValid(EmailEntry) && IsPasswordValid(PasswordEntry);

    [RelayCommand]
    private void CheckEmailEntry()
    {
        IsEmailWarning = true;

        if (!IsEmailValid(EmailEntry))
        {   
            EmailWarningColor = Colors.Red;
            EmailWarningText = "Invalid email address!";
        }
        else
        {
            EmailWarningColor = Colors.Green;
            EmailWarningText = "Valid email address!";
        }
    }
    
    [RelayCommand]
    private void CheckPasswordEntry()
    {
        IsPasswordWarning = true;

        if (!IsPasswordValid(PasswordEntry))
        {
            PasswordWarningColor = Colors.Red;
            PasswordWarningText = "Password must be at least 5 characters long!";
        } 
        else 
        {
            PasswordWarningColor = Colors.Green;
            PasswordWarningText = "Password is valid!";
        }
    }

    [RelayCommand]
    private void SaveUser()
    {
        // The last 2 guards are TEMP, later we don't let the user click the button if those 2 aren't met
        if (UsersRepo.ContainsEmail(EmailEntry) || IsEmailValid(EmailEntry) || !IsPasswordValid(PasswordEntry))
            return;

        UsersRepo.Save(new UserProfile(EmailEntry, PasswordEntry));
    }

    public bool IsEmailValid(string email)
    {
        if (UsersRepo.ContainsEmail(EmailEntry) || EmailEntry is null)
            return false;

        try
        {
            var addr = new MailAddress(EmailEntry);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public bool IsPasswordValid(string password)
    {
        if (PasswordEntry.Length > 5 || PasswordEntry is null)
            return false;

        return true;
    }
}