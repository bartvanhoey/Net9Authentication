namespace Net9Auth.BlazorWasm.Services.Clipboard;

public interface IClipboardService 

{
    Task CopyToClipboard(string text);
}