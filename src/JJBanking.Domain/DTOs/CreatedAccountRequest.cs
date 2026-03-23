namespace JJBanking.Domain.DTOs;

public record CreatedAccountRequest(string Owner, string Cpf, decimal InitialDeposit);
