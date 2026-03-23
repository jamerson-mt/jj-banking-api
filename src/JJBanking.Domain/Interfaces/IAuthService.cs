using JJBanking.Domain.DTOs;

// interface para o serviço de autenticação

namespace JJBanking.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateAsync(string email, string password);
        Task<AuthResponse> RegisterAsync(AccountRegister registerRequest);
    }
}
// quem he AuthResponse? >
