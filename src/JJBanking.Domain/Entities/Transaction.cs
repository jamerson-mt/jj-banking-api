using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JJBanking.Domain.Enums; // <--- ESTA LINHA É A CHAVE PARA O ERRO SUMIR

namespace JJBanking.Domain.Entities;

[Table("Transactions")]
public class Transaction
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required]
    public Guid AccountId { get; private set; }

    [ForeignKey("AccountId")]
    public Account Account { get; private set; } = null!; // informo que ela nao estara vazia na execucao

    [Required]
    [Column(TypeName = "decimal(8,2)")]
    public decimal Amount { get; private set; }

    [Required]
    public TransactionType Type { get; private set; }

    [MaxLength(255)]
    public string Description { get; private set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; private set; }

    private Transaction() { }

    public Transaction(Guid accountId, decimal amount, TransactionType type, string description)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        Amount = amount;
        Type = type;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }
}
