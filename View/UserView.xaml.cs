using ViewModel;

namespace View;

public partial class UserView : ContentPage
{
    private readonly UserViewModel _vm;
    private List<ContentView> _nestedUserViews = new List<ContentView>();
    private int _currentNestedViewID;

    public UserView(string email)
    {
        InitializeComponent();
        _vm = new UserViewModel(email);
        BindingContext = _vm;
        
        _nestedUserViews.Add(new HomepageView());
        _nestedUserViews.Add(new OngoingVotesView());
        _currentNestedViewID = 0;
        
        DynamicsArea.Content = _nestedUserViews.ElementAt(_currentNestedViewID);
    }

    async private void Signout_OnClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Sign Out?", "Are you sure you want to log out?", "Yes", "No");

        if (answer)
        {
            Application.Current.MainPage = new LoginView();
        }
    }

    private void HomepageHeader_Clicked(object sender, EventArgs e)
    {
        _currentNestedViewID = 0;
        DynamicsArea.Content = _nestedUserViews.ElementAt(_currentNestedViewID);
    }

    private void OngoingVotesHeader_Clicked(object sender, EventArgs e)
    {
        _currentNestedViewID = 1;
        DynamicsArea.Content = _nestedUserViews.ElementAt(_currentNestedViewID);
    }
}
