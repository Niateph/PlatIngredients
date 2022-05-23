using System.Diagnostics;
using AutoMapper.QueryableExtensions;
using PlatsToCourses.Data.Entities;

namespace PlatsToCourses.Services;

public class IngredientService : IIngredientService
{
	private readonly BDDPlatsToCoursesContext context;
	private readonly IMapper mapper;

	public IngredientService(BDDPlatsToCoursesContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
	}
	public List<IngredientListeDto> GetAll(int page, int nbByPage)
	{
		List<IngredientListeDto> ingredientsDto = new();
		foreach (Ingredient ingredient in this.context.Ingredients.Skip<Ingredient>((page - 1) * nbByPage).Take<Ingredient>(nbByPage))
		{
			IngredientListeDto ingredientDtoToSend = new();
			ingredientDtoToSend.Id = ingredient.IngredientId;
			ingredientDtoToSend.Nom = ingredient.Nom;
			ingredientDtoToSend.Prix = ingredient.Prix;
			ingredientsDto.Add(ingredientDtoToSend);
		}
		return ingredientsDto;
	}

	public Ingredient AddOne(IngredientNewDto ingredientToAdd)
	{
		Ingredient workingIngredient = new();
		workingIngredient.Nom = ingredientToAdd.Nom;
		workingIngredient.Prix = ingredientToAdd.Prix;
		if (ingredientToAdd.Unit != null)
		{
			workingIngredient.Unit = ingredientToAdd.Unit;
		}
		this.context.Ingredients.Add(workingIngredient);
		this.context.SaveChanges();
		return workingIngredient;
	}

	public bool DeleteOne(int Id)
	{
		this.context.Plats.Remove(this.context.Plats.Find(Id));
		this.context.SaveChanges();
		return true;
	}

	public Ingredient UpdateOne(IngredientNewDto ingredientDto)
	{
		Ingredient ingredientToUpdate = this.context.Ingredients.SingleOrDefault(e => e.IngredientId == ingredientDto.Id);
		if (ingredientToUpdate != null)
		{
			ingredientToUpdate.Unit = ingredientDto.Unit == "" || ingredientDto.Unit == null ? ingredientToUpdate.Unit : ingredientDto.Unit;
			ingredientToUpdate.Prix = ingredientDto.Prix == 0 ? ingredientToUpdate.Prix : ingredientDto.Prix;
			ingredientToUpdate.Nom = ingredientDto.Nom == "" || ingredientDto.Nom == null ? ingredientToUpdate.Nom : ingredientDto.Nom;
			if (ingredientDto.Amount > 0)
			{
				if (this.context.PlatIngredients.SingleOrDefault(e => e.IngredientId == ingredientDto.Id && e.PlatId == ingredientDto.IdPlat) != null)
				{
					this.context.PlatIngredients.Single(e => e.IngredientId == ingredientDto.Id && e.PlatId == ingredientDto.IdPlat).Amount = ingredientDto.Amount;
				}
			}
			this.context.SaveChanges();
		}
		return ingredientToUpdate;
	}
}

