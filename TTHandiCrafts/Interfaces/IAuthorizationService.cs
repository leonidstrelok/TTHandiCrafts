using System.Threading.Tasks;
using TTHandiCrafts.Models;

namespace TTHandiCrafts.Interfaces
{
    public interface IAuthorizationService
    {
        Task Auhtorization(Login login);
        Task<bool> Registration(Register register);
        Task<string> ResetPasswordByEmail(string email);
        Task PasswordReset(string token);
    }
}
