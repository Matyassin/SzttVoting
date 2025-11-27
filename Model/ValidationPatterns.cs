namespace Model;

public static class ValidationPatterns
{
    public const string EmailPattern = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}";
    public const string PasswordPattern = @"^(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]+$";
}
