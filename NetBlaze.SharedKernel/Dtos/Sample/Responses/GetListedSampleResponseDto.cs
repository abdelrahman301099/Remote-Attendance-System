using NetBlaze.SharedKernel.Enums;

namespace NetBlaze.SharedKernel.Dtos.Sample.Responses
{
    public sealed record GetListedSampleResponseDto(
        long Id,
        string Name,
        SampleStatus Status
    );
}
