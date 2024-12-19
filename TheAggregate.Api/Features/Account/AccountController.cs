using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAggregate.Api.Features.Account.LoginUser;
using TheAggregate.Api.Features.Account.Types;
using TheAggregate.Api.Features.Identity.RegisterUser;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Exceptions;
using TheAggregate.Api.Shared.Services;
using TheAggregate.Api.Shared.Types;

namespace TheAggregate.Api.Features.Account;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly IJwtService _jwtService;
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService, IJwtService jwtService)
    {
        _accountService = accountService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse>> Register(RegisterUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            return BadRequest(new ApiResponse
            {
                Success = false,
                Errors = errors.ToList(),
            });
        }

        var registerResponse = await _accountService.Register(request);
        if (!registerResponse.Success) return BadRequest(registerResponse);
        return Ok(registerResponse);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login(LoginUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            return BadRequest(new LoginUserResponse
            {
                Success = false,
                Errors = errors.ToList(),
            });
        }

        var loginResponse = await _accountService.Login(request);
        if(!loginResponse.Success) return BadRequest(loginResponse);
        if(loginResponse.User is null) return BadRequest(loginResponse);
        try
        {
            var tokenResponse = _jwtService.MakeToken(loginResponse.User);
            if (!tokenResponse.Success) return BadRequest(tokenResponse);
            return Ok(tokenResponse);
        }
        catch (Exception e)
        {
            return StatusCode(500, new AuthenticationResponse
            {
                Success = false,
                Errors = [e.Message]
            });
        }
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<UserWithRolesResponse>>> GetUser()
    {
        ApplicationUser? user = null;
        try
        {
            user = await _accountService.GetUser();
        }
        catch (NotFoundException e)
        {
            NotFound(new ApiResponse
            {
                Success = false,
                Message = "User not found."
            });
        }

        if(user is null) return NotFound(new ApiResponse
        {
            Success = false,
            Message = "User not found."
        });

        return Ok(new ApiResponse<UserWithRolesResponse>
        {
            Success = true,
            Data = new UserWithRolesResponse
            {
                Email = user.Email!,
                Name = user.Name,
                Roles = [] // TODO: get roles
            }
        });
    }
}