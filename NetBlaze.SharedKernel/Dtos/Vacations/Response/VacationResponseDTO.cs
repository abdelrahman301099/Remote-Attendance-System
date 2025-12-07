namespace NetBlaze.SharedKernel.Dtos.Vacations.Response
{
    public class VacationResponseDTO
    {
        public int Id { get; set; }
        public string DayName { get; set; } = null!;
        public DateTime DayDate { get; set; }
        public int VacationDuration { get; set; }
        public string? Clarification { get; set; }
    }
}
