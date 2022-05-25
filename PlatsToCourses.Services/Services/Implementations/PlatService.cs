using System.Diagnostics;
using PlatsToCourses.Data.Entities;
using System.Data.Entity;

namespace PlatsToCourses.Services;
public class PlatService : IPlatService
{
	private readonly BDDPlatsToCoursesContext context;
	private readonly IMapper mapper;
	readonly IngredientService ingredientService;

	public PlatService(BDDPlatsToCoursesContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
		ingredientService = new IngredientService(this.context, this.mapper);
	}
	public List<PlatDto> GetAll(int page, int nbByPage)
	{
		List<PlatDto> listToSend = new();
		foreach (Plat plat in this.context.Plats)//Skip<Plat>((page - 1) * nbByPage).Take<Plat>(nbByPage)
		{
			PlatDto platDto = new();
			platDto.Nom = plat.Nom;
			platDto.Id = plat.PlatId;
			platDto.IngredientDtos = new List<IngredientListeInPlatDto>();
			platDto.Prix = 0f;
			//Transforme les ingredients liés en ingrédients destinés à être vue
			//dans la recette du Plat
			foreach (PlatIngredient platIngredient in plat.PlatIngredients)
			{
				IngredientListeInPlatDto ingreDto = new();
				ingreDto.Nom = platIngredient.Ingredient.Nom;
				ingreDto.Id = platIngredient.IngredientId;
				ingreDto.Amount = platIngredient.Amount;
				platDto.IngredientDtos.Add(ingreDto);
				platDto.Prix += platIngredient.Ingredient.Prix * ingreDto.Amount;
			}
			listToSend.Add(platDto);
		}
		return listToSend;
		//return this.context.Plats.ProjectTo<PlatDto>(this.mapper.ConfigurationProvider).ToList();
	}



	public Plat AddOne(PlatNewDto platAAjouter)
	{
		var transaction = this.context.Database.BeginTransaction();
		try
		{
			//Init et sets basique du Plat à ajouter
			Plat thisPlat = new();
			thisPlat.Nom = platAAjouter.Nom;
			this.context.Plats.Add(thisPlat);
			this.context.SaveChanges();
			//Init et sets des Ingredients contenu dans le Plat à ajouter
			thisPlat.PlatIngredients = new List<PlatIngredient>();
			//Autant d'Ingredients que d'IngredientNewDtos contenu dans PlatNewDto
			foreach (IngredientNewDto ingredientDto in platAAjouter.IngredientDtos)
			{
				//Pour chaque IngredientNewDto on vérifie si il est déjà présent dans le context ou 
				//si il faut le créer
				Ingredient thisIngredient = new();
				ingredientDto.IdPlat = thisPlat.PlatId;
				//Délégué à ingrédient Service
				if (this.context.Ingredients.SingleOrDefault(e => e.IngredientId == ingredientDto.Id) == null)
				{
					thisIngredient = ingredientService.AddOne(ingredientDto);
				}
				else
				{
					thisIngredient = ingredientService.UpdateOne(ingredientDto);
				}
				this.context.SaveChanges();
				//Maintenant que tous les POCO sont crées, on gère la jointure
				PlatIngredient jointure = new();
				//Est ce que la jointure existe déjà ?
				if (this.context.PlatIngredients.SingleOrDefault(e => e.PlatId == thisPlat.PlatId && e.IngredientId == thisIngredient.IngredientId) == null)
				{
					jointure.Plat = thisPlat;
					jointure.PlatId = thisPlat.PlatId;
					jointure.Ingredient = thisIngredient;
					jointure.IngredientId = thisIngredient.IngredientId;
				}
				else
				{
					jointure = this.context.PlatIngredients.Single(e => e.PlatId == thisPlat.PlatId && e.IngredientId == thisIngredient.IngredientId);					
				}
				jointure.Amount = ingredientDto.Amount;
				jointure.Unit = ingredientDto.Unit;
				thisPlat.PlatIngredients.Add(jointure);
				this.context.PlatIngredients.Add(jointure);
				this.context.SaveChanges();
			}
			this.context.SaveChanges();
			transaction.Commit();
			return thisPlat;
		}
		catch (Exception ex)
		{
			transaction.Rollback();
			Debug.WriteLine(ex.Message);
			return null;
		}
	}

	public bool DeleteOne(int Id)
	{
		if (this.context.Plats.Single(e => e.PlatId == Id) != null)
		{
			this.context.Plats.Remove(this.context.Plats.Single(e => e.PlatId == Id));
			this.context.SaveChanges();
			return true;
		}
		return false;
	}

	public Plat UpdateOne(PlatDto platAUpdate)
	{
		Plat platToUpdate = this.context.Plats.Find(platAUpdate.Id);
		platToUpdate = mapper.Map<PlatDto, Plat>(platAUpdate, platToUpdate);
		this.context.SaveChanges();
		return platToUpdate;
	}
}
