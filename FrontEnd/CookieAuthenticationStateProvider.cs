using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace FrontEnd;

public class CookieAuthenticationStateProvider(HttpClient client) : AuthenticationStateProvider
{
	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var authState = new AuthenticationState(await this.GetUserStateAsync());
		return authState;
	}

	public async Task Login()
	{
		try
		{
			var result = await client.PostAsync(client.BaseAddress + "api/users/login", null);

			if (result.IsSuccessStatusCode)
			{
				this.NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
	}

	private async Task<ClaimsPrincipal> GetUserStateAsync()
	{
		Dictionary<string, string> claims = [];

		try
		{
			var result = await client.GetAsync(client.BaseAddress + "api/users/user");
			if (result is null || !result.IsSuccessStatusCode || result.Content is null)
			{
				return new ClaimsPrincipal(new ClaimsIdentity());
			}

			claims = await result.Content.ReadFromJsonAsync<Dictionary<string, string>>();

		}
		catch (Exception ex)
		{
			Console.Write(ex);
		}

		if (claims == null || claims.Count == 0)
		{
			return new ClaimsPrincipal(new ClaimsIdentity());
		}

		return new ClaimsPrincipal(
					new ClaimsIdentity(
						claims.Select(x => new Claim(x.Key, x.Value)), "cookie"
					)
				);
	}
}