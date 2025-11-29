using ViewModel;

namespace View;

public partial class UserView : TabbedPage
{
    private readonly UserViewModel _vm;

    public UserView(string email)
    {
        InitializeComponent();
        _vm = new UserViewModel(email);
        BindingContext = _vm;
    }

    private async void Signout_OnClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Sign Out?", "Are you sure you want to log out?", "Yes", "No");
        if (answer)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
