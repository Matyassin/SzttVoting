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

    [ObservableProperty] private PollData? _selectedPoll;
    [ObservableProperty] private OptionData? _selectedOption;

    public bool CanVote =>
        SelectedPoll != null &&
        OtherPolls.Contains(SelectedPoll);

    public bool CanCloseVote =>
        SelectedPoll != null &&
        UserPolls.Contains(SelectedPoll);

    public bool CanModifyVote =>
        // and no one has voted on this current poll &&
        CanCloseVote;

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

        OnPropertyChanged(nameof(CanVote));
        OnPropertyChanged(nameof(CanModifyVote));
        OnPropertyChanged(nameof(CanCloseVote));

        SelectedOption = PollService.LoadPoll(UserService.LoggedInUser.Guid, value);
    }

    [RelayCommand]
    private void SubmitVote()
    {
        if (!CanVote && SelectedOption == null)
            return;
        
        var newVote = new VotesData(UserService.LoggedInUser.Guid, SelectedOption.Id);
        PollService.AddOrModifyVote(SelectedPoll, newVote);
    }

    [RelayCommand]
    private void SelectOption(OptionData option)
    {
        if (option == null)
            return;

        SelectedOption = option;
    }
}
