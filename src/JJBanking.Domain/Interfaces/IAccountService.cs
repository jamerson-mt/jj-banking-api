using JJBanking.Domain.Entities;

namespace JJBanking.Domain.Interfaces;

public interface IAccountService
{
    // DEFINIÇÃO DOS MÉTODOS PARA GERENCIAR CONTAS E TRANSAÇÕES
    Task<Account> CreateAccountAsync(string owner, string cpf, decimal initialDeposit); // O MÉTODO PARA CRIAR UMA NOVA CONTA COM DEPÓSITO INICIAL
    Task<Transaction> DepositAsync(Guid accountId, decimal amount, string description);
    Task<Transaction> WithdrawAsync(Guid accountId, decimal amount, string description);
    Task<IEnumerable<Transaction>> GetStatementAsync(Guid accountId);
}
