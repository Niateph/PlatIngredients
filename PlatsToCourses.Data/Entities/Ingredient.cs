
namespace PlatsToCourses.Data.Entities;

	public class Ingredient
	{
		public int Id { get; set; }
		public string Nom { get; set; }
		public float Prix { get; set; }
		public string Unit { get; set; }
		public virtual ICollection<PlatIngredient> PlatIngredients { get; set; }
}

