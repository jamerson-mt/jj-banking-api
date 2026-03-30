namespace JJBanking.Domain.DTOs;

public record TransferRequest(Guid OriginAccountId, Guid DestinationAccountId, decimal Amount);
