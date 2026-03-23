namespace JJBanking.Domain.DTOs;

// DTO de Registro (Entrada)
public record AccountRegister(string Email, string Password, string FullName, string Cpf);
