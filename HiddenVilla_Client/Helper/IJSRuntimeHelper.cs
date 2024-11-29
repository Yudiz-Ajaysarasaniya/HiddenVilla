using Microsoft.JSInterop;

namespace HiddenVilla_Client.Helper
{
    public static class IJSRuntimeHelper
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }

        public static async ValueTask ToastrError(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }
        public static async ValueTask Success(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("SuccessToast", "success", message);
        }
    }
}
