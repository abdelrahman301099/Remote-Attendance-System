using NetBlaze.SharedKernel.HelperUtilities.Constants;
using System.ComponentModel.DataAnnotations;

namespace NetBlaze.Domain.Common
{
    public abstract class BaseAuditableEntity
    {
        public DateTimeOffset CreatedAt { get; private set; }

        [MaxLength(CommonStringLength.LongContentText)]
        public string? CreatedBy { get; private set; }

        public DateTimeOffset? LastModifiedAt { get; private set; }

        [MaxLength(CommonStringLength.LongContentText)]
        public string? LastModifiedBy { get; private set; }

        public DateTimeOffset? DeletedAt { get; private set; }

        [MaxLength(CommonStringLength.LongContentText)]
        public string? DeletedBy { get; private set; }
    }
}