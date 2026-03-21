using JJBanking.Domain.Entities;
using JJBanking.Domain.Enums;
using JJBanking.Domain.Interfaces;
using JJBanking.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace JJBanking.Infra.Services;

public class AccountService : IAccountService
{
    private readonly BankDbContext _context;

    public AccountService(BankDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction> DepositAsync(Guid accountId, decimal amount, string description)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account == null)
            throw new Exception("Conta não encontrada.");

        // 1. Atualiza o saldo na entidade Account
        account.Deposit(amount);

        // 2. Cria o registro da transação
        var transaction = new Transaction(accountId, amount, TransactionType.Credit, description);

        // 3. Salva ambos no banco (O EF faz isso em uma única transação SQL)
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }

    public async Task<Transaction> WithdrawAsync(Guid accountId, decimal amount, string description)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account == null)
            throw new Exception("Conta não encontrada.");

        // O método Withdraw da Account deve validar se tem saldo!
        account.Withdraw(amount);

        var transaction = new Transaction(accountId, amount, TransactionType.Debit, description);

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetStatementAsync(Guid accountId)
    {
        return await _context
            .Transactions.Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Account> CreateAccountAsync(string owner, string cpf, decimal initialDeposit)
    {
        var account = new Account(owner, cpf,initialDeposit);
        _context.Accounts.Add(account);

        // Se houver depósito inicial, gera a primeira transação
        if (initialDeposit > 0)
        {
            account.Deposit(initialDeposit);
            var firstTransaction = new Transaction(
                account.Id,
                initialDeposit,
                TransactionType.Credit,
                "Depósito Inicial"
            );
            _context.Transactions.Add(firstTransaction);
        }

        await _context.SaveChangesAsync();
        return account;
    }
}
