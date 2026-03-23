using JJBanking.Domain.DTOs;
using JJBanking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JJBanking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // /api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AccountRegister request)
    {
        // O Controller apenas valida se o modelo é válido e chama o Service
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            // O Service é quem tem a lógica de negócio, o Controller só orquestra
            var result = await _authService.RegisterAsync(request);
            return Ok(result); // 200 OK
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
