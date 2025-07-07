using MudBlazor;
using MudBlazor.Utilities;

namespace NetBlaze.Ui.Client.InternalHelperTypes.Constants
{
    public static class SharedTheme
    {
        // Light Theme Colors:
        static readonly MudColor NetBlazePrimary = new("#5B2A86");
        static readonly MudColor NetBlazeSecondary = new("#7785AC");
        static readonly MudColor NetBlazeTertiary = new("#108a00");
        static readonly MudColor NetBlazeWarning = new("#ec6313");
        static readonly MudColor NetBlazeError = new("#f44336");
        static readonly MudColor NetBlazebarBackground = new("#f3ecf9");
        static readonly MudColor NetBlazeSucNetBlazesText = new("#478c5c");
        static readonly MudColor NetBlazeDrawerBackground = new("#f3ecf9");
        static readonly MudColor NetBlazeTextParagraph = new("#000000");


        // Light Theme Object:
        public static readonly MudTheme NetBlazeTheme = new()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = NetBlazePrimary,
                Secondary = NetBlazeSecondary,
                Success = NetBlazeSucNetBlazesText,
                Error = NetBlazeError,
                Warning = NetBlazeWarning,
                Tertiary = NetBlazeTertiary,
                TextPrimary = NetBlazeTextParagraph,
                TextSecondary = NetBlazeSecondary,
                AppbarBackground = NetBlazebarBackground,
                DrawerBackground = NetBlazeDrawerBackground,
                DrawerText = NetBlazeSecondary,
                HoverOpacity = 0.06,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Blue.Lighten1
            },
            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px"
            }
        };
    }
}
