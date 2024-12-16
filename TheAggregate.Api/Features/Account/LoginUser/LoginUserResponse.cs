using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Types;

namespace TheAggregate.Api.Features.Account.LoginUser;

public record LoginUserResponse : ApiResponse<UserDto>
{
    public ApplicationUser? User { get; init; }
}