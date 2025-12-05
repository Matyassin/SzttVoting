using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Model;

public partial class Poll
    (UserData user, string title, string description, DateTime? deadline, List<OptionData> options, List<VotesData> votes) 
    : ObservableObject
{
    [ObservableProperty]  private string _creatorID = user.Guid;
    [ObservableProperty]  private string _id = Guid.NewGuid().ToString();
    [ObservableProperty]  private string _title = title;
    [ObservableProperty]  private string _description = description;
    [ObservableProperty]  private DateTime _created = DateTime.Now;
    [ObservableProperty]  private DateTime? _deadline = deadline;
    [ObservableProperty]  private bool _isActive = true;

    [ObservableProperty]  private List<OptionData> _options = options;
    [ObservableProperty]  private List<VotesData> _votes = votes;
}

public partial class OptionData(string text) : ObservableObject
{
    [ObservableProperty] private string _id = Guid.NewGuid().ToString();
    [ObservableProperty] private string _text = text;
}

public partial class VotesData(UserData user, OptionData option) : ObservableObject
{
    [ObservableProperty] private string _id;
    [ObservableProperty] private string _voterID = user.Guid;
    [ObservableProperty] private string _relatedOption = option.Id;
    [ObservableProperty] private DateTime _created = DateTime.Now;
}
