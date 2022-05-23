using PlatsToCourses.Data.Entities;

namespace PlatsToCourses.Services.Mapping;

public class IngredientProfile : Profile
{
	public IngredientProfile()
	{
		this.CreateMap<Ingredient, IngredientListeDto>().ReverseMap();
	}

}

