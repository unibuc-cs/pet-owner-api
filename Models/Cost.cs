using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class Cost
	{
		[Key]
		public int CostId { get; set; }
		public int GroupId { get; set; }
		public DateTime RecordStamp { get; set; }
		public virtual Group Group { get; set; }
		public ICollection<Item> Items { get; set; }
	}
}
