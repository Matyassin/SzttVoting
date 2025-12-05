using Model;
using Services;
using System.Collections.ObjectModel;

namespace ViewModel;

public class ListPollsViewModel : BaseViewModel
{
    public ObservableCollection<PollData> UserPolls { get; private set; } = new();
    public ObservableCollection<PollData> OtherPolls { get; private set; } = new();

    public PollServices PollService { get; private set; }
    public UserServices UserService { get; private set; }

    public ListPollsViewModel(UserServices userServices, PollServices pollServices)
    {
        PollService = pollServices;
        UserService = userServices;
        SeperatePolls();
    }

    public void SeperatePolls()
    {
        foreach (var pollData in PollService.Polls.Values.ToList())
        {
            if (UserService.LoggedInUser.Guid == pollData.CreatorID)
            {
                UserPolls.Add(pollData);
            }
            else
            {
                OtherPolls.Add(pollData);
            }
        }
    }
}
