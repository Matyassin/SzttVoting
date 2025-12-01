using Services;

namespace ViewModel.UserViewModels;

public class CreateVoteViewModel : BaseViewModel
{
    private UserServices _userServices;
    private VoteServices _voteServices;
    
    public CreateVoteViewModel(UserServices userServices, VoteServices voteServices)
    {
        _userServices = userServices;
        _voteServices = voteServices;
        
    }
}