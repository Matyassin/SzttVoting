using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace ViewModel;

public partial class UserViewModel : BaseViewModel
{
    public string UserEmail {get { return _userServices.LoggedInUser.Email; }}
    public string UserName 
    {
        get
        {
            var temp = UserEmail.Split('@').First();
            return char.ToUpper(temp[0]) + temp.Substring(1);
        }
    }
    public string HeaderMainText
    {
        get { return $"Welcome, {UserName}!"; }
    }
    
    private readonly UserServices _userServices;

    public UserViewModel(UserServices userServices)
    {
        _userServices = userServices;
    }
}
