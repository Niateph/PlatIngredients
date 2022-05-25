namespace PlatsToCourses.Common;
public class PlatNewDto
{
	public int Id { get; set; }
	public string Nom { get; set; }
	public List<IngredientNewDto> IngredientDtos { get; set; }
}
