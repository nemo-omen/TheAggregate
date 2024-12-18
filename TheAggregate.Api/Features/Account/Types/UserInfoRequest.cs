namespace TheAggregate.Api.Features.Account.Types;

public record UserInfoRequest
{
    public required string Email { get; init; }
}