namespace NetBlaze.Ui.Client.Services.CommonServices
{
    public class BaseBlazService
    {
        protected readonly ExternalHttpClientWrapper _externalHttpClientWrapper;

        protected readonly CentralizedSnackbarProvider _centralizedSnackbarProvider;

        public BaseBlazService(ExternalHttpClientWrapper externalHttpClientWrapper, CentralizedSnackbarProvider centralizedSnackbarProvider)
        {
            _externalHttpClientWrapper = externalHttpClientWrapper;
            _centralizedSnackbarProvider = centralizedSnackbarProvider;
        }
    }
}
