namespace Model;

public class Poll(UserData user, string title, string description, DateTime? deadline)
{
    public string CreatorID = user.Guid;
    public string Id = Guid.NewGuid().ToString();
    public string Title = title;
    public string Description = description;
    public DateTime Created = DateTime.Now;
    public DateTime? Deadline = deadline;
    public bool IsActive = true;

    public List<OptionData> Options = new List<OptionData>();
    private List<VotesData> Votes = new List<VotesData>();
}

public struct OptionData(string text)
{
    public string Id = Guid.NewGuid().ToString();
    public string Text;
}

public struct VotesData
{
    public string Id;
    public UserData Voter;
    public OptionData RelatedOption;
    public DateTime Created;
}
