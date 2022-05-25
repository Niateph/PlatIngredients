using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlatsToCourses.Data.Entities
{
	public class PlatIngredient
	{
		public int PlatId { get; set; }
		public int IngredientId { get; set; }
		public virtual Plat Plat { get; set; }
		public virtual Ingredient Ingredient { get; set; }	
		public float Amount { get; set; }
		public string Unit { get; set; }
	}
}
