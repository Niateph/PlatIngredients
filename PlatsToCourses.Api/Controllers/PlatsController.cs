using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatsToCourses.Api.Filters;

namespace PlatsToCourses.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
//[Authorize]

public class PlatsController : ControllerBase
{
	readonly IPlatService platService;

	public PlatsController(IPlatService platService)
	{
		this.platService = platService;
	}


	[HttpGet]
	[ProducesResponseType(typeof(List<PlatDto>), 200)]
	public ActionResult GetAll()
	{
		return this.Ok(this.platService.GetAll());
	}

	[HttpPost]
	public ActionResult AddOne(PlatDto plat)
	{
		return this.Ok(this.platService.AddOne(plat));
	}

	[HttpDelete]
	public ActionResult DeletOne(PlatDto plat)
	{
		return this.Ok(this.platService.DeleteOne(plat));
	}
}
