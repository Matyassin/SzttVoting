using Services;

namespace View;

public partial class ListOngoingVotesView : ContentPage
{
    private UserServices _userServices;

    public ListOngoingVotesView(UserServices userServices)
    {
        InitializeComponent();
        _userServices = userServices;
    }
}
