using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatsToCourses.Api.Filters;
using PlatsToCourses.Data.Entities;

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

	/// <summary>
	/// Recupère la page int i contenant int y PlatDto
	/// </summary>
	////// <returns></returns>
	[HttpGet]
	[ProducesResponseType(typeof(List<PlatDto>), 8000)]
	public ActionResult GetAll(int page, int nbByPage)
	{
		return this.Ok(this.platService.GetAll(page,nbByPage));
	}
	/// <summary>
	/// Ajoute un plat vide à parti d'un plat Dto possiblement vide
	/// </summary>
	/// <param name="plat"> plat DTO</param>
	/// <returns></returns>
	[HttpPost]
	public ActionResult AddOne(PlatNewDto plat)
	{
		Plat platAdded = this.platService.AddOne(plat);
		return this.Ok(new {platAdded.Nom});
	}

	/// <summary>
	/// Supprime un plat de la table associée à la PK
	/// </summary>
	/// <param name="Id"></param>
	/// <returns></returns>
	[HttpDelete]
	public ActionResult DeletOne(int Id)
	{
		return this.Ok(this.platService.DeleteOne(Id));
	}
	/// <summary>
	/// Met à jour un plat dans la table en fonction de l'id du platDto 
	/// </summary>
	/// <param name="plat"></param>
	/// <returns></returns>
	[HttpPut]
	public ActionResult UpdateOne(PlatDto plat)
	{
		return this.Ok(this.platService.UpdateOne(plat));
	}
}
