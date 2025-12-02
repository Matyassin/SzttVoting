using Model;
using Newtonsoft.Json;

namespace Services;

public class UserServices
{
    public UserData LoggedInUser { get; private set; }

    private Dictionary<string, UserData> _users = new();
    private readonly string _fileName = "userprofiles.json";
    
    public void SetLoggedInUser(string email)
    {
        LoggedInUser = _users[email];
    }
    
    public void ClearLoggedInUser()
    {
        LoggedInUser = default;
    }
    
    public void Save(string username, string email, string password)
    {
        _users.Add(email,
                   new UserData(Guid.NewGuid().ToString(),
                                username,
                                email,
                                CryptographyServices.HashPassword(password))
        );

        string json = JsonConvert.SerializeObject(_users, Formatting.Indented);
        string? slnPath = Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent?
            .Parent?
            .Parent?
            .Parent?
            .FullName;

        string filePath = Path.Combine(slnPath, _fileName);
        File.WriteAllText(filePath, json);
    }

    public void Load()
    {
        string? slnPath = Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent?
            .Parent?
            .Parent?
            .Parent?
            .FullName;

        string filePath = Path.Combine(slnPath, _fileName);

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "{}");
            return;
        }

        string json = File.ReadAllText(filePath);
        _users = JsonConvert.DeserializeObject<Dictionary<string, UserData>>(json);
    }

    public bool ContainsEmail(string email)
    {
        return _users.ContainsKey(email);
    }

    public bool ValidateUser(string emailToBeValidated, string password)
    {
        bool userIsFound = _users.TryGetValue(emailToBeValidated, out UserData user);

        if (!userIsFound)
            return false;

        bool passwordMatches = CryptographyServices.IsPasswordValid(password, user.Password);
        return userIsFound && passwordMatches;
    }
}
