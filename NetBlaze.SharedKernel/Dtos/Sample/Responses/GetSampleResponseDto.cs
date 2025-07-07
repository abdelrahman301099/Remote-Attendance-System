using NetBlaze.SharedKernel.Enums;

namespace NetBlaze.SharedKernel.Dtos.Sample.Responses
{
    public sealed record GetSampleResponseDto(
        long Id,
        int IntegerValue,
        decimal DecimalValue,
        string Name,
        string Description,
        DateOnly StartDate,
        Guid UniqueIdentifier,
        SampleStatus Status
    );
}
