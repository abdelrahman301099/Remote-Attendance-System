using Microsoft.AspNetCore.Components;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using System.Globalization;

namespace NetBlaze.Ui.SharedRazor
{
    public partial class App
    {
        [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (HttpContext != null)
            {
                var currentLanguage = HttpContext.Request.Cookies[MiscConstants.currentCultureCode];

                if (!string.IsNullOrWhiteSpace(currentLanguage))
                {
                    CultureInfo.CurrentCulture = new CultureInfo(currentLanguage);
                    CultureInfo.CurrentUICulture = new CultureInfo(currentLanguage);
                }
            }
        }
    }
}
