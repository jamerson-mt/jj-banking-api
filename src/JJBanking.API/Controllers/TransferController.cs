using JJBanking.Domain.DTOs;
using JJBanking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JJBanking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Transferências Bancárias")]
public class TransferController : ControllerBase
{
    private readonly IAccountService _accountService;

    public TransferController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    //Realiza uma transferencia
    ///<summary>
    /// realiza uma trasnferencia para uma conta destino
    /// </summary>
    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] TransferRequest request)
    {
        try
        {
            // Chamada do service passando as propriedades do request (corrigido o ponto)
            var result = await _accountService.TransferAsync(
                request.OriginAccountId,
                request.DestinationAccountId,
                request.Amount
            );

            // Retorna 200 OK com o DTO de sucesso que criamos
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            // Erros de regra de negócio (saldo insuficiente, conta não existe)
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Erros inesperados do sistema
            return StatusCode(
                500,
                new
                {
                    message = "Ocorreu um erro interno no processamento da transferência.",
                    details = ex.Message,
                }
            );
        }
    }
}
