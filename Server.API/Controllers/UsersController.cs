using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Server.API.Controllers;
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
	[HttpGet, Route("user")]
	public IActionResult GetCurrentUser()
	{
		if (HttpContext.User.Identity is null || !HttpContext.User.Identity.IsAuthenticated)
		{
			return this.Unauthorized();
		}

		return this.Ok(HttpContext.User.Claims
							.ToDictionary(x => x.Type, x => x.Value));
	}

	[HttpPost, Route("login")]
	public async Task<IActionResult> Login()
	{
		var claims = new List<Claim>
		{
			new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
			new(ClaimTypes.Name, "Test User"),
			new(ClaimTypes.Email, "")
		};

		var claimsIdentity = new ClaimsIdentity(claims,
							authenticationType: CookieAuthenticationDefaults.AuthenticationScheme);

		//var authProperties = new AuthenticationProperties
		//{
		//	AllowRefresh = true,
		//	// Refreshing the authentication session should be allowed.

		//	//ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
		//	// The time at which the authentication ticket expires. A 
		//	// value set here overrides the ExpireTimeSpan option of 
		//	// CookieAuthenticationOptions set with AddCookie.

		//	IsPersistent = true,
		//	// Whether the authentication session is persisted across 
		//	// multiple requests. When used with cookies, controls
		//	// whether the cookie's lifetime is absolute (matching the
		//	// lifetime of the authentication ticket) or session-based.

		//	IssuedUtc = DateTimeOffset.UtcNow,
		//	// The time at which the authentication ticket was issued.

		//	RedirectUri = "/",
		//	// The full path or absolute URI to be used as an http 
		//	// redirect response value.
		//};

		//await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperties);
		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
		return this.Ok();
	}
}