using Services;
using ViewModel;

namespace View.AdminViews;

public partial class ListUsersView : ContentPage
{
    private readonly ListUsersViewModel _vm;

    public ListUsersView(UserServices userServices)
    {
        InitializeComponent();
        _vm = new ListUsersViewModel(userServices);
        BindingContext = _vm;
    }

    private void Block_Btn_Clicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is UserDisplayModel user)
        {
            _vm.UserServices.ToggleUserBlockStatus(user.Email);
            user.IsBlocked = !user.IsBlocked;
            btn.Text = user.IsBlocked ? "Unblock" : "Block";
        }
    }
}
