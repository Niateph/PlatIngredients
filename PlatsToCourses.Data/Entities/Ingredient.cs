
namespace PlatsToCourses.Data.Entities;

	public class Ingredient
	{
		public int IngredientId { get; set; }
		public string Nom { get; set; }
		public float Prix { get; set; }
		public string Unit { get; set; }
		public  ICollection<PlatIngredient> PlatIngredients { get; set; } 
	}

