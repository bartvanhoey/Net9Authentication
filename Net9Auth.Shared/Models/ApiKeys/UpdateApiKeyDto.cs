namespace Net9Auth.Shared.Models.ApiKeys;

public class UpdateApiKeyDto
{
    public Guid Id { get; set; }
    public bool Revoked { get; set; }
    public DateTime RevokedAt { get; set; }
    public string? RevokedBy { get; set; }
    public string? RevokedReason { get; set; }
}