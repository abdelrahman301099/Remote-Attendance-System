
using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.LogIn.ResponseDTOs
{
    public class LogInResponseDTO
    {
        [Required]
        public string AccessToken { get; set; } = null!;

        public string? RefreshToken { get; set; }

        public DateTime Expiration { get; set; }

        //[Required]
        //public UserInfoDto User { get; set; } = null!;
    }
}
