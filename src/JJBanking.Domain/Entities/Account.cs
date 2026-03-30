using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJBanking.Domain.Entities;

[Table("Accounts")]
public class Account
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required]
    public string AccountNumber { get; private set; } = default!; // Ex: "54321-0"

    [Required]
    public string Branch { get; private set; } = "0001"; // Agência fixa para começar

    [Required]
    [Column(TypeName = "decimal(18,2)")] // Aumentei a precisão para 18,2 (padrão bancário)
    public decimal Balance { get; private set; }

    [Required]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    // --- RELACIONAMENTO COM USER ---
    [Required]
    public Guid UserId { get; private set; } // FK para o IdentityUser

    [ForeignKey("UserId")] // Especifica que UserId é a chave estrangeira para a entidade User
    public virtual User User { get; private set; } = null!;

    // -------------------------------

    public virtual ICollection<Transaction> Transactions { get; private set; } =
        new List<Transaction>();

    private Account() { }

    // Construtor atualizado: agora recebe o UserId em vez de Nome/CPF
    public Account(Guid userId, decimal initialDeposit, string accountNumber)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("O ID do usuário é obrigatório.", nameof(userId));

        if (initialDeposit < 0)
            throw new ArgumentException(
                "O depósito inicial não pode ser negativo.",
                nameof(initialDeposit)
            );

        UserId = userId;
        Balance = initialDeposit;
        AccountNumber = accountNumber;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Depósito inválido");
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("O valor deve ser positivo.");
        if (amount > Balance)
            throw new InvalidOperationException("Saldo insuficiente.");

        Balance -= amount;
    }
}
