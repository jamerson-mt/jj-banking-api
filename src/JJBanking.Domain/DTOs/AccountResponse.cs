namespace JJBanking.Domain.DTOs;

// DTO de Resposta (Saída)
public record AccountResponse(Guid Id, string Owner, string Cpf, decimal Balance);
