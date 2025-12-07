using NetBlaze.SharedKernel.HelperUtilities.Constants;
using System.ComponentModel.DataAnnotations;

namespace NetBlaze.Domain.Common
{
    public abstract class BaseAuditableEntity
    {
        public DateTimeOffset CreatedAt { get;  set; }

        [MaxLength(CommonStringLength.LongContentText)]
        public string? CreatedBy { get;  set; }

        public DateTimeOffset? LastModifiedAt { get;  set; }

        [MaxLength(CommonStringLength.LongContentText)]
        public string? LastModifiedBy { get;  set; }

        public DateTimeOffset? DeletedAt { get;  set; }

        [MaxLength(CommonStringLength.LongContentText)]
        public string? DeletedBy { get;  set; }
    }
}