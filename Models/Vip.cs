using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class Vip
	{
		[Key]
		public int VipId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public Decimal ExpMultiplier { get; set; }
		public virtual User User { get; set; } 
	}
}
