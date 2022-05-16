using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PlatsToCourses.Services;

public class UserService : IUserService
{
	private readonly IHttpContextAccessor httpContextAccessor;
	public UserService(IHttpContextAccessor httpContextAccessor)
	{
		this.httpContextAccessor = httpContextAccessor;
	}

	/// <summary>
	/// Jwt
	/// </summary>
	public UserDto FromClaimUser()
	{
		if (this.httpContextAccessor.HttpContext == null)
		{
			return null;
		}

		var principal = this.httpContextAccessor.HttpContext.User;

		if (!principal.Identity.IsAuthenticated)
		{
			return null;
		}

		var identity = (ClaimsIdentity)principal.Identity;
		var user = new UserDto
		{
			DisplayName = principal.Identity.Name
		};

		if (principal.HasClaim(c => c.Type == ClaimTypes.Email))
		{
			user.Email = principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
		}

		if (principal.HasClaim(c => c.Type == CustomClaims.CustomClaimCompanyName))
		{
			user.Company = principal.Claims.Single(c => c.Type == CustomClaims.CustomClaimCompanyName).Value;
		}

		user.Profile = ((int)ProfileEnum.None).ToString();
		if (principal.HasClaim(c => c.Type == CustomClaims.CustomClaimProfile))
		{
			user.Profile = principal.Claims.Single(c => c.Type == CustomClaims.CustomClaimProfile).Value;
		}

		return user;
	}
}
