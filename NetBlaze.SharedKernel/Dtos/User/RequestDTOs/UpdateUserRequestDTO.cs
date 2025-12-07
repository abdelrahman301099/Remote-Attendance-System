using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.SharedKernel.SharedResources;
using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.LogIn.RequestDTOs
{
    public class UpdateUserRequestDTO
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        public long UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.MediumText, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NameLength))]
        [RegularExpression(RegexTemplate.Email)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.LongText, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NameLength))]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.ExtendedText, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NameLength))]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(CommonStringLength.LongText, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NameLength))]
        public string? DisplayName { get; set; }
    }
}
