using Services;
using Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel;

public partial class ListUsersViewModel : BaseViewModel
{
    public ObservableCollection<UserData> Users { get; private set; }
    public UserServices UserServices { get; private set; }
        
    public ListUsersViewModel(UserServices userServices)
    {
        UserServices = userServices;
        Users = new ObservableCollection<UserData>(userServices.Users.Values.ToList());
    }

    [RelayCommand]
    private void ToggleBlock(UserData user)
    {
        if (user == null)
            return;
        
        UserServices.ToggleUserBlockStatus(user.Email);
    }
}
