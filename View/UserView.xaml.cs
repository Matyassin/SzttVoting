using ViewModel;

namespace View;

public partial class UserView : ContentPage
{
    private readonly UserViewModel _vm;

    public UserView(string email)
    {
        InitializeComponent();
        _vm = new UserViewModel(email);
        BindingContext = _vm;
    }

    private void Signout_OnClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginView();
    }
}
