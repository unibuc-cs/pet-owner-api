using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class Item
	{
		[Key]
		public int ItemId { get; set; }
		public string Category { get; set; }
		public string ItemName { get; set; }
		public Decimal Money { get; set; }
		public int CostId { get; set; }
		public virtual Cost Cost { get; set; }
	}
}
