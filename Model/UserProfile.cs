using Newtonsoft.Json;

namespace Model;

public struct UserProfile
{
    [JsonProperty]
    private string _email;

    [JsonProperty]
    private string _password;

    public UserProfile(string name, string password)
    {
        _email = name;
        _password = password;
    }
}