using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.ApiKeys;

public class CreateApiKeyDto
{
    [Required] public string Purpose { get; set; } = "";
    [Required] [DataType(DataType.Date)] public DateTime CreatedAt { get; set; }
    [EmailAddress] [Required] public string? CreatedBy { get; set; }
}