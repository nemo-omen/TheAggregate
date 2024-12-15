using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TheAggregate.Api.Features.Identity.RegisterUser;

public record RegisterUserRequest
{
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; init; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    [Remote(action: "IsEmailRegistered", controller: "Account", ErrorMessage = "Email is already registered")]
    public required string Email { get; init; }
    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; init; }
    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("Password", ErrorMessage = "Password and confirmation must match")]
    public required string PasswordConfirmation { get; init; }
}