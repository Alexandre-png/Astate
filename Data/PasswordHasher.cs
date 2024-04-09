using System;
using System.Security.Cryptography;

public class PasswordHasher
{
    // Génère un sel aléatoire
    public byte[] GenerateSalt(int saltSize)
    {
        byte[] salt = new byte[saltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    // Hash le mot de passe avec PBKDF2
    public byte[] HashPassword(string password, byte[] salt, int iterations, int outputBytes)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
        {
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}