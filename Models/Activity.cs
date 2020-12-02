using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class Activity
	{
		[Key]
		public int ActivityId { get; set; }
		public string Description { get; set; }
		public int ExpPoints { get; set; }
		public ICollection<PetActivity> PetActivities { get; set; }
	}
}
