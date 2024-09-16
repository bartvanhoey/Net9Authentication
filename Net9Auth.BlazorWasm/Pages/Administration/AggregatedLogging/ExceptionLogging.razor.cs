using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Net9Auth.BlazorWasm.Services.Administration.AggregatedLogging.ExceptionLogging;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;
using static System.Array;




namespace Net9Auth.BlazorWasm.Pages.Administration.AggregatedLogging;

public partial class ExceptionLogging : ComponentBase
{
    private readonly PaginationState? _pagination = new() { ItemsPerPage = 10 };
    private GridItemsProvider<ExceptionLogDto>? _exceptionLogsProvider;
    [Inject] protected NavigationManager? NavigationManager { get; set; }
    [Inject] protected IExceptionLogService? ExceptionLogService { get; set; }

    private long _numResults = 0;
    protected bool ShowSpinner { get; set; }
    public string ExceptionLog { get; set; } = String.Empty;
    public string Message { get; set; } = String.Empty;

    protected override Task OnInitializedAsync() => LoadExceptionLogsAsync();

    private Task LoadExceptionLogsAsync()
    {
        _exceptionLogsProvider = async req =>
        {
            var result = await GetExceptionLogsAsync();
            if (result.IsFailure) return GridItemsProviderResult.From(Empty<ExceptionLogDto>(), 0);


            if (result.Value?.Items == null || result.Value.Items.Count == 0)
            {
                if (result.Value != null) _numResults = result.Value.TotalCount;
                return GridItemsProviderResult.From(Empty<ExceptionLogDto>(), 0);
            }

            StateHasChanged();

            return GridItemsProviderResult.From(
                result.Value.Items.Skip(req.StartIndex).Take(10).ToList(),
                result.Value.Items.Count);
        };
        return Task.CompletedTask;
    }

    private async Task ExceptionLogCreated_Handler(ExceptionLogDto exceptionLogDto)
    {
        // ExceptionLog = exceptionLogDto.Key;
        
        await LoadExceptionLogsAsync();
    }


    private async Task<Result<PagedResultDto<ExceptionLogDto>?>> GetExceptionLogsAsync()
    {
        Result<PagedResultDto<ExceptionLogDto>?> ret;
        if (ExceptionLogService == null) ret = Result.Fail<PagedResultDto<ExceptionLogDto>?>(ResultErrorFactory.ApiServiceIsNull(""));
        else
        {
            var getListResult = await ExceptionLogService.GetListAsync(new GetExceptionLogListCtrlInput());
            ret = getListResult.IsSuccess ? getListResult : Result.Fail<PagedResultDto<ExceptionLogDto>?>(getListResult.Error);
        }

        return ret;
    }

    private void GotoDetail(ExceptionLogDto user) => NavigationManager?.NavigateTo($"admin/exception-log/detail/{user.Id}", true);

    private async Task CopyToClipboard(ExceptionLogDto context)
    {
        await Task.CompletedTask;
    }
    
}