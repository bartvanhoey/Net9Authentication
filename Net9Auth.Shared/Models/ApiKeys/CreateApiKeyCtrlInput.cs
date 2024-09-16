using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.ApiKeys;

public class CreateApiKeyCtrlInput
{
    [Required] public string Purpose { get; set; } = "";
    [Required] [StringLength(50)] public  string ApplicationName { get; set; }
    [StringLength(50)] public string? InternalCompany { get; set; }
    [StringLength(50)] public string? ExternalCompany { get; set; }
    
}