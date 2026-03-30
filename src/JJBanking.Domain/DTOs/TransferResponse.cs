namespace JJBanking.Domain.DTOs;

public class TransferResponse
{
    public Guid TransactionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
