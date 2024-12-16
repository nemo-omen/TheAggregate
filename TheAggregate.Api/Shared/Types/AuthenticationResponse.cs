namespace TheAggregate.Api.Shared.Types;

public record AuthenticationResponse : ApiResponse
{
    public string? Token { get; init; }
    public DateTime Expiration { get; init; }
}