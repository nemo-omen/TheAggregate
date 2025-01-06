namespace TheAggregate.Api.Shared.Types;

public record ApiError
{
    public required string Type { get; init; }
    public required string Title { get; init; }
    public required string Detail { get; init; }
    public required int Status { get; init; }
}