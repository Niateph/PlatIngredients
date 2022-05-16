

namespace PlatsToCourses.Data.Entities
{
	public class Plat
	{
		public int Id { get; set; }
		public string Nom { get; set; }

		public List<Ingredient> Ingredients { get; set; }
	}
}
