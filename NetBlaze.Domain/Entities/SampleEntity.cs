using NetBlaze.Domain.Common;
using NetBlaze.SharedKernel.Enums;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Domain.Entities
{
    public class SampleEntity : BaseEntity<long>
    {
        // Properties

        public int IntegerValue { get; private set; }

        public decimal DecimalValue { get; private set; }

        public string Name { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public DateOnly StartDate { get; private set; }

        public Guid UniqueIdentifier { get; private set; }

        public SampleStatus Status { get; private set; }


        // Domain Methods

        public static SampleEntity Create<TDto>(TDto sampleEntityDto) where TDto : class
        {
            return ReflectionMapper.MapToNew<TDto, SampleEntity>(sampleEntityDto);
        }

        public void Update<TDto>(TDto sampleEntityDto) where TDto : class
        {
            this.MapToExisting(sampleEntityDto);
        }
    }
}