using Model;
using Services;
using System.Collections.ObjectModel;

namespace ViewModel;

public class ListPollsViewModel : BaseViewModel
{
    public ObservableCollection<PollData> UsersPolls = new ObservableCollection<PollData>();
    public ObservableCollection<PollData> OthersPolls = new ObservableCollection<PollData>();
    public PollServices PollService { get; private set; }
    public UserServices UserService { get; private set; }

    public ListPollsViewModel(UserServices userServices, PollServices pollServices)
    {
        PollService = pollServices;
        UserService = userServices;
        SeperatePools();
    }

    public void SeperatePools()
    {
        var userGuid = UserService.LoggedInUser.Guid;
        foreach (var pollData in PollService.Polls.Values.ToList())
        {
            if (userGuid == pollData.CreatorID)
            {
                UsersPolls.Add(pollData);
            }
            else
            {
                OthersPolls.Add(pollData);
            }
        }
    }
}
