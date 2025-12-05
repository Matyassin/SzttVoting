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
        // block the user
    }

    private void Unblock_Btn_Clicked(object sender, EventArgs e)
    {
        // unblock the user
    }
}
