﻿using Microsoft.AspNetCore.Components;
using Net9Auth.BlazorWasm.Services.ApiKeys;
using Net9Auth.BlazorWasm.Services.Clipboard;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.BlazorWasm.Pages.ApiKeys;

public partial class AddApiKey : ComponentBase
{
    [Parameter] public EventCallback<ApiKeyDto> OnApiKeyCreated { get; set; }
    [Inject] IApiKeyService? ApiKeyService { get; set; }
    [Inject] IClipboardService? ClipboardService { get; set; }

    [SupplyParameterFromForm] private CreateApiKeyCtrlInput? Model { get; set; }

    public string? ApiKey { get; set; }

    protected override void OnInitialized() =>
        Model ??= new();

    private async Task CreateApiKeyAsync()
    {
        if (Model != null && ApiKeyService != null)
        {
            var result = await ApiKeyService.CreateAsync(Model);
            if (result.IsSuccess)
            {
                await OnApiKeyCreated.InvokeAsync(result.Value);
                ApiKey = result.Value?.Key;
            }
        }
    }


    private async Task CopyApiKey()
    {
        if (ApiKey != null && ClipboardService != null)
            await ClipboardService.CopyToClipboard(ApiKey);
    }
}