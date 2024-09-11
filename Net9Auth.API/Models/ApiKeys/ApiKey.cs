namespace Net9Auth.API.Models.ApiKeys;

public class ApiKey
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