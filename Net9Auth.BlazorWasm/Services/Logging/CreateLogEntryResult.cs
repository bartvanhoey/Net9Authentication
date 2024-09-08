namespace Net9Auth.BlazorWasm.Services.Logging;

public class CreateLogEntryResult(bool isSuccessFul)
{
    public CreateLogEntryResult() : this(true)
    {
    }
    public bool IsSuccessFul { get; set; } = isSuccessFul;
}