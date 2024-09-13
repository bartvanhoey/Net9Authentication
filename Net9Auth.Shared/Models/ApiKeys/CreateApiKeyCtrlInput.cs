using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.ApiKeys;

public class CreateApiKeyCtrlInput
{
    [Required] public string Purpose { get; set; } = "";
    
}