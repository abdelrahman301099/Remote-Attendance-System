using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IFido2Service
    {
        Task<ApiResponse<bool>> BeginRegistrationAsync(string email, CancellationToken cancellationToken = default);

        Task<ApiResponse<bool>> CompleteRegistrationAsync(string email, string attestationJson, CancellationToken cancellationToken = default);

        Task<ApiResponse<bool>> BeginAssertionAsync(string email, CancellationToken cancellationToken = default);

        Task<ApiResponse<bool>> ValidateAssertionForUserAsync(string email, string assertionJson, CancellationToken cancellationToken = default);
    }
}
