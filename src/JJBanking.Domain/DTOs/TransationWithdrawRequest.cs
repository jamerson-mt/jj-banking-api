// DTO para saque, contendo as informações necessárias para realizar a operação de saque

using System.ComponentModel.DataAnnotations;

namespace JJBanking.Domain.DTOs;

public record TransationWithdrawRequest(
    [Required] Guid AccountId, // ID da conta de onde o dinheiro será retirado
    [Required] decimal Amount, // Valor do saque
    [Required] string Description // Descrição do saque
);
