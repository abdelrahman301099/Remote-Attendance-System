namespace NetBlaze.Domain.Entities.Views;

public class AttendanceReportDTO
{
    public long UserId { get; set; }

    public string UserName { get; set; } = null!;

    public DateTime Date { get; set; }

    public TimeOnly Time { get; set; }

    public string PolicyType { get; set; } = null!;

    public double PolicyAction { get; set; }


}
