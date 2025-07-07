using Microsoft.AspNetCore.Components;
using MudBlazor;
using NetBlaze.SharedKernel.Dtos.Sample.Responses;
using NetBlaze.Ui.Client.Components.Generic;
using NetBlaze.Ui.Client.Dialogs.Sample;
using NetBlaze.Ui.Client.Services;

namespace NetBlaze.Ui.Client.Components.Concrete.Sample
{
    public partial class SampleCrudOperationsComponent
    {
        [Inject] IDialogService DialogService { get; set; } = null!;

        [Inject] BlazSampleService BlazSampleService { get; set; } = null!;

        private List<GetListedSampleResponseDto> _samples = [];

        private CancellationTokenSource cts = new();

        private bool _isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadSamplesAsync();
        }

        private async Task LoadSamplesAsync()
        {
            _isLoading = true;
            StateHasChanged();

            _samples.Clear();

            await foreach (var sample in BlazSampleService.GetListedSample(cts.Token))
            {
                _samples.Add(sample);

                await InvokeAsync(StateHasChanged);
            }

            _isLoading = false;
            StateHasChanged();
        }

        private async Task OpenAddDialogAsync()
        {
            var parameters = new DialogParameters();

            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Large,
                FullWidth = true
            };

            var dialog = await DialogService.ShowAsync<AddSampleDialog>("Add New Sample", parameters, options);

            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await LoadSamplesAsync();
            }
        }

        private async Task OpenUpdateDialogAsync(GetListedSampleResponseDto sample)
        {
            var fullSampleResponse = await BlazSampleService.GetSampleAsync(sample.Id);

            if (!fullSampleResponse.Success)
            {
                return;
            }

            var parameters = new DialogParameters<UpdateSampleDialog>
            {
                { x => x.GetSampleResponseDto, fullSampleResponse.Data }
            };

            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Large,
                FullWidth = true
            };

            var dialog = await DialogService.ShowAsync<UpdateSampleDialog>("Update Existing Sample", parameters, options);

            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await LoadSamplesAsync();
            }
        }

        private async Task DeleteSampleAsync(long id)
        {
            var parameters = new DialogParameters<GenericDialog>
            {
                { x => x.Title, "Delete Existing Sample"},
                { x => x.Content, "Are you sure you want to delete this sample? This action cannot be undone." },
                { x => x.CancelText, "Cancel" },
                { x => x.SubmitText, "Delete" }
            };

            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };
            var dialog = await DialogService.ShowAsync<GenericDialog>(string.Empty, parameters, options);

            var result = await dialog.Result;

            if (!result.Canceled)
            {
                _isLoading = true;
                StateHasChanged();

                var response = await BlazSampleService.DeleteSampleAsync(id);

                if (response.Success)
                {
                    await LoadSamplesAsync();
                }

                _isLoading = false;
                StateHasChanged();
            }
        }
    }
}
