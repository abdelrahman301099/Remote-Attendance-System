

namespace NetBlaze.Domain.Entities.Views
{
    public class UserPolicyDTO
    {
        //from Attendance Entity
        public long UserId { get; set; }

        public DateTime DayDate { get; set; }

        public TimeOnly CheckIn { get; set; }

        //from CompanyPloicy Entity
        public int PolicyId { get; set; }

        public string PolicyName { get; set; }

        public double Action { get; set; }


    }
}
