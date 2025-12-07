using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.Vacations.Request
{
    public class UpdateVacationRequestDTO
    {
        [Required]
        public int Id { get; set; }

        public string? DayName { get; set; }

        public DateTime? DayDate { get; set; }

        public int? VacationDuration { get; set; }

        public string? Clarification { get; set; }
    }
}
