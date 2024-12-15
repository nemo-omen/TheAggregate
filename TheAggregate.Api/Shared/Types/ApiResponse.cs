using System.Net;

namespace TheAggregate.Api.Shared.Types;

public record ApiResponse
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public List<string>? Errors { get; init; }
    public bool HasErrors => Errors?.Count > 0;
}

public record ApiResponse<T>
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public List<string>? Errors { get; init; }
    public bool HasErrors => Errors?.Count > 0;
    public T? Data { get; init; }
}