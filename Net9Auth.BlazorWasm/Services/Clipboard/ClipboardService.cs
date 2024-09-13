using Microsoft.JSInterop;

namespace Net9Auth.BlazorWasm.Services.Clipboard;

public class ClipboardService(IJSRuntime jsRuntime) : IClipboardService
{
    public async Task CopyToClipboard(string text) 
        => await jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
}