using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class Pet
	{
		[Key]
		public int PetId  { get; set; }
		public string PetName { get; set; }
		public int Age { get; set; }
		public Decimal Weigth { get; set; }
		public string Photo { get; set; }
		public string Species { get; set; }
		public string Race { get; set; }
		public int GroupId { get; set; }
		public virtual Group Group { get; set; }
		public ICollection<PetActivity> PetActivities { get; set; }
	}
}
