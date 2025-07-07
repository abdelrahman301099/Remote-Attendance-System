using NetBlaze.SharedKernel.HelperUtilities.Constants;

namespace NetBlaze.Ui.Client.Services.CommonServices
{
    public class HttpRequestHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name == LanguageCode.ARABIC_CODE ? LanguageCode.ARABIC_CODE : LanguageCode.ENGLISH_CODE;

            request.Headers.AcceptLanguage.Clear();

            request.Headers.AcceptLanguage.Add(new(currentCulture));

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
