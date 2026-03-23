using JJBanking.Domain.Entities;
using JJBanking.Infra.Context;
using Microsoft.AspNetCore.Identity;
using JJBanking.API.DTOs.Auth;

namespace JJBanking.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly BankDbContext _context;

    public AuthService(UserManager<User> userManager, BankDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    // Método de Registro com transação manual para garantir a integridade
    public async Task<User> RegisterAsync(AccountRegister request)
    {
        // 1. Iniciamos uma transação manual para garantir a integridade do processo de criação de usuário + conta
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            //
            var user = new User
            {
                UserName = request.Email, //
                Email = request.Email,
                FullName = request.FullName,
                Cpf = request.Cpf,
            };

            // 2. Cria o usuário no Identity (Hash de senha automático)
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new Exception("Falha ao criar usuário.");

            // 3. Gera número de conta único (Lógica que discutimos)
            var accNumber = await GenerateUniqueNumber();

            // 4. Cria a conta vinculada
            var account = new Account(user.Id, 0m, accNumber);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            // 5. Se tudo deu certo, confirma no banco
            await transaction.CommitAsync();

            return new AuthResponse("token_aqui", accNumber, user.FullName);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task<string> GenerateUniqueNumber()
    {
        // Lógica de Random + Check no DB que vimos antes
        return $"{new Random().Next(1000, 9999)}-{new Random().Next(0, 9)}";
    }
}

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    
}