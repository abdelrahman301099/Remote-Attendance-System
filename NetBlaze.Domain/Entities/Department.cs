

using NetBlaze.Domain.Common;
using NetBlaze.Domain.Entities.Identity;

namespace NetBlaze.Domain.Entities
{
    public class Department:BaseEntity<int>
    {
        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();


    }
}
