using Net9Auth.Shared.Infrastructure.Models;

namespace Net9Auth.Shared.Models.ApiKeys;

public class GetApiKeyListCtrlInput : PagedRequestDto
{
    public GetApiKeyListCtrlInput(string? purpose = null) => Purpose = purpose;
    public string? Purpose { get; set; }
}