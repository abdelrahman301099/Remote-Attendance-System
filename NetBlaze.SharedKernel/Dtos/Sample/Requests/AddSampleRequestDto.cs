using NetBlaze.SharedKernel.Enums;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.SharedKernel.SharedResources;
using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.Sample.Requests
{
    public sealed record AddSampleRequestDto
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        public int IntegerValue { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        public decimal DecimalValue { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.ExtendedText, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NameLength))]
        public string Name { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        [MaxLength(CommonStringLength.LongContentText, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NameLength))]
        public string Description { get; set; } = null!;

        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        public Guid UniqueIdentifier { get; set; } = Guid.NewGuid();

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        public SampleStatus Status { get; set; }
    }
}