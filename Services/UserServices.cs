using Model;
using Newtonsoft.Json;

namespace Services;

public class UserServices : IDataService
{
    public UserData LoggedInUser { get; private set; }

    public Dictionary<string, UserData> Users { get; private set; } = new();
    private readonly string _fileName = "userprofiles.json";

    public const string EmailPattern = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}";
    public const string PasswordPattern = @"^(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]+$";

    public void SetLoggedInUser(string email)
    {
        LoggedInUser = Users[email];
    }
    
    public void ClearLoggedInUser()
    {
        LoggedInUser = default;
    }

    public void AddUser(string username, string email, string password)
    {
        Users.Add(email,
            new UserData(
                Guid.NewGuid().ToString(),
                username,
                email,
                BCrypt.Net.BCrypt.HashPassword(password)
            )
        );
        
        Save();
    }
    
    public void Save()
    {
        string json = JsonConvert.SerializeObject(Users, Formatting.Indented);
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
        Users = JsonConvert.DeserializeObject<Dictionary<string, UserData>>(json);
    }

    public bool ContainsEmail(string email)
    {
        return Users.ContainsKey(email);
    }

    public bool TryValidateUser(string emailToValidate, string passwordToValidate)
    {
        bool userIsFound = Users.TryGetValue(emailToValidate, out UserData user);

        if (!userIsFound)
            return false;

        bool passwordMatches = BCrypt.Net.BCrypt.Verify(passwordToValidate, user.Password);
        return userIsFound && passwordMatches;
    }
}
