using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.Vacations.Request
{
    public class DeleteVacationRequestDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
