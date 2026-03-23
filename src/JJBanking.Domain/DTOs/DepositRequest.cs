namespace JJBanking.Domain.DTOs;

public record DepositRequest(Guid AccountId, decimal Amount, string Description);
