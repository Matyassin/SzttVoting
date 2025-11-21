using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _emailEntry = "example@example.com";
    
    [ObservableProperty]
    private string _passwordEntry = "";
}