using System.Text.Json;

namespace NetBlaze.SharedKernel.HelperUtilities.General
{
    public static class CustomJsonSerializerOptions
    {
        public static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }
}