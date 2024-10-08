﻿using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;

namespace Net9Auth.BlazorWasm.Services.Administration.AggregatedLogging.ExceptionLogging;

public interface IExceptionLogService
{
    Task<Result<ExceptionLogDto?>> CreateAsync(CreateExceptionLogDto dto);
    Task<Result<PagedResultDto<ExceptionLogDto>?>> GetListAsync(GetExceptionLogListDto dto);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<ExceptionLogDto?>> GetAsync(Guid id);
}