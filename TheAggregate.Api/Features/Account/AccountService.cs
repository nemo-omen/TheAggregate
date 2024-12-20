using Microsoft.AspNetCore.Identity;
using TheAggregate.Api.Features.Account.LoginUser;
using TheAggregate.Api.Features.Identity.RegisterUser;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Exceptions;
using TheAggregate.Api.Shared.Types;

namespace TheAggregate.Api.Features.Account;

public interface IAccountService
{
    Task<ApplicationUser> GetUser();
    Task<bool> IsEmailRegistered(string email);
    Task<ApiResponse> Register(RegisterUserRequest request);
    Task<LoginUserResponse> Login(LoginUserRequest request);
    Task<ApiResponse> Logout();
}

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signinManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signinManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signinManager = signinManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApplicationUser> GetUser()
    {
        var ctx = _httpContextAccessor.HttpContext;
        var sessionIdName = ctx.User.Identity.Name;
        var ctxUser = await _userManager.FindByEmailAsync(sessionIdName);
        if (ctxUser is null)
        {
            throw new NotFoundException("User not found.");
        }
        return ctxUser;
    }

    public async Task<bool> IsEmailRegistered(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return true;
        }
        return false;
    }

    public async Task<ApiResponse> Register(RegisterUserRequest request)
    {
        var user = new ApplicationUser
        {
            Name = request.Name,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            // await _signinManager.SignInAsync(user, isPersistent: false);
            var successResponse = new ApiResponse
            {
                Success = true,
                Message = "User created successfully. Welcome to The Aggregate!"
            };
            return successResponse;
        }

        return new ApiResponse
        {
            Success = false,
            Errors = result.Errors.Select(e => e.Description).ToList(),
            Message = "Failed to register user!"
        };
    }

    public async Task<LoginUserResponse> Login(LoginUserRequest request)
    {
        var signinResult = await _signinManager
            .PasswordSignInAsync(
                request.Email, request.Password, false, false);

        if (!signinResult.Succeeded)
        {
            return new LoginUserResponse
            {
                Success = false,
                Errors = ["Invalid email or password."],
                Message = "Failed to login."
            };
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return new LoginUserResponse
            {
                Success = false,
                Message = "User not found",
                Errors = ["User not found"],
            };

        return new LoginUserResponse
        {
            Success = true,
            Message = "User logged in.",
            User = user
        };
    }

    public async Task<ApiResponse> Logout()
    {
        await _signinManager.SignOutAsync();
        return new ApiResponse
        {
            Success = true,
            Message = "User logged out. Come back soon!"
        };
    }
}