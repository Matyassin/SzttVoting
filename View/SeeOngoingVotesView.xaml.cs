using Services;

namespace View.UserViews;

public partial class SeeOngoingVotesView : ContentPage
{
    private UserServices _userServices;

    public SeeOngoingVotesView(UserServices userServices)
    {
        InitializeComponent();
        _userServices = userServices;
    }
}
