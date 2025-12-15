using NetBlaze.SharedKernel.SharedResources;
using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.Department.Request
{
    public class CreateDepartmentRequestDTO
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        public string DepartmentName { get; set; } = null!;

        public string? Description { get; set; }
    }
}
