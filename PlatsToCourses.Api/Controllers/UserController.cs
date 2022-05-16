using CommonPackages.Jwt.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlatsToCourses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
	private readonly ILogService loggingService;

	private readonly IUserService userService;

	private readonly IProfileService profileService;

	public UserController(ILogService loggingService, IUserService userService, IProfileService profileService)
	{
		this.loggingService = loggingService;
		this.userService = userService;
		this.profileService = profileService;
	}


	[HttpGet]
	[ProducesResponseType(typeof(List<UserDto>), 200)]
	public ActionResult GetUser([FromServices] ITokenService tokenService)
	{
		return this.Ok(tokenService.FromClaimUser(this.User));
	}

	/// <summary>
	/// Renvoie le token JWT si l'utilisateur est authentifié. 
	/// </summary>
	/// <param name="tokenService">Service de génération du token.</param>
	/// <returns>Token jwt.</returns>
	[HttpGet("authenticate")]
	[AllowAnonymous]
	public ActionResult Authenticate([FromServices] ITokenService tokenService)
	{
		if (tokenService == null)
		{
			throw new ArgumentNullException(nameof(tokenService));
		}

		if (!this.User.Identity.IsAuthenticated)
		{
			return this.Unauthorized();
		}

		var userRole = this.profileService.GetUserRole(this.User);
		string token = tokenService.GenerateToken(this.User.Identity, new List<string> { userRole.ToString() });
		if (token == null)
		{
			return this.Unauthorized();
		}

		return this.Ok(new { Token = token });
	}
}
