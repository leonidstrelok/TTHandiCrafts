using System.Threading;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Interfaces.Models;

namespace TTHandiCrafts.Infrastructure.Interfaces.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Email emailMessage, CancellationToken cancellationToken = default);
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}