using Services;

namespace View;

public partial class SeeOngoingVotesView : ContentPage
{
    private UserServices _userServices;

    public SeeOngoingVotesView(UserServices userServices)
    {
        InitializeComponent();
        _userServices = userServices;
    }
}
