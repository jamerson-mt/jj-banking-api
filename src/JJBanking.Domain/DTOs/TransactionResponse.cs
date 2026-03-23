namespace JJBanking.Domain.DTOs;

public record TransactionResponse(
    Guid Id,
    decimal Amount,
    string Type,
    string Description,
    DateTime CreatedAt
);
