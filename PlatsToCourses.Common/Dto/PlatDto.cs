namespace PlatsToCourses.Common;
public class PlatDto
{
	public int Id { get; set; }
	public string Nom { get; set; }
	public float Prix { get; set; }
	public List<IngredientListeInPlatDto> IngredientDtos { get; set; }
}
