using JJBanking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JJBanking.Infra.Context;

// Alteramos a herança para suportar o Identity com User e Roles usando Guid
public class BankDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // IMPORTANTE: Chama o mapeamento padrão do Identity
        // Sem isso, as tabelas de login (AspNetUsers, etc) não serão criadas
        base.OnModelCreating(modelBuilder);

        // (USER) - Configurações para a entidade User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Cpf).IsUnique(); // CPF único agora é no USER
            entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
        });

        // (ACCOUNT) - Configurações para a entidade Account
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.HasIndex(a => a.AccountNumber).IsUnique(); // Número de conta deve ser único

            // Note: Removemos o índice de CPF da Account, pois o CPF não existe mais nela
            entity.Property(a => a.Balance).HasColumnType("decimal(18,2)"); // Aumentei para 18,2 (padrão financeiro)

            // Relacionamento 1:1 (Um USUÁRIO tem uma CONTA)
            entity
                .HasOne(a => a.User)
                .WithOne(u => u.Account)
                .HasForeignKey<Account>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Se um usuário for deletado, a conta associada também será deletada
        });

        // (TRANSACTION) - Configurações para a entidade Transaction
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Amount).HasColumnType("decimal(18,2)");

            // Relacionamento 1:N (Uma CONTA tem muitas TRANSAÇÕES)
            entity
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId);
        });
    }
}
