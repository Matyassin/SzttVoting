using Services;
using ViewModel;

namespace View.AdminViews;

public partial class ListUsersView : ContentPage
{
    private readonly AdminViewModel _vm;

    public ListUsersView(UserServices userServices)
    {
        InitializeComponent();
        _vm = new AdminViewModel(userServices);
        BindingContext = _vm;
    }
}
