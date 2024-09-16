using System.Net.Http.Json;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.ApiKeys;
using static Net9Auth.Shared.Infrastructure.Functional.Errors.ResultErrorFactory;
using static Net9Auth.Shared.Infrastructure.Functional.Result;

namespace Net9Auth.BlazorWasm.Services.Administration.ApiKeys;

public class ApiKeyService(IHttpClientFactory clientFactory) : IApiKeyService
{
    private readonly HttpClient _http = clientFactory.CreateClient("ServerAPI");
   
    
    public async Task<Result<ApiKeyDto?>> CreateAsync(CreateApiKeyCtrlInput input)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/api-key/create", input);

            var result = await response.Content.ReadFromJsonAsync<CreateApiKeyCtrlResult>();
            
            return result is { IsSuccess: true }
                ? Ok(result.ApiKeyDto) : Fail<ApiKeyDto?>(result?.ErrorMessage);
        }
        catch (Exception exception)
        {
            return Fail<ApiKeyDto?>(exception);
        }
    }

    public Task<Result> UpdateAsync(Guid id, UpdateApiKeyCtrlInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedResultDto<ApiKeyDto>?>> GetListAsync(GetApiKeyListCtrlInput input)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/api-key", input);
            return Ok(await response.Content.ReadFromJsonAsync<PagedResultDto<ApiKeyDto>>());
        }
        catch (Exception exception)
        {
            return Fail<PagedResultDto<ApiKeyDto>?>(exception);
        }
    }

    public Task<Result> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RevokeAsync(RevokeApiKeyCtrlInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ApiKeyDto?>> GetAsync(Guid id)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/api-key/by-id", new GetApiKeyCtrlInput {Id = id});
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