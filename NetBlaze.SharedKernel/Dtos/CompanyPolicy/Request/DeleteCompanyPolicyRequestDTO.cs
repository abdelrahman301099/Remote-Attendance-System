using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.CompanyPolicy.Request
{
    public class DeleteCompanyPolicyRequestDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
