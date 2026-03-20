using JJBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JJBanking.Infra.Context;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasKey(a => a.Id);

        //CPF UNICO EM TODA A TABELA ACCOUNT
        modelBuilder.Entity<Account>().HasIndex(a => a.Cpf).IsUnique();

        //GARANTE PRECISAO DO SALDO NO BANCO DE DADOS (8 digitos, 2 decimais)
        modelBuilder.Entity<Account>().Property(a => a.Balance).HasColumnType("decimal(8,2)");

        base.OnModelCreating(modelBuilder);
    }
}
