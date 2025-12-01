using Newtonsoft.Json;
using Model;

namespace Services;

public class UserServices
{
    public UserData LoggedInUser { get; private set; }

    private Dictionary<string, UserData> _userProfiles = new();
    private readonly string _fileName = "userprofiles.json";
    
    public void SetLoggedInUser(string email)
    {
        LoggedInUser = _userProfiles[email];
    }
    
    public void ClearLoggedInUser()
    {
        LoggedInUser = default;
    }

    public void AddUser(string username, string email, string password)
    {
        _userProfiles.Add(email, CreateUserDataObj(username, email, password));
        Save();
    }
    
    private void Save()
    {
        string json = JsonConvert.SerializeObject(_userProfiles, Formatting.Indented);
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
        _userProfiles = JsonConvert.DeserializeObject<Dictionary<string, UserData>>(json);
    }

    public bool ContainsEmail(string email)
    {
        return _userProfiles.ContainsKey(email);
    }

    public bool ValidateUser(string emailToBeValidated, string password)
    {
        bool userIsFound = _userProfiles.TryGetValue(emailToBeValidated, out UserData user);

        if (!userIsFound)
            return false;

        bool passwordMatches = CryptographyServices.IsPasswordValid(password, user.Password);
        return userIsFound && passwordMatches;
    }

    private UserData CreateUserDataObj(string username, string email, string password)
    {
        return new UserData(
            Guid.NewGuid().ToString(),
            username,
            email,
            CryptographyServices.HashPassword(password)
        );
    }
}
