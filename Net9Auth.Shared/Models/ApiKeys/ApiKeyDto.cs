namespace Net9Auth.Shared.Models.ApiKeys;

public class ApiKeyDto
{
    public Guid Id { get; set; }
    public required string Key { get; set; }
    public required string Purpose { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? RevokedBy { get; set; }
    public string? RevokeReason { get; set; }
    public bool IsDeleted { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public string? DeleteReason { get; set; }
}