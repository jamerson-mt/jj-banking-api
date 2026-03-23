using JJBanking.Domain.DTOs;
using JJBanking.Domain.Entities;
using JJBanking.Domain.Interfaces;
using JJBanking.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly BankDbContext _context;

    public AuthService(UserManager<User> userManager, BankDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    // Corrigido o tipo de retorno para AuthResponse para bater com a Interface
    public async Task<AuthResponse> RegisterAsync(AccountRegister request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                Cpf = request.Cpf,
            };

            // 1. Cria o usuário
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault()?.Description ?? "Erro ao criar usuário.";
                throw new Exception(error);
            }

            // 2. Gera número de conta ÚNICO (com check no banco)
            var accNumber = await GenerateUniqueNumber();

            // 3. Cria a conta
            var account = new Account(user.Id, 0m, accNumber);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            // Aqui você depois vai implementar a geração real do Token JWT
            return new AuthResponse("token_provisorio", accNumber, user.FullName);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<AuthResponse> AuthenticateAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            throw new Exception("Credenciais inválidas.");
        }

        // Busca o número da conta vinculado ao usuário
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == user.Id);

        var accountNumber = account?.AccountNumber ?? "0000-0";

        // Aqui geraremos o JWT em breve
        return new AuthResponse("token_real_vindo_daqui_a_pouco", accountNumber, user.FullName);
    }

    private async Task<string> GenerateUniqueNumber()
    {
        var random = new Random();
        string number;
        bool exists;

        do
        {
            number = $"{random.Next(1000, 9999)}-{random.Next(0, 9)}";
            // Verifica se já existe uma conta com esse número no banco
            exists = await _context.Accounts.AnyAsync(a => a.AccountNumber == number);
        } while (exists);

        return number;
    }
}
