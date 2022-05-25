using System.Diagnostics;
using PlatsToCourses.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PlatsToCourses.Services;
public class PlatService : IPlatService
{
	private BDDPlatsToCoursesContext context;
	private readonly IMapper mapper;
	readonly IngredientService ingredientService;

	public PlatService(BDDPlatsToCoursesContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
		ingredientService = new IngredientService(this.context, this.mapper);
	}
	public List<PlatDto> GetAll(int page = 1, int nbByPage = 10)
	{
		try
		{
			List<PlatDto> listToSend = new();
			//List<PlatIngredient> listpi = this.context.PlatIngredients.ToList();
			//List<Ingredient> listi = this.context.Ingredients.ToList();
			var listePlats = context.Plats
				.ToList();
				/*.Skip((page - 1) * nbByPage)
				.Take(nbByPage).ToList();*/
			Debug.WriteLine("Breakpoint");
			foreach (Plat plat in listePlats)
			{
				PlatDto platDto = new();
				platDto.Nom = plat.Nom;
				platDto.Id = plat.Id;
				platDto.IngredientIds = new List<int>();
				platDto.Prix = 0f;
				var ingredients = plat.PlatIngredients;
				if (ingredients == null) { ingredients = new List<PlatIngredient>(); }
				foreach (PlatIngredient ingredient in ingredients)
				{

					platDto.IngredientIds.Add(ingredient.IngredientId);
					platDto.Prix += ingredient.Ingredient.Prix * ingredient.Amount;
				}

				listToSend.Add(platDto);
			}
			return listToSend;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			return null;
		}
	}
	//return this.context.Plats.ProjectTo<PlatDto>(this.mapper.ConfigurationProvider).ToList();*/




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

			if (platAAjouter.IngredientDtos != null)
			{
				//Autant d'Ingredients que d'IngredientNewDtos contenu dans PlatNewDto
				foreach (IngredientNewDto ingredientDto in platAAjouter.IngredientDtos)
				{
					//Pour chaque IngredientNewDto on vérifie si il est déjà présent dans le context ou 
					//si il faut le créer
					Ingredient thisIngredient = new();
					ingredientDto.IdPlat = thisPlat.Id;
					//Délégué à ingrédient Service
					if (this.context.Ingredients.SingleOrDefault(e => e.Id == ingredientDto.Id) == null)
					{
						//Ingrédient contenu dans le PlatNewDto n'existe pas dans la bdd
						//Il est ajouté via ingredientService
						thisIngredient = ingredientService.AddOne(ingredientDto);
					}
					else
					{
						//Ingrédient contenu dans le PlatNewDto existe déjà dans la bdd
						//Il est mis à jour au cas où des champs seraient modifiés depuis la dernière entrée
						thisIngredient = this.ingredientService.UpdateOne(ingredientDto);
					}
					this.context.SaveChanges();
					//Maintenant que tous les POCO sont crées, on gère la jointure
					PlatIngredient jointure = new();
					//Est ce que la jointure existe déjà ?
					if (this.context.PlatIngredients.SingleOrDefault(e => e.PlatId == thisPlat.Id && e.IngredientId == thisIngredient.Id) == null)
					{
						jointure.Plat = thisPlat;
						jointure.PlatId = thisPlat.Id;
						jointure.Ingredient = thisIngredient;
						jointure.IngredientId = thisIngredient.Id;
					}
					else
					{
						jointure = this.context.PlatIngredients.Single(e => e.PlatId == thisPlat.Id && e.IngredientId == thisIngredient.Id);
					}
					jointure.Amount = ingredientDto.Amount;
					jointure.Unit = ingredientDto.Unit;
					this.context.PlatIngredients.Add(jointure);
					this.context.SaveChanges();
				}
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
		if (this.context.Plats.Single(e => e.Id == Id) != null)
		{
			this.context.Plats.Remove(this.context.Plats.Single(e => e.Id == Id));
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
