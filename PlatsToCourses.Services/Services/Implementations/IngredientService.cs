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
		foreach (Ingredient ingredient in this.context.Ingredients.Skip((page - 1) * nbByPage).Take(nbByPage))
		{
			IngredientListeDto ingredientDtoToSend = new();
			ingredientDtoToSend.Id = ingredient.Id;
			ingredientDtoToSend.Nom = ingredient.Nom;
			ingredientDtoToSend.Prix = ingredient.Prix;
			ingredientsDto.Add(ingredientDtoToSend);
		}
		return ingredientsDto;
	}

	public Ingredient AddOne(IngredientNewDto ingredientToAdd)
	{
		var transaction = this.context.Database.BeginTransaction();
		try {
			Ingredient workingIngredient = new();
			workingIngredient.Nom = ingredientToAdd.Nom;
			workingIngredient.Prix = ingredientToAdd.Prix;
			if (ingredientToAdd.Unit != null)
			{
				workingIngredient.Unit = ingredientToAdd.Unit;
			}
			this.context.Ingredients.Add(workingIngredient);
			this.context.SaveChanges();
			transaction.Commit();
			return workingIngredient;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			transaction.Rollback();
			return null;
		}
	}


	public bool DeleteOne(int Id)
	{
		var transaction = this.context.Database.BeginTransaction();
		try
		{
			this.context.Plats.Remove(this.context.Plats.Find(Id));
			this.context.SaveChanges();
			transaction.Commit();
			return true;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			transaction.Rollback();
			return false;
		}
	}

	public Ingredient UpdateOne(IngredientNewDto ingredientDto)
	{
	 	var transaction = this.context.Database.BeginTransaction();
		try
		{
			Ingredient ingredientToUpdate = this.context.Ingredients.SingleOrDefault(e => e.Id == ingredientDto.Id);
			if (ingredientToUpdate != null)
			{
				ingredientToUpdate.Unit = ingredientDto.Unit ?? ingredientToUpdate.Unit;
				ingredientToUpdate.Prix = ingredientDto.Prix == 0 ? ingredientToUpdate.Prix : ingredientDto.Prix;
				ingredientToUpdate.Nom = ingredientDto.Nom ?? ingredientToUpdate.Nom;
				if (ingredientDto.Amount > 0)
				{
					if (this.context.PlatIngredients.SingleOrDefault(e => e.IngredientId == ingredientDto.Id && e.PlatId == ingredientDto.IdPlat) != null)
					{
						this.context.PlatIngredients.Single(e => e.IngredientId == ingredientDto.Id && e.PlatId == ingredientDto.IdPlat).Amount = ingredientDto.Amount;
					}
				}
				this.context.SaveChanges();
				transaction.Commit();
			}
			return ingredientToUpdate;
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
			transaction.Rollback();
			return null;
		}
	}
}

