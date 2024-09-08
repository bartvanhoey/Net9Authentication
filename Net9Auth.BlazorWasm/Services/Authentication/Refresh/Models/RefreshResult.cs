﻿namespace Net9Auth.BlazorWasm.Services.Authentication.Refresh.Models;

public class RefreshResult : IResponseContentResult
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime ValidTo { get; set; }
    public bool Successful{ get; set; }
    public string? Status { get; set; }
    public string? Message { get; set; 
    }

    public IEnumerable<HttpResultError>? Errors { get; set; }
}