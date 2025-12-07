using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.SharedKernel.SharedResources;
using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.LogIn.RequestDTOs
{
    public class LogInRequestDTO
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.MediumText, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NameLength))]
        [RegularExpression(RegexTemplate.Email)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.MediumText, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NameLength))]
        public string Password { get; set; } = null!;

       
    }
}
