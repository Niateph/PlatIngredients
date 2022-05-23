using System.ComponentModel.DataAnnotations;
namespace PlatsToCourses.Common;

public class IngredientNewDto
{
	public int Id { get; set; }
	public string Nom { get; set; }
	public float Prix { get; set; }
	[StringLength(10)]
	public string Unit { get; set; }
	public int IdPlat { get; set; }
	public float Amount { get; set; }
}
