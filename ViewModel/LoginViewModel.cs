using System.Net.Mail;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.HotReload;

namespace ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty] private string _emailEntry = "";
    [ObservableProperty] private string _passwordEntry = "";
    
    [ObservableProperty] private Boolean _isEmailWarning;
    [ObservableProperty] private string _emailWarningText;
    [ObservableProperty] private Color _emailWarningColor;
    
    [ObservableProperty] private Boolean _isPasswordWarning;
    [ObservableProperty] private string _passwordWarningText;
    [ObservableProperty] private Color _passwordWarningColor;
    
    private bool _isEmailValid;
    
    private bool _isPasswordValid => PasswordEntry.Length > 5;
    
    [RelayCommand]
    private void CheckEmailEntry()
    {
        if (!IsEmailValid())
        {
            IsEmailWarning = true;
            EmailWarningText = "Invalid email address!";
            EmailWarningColor = Colors.Red;
            _isEmailValid = false;
        }
        else
        {
            IsEmailWarning = true;
            EmailWarningText = "Valid email address!";
            EmailWarningColor = Colors.Green;
            _isEmailValid = true;
        }
    }

    private bool IsEmailValid()
    {
        if (string.IsNullOrWhiteSpace(EmailEntry))
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

    [RelayCommand]
    private void CheckPasswordEntry()
    {
        IsPasswordWarning = true;
        if (!_isPasswordValid)
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
        
}