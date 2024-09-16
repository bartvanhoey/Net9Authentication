using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.ApiKeys;

public class CreateApiKeyDto
{
    [Required] public string Purpose { get; set; } = "";
    [Required] [StringLength(50)] public string ApplicationName { get; set; } = "";
    [StringLength(50)] public string? InternalCompany { get; set; }
    [StringLength(50)] public string? ExternalCompany { get; set; }
    [EmailAddress] public string? CreatedBy { get; set; }
}