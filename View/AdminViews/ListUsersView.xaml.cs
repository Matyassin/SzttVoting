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
}
