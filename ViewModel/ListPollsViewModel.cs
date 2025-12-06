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
    
    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(SubmitVoteCommand))] 
    private OptionData? _selectedOption;
    
    

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
            if (pollData.IsActive == false || pollData.Deadline < DateTime.Now)
            {
                ArchivedPolls.Add(pollData);
            }
            else 
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
    
    #region Vote

    [RelayCommand]
    private void SubmitVote()
    {
        if (!CanVote()) return;
        
        var newVote = new VotesData(UserService.LoggedInUser.Guid, SelectedOption);
        PollService.AddVote(SelectedPoll ,newVote);
    }
    
    private bool CanVote() {
        return SelectedOption != null && SelectedPoll != null;
    }

    [RelayCommand]
    private void SelectOption(OptionData option)
    {
        if (option == null) return;

        SelectedOption = option;
    }

    #endregion
}
