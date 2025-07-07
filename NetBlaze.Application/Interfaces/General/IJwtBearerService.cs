using NetBlaze.SharedKernel.Dtos.General;

namespace NetBlaze.Application.Interfaces.General
{
    public interface IJwtBearerService
    {
        string GenerateToken(GenerateTokenRequestDto generateTokenRequestDto);

        Task<GetTokenValidationResultResponseDto> ValidateTokenForUserAsync(string? bearerToken);
    }
}
