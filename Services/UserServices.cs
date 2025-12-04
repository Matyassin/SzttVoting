using Model;
using Newtonsoft.Json;

namespace Services;

public class UserServices
{
    public UserData LoggedInUser { get; private set; }

    private Dictionary<string, UserData> _users = new();
    private readonly string _fileName = "userprofiles.json";

    public const string EmailPattern = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}";
    public const string PasswordPattern = @"^(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]+$";

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
            new UserData(
                Guid.NewGuid().ToString(),
                username,
                email,
                BCrypt.Net.BCrypt.HashPassword(password)
            )
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

    public bool TryValidateUser(string emailToValidate, string passwordToValidate)
    {
        bool userIsFound = _users.TryGetValue(emailToValidate, out UserData user);

        if (!userIsFound)
            return false;

        bool passwordMatches = BCrypt.Net.BCrypt.Verify(passwordToValidate, user.Password);
        return userIsFound && passwordMatches;
    }
}
