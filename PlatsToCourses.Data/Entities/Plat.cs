

namespace PlatsToCourses.Data.Entities
{
	public class Plat
	{
		public int PlatId { get; set; }
		public string Nom { get; set; }
		public virtual ICollection<PlatIngredient> PlatIngredients { get; set; }
	}
}
