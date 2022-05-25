using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatsToCourses.Api.Filters;
using PlatsToCourses.Data.Entities;

namespace PlatsToCourses.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
//[Authorize]

public class IngredientsController : ControllerBase
{
	readonly IIngredientService ingredientService;

	public IngredientsController(IIngredientService ingredientService)
	{
		this.ingredientService = ingredientService;
	}

	/// <summary>
	/// Recupère la page int i contenant int y PlatDto
	/// </summary>
	////// <returns></returns>
	[HttpGet]
	[ProducesResponseType(typeof(List<IngredientListeDto>), 8000)]
	public ActionResult GetAll(int page, int nbByPage)
	{
		return this.Ok(this.ingredientService.GetAll(page,nbByPage));
	}
	/// <summary>
	/// Ajoute un plat vide à parti d'un plat Dto possiblement vide
	/// </summary>
	/// <param name="ingredient"> plat DTO</param>
	/// <returns></returns>
	[HttpPost]
	public ActionResult AddOne(IngredientNewDto ingredient)
	{
		return this.Ok(new { this.ingredientService.AddOne(ingredient).Nom });
	}

	/// <summary>
	/// Supprime un plat de la table associée à la PK
	/// </summary>
	/// <param name="Id"></param>
	/// <returns></returns>
	[HttpDelete]
	public ActionResult DeletOne(int Id)
	{
		return this.Ok(this.ingredientService.DeleteOne(Id));
	}
	/// <summary>
	/// Met à jour un plat dans la table en fonction de l'id du platDto 
	/// </summary>
	/// <param name="ingredientToUpdate"></param>
	/// <returns></returns>
	[HttpPut]
	public ActionResult UpdateOne(IngredientNewDto ingredientToUpdate)
	{
		return this.Ok(this.ingredientService.UpdateOne(ingredientToUpdate));
	}
}
