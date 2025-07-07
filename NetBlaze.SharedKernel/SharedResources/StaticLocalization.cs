namespace NetBlaze.SharedKernel.SharedResources
{
    public partial class StaticLocalization
    {
        public static string Home => ResourceManager.GetString(nameof(Home), resourceCulture)!;

        public static string Sample => ResourceManager.GetString(nameof(Sample), resourceCulture)!;

        public static string NetBlazeSolutionTemplate => ResourceManager.GetString(nameof(NetBlazeSolutionTemplate), resourceCulture)!;

        public static string AllRightsReserved => ResourceManager.GetString(nameof(AllRightsReserved), resourceCulture)!;
    }
}
