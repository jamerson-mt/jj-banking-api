using JJBanking.API.DTOs;
using JJBanking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JJBanking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IAccountService _accountService;

    public TransactionController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositRequest request)
    {
        try
        {
            // Chamamos o serviço e damos um "await" na promessa (Task)
            var transaction = await _accountService.DepositAsync(
                request.AccountId,
                request.Amount,
                request.Description
            );

            var response = new TransactionResponse(
                transaction.Id,
                transaction.Amount,
                transaction.Type.ToString(), // Converte o Enum (0, 1) para texto ("Credit")
                transaction.Description,
                transaction.CreatedAt
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] DepositRequest request)
    {
        try
        {
            var transaction = await _accountService.WithdrawAsync(
                request.AccountId,
                request.Amount,
                request.Description
            );

            return Ok(transaction);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // --- A MESMA LÓGICA PARA O WITHDRAW ---
    [HttpGet("statement/{accountId}")]
    public async Task<IActionResult> GetStatement(Guid accountId)
    {
        var transactions = await _accountService.GetStatementAsync(accountId);
        // Converte a lista de Entidades para uma lista de DTOs
        var response = transactions.Select(t => new TransactionResponse(
            t.Id,
            t.Amount,
            t.Type.ToString(),
            t.Description,
            t.CreatedAt
        ));
        return Ok(response);
    }
}

// DTO (Data Transfer Object) - Apenas para receber os dados do JSON
public record DepositRequest(Guid AccountId, decimal Amount, string Description);
