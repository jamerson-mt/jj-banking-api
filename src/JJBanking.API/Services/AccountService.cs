using JJBanking.Domain.DTOs;
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

    //  REALIZA UMA TRANSAÇÃO DE DEPÓSITO
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

    // REALIZA UMA TRANSAÇÃO DE SAQUE
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

    //
    public async Task<IEnumerable<Transaction>> GetStatementAsync(Guid accountId)
    {
        return await _context
            .Transactions.Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    //  REALIZA UMA TRANSAÇÃO DE DEPÓSITO
    public async Task<TransferResponse> TransferAsync(
        Guid originAccountId,
        Guid destinationAccountId,
        decimal amount
    )
    {
        // Cria uma lista com os dois IDs
        var accountIds = new List<Guid> { originAccountId, destinationAccountId };

        // Busca todas as contas que estão nessa lista
        var accounts = await _context.Accounts.Where(a => accountIds.Contains(a.Id)).ToListAsync();

        // Validação
        if (accounts.Count < 2)
        {
            // Aqui você identifica qual delas falta ou retorna erro genérico
            throw new Exception("Uma ou ambas as contas não existem.");
        }

        // Para facilitar o uso depois, você separa as variáveis
        var originAccount = accounts.First(a => a.Id == originAccountId);
        var destinationAccount = accounts.First(a => a.Id == destinationAccountId);

        // 1. Atualiza os saldos nas entidades (Lógica de domínio)
        originAccount.Withdraw(amount);
        destinationAccount.Deposit(amount);

        // 2. Cria os registros da transação (Entrada Dupla)
        var originTransaction = new Transaction(
            originAccountId,
            amount,
            TransactionType.Debit,
            $"Transferência enviada para conta {destinationAccount.AccountNumber}" // Melhorar a descrição ajuda no extrato
        );

        var destinationTransaction = new Transaction(
            destinationAccountId,
            amount,
            TransactionType.Credit,
            $"Transferência recebida da conta {originAccount.AccountNumber}"
        );

        // 3. Salva tudo no banco
        // Usamos AddRange para adicionar a lista de transações de uma vez
        _context.Transactions.AddRange(originTransaction, destinationTransaction);

        // O EF Core enviará os 2 UPDATES das contas e os 2 INSERTS das transações
        // em uma única transação atômica do SQL.
        await _context.SaveChangesAsync();

        // ... (após o SaveChangesAsync)

        // Mapeamento manual para o DTO
        var response = new TransferResponse
        {
            TransactionId = originTransaction.Id, // Usamos o ID da transação de débito
            Amount = amount,
            Date = DateTime.UtcNow,
        };

        return response;
        // Você pode retornar a transação de origem ou um objeto customizado
    }
}
