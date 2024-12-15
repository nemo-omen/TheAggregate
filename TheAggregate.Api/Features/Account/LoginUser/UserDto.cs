namespace TheAggregate.Api.Features.Account.LoginUser;

public record UserDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
}