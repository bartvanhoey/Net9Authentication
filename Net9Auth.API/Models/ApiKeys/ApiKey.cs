using System.ComponentModel.DataAnnotations;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Net9Auth.API.Models.ApiKeys;

public class ApiKey
{
    public Guid Id { get; set; }
    [Required] [StringLength(150)] public required string Key { get; set; }
    [Required] [StringLength(50)] public required string Purpose { get; set; }
    [Required] [StringLength(50)] public required string ApplicationName { get; set; }
    [StringLength(50)] public string? InternalCompany { get; set; }
    [StringLength(50)] public string? ExternalCompany { get; set; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    [Required] public required DateTime CreatedAt { get; set; }
    [Required] [StringLength(255)] [EmailAddress] public required string CreatedBy { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedAt { get; set; }
    [StringLength(255)] [EmailAddress] public string? RevokedBy { get; set; }
    [StringLength(255)] public string? RevokeReason { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    [StringLength(255)] public string? DeletedBy { get; set; }
    [StringLength(255)] public string? DeleteReason { get; set; }
}