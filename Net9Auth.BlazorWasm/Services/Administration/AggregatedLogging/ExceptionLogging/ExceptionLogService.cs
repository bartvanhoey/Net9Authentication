using System.Net.Http.Json;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;
using Net9Auth.Shared.Models.ApiKeys;
using static Net9Auth.Shared.Infrastructure.Functional.Result;

namespace Net9Auth.BlazorWasm.Services.Administration.AggregatedLogging.ExceptionLogging;

public class ExceptionLogService(IHttpClientFactory clientFactory) : IExceptionLogService
{
    private readonly HttpClient _http = clientFactory.CreateClient("ServerAPI");
    
    public Task<Result<ExceptionLogDto?>> CreateAsync(CreateExceptionLogDto input)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedResultDto<ExceptionLogDto>?>> GetListAsync(GetExceptionLogListCtrlInput input)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/exception-log", input);
            return Ok(await response.Content.ReadFromJsonAsync<PagedResultDto<ExceptionLogDto>>());
        }
        catch (Exception exception)
        {
            return Fail<PagedResultDto<ExceptionLogDto>?>(exception);
        }
    }

    public Task<Result> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ExceptionLogDto?>> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}