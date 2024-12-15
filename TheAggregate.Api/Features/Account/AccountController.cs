using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheAggregate.Api.Features.Identity.RegisterUser;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Account;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signinManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signinManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signinManager = signinManager;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApplicationUser>> Register(RegisterUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            var errorResponse = new RegisterUserResponse
            {
                Success = false,
                Errors = errors.ToList(),
            };

            return BadRequest(errorResponse);
        }

        var user = new ApplicationUser
        {
            Name = request.Name,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await _signinManager.SignInAsync(user, isPersistent: false);
            var successResponse = new RegisterUserResponse
            {
                Success = true,
                Message = "User created successfully. Welcome to The Aggregate!"
            };
            return Ok(successResponse);
        }

        var failedResponse = new RegisterUserResponse
        {
            Success = false,
            Errors = result.Errors.Select(e => e.Description).ToList()
        };
        return BadRequest(failedResponse);
    }

    [HttpGet]
    public async Task<IActionResult> IsEmailRegistered(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Ok(true);
        }
        return Ok(false);
    }
}