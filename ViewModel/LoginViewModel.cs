using Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel;

public partial class LoginViewModel : BaseViewModel, ICredentialsValidator
{
    [ObservableProperty] private bool _isEmailWarning;
    [ObservableProperty] private string _emailWarningText;
    [ObservableProperty] private Color _emailWarningColor;
    
    [ObservableProperty] private bool _isPasswordWarning;
    [ObservableProperty] private string _passwordWarningText;
    [ObservableProperty] private Color _passwordWarningColor;

    [ObservableProperty] private string _emailEntry = "";
    [ObservableProperty] private string _passwordEntry = "";

    public bool LoginButtonEnabled =>
        IsEmailValid(EmailEntry) &&
        IsPasswordValid(PasswordEntry);

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
            EmailWarningText = "";
        }
    }

    [RelayCommand]
    private void CheckPasswordEntry(string password)
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
            PasswordWarningText = "";
        }
    }

    public bool IsEmailValid(string email)
    {
        if (!UsersRepo.ContainsEmail(EmailEntry))
            return false;

        return true;
    }

    public bool IsPasswordValid(string password)
    {
        return PasswordEntry.Length > 5;
    }

    partial void OnEmailEntryChanged(string value)
    {
        OnPropertyChanged(nameof(LoginButtonEnabled));
    }

    partial void OnPasswordEntryChanged(string value)
    {
        OnPropertyChanged(nameof(LoginButtonEnabled));
    }
}