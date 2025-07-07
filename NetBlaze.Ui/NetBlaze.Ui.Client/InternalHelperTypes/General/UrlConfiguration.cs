namespace NetBlaze.Ui.Client.InternalHelperTypes.General
{
    public sealed record UrlConfiguration
    {
        public string ApiBaseUrl { get; init; } = null!;

        public string UiBaseUrl { get; init; } = null!;
    }
}
