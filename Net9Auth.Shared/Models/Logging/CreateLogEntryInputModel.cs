﻿namespace Net9Auth.Shared.Models.Logging;

public class CreateLogEntryInputModel(string level, string message)
{
    public string? Message { get; } = message;
    public string? Level { get; } = level;
}