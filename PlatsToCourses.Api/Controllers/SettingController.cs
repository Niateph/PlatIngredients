using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatsToCourses.Api.Filters;

namespace PlatsToCourses.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SettingController : ControllerBase
{
	private readonly ILogger<SettingController> logger;
	private readonly ISettingService settingService;

	public SettingController(ILogger<SettingController> logger, ISettingService settingService)
	{
		this.logger = logger;
		this.settingService = settingService;
	}

	/// <summary>
	/// Renvoie les paramètres de l'application.
	/// </summary>
	[HttpGet]
	[SecurityFilter(ProfileEnum.Administrator, ProfileEnum.User)]
	[ProducesResponseType(typeof(List<SettingDto>), 200)]
	public ActionResult Get()
	{
		return this.Ok(this.settingService.GetAll());
	}
}
