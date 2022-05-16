using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace PlatsToCourses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VersionController : ControllerBase
{
	private readonly ILogService loggingService;
	private readonly IWebHostEnvironment hostingEnvironment;

	public VersionController(ILogService loggingService, IWebHostEnvironment hostingEnvironment)
	{
		this.loggingService = loggingService;
		this.hostingEnvironment = hostingEnvironment;
	}

	/// <summary>
	/// Renvoie la version de l'API.
	/// </summary>
	[HttpGet]
	public ActionResult GetVersion()
	{
		Assembly assembly = Assembly.GetEntryAssembly();
		return this.Ok(new
		{
			Version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
			AssemblyVersion = assembly.GetName().Version,
			FileVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version,
			Environment = this.hostingEnvironment.EnvironmentName
		});
	}
}
