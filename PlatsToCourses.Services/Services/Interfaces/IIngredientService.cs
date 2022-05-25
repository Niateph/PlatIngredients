using PlatsToCourses.Data.Entities;

namespace PlatsToCourses.Services;
public interface IIngredientService
{
	List<IngredientListeDto> GetAll(int page, int nbByPage);
	Ingredient AddOne(IngredientNewDto ingredientToAdd);
}
