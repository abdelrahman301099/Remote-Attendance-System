namespace NetBlaze.SharedKernel.Dtos.Department.Response
{
    public class DepartmentResponseDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
