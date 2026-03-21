using System.ComponentModel.DataAnnotations; // Necessário para usar as anotações de validação, como [Required] e [StringLength]
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // Necessário para usar as anotações de mapeamento, como [Table] e [Column]

namespace JJBanking.Domain.Entities;

[Table("Accounts")] // Especifica o nome da tabela no banco de dados
public class Account
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid(); // Gera um ID único para cada conta

    [Required]
    [StringLength(14, MinimumLength = 11)]
    public string Cpf { get; private set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Owner { get; private set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(8,2)")]
    public decimal Balance { get; private set; }

    [Required]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow; // Data de criação da conta

   
    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    // Construtor necessário para o Entity Framework
    private Account() { } // O EF precisa de um construtor sem parâmetros para criar instâncias da entidade

    // Construtor público para criar uma nova conta com um depósito inicial
    public Account(string owner, string cpf, decimal initialDeposit)
    {
        // Validações básicas para garantir que os dados fornecidos são válidos
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException("O nome do proprietário é obrigatório.", nameof(owner));
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("O CPF é obrigatório.", nameof(cpf));
        if (initialDeposit < 0)
            throw new ArgumentException(
                "O depósito inicial não pode ser negativo.",
                nameof(initialDeposit)
            );
        // Atribui os valores fornecidos aos campos da conta
        Owner = owner; // Define o proprietário da conta
        Cpf = cpf; // Define o CPF do proprietário da conta
        Balance = initialDeposit; // Define o saldo inicial da conta
    }

    //METODO DE DEPOSITO
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Depósito inválido");

        Balance += amount;
    }

    // METODO DE SAQUE
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("O valor do saque deve ser positivo.");
        }
        else if (amount > Balance)
        {
            throw new InvalidOperationException("Saldo insuficiente.");
        }
        else
        {
            Balance -= amount;
        }
    }
}
