using CommunityToolkit.Mvvm.ComponentModel;

namespace Model;

public partial class UserData
    (string id, string username,string email, string password, bool isBlocked = false)
    : ObservableObject
{
    [ObservableProperty] private string _guid = id;
    [ObservableProperty] private string _username = username;
    [ObservableProperty] private string _email = email;
    [ObservableProperty] private string _password = password;
    [ObservableProperty] private bool _isBlocked = isBlocked;
    [ObservableProperty] private bool _isAdmin = false;
}