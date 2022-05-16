namespace PlatsToCourses.Common;
public class PlatDto
{
	public int Id { get; set; }
	public string Nom { get; set; }
	public List<IngredientDto> IngredientDtos { get; set; }
}
