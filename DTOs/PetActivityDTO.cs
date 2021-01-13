using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.DTOs
{
	public class PetActivityDTO
	{
		public int PetId { get; set; }
		public int ActivityId { get; set; }
		public DateTime Data { get; set; }
		public bool Recurring { get; set; }
		public int RecurringInterval { get; set; }
		public string Description { get; set; }
		public int ExpPoints { get; set; }
		public string Title { get; set; }

	}
}
