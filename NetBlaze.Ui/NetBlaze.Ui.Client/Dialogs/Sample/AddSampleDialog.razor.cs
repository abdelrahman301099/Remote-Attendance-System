using Microsoft.AspNetCore.Components;
using MudBlazor;
using NetBlaze.SharedKernel.Dtos.Sample.Requests;
using NetBlaze.Ui.Client.Services;

namespace NetBlaze.Ui.Client.Dialogs.Sample
{
    public partial class AddSampleDialog
    {
        [Inject] BlazSampleService BlazSampleService { get; set; } = null!;

        [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = null!;

        private AddSampleRequestDto addSampleRequestDto = new();

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SubmitAsync()
        {
            var response = await BlazSampleService.AddSampleAsync(addSampleRequestDto);

            if (response.Success)
            {
                MudDialog.Close(DialogResult.Ok(addSampleRequestDto));
            }
        }
    }
}
