using Model;
using Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel;

public partial class ListPollsViewModel : BaseViewModel
{
    public ObservableCollection<PollData> UserPolls { get; private set; } = new();
    public ObservableCollection<PollData> OtherPolls { get; private set; } = new();
    public ObservableCollection<PollData> ArchivedPolls { get; private set; } = new();

    public PollServices PollService { get; private set; }
    public UserServices UserService { get; private set; }

    [ObservableProperty] private PollData? _selectedPoll;
    [ObservableProperty] private OptionData? _selectedOption;

    private bool CanVote =>
        SelectedOption != null && SelectedPoll != null;

    public ListPollsViewModel(UserServices userServices, PollServices pollServices)
    {
        PollService = pollServices;
        UserService = userServices;

        SeperatePolls();
    }

    public void SeperatePolls()
    {
        UserPolls.Clear();
        OtherPolls.Clear();
        ArchivedPolls.Clear();
        
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

    partial void OnSelectedPollChanged(PollData? value)
    {
        SelectedOption = null;

        if (value is null)
            return;

        SelectedOption = PollService.LoadPoll(UserService.LoggedInUser.Guid, value);
    }

    #region Vote

    [RelayCommand]
    private void SubmitVote()
    {
        if (!CanVote)
            return;
        
        var newVote = new VotesData(UserService.LoggedInUser.Guid, SelectedOption.Id);
        PollService.AddOrModifyVote(SelectedPoll ,newVote);
    }

    [RelayCommand]
    private void SelectOption(OptionData option)
    {
        if (option == null)
            return;

        SelectedOption = option;
    }

    /*private OptionData? GetOptionOrNull()
    {
        VotesData? foundVote = null;
        var votes = SelectedPoll.Votes;
        foreach (var vote in votes)
        {
            if (vote.VoterID == UserService.LoggedInUser.Guid) 
                foundVote = vote;
                break;
        }
        if (foundVote == null) return null;
        return GetOptionByVoteAndId(SelectedPoll, foundVote);
    }

    private OptionData? GetOptionByVoteAndId(PollData poll, VotesData vote)
    {
        foreach (var i in poll.Options)
        {
            if (i.Id == vote.RelatedOption)
                return i;
        }
        return null;
    }*/

    #endregion
}
