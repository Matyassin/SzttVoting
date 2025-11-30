using System;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace Services;

public static class CryptographyServices
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool IsPasswordValid(string passwordToBeChecked, string encryptedPasswordHash)
    {
        return BCrypt.Net.BCrypt.Verify(passwordToBeChecked, encryptedPasswordHash);
    }
}