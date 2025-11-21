namespace Model;

public class UserProfile
{
    private string _email;
    private string _password;

    public UserProfile(string name, string password)
    {
       _email = name;
       _password = password;
    }
}