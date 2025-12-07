using NetBlaze.Domain.Common;

namespace NetBlaze.Domain.Entities
{
    public class Vacations:BaseEntity<int>
    { 
        public string DayName { get; set; }

        public DateTime DayDate { get; set; }

        public int VacationDuration { get; set; }

        public string Clarification { get; set; }

    }
}
