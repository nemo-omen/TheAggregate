using TheAggregate.Api.Shared.Types;

namespace TheAggregate.Api.Features.Account.LoginUser;

public record LoginUserResponse : ApiResponse<UserDto>
{
    public new UserDto? Data { get; set; }
}