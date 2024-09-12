using Net9Auth.Shared.Infrastructure.Models;

namespace Net9Auth.Shared.Models.ApiKeys;

public class GetApiKeyListDto : PagedRequestDto
{
    public GetApiKeyListDto(string? purpose = null) => Purpose = purpose;
    public string? Purpose { get; set; }
}