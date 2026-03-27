namespace JJBanking.Domain.DTOs;

public record AuthResponse(string Token, string AccountNumber, string FullName);
