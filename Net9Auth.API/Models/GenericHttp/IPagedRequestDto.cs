namespace Net9Auth.API.Models.GenericHttp;

public interface IPagedRequestDto
{
    int SkipCount { get; set; }
    int MaxResultCount { get; set; } 
    string? Sorting { get; set; }
    
}