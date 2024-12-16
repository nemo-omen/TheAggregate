using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAggregate.Api.Features.Account.LoginUser;
using TheAggregate.Api.Features.Identity.RegisterUser;
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

    [HttpGet("logout")]
    public async Task<ActionResult<ApiResponse>> Logout()
    {
        return Ok(await _accountService.Logout());
    }

    [HttpGet]
    public async Task<IActionResult> IsEmailRegistered(string email)
    {
        return Ok(await _accountService.IsEmailRegistered(email));
    }
}