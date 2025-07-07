using MudBlazor;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Ui.Client.Services.CommonServices
{
    public class CentralizedSnackbarProvider
    {
        private readonly ISnackbar _snackbar;

        public CentralizedSnackbarProvider(ISnackbar snackbar) => _snackbar = snackbar;

        public void ShowApiResponseSnackbar<TResponseDto>(ApiResponse<TResponseDto> response)
        {
            var severity = response.Success ? Severity.Success : Severity.Error;

            _snackbar.Add(response.Message, severity, config =>
            {
                config.ShowCloseIcon = true;
                config.VisibleStateDuration = 4000;
                config.ShowTransitionDuration = 500;
                config.HideTransitionDuration = 500;
                config.DuplicatesBehavior = SnackbarDuplicatesBehavior.Allow;
            });
        }

        public void ShowNormalSnackbar(string message, Severity severity)
        {
            _snackbar.Add(message, severity, config =>
            {
                config.ShowCloseIcon = true;
                config.VisibleStateDuration = 4000;
                config.ShowTransitionDuration = 500;
                config.HideTransitionDuration = 500;
                config.DuplicatesBehavior = SnackbarDuplicatesBehavior.Allow;
            });
        }
    }
}
