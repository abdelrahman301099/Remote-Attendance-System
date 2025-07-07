namespace NetBlaze.SharedKernel.HelperUtilities.Constants
{
    public static class RegexTemplate
    {
        public const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public const string Password = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$";

        public const string ValidStoredProcedureName = @"^[a-zA-Z0-9_\.]+$";

        public const string ValidStoredProcedureParameterName = @"^[a-zA-Z0-9_]+$";
    }
}