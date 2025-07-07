using Microsoft.AspNetCore.Components;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.Ui.Client.Services.CommonServices;
using System.Globalization;

namespace NetBlaze.Ui.Client.Components.Concrete.CultureSelector
{
    public partial class CultureSelector
    {
        [Inject] private CookieService CookieService { get; set; } = null!;

        private bool IsRequestedLanguageEnglish { get; set; } = CultureInfo.CurrentCulture.Name == LanguageCode.ENGLISH_CODE;

        private async Task ToggleLanguageAsync()
        {
            IsRequestedLanguageEnglish = !IsRequestedLanguageEnglish;

            var newCulture = IsRequestedLanguageEnglish
                ? new CultureInfo(LanguageCode.ENGLISH_CODE)
                : new CultureInfo(LanguageCode.ARABIC_CODE);

            if (CultureInfo.CurrentCulture != newCulture)
            {
                await CookieService.SetCookieAsync(MiscConstants.currentCultureCode, newCulture.Name, 365);

                NavigationManager.Refresh(true);
            }
        }
    }
}
