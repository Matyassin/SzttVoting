using System.Net.Mail;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.HotReload;

namespace ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty] private string _emailEntry;
    
    [ObservableProperty] private string _passwordEntry;
    
    [ObservableProperty] private Boolean _isEmailWarning;

    [ObservableProperty] private string _emailWarningText;
    
    [ObservableProperty] private Color _emailWarningColor;
    
    [ObservableProperty] private Boolean _isPasswordWarning;

    [ObservableProperty] private string _passwordWarningText;

    [ObservableProperty] private Color _passwordWarningColor;

    public LoginViewModel()
    {
        //Default values
        _emailEntry = "example@example.com";
        _passwordEntry = "";
        _isEmailWarning = false;
        _isPasswordWarning = false;
    }

    [RelayCommand]
    private void CheckEmailEntry()
    {
        if (!IsEmailValid(EmailEntry))
        {
            IsEmailWarning = true;
            EmailWarningText = "Invalid email address!";
            EmailWarningColor = Colors.Red;
        }
        else
        {
            IsEmailWarning = true;
            EmailWarningText = "Valid email address!";
            EmailWarningColor = Colors.Green;
        }
    }

    private bool IsEmailValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var addr = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    [RelayCommand]
    private void CheckPasswordEntry()
    {
        if (!IsPasswordValid())
        {
            IsPasswordWarning = true;
            PasswordWarningColor = Colors.Red;
        }
        else
        {
            IsPasswordWarning = true;
            PasswordWarningColor = Colors.Green;
        }
    }

    private Boolean IsPasswordValid()
    {
        if (string.IsNullOrWhiteSpace(PasswordEntry) || PasswordEntry.Length < 5)
        {
            PasswordWarningText = "Password must be at least 5 characters long!";
            return false;
        }

        PasswordWarningText = "Password is valid!";
        return true;
    }
}