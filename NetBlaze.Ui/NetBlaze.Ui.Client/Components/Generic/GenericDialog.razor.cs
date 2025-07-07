using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace NetBlaze.Ui.Client.Components.Generic
{
    public partial class GenericDialog
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; } = null!;

        [Parameter]
        public string Title { get; set; } = null!;

        [Parameter]
        public string Content { get; set; } = null!;

        [Parameter]
        public string CancelText { get; set; } = null!;

        [Parameter]
        public string SubmitButtonStartIcon { get; set; } = string.Empty;

        [Parameter]
        public Color SubmitButtonColor { get; set; } = Color.Error;

        [Parameter]
        public string SubmitText { get; set; } = null!;

        private void Submit() => MudDialog.Close(DialogResult.Ok(true));

        private void Cancel() => MudDialog.Cancel();
    }
}
