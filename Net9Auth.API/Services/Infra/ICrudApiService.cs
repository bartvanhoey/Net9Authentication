
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Models;

namespace Net9Auth.API.Services.Infra;

public interface ICrudApiService<T, in TC, in TU, in TL>
{
    Task<Result<T>> GetAsync(Guid id);
    
    Task<Result<PagedResultDto<T>>> GetListAsync(TL input);
    
    Task<Result<T>> CreateAsync(TC createDto);
    
    Task<Result> UpdateAsync(Guid id, TU input);
    
    Task<Result> DeleteAsync(Guid id);
}