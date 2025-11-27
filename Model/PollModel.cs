namespace Model;

public class PollModel
{
    private static int _lastPoolId = 0;
    public UserData Creator { get; private set; }
    public string Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Deadline { get; private set; }
    public Boolean IsActive { get; private set; }
    
    public List<OptionStruct> Options = new List<OptionStruct>();

    private List<VotesStruct> Votes = new List<VotesStruct>();
    
    public PollModel(UserData user, string title, string description, DateTime deadline)
    {
        Creator = user;
        Id = user.Email + (++_lastPoolId).ToString();
        Title = title;
        Description = description;
        Created = DateTime.Now;
        Deadline = deadline;
        IsActive = false;
    }

}

public struct OptionStruct
{
    public int Id;
    public string Text;
}

public struct VotesStruct
{
    public int Id;
    public UserData Voter;
    public OptionStruct RelatedOption;
    public DateTime Created;
}