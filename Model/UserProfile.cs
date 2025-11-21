using Newtonsoft.Json;

namespace Model;

public struct UserProfile
{
    [JsonProperty]
    public string Email;

    [JsonProperty]
    public string Password;

    public UserProfile(string name, string password)
    {
        Email = name;
        Password = password;
    }
}