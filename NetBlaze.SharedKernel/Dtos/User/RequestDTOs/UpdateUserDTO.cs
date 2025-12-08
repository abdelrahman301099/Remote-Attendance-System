using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.SharedKernel.SharedResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.SharedKernel.Dtos.User.RequestDTOs
{
    public class UpdateUserDTO
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        public string? UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.MediumText)]
        public string? UserName { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.MediumText)]
        public string? Displayname { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [RegularExpression(RegexTemplate.Email)]
        public string? UserEmail { get; set; }
    }
}
