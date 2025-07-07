namespace NetBlaze.SharedKernel.Dtos.General
{
    public sealed record GenerateTokenRequestDto(
        long UserId,
        string UserName,
        string Email,
        List<string> Roles
    );
}
