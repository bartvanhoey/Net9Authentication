using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.ApiKeys;

public class UpdateApiKeyCtrlInput
{
    public Guid Id { get; set; }
    [Required] public bool Revoked { get; set; }
    [Required] [DataType(DataType.Date)] public DateTime RevokedAt { get; set; }
    [EmailAddress] [Required] public string? RevokedBy { get; set; }
    [Required] public string? RevokedReason { get; set; }
}

public class RevokeApiKeyCtrlInput
{
    public Guid Id { get; set; }
    [Required] public string? RevokedReason { get; set; }
}