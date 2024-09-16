namespace Net9Auth.Shared.Models.ApiKeys;

public class CreateApiKeyDto
{
    public string Purpose { get; set; } = "";
    public string CreatedBy { get; set; } = "";

    public required string ApplicationName { get; set; }
    public string? InternalCompany { get; set; }
    public string? ExternalCompany { get; set; }
}