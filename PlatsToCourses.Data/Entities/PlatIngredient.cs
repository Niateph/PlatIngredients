using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatsToCourses.Data.Entities
{
	public class PlatIngredient
	{
		public int PlatId { get; set; }
		public int IngredientId { get; set; }
		public Plat Plat { get; set; }
		public Ingredient Ingredient { get; set; }	
		public float Amount { get; set; }
		public string Unit { get; set; }
	}
}
