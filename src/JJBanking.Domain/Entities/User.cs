using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace JJBanking.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Cpf { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // relacionamento com as contas do usuário
        public virtual Account? Account { get; set; }

        public User(string email, string fullName, string cpf)
        {
            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length > 100)
                throw new ArgumentException(
                    "O Nome é obrigatória e deve ter no máximo 100 caracteres."
                );
            if (string.IsNullOrWhiteSpace(email) || email.Length > 100)
                throw new ArgumentException(
                    "A descrição é obrigatória e deve ter no máximo 250 caracteres."
                );
            UserName = email;
            Email = email;
            FullName = fullName;
            Cpf = cpf;
        }
    }
}
