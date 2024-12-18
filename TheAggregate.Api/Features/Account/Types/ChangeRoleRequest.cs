namespace TheAggregate.Api.Features.Account.Types;

public record ChangeRoleRequest
{
    public required string Email { get; init; }
    public required string Role { get; init; }
}