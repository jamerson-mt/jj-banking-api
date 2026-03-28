namespace JJBanking.Domain.DTOs;

public record AuthResponse(
    string Token,
    string FullName,
    Guid AccountId, // Essencial para as rotas de Transações
    string AccountNumber,
    string Branch, // Agência (geralmente "0001")
    decimal Balance // Para o App já abrir com o saldo na tela
);
