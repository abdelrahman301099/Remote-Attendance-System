namespace NetBlaze.SharedKernel.HelperUtilities.General
{
    public sealed record JwtSettings
    {
        public string Key { get; init; } = null!;

        public string Issuer { get; init; } = null!;

        public string Audience { get; init; } = null!;

        public double ExpiryInDays { get; init; }
    }
}
