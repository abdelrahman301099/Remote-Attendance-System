using NetBlaze.Domain.Entities.Identity;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IEmailSender
    {
        Task SendPasswordResetCodeAsync(string email, string code, CancellationToken cancellationToken = default);
    }
}
