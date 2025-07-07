namespace NetBlaze.SharedKernel.HelperUtilities.Constants
{
    public static class ApiRelativePaths
    {
        // Common Api Path
        public const string API_COMMON_PREFIX = "/api";

        // Sample Paths
        public const string SAMPLE_BASE = $"{API_COMMON_PREFIX}/sample";
        public const string SAMPLE_LIST = $"{SAMPLE_BASE}/list";
        public const string SAMPLE_GET = SAMPLE_BASE;
        public const string SAMPLE_ADD = $"{SAMPLE_BASE}/add";
        public const string SAMPLE_UPDATE = $"{SAMPLE_BASE}/update";
        public const string SAMPLE_DELETE = $"{SAMPLE_BASE}/delete";
    }
}