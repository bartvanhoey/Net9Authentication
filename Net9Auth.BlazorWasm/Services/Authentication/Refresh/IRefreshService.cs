﻿using Net9Auth.BlazorWasm.Services.Authentication.Refresh.Models;

namespace Net9Auth.BlazorWasm.Services.Authentication.Refresh;

public interface IRefreshService
{
    Task<AuthRefreshResult> RefreshAsync();
}