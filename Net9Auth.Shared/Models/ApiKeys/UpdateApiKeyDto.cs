using System.ComponentModel.DataAnnotations;
using Net9Auth.Shared.Infrastructure.Models;

namespace Net9Auth.Shared.Models.ApiKeys;

public class UpdateApiKeyDto
{
    public Guid Id { get; set; }
    [Required] public bool Revoked { get; set; }
    [Required] [DataType(DataType.Date)] public DateTime RevokedAt { get; set; }
    [EmailAddress] [Required] public string? RevokedBy { get; set; }
    [Required] public string? RevokedReason { get; set; }
}

public class CreateApiKeyDto
{
    [Required] public string Purpose { get; set; } = "";
    [Required] [DataType(DataType.Date)] public DateTime CreatedAt { get; set; }
    [EmailAddress] [Required] public string? CreatedBy { get; set; }
}

public class GetApiKeyListDto : PagedRequestDto
{
}

public class ApiKeyDto
{
    public Guid Id { get; set; }
    public required string Key { get; set; }
    public required string Purpose { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Revoked { get; set; }
    public DateTime RevokedAt { get; set; }
    public string? RevokedBy { get; set; }
    public string? RevokedReason { get; set; }
}

public interface IHaveISuccessAndErrorMessage
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}

public class ApiKeyCreatedDto : IHaveISuccessAndErrorMessage
{
    public ApiKeyDto? ApiKeyDto { get; }

    public ApiKeyCreatedDto(ApiKeyDto? apiKeyDto)
    {
        ApiKeyDto = apiKeyDto;
        IsSuccess = true;
    }

    public ApiKeyCreatedDto(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}