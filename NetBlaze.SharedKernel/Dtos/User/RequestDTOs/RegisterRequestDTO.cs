using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.SharedKernel.SharedResources;
using System.ComponentModel.DataAnnotations;


namespace NetBlaze.SharedKernel.Dtos.LogIn.RequestDTOs
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.MediumText)]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.MediumText, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NameLength))]
        [RegularExpression(RegexTemplate.Email)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.MediumText)]
        [RegularExpression(RegexTemplate.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.MediumText)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.LongText, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NameLength))]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        public int? RoleId { get; set; }

    }
}
