using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwner.Models
{
	public class PetActivity
	{
		public int PetId { get; set; }
		public int ActivityId { get; set; }
		public DateTime Data { get; set; }
		public bool Recurring { get; set; }
		public int RecurringInterval { get; set; }
		public virtual Pet Pets { get; set; }
		public virtual Activity Activities { get; set; }

	}
}
