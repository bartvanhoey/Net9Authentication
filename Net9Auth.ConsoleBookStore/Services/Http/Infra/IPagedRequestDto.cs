namespace Net9Auth.ConsoleBookStore.Services.Http.Infra;

public interface IPagedRequestDto
{
    int SkipCount { get; set; }
    int MaxResultCount { get; set; } 
    string? Sorting { get; set; }
    
}