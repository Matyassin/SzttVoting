using Model;
using Services;
using CommunityToolkit.Mvvm.Input;
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

    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsStatisticsVisible))] 
    private PollData? _selectedPoll;
    
    [ObservableProperty] private OptionData? _selectedOption;

    public bool CanSubmitVote =>
        SelectedPoll != null &&
        OtherPolls.Contains(SelectedPoll);

    public bool CanCloseVote =>
        SelectedPoll != null &&
        (UserPolls.Contains(SelectedPoll) || (UserService.LoggedInUser.IsAdmin && SelectedPoll.IsActive));

    public bool CanModifyVote =>
        SelectedPoll != null &&
        SelectedPoll.Votes.Count == 0 &&
        (UserPolls.Contains(SelectedPoll) || (UserService.LoggedInUser.IsAdmin && SelectedPoll.IsActive));

    public bool CanDeleteVote =>
        SelectedPoll != null &&
        UserService.LoggedInUser.IsAdmin;

    public bool IsStatisticsVisible =>
        SelectedPoll != null && !SelectedPoll.IsActive;


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
            if (DateTime.Now > pollData.Deadline || !pollData.IsActive)
            {
                pollData.IsActive = false;
                ArchivedPolls.Add(pollData);
                PollService.RecalculatePercentage(pollData);
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

        OnPropertyChanged(nameof(CanSubmitVote));
        OnPropertyChanged(nameof(CanCloseVote));
        OnPropertyChanged(nameof(CanModifyVote));

        SelectedOption = PollService.LoadPoll(UserService.LoggedInUser.Guid, value);
    }

    [RelayCommand]
    private void SubmitVote()
    {
        if (!CanSubmitVote || SelectedOption == null)
            return;
        
        var newVote = new VotesData(UserService.LoggedInUser.Guid, SelectedOption.Id);
        PollService.AddOrModifyVote(SelectedPoll, newVote);
    }

    [RelayCommand]
    private void ModifyVote()
    {
        return;
    }

    [RelayCommand]
    private void CloseVote()
    {
        if (SelectedPoll == null)
            return;

        SelectedPoll.IsActive = false;
        SeperatePolls();
        PollService.Save();
        
        CloseVoteCommand.NotifyCanExecuteChanged();

        OnPropertyChanged(nameof(CanSubmitVote));
        OnPropertyChanged(nameof(CanCloseVote));
        OnPropertyChanged(nameof(CanModifyVote));
    }

    [RelayCommand]
    private void DeleteVote()
    {
        if (SelectedPoll == null)
            return;

        PollService.RemovePoll(SelectedPoll.Title);

        if (UserPolls.Contains(SelectedPoll))
        {
            UserPolls.Remove(SelectedPoll);
        }
        else if (OtherPolls.Contains(SelectedPoll))
        {
            OtherPolls.Remove(SelectedPoll);
        }
        else if (ArchivedPolls.Contains(SelectedPoll))
        {
            ArchivedPolls.Remove(SelectedPoll);
        }

        OnPropertyChanged(nameof(CanSubmitVote));
        OnPropertyChanged(nameof(CanCloseVote));
        OnPropertyChanged(nameof(CanModifyVote));
    }

    [RelayCommand]
    private void SelectOption(OptionData option)
    {
        if (option == null)
            return;

        SelectedOption = option;
    }
}
