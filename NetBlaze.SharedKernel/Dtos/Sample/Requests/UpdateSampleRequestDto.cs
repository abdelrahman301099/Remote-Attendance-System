using NetBlaze.SharedKernel.Enums;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetBlaze.SharedKernel.Dtos.Sample.Requests
{
    public sealed record UpdateSampleRequestDto
    {
        [IgnoreReflectionMapping]
        public long Id { get; set; }

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

        public DateOnly StartDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        public Guid UniqueIdentifier { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.FieldRequired))]
        public SampleStatus Status { get; set; }

        public UpdateSampleRequestDto() { }

        [JsonConstructor]
        public UpdateSampleRequestDto(
            long id,
            int integerValue,
            decimal decimalValue,
            string name,
            string description,
            DateOnly startDate,
            Guid uniqueIdentifier,
            SampleStatus status
        )
        {
            Id = id;
            IntegerValue = integerValue;
            DecimalValue = decimalValue;
            Name = name;
            Description = description;
            StartDate = startDate;
            UniqueIdentifier = uniqueIdentifier;
            Status = status;
        }
    }
}
