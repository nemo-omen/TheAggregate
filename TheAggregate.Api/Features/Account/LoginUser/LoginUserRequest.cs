using System.ComponentModel.DataAnnotations;

namespace TheAggregate.Api.Features.Account.LoginUser;

public record LoginUserRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; init; }
    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; init; }
}