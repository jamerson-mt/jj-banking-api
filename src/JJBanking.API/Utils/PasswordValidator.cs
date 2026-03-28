namespace JJBanking.API.Utils;

public static class PasswordValidator
{
    public static bool IsStrong(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        bool hasMinimumLen = password.Length >= 8;
        bool hasUpperChar = password.Any(char.IsUpper);
        bool hasLowerChar = password.Any(char.IsLower);
        bool hasNumber = password.Any(char.IsDigit);
        bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

        return hasMinimumLen && hasUpperChar && hasLowerChar && hasNumber && hasSpecialChar; // Requer pelo menos um caractere especial, retorna true se a senha for forte
    }
}
