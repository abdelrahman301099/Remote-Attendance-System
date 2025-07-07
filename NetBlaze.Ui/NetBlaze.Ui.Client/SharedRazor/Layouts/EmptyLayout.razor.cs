using MudBlazor;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.Ui.Client.InternalHelperTypes.Constants;
using System.Globalization;

namespace NetBlaze.Ui.Client.SharedRazor.Layouts
{
    public partial class EmptyLayout
    {
        private MudTheme NetBlazeTheme { get; init; } = SharedTheme.NetBlazeTheme;

        private readonly bool _rightToLeft = CultureInfo.CurrentCulture.Name == LanguageCode.ARABIC_CODE;
    }
}
