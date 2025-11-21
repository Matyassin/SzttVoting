using System.Net.Mail;
using Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel;

public partial class RegisterViewModel: BaseViewModel
{
    [ObservableProperty] private Boolean _isEmailWarning;
    [ObservableProperty] private string _emailWarningText;
    [ObservableProperty] private Color _emailWarningColor;
    
    [ObservableProperty] private Boolean _isPasswordWarning;
    [ObservableProperty] private string _passwordWarningText;
    [ObservableProperty] private Color _passwordWarningColor;
    
    [ObservableProperty] private string _emailEntry = "";
    [ObservableProperty] private string _passwordEntry = "";

    private bool _isEmailValid = false;

    [RelayCommand]
    private void SaveUser()
    {
        UsersRepo.Save(new UserProfile(EmailEntry, PasswordEntry));
    }
    
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
    
    [RelayCommand]
    private void CheckPasswordEntry()
    {
        IsPasswordWarning = true;
        if (!IsPasswordValid())
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

    private bool IsPasswordValid()
    {
        return PasswordEntry.Length > 5;
    }
}