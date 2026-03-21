using JJBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JJBanking.Infra.Context;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options) { }

    public DbSet<Account> Accounts => Set<Account>(); // Propriedade para acessar a tabela de contas
    public DbSet<Transaction> Transactions => Set<Transaction>(); // Propriedade para acessar a tabela de transações

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasKey(a => a.Id);

        //CPF UNICO EM TODA A TABELA ACCOUNT
        modelBuilder.Entity<Account>().HasIndex(a => a.Cpf).IsUnique();

        //GARANTE PRECISAO DO SALDO NO BANCO DE DADOS (8 digitos, 2 decimais)
        modelBuilder.Entity<Account>().Property(a => a.Balance).HasColumnType("decimal(8,2)");

        // --- ADICIONE ISSO PARA A TRANSACTION ---
        modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasColumnType("decimal(18,2)");

        // Relacionamento 1:N (Uma conta tem muitas transações)
        modelBuilder
            .Entity<Transaction>()
            .HasOne(t => t.Account)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.AccountId);

        base.OnModelCreating(modelBuilder);
    }
}
