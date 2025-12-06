using Model;
using Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel;

public partial class ListPollsViewModel : BaseViewModel
{
    public ObservableCollection<PollData> UserPolls { get; private set; } = new();
    public ObservableCollection<PollData> OtherPolls { get; private set; } = new();
    public ObservableCollection<PollData> ArchivedPolls { get; private set; } = new();
    
    public PollServices PollService { get; private set; }
    public UserServices UserService { get; private set; }

    [ObservableProperty] private PollData? _selectedPoll;

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
            if (DateTime.Now > pollData.Deadline)
            {
                pollData.IsActive = false;
                ArchivedPolls.Add(pollData);
            }
            else if (UserService.LoggedInUser.Guid == pollData.CreatorID)
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
