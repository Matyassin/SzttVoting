using Newtonsoft.Json;

namespace Model;

public struct UserData(string name, string password)
{
    [JsonProperty]
    public string Email = name;

    [JsonProperty]
    public string Password = password;
}