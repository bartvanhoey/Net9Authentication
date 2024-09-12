using System.Net.Http.Json;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.ApiKeys;
using static Net9Auth.Shared.Infrastructure.Functional.Errors.ResultErrorFactory;
using static Net9Auth.Shared.Infrastructure.Functional.Result;

namespace Net9Auth.BlazorWasm.Services.ApiKeys;

public class ApiKeyService(IHttpClientFactory clientFactory) : IApiKeyService
{
    private readonly HttpClient _http = clientFactory.CreateClient("ServerAPI");
   
    
    public Task<Result<ApiKeyDto>> CreateAsync(CreateApiKeyDto createDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(Guid id, UpdateApiKeyDto input)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedResultDto<ApiKeyDto>?>> GetListAsync(GetApiKeyListDto dto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/api-key", dto);
            return Ok(await response.Content.ReadFromJsonAsync<PagedResultDto<ApiKeyDto>>());
        }
        catch (Exception exception)
        {
            return Fail<PagedResultDto<ApiKeyDto>?>(new BasicResultError(""));
        }
    }

    public Task<Result> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RevokeAsync(Guid id, string revokeReason)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ApiKeyDto?>> GetAsync(Guid id)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/api-key/by-id", new GetApiKeyModel(){Id = id});
            var result = await response.Content.ReadFromJsonAsync<GetApiKeyByIdResultDto>();
            if (result == null) Fail<ApiKeyDto?>(ResponseIsNull());
            return result is { IsSuccess: true } ? Ok(result.ApiKeyDto) : Fail<ApiKeyDto?>(BasicError(result?.ErrorMessage ?? ""));
        }
        catch (Exception exception)
        {
            return Fail<ApiKeyDto?>(exception);
        }
    }
}