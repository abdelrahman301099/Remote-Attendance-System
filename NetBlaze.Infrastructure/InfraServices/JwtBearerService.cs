using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.SharedKernel.Dtos.General;
using NetBlaze.SharedKernel.HelperUtilities.General;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetBlaze.Infrastructure.InfraServices
{
    public class JwtBearerService : IJwtBearerService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly SymmetricSecurityKey _symmetricSecurityKey;

        public JwtBearerService(IConfiguration configuration)
        {
            _jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        }

        public string GenerateToken(GenerateTokenRequestDto generateTokenRequestDto)
        {
            var credentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sid, generateTokenRequestDto.UserId.ToString()),
                new(ClaimTypes.NameIdentifier, generateTokenRequestDto.UserName),
                new(ClaimTypes.Email, generateTokenRequestDto.Email),
                new(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer),
                new(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience),
                new(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddDays(_jwtSettings.ExpiryInDays).ToUnixTimeSeconds().ToString()),
            };

            generateTokenRequestDto.Roles.ForEach(x => claims.Add(new(ClaimTypes.Role, x)));

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<GetTokenValidationResultResponseDto> ValidateTokenForUserAsync(string? bearerToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = _symmetricSecurityKey,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };

            var validationResult = await tokenHandler.ValidateTokenAsync(bearerToken, validationParameters);

            if (validationResult.IsValid)
            {
                return new GetTokenValidationResultResponseDto(true);
            }

            return new GetTokenValidationResultResponseDto(false);
        }
    }
}
