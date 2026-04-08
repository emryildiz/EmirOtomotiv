
using System.Security.Claims;
using EmirOtomotiv.Core.Application.Features.Auth.Commands.Login;
using EmirOtomotiv.Core.Application.Features.Auth.Queries.GetMe;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace EmirOtomotiv.Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        Claim? userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier);

        if(userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            return Unauthorized();
        }

        GetMeResponse user = await this._mediator.Send(new GetMeRequest(userId.ToString()));

        return this.Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        LoginResponse response = await this._mediator.Send(request);
        this.SetAccessToken(response.Token);

        this.SetRefreshToken(response.RefreshToken, response.RefreshTokenExpiry);
        this.SetRole(response.Role);
        return this.Ok(response);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        this.DeleteAllCookies();
        
        return this.Ok();
    }

    private void SetRefreshToken(string refreshToken, DateTime expires)
    {
        Response.Cookies.Append("refreshToken", refreshToken, CookieOptions(expires));
    }

    private void SetAccessToken(string token)
    {
        Response.Cookies.Append("accessToken", token, CookieOptions(DateTime.UtcNow.AddMinutes(15)));
    }

    private void SetRole(string role)
    {
        Response.Cookies.Append("role", role, CookieOptions(DateTime.UtcNow.AddDays(7)));
    }

    private static CookieOptions CookieOptions(DateTime expires) => new CookieOptions
    {
        HttpOnly = true,
        Expires = expires,
        SameSite = SameSiteMode.None,
        Secure = true
    };

    private void DeleteAccessToken()
    {
        this.Response.Cookies.Delete("accessToken");
    }

    private void DeleteRefreshToken()
    {
        this.Response.Cookies.Delete("refreshToken");
    }

    private void DeleteRole()
    {
        this.Response.Cookies.Delete("role");
    }

    private void DeleteAllCookies()
    {
        this.DeleteAccessToken();
        this.DeleteRefreshToken();
        this.DeleteRole();
    }
}