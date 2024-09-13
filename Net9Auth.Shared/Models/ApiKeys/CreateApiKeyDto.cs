namespace Net9Auth.Shared.Models.ApiKeys;

public class CreateApiKeyDto
{
    public string Purpose { get; set; } = "";
    public string CreatedBy { get; set; } = "";
}