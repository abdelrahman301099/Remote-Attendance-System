using Microsoft.AspNetCore.Components;
using MudBlazor;
using NetBlaze.SharedKernel.Dtos.Sample.Requests;
using NetBlaze.SharedKernel.Dtos.Sample.Responses;
using NetBlaze.Ui.Client.Services;

namespace NetBlaze.Ui.Client.Dialogs.Sample
{
    public partial class UpdateSampleDialog
    {
        [Inject] BlazSampleService BlazSampleService { get; set; } = null!;

        [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = null!;

        [Parameter] public GetSampleResponseDto GetSampleResponseDto { get; set; } = null!;

        private UpdateSampleRequestDto updateSampleRequestDto = new();

        protected override void OnInitialized()
        {
            if (GetSampleResponseDto != null)
            {
                updateSampleRequestDto = new UpdateSampleRequestDto(
                    GetSampleResponseDto.Id,
                    GetSampleResponseDto.IntegerValue,
                    GetSampleResponseDto.DecimalValue,
                    GetSampleResponseDto.Name,
                    GetSampleResponseDto.Description,
                    GetSampleResponseDto.StartDate,
                    GetSampleResponseDto.UniqueIdentifier,
                    GetSampleResponseDto.Status
                );
            }
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SubmitAsync()
        {
            var response = await BlazSampleService.UpdateSampleAsync(updateSampleRequestDto);

            if (response.Success)
            {
                MudDialog.Close(DialogResult.Ok(updateSampleRequestDto));
            }
        }
    }
}
